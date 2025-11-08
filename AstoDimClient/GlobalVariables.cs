using System;
using System.Windows.Forms;
namespace SetupTool
{
    public static class GlobalVariables
    {
        public const string DLL_NAME = "advapi64.dll";
        public const string DLL_PATH = @"MediaCache\" + DLL_NAME;

        public const string LICENSING_FILE_NAME = "assets_index.json";

        public const string HOST_URL = "https://astodim.com.tr/";
    }
}
