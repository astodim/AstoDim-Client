using SetupTool.ApiLibrary;
using SetupTool.Properties;
using System.Drawing.Text;
using System.Management;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SetupTool
{
    public partial class frmClientMain : Form
    {
        private PrivateFontCollection privateFonts = new PrivateFontCollection();


        private void LoadCustomFont()
        {
            // Load font from embedded resource
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = "SetupTool.Resources.VCR_OSD_MONO.ttf";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    byte[] fontData = new byte[stream.Length];
                    stream.Read(fontData, 0, (int)stream.Length);

                    IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                    Marshal.Copy(fontData, 0, fontPtr, fontData.Length);

                    privateFonts.AddMemoryFont(fontPtr, fontData.Length);

                    Marshal.FreeCoTaskMem(fontPtr);
                }
            }
        }

        private void ApplyCustomFont()
        {
            LoadCustomFont();

            Font customFontSmallUnderline = new Font(privateFonts.Families[0], 9.75F, FontStyle.Underline);
            Font customFontSmall = new Font(privateFonts.Families[0], 9.75F);
            Font customFontSmallMedium = new Font(privateFonts.Families[0], 12F);
            Font customFontMedium = new Font(privateFonts.Families[0], 14.25F);
            Font customFontLarge = new Font(privateFonts.Families[0], 15.75F);
            Font customFontXL = new Font(privateFonts.Families[0], 18F);

            label3.Font = customFontSmallUnderline;
            lblLicenseKey.Font = customFontSmall;
            label1.Font = customFontMedium;
            lblRemaining.Font = customFontMedium;
            mskLicenseKey.Font = customFontXL;
            btnActivateLicense.Font = customFontLarge;
            btnInjectBot.Font = customFontLarge;
            lblVersion.Font = customFontSmallMedium;
        }

        public frmClientMain()
        {
            InitializeComponent();

            string fontName = "VCR OSD Mono";

            if (!FontInstaller.IsFontInstalled(fontName))
            {
                // Extract to app's local data folder
                string appDataFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "SetupTool"
                );

                Directory.CreateDirectory(appDataFolder);
                string fontPath = Path.Combine(appDataFolder, "VCR_OSD_Mono.ttf");

                // Extract if not already extracted
                if (!File.Exists(fontPath))
                {
                    if (!ExtractEmbeddedFontToFile("SetupTool.Resources.VCR_OSD_MONO.ttf", fontPath))
                    {
                        return;
                    }
                }

                // Install the font
                if (FontInstaller.InstallFont(fontPath, fontName + " (TrueType)"))
                {

                }
            }
            else
            {
                ApplyCustomFont();
            }
            ApiHelper.InitializeClient();

        }

        private bool ExtractEmbeddedFontToFile(string resourceName, string outputPath)
        {
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        return false;

                    using (FileStream fileStream = File.Create(outputPath))
                    {
                        stream.CopyTo(fileStream);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting font: {ex.Message}");
                return false;
            }
        }

        string licenseKey;
        string HWID;
        ApiKey? globalKey;
        LicenseKey licenseKeyGlobal;

        static string GetMotherboardID()
        {
            string cpuId = GetWmiValue("Win32_Processor", "ProcessorId");
            string boardId = GetWmiValue("Win32_BaseBoard", "SerialNumber");
            string biosId = GetWmiValue("Win32_BIOS", "SerialNumber");

            string combined = cpuId + boardId + biosId;

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combined);
                byte[] hash = sha.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        async void ActivateLicense(bool isFromButton = false, bool isFromTimer = false)
        {
            btnActivateLicense.Enabled = false;
            licenseKey = String.Empty;
            HWID = GetMotherboardID();

            if (File.Exists(Application.StartupPath + GlobalVariables.LICENSING_FILE_NAME))
            {
                if (licenseKeyGlobal is null)
                {
                    if (!mskLicenseKey.MaskFull)
                    {
                        if (!isFromTimer) MessageBox.Show("Lütfen doğru bir lisans anahtarı girdiğinizden emin olunuz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnActivateLicense.Enabled = true;
                        return;
                    }
                    licenseKey = mskLicenseKey.Text;
                }
                else
                    licenseKey = licenseKeyGlobal.ProductKey;
            }
            else if (isFromButton && !isFromTimer)
            {
                if (!mskLicenseKey.MaskFull)
                {
                    MessageBox.Show("Lütfen doğru bir lisans anahtarı girdiğinizden emin olunuz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnActivateLicense.Enabled = true;
                    return;
                }
                licenseKey = mskLicenseKey.Text;
            }


            (ApiKey? apiKey, string message) checkResult = await ApiProcessor.CheckLicense(licenseKey);
            if (checkResult.apiKey is not null)
            {
                if (!checkResult.apiKey.IsActivated)
                {
                    if (!isFromTimer)
                    {
                        DialogResult dialog = MessageBox.Show("Lisansı aktifleştirmek istediğinize emin misiniz?\nLisansı aktifleştirmek kiralama süresini başlatacaktır ve lisans anahtarını bilgisayarınızla eşleştirecektir.", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            (bool status, string message) result = await ApiProcessor.ActivateLicense(licenseKey, HWID);

                            if (result.status)
                            {
                                MessageBox.Show(result.message, "Aktifleştirme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnHideKey.Visible = true;

                                (ApiKey? apiKey, string message) activateResult = await ApiProcessor.CheckLicense(licenseKey);
                                globalKey = activateResult.apiKey;

                                LicenseKey licenseKeyGlobal = new LicenseKey
                                {
                                    ProductKey = checkResult.apiKey.ProductKey,
                                    HWID = HWID
                                };
                                JsonHelper.WriteKeyToFile(licenseKeyGlobal);

                                label1.Visible = false;
                                isTextHidden = true;
                                mskLicenseKey.Visible = false;
                                btnHideKey.BackgroundImage = Resources.see_eye_visible_icon_1878261;
                                btnActivateLicense.Visible = false;
                                btnInjectBot.Visible = true;

                                if (activateResult.apiKey is not null)
                                {
                                    lblRemaining.Visible = true;
                                    lblRemaining.Text = $"Lisansin kalan süresi: {activateResult.apiKey.DaysLeft} gün";
                                    timer1.Enabled = true;
                                }
                            }
                            else
                            {
                                mskLicenseKey.Clear();
                                JsonHelper.RemoveLicenseFile();
                                MessageBox.Show(result.message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    bool isLicenseExpired = !checkResult.apiKey.licenseResult;
                    if (isLicenseExpired)
                    {
                        JsonHelper.RemoveLicenseFile();
                        globalKey = null;
                        MessageBox.Show("Maalesef ki bu lisans anahtarının süresi dolmuş. Lütfen yeni bir lisans anahtarı satın alınız.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (checkResult.apiKey.HWID == HWID)
                        {
                            if (!isFromTimer) MessageBox.Show("Lisans anahtarı başarıyla etkinleştirildi. İyi oyunlar dileriz!", "Etkinleştirme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnHideKey.Visible = true;
                            globalKey = checkResult.apiKey;

                            LicenseKey licenseKeyGlobal = new LicenseKey
                            {
                                ProductKey = checkResult.apiKey.ProductKey,
                                HWID = HWID
                            };
                            JsonHelper.WriteKeyToFile(licenseKeyGlobal);

                            label1.Visible = false;
                            isTextHidden = true;
                            mskLicenseKey.Visible = false;
                            btnHideKey.BackgroundImage = Resources.see_eye_visible_icon_1878261;
                            btnActivateLicense.Visible = false;
                            btnInjectBot.Visible = true;
                            lblRemaining.Visible = true;
                            lblRemaining.Text = $"Lisansin kalan süresi: {checkResult.apiKey.DaysLeft} gün";
                            timer1.Enabled = true;

                        }
                        else
                        {
                            JsonHelper.RemoveLicenseFile();
                            MessageBox.Show("Bu lisans anahtarı zaten başka bir bilgisayara aktifleştirilmiş. Eğer bunun bir hata olduğunu düşünüyorsanız lütfen lisans anahtarını aldığınız yerle iletişime geçiniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else if (isFromButton)
            {
                mskLicenseKey.Clear();
                MessageBox.Show(checkResult.message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (isFromTimer)
            {
                globalKey = null;
                MessageBox.Show(checkResult.message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mskLicenseKey.Clear();
                JsonHelper.RemoveLicenseFile();
            }
            btnActivateLicense.Enabled = true;
        }

        private static string GetWmiValue(string className, string property)
        {
            try
            {
                var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {className}");
                foreach (var obj in searcher.Get())
                {
                    return obj[property]?.ToString()?.Trim();
                }
            }
            catch { }
            return "null";
        }

        private void maskLicenseKey()
        {
            isTextHidden = !isTextHidden;
            if (isTextHidden)
            {
                lblLicenseKey.Text = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
                btnHideKey.BackgroundImage = Resources.see_eye_visible_icon_1878261;
            }
            else
            {
                lblLicenseKey.Text = licenseKey;
                btnHideKey.BackgroundImage = Resources.no_see_visible_hidde_icon_187886;
            }
        }

        private void mskLicenseKey_Enter(object sender, EventArgs e)
        {
            mskLicenseKey.Select(0, 0);
        }

        bool isTextHidden;
        private void btnHideKey_Click(object sender, EventArgs e)
        {
            maskLicenseKey();
        }

        private void frmClientMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(GlobalVariables.LICENSING_FILE_NAME))
            {
                licenseKeyGlobal = JsonHelper.ReadKeyFromFile();
            }
            ActivateLicense();
            ApplyCustomFont();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            globalKey = null;
            ActivateLicense(false, true);
            if (globalKey != null)
            {
                int daysRemaining = globalKey.DaysLeft;
                lblRemaining.Text = $"Lisansin kalan süresi: {daysRemaining} gün";
                bool isLicenseExpired = !globalKey.licenseResult;
                if (isLicenseExpired)
                {
                    btnHideKey.Visible = false;
                    globalKey = null;

                    lblLicenseKey.Text = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
                    label1.Visible = true;
                    mskLicenseKey.Visible = true;
                    btnActivateLicense.Visible = true;
                    btnInjectBot.Visible = false;
                    lblRemaining.Visible = false;
                    lblRemaining.Text = $"Aktif lisans bulunamadı.";
                    timer1.Enabled = false;
                }
            }
            else
            {
                btnHideKey.Visible = false;

                lblLicenseKey.Text = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
                label1.Visible = true;
                mskLicenseKey.Visible = true;
                btnActivateLicense.Visible = true;
                btnInjectBot.Visible = false;
                lblRemaining.Visible = false;
                lblRemaining.Text = $"Aktif lisans bulunamadı.";
                timer1.Enabled = false;
            }

        }

        private void btnActivateLicense_Click(object sender, EventArgs e)
        {
            ActivateLicense(true);
        }

        private void btnActivateLicense_MouseEnter(object sender, EventArgs e)
        {
            btnActivateLicense.BackgroundImage = Resources.button2w;
        }

        private void btnActivateLicense_MouseLeave(object sender, EventArgs e)
        {
            btnActivateLicense.BackgroundImage = Resources.button2;
        }

        private void btnInjectBot_MouseEnter(object sender, EventArgs e)
        {
            btnInjectBot.BackgroundImage = Resources.button1w;
        }

        private void btnInjectBot_MouseLeave(object sender, EventArgs e)
        {
            btnInjectBot.BackgroundImage = Resources.button1;
        }

        private void btnInjectBot_Click_1(object sender, EventArgs e)
        {
            InjectionHelper.StartInjection();
        }

        private void linkDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link != null)
            {
                System.Diagnostics.Process.Start("explorer.exe", "https://discord.gg/tqN6jV2hDv");
            }
        }
    }
}
