using SetupTool.ApiLibrary;
using SetupTool.Properties;
using System.Management;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SetupTool
{
    public partial class frmClientMain : Form
    {
        public frmClientMain()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
        }
        string licenseKey;
        string HWID;
        ApiKey? globalKey;
        LicenseKey licenseKeyGlobal;

        static DateTime GetDateTime()
        {
            DateTime dateTimeNow = ApiProcessor.GetDateTime();
            if (dateTimeNow == DateTime.MinValue)
                MessageBox.Show("Sunuculara erişilemedi. Lütfen internet bağlantınızı kontrol ediniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return dateTimeNow;
        }

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

        async void ActivateLicense(bool isFromButton = false)
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
                        MessageBox.Show("Lütfen doğru bir lisans anahtarı girdiğinizden emin olunuz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnActivateLicense.Enabled = true;
                        return;
                    }
                    licenseKey = mskLicenseKey.Text;
                }
                else
                    licenseKey = licenseKeyGlobal.ProductKey;
            }
            else if (isFromButton)
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
                else
                {
                    bool isLicenseExpired = checkResult.apiKey.ExpireDate.CompareTo(GetDateTime()) < 0;
                    if (isLicenseExpired)
                    {
                        JsonHelper.RemoveLicenseFile();
                        MessageBox.Show("Maalesef ki bu lisans anahtarının süresi dolmuş. Lütfen yeni bir lisans anahtarı satın alınız.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (checkResult.apiKey.HWID == HWID)
                        {
                            MessageBox.Show("Lisans anahtarı başarıyla etkinleştirildi. İyi oyunlar dileriz!", "Etkinleştirme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (globalKey is not null)
            {
                int daysRemaining = (globalKey.ExpireDate - GetDateTime()).Days;
                lblRemaining.Text = $"Lisansin kalan süresi: {daysRemaining} gün";
                bool isLicenseExpired = globalKey.ExpireDate.CompareTo(GetDateTime()) < 0;
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

                    MessageBox.Show("Maalesef ki lisansınızın süresi doldu. Lütfen yeni bir lisans anahtarı satın alınız.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void btnActivateLicense_Click(object sender, EventArgs e)
        {
            ActivateLicense(true);
        }

        private void btnActivateLicense_MouseEnter(object sender, EventArgs e)
        {
            btnActivateLicense.BackgroundImage = Resources.pipis2;
        }

        private void btnActivateLicense_MouseLeave(object sender, EventArgs e)
        {
            btnActivateLicense.BackgroundImage = Resources.pipis;
        }

        private void btnInjectBot_MouseEnter(object sender, EventArgs e)
        {
            btnInjectBot.BackgroundImage = Resources.popos21;
        }

        private void btnInjectBot_MouseLeave(object sender, EventArgs e)
        {
            btnInjectBot.BackgroundImage = Resources.popos1;
        }

        private void btnInjectBot_Click_1(object sender, EventArgs e)
        {
            InjectionHelper.StartInjection();
        }
    }
}
