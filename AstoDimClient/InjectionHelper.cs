using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupTool
{
    public class InjectionHelper
    {
        static string dllPath = Application.StartupPath + GlobalVariables.DLL_PATH;
        public static void StartInjection()
        {
            if(File.Exists(dllPath))
            {
                Injector.InjectDll(dllPath);
            }
            else
            {
                MessageBox.Show("Maalesef ki enjekte edilecek DLL dosyası bulunamadı.\nLütfen dosyaları kontrol ediniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
