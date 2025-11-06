using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupTool
{
    public class ApiKey
    {
        public string Owner { get; set; }
        public string HWID { get; set; }
        public string ProductKey { get; set; }
        public int DaysLeft { get; set; }
        public bool IsActivated { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool licenseResult { get; set; }
    }
}
