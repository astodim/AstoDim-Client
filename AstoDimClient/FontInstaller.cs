using System.Runtime.InteropServices;
using Microsoft.Win32;

public class FontInstaller
{
    [DllImport("gdi32.dll")]
    private static extern int AddFontResource(string lpFilename);

    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    private const int WM_FONTCHANGE = 0x001D;
    private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);

    public static bool InstallFont(string fontFilePath, string fontName)
    {
        try
        {
            // Copy font to Windows Fonts folder
            string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            string fontFileName = Path.GetFileName(fontFilePath);
            string destPath = Path.Combine(fontsFolder, fontFileName);

            if (!File.Exists(destPath))
            {
                File.Copy(fontFilePath, destPath, false);
            }

            // Register font with Windows
            AddFontResource(destPath);
            SendMessage(HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);

            // Add registry entry
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", true))
            {
                if (key != null)
                {
                    key.SetValue(fontName, fontFileName);
                }
            }

            return true;
        }
        catch (UnauthorizedAccessException)
        {
            // Requires admin privileges
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error installing font: {ex.Message}");
            return false;
        }
    }

    public static bool IsFontInstalled(string fontName)
    {
        using (Font testFont = new Font(fontName, 12f))
        {
            return testFont.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase);
        }
    }
}