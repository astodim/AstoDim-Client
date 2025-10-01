using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstoDimClient.ApiLibrary
{
    public class ApiProcessor
    {
        public static async Task<(bool success, string status)> ActivateLicense(string licenseKey, string HWID)
        {
            if (!String.IsNullOrEmpty(licenseKey) || !String.IsNullOrEmpty(HWID))
            {
                string url = $"https://localhost:5025/api/licences/activatelicence?productKey={licenseKey}&HWID={HWID}";

                try
                {
                    using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            bool statusResult = JsonConvert.DeserializeObject<bool>(result);
                            return statusResult ?
                                (true, "Lisans başarıyla aktifleştirildi.") :
                                (false, "Lisans aktifleştirme başarısız oldu. Lütfen lisans anahtarınızı kontrol ediniz.");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            return (false, "Maalesef ki lisanslama sunucularına erişilemedi. Lütfen internet bağlantınızı kontrol ediniz.");
                        }
                        else
                        {
                            return (false, "Maalesef ki aktifleştirme işleminde bir sorun çıktı. Lütfen lisans anahtarınızın doğru olduğundan ve süresinin geçmemiş olduğundan emin olunuz.");
                        }
                    }
                }
                catch (Exception)
                {
                    return (false, "Maalesef ki lisanslama sunucularına erişilemedi, lütfen internet bağlantınız olduğundan emin olunuz.");
                }

            }
            return (false, "Lütfen doğru bir lisans anahtarı girdiğinizden emin olunuz.");
        }
        public static async Task<(ApiKey? apiKey, string message)> CheckLicense(string licenseKey)
        {
            if (!String.IsNullOrEmpty(licenseKey))
            {
                string url = $"https://localhost:5025/api/licences/checklicencewithkey?productKey={licenseKey}";

                try
                {
                    using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        try
                        {
                            ApiKey? apiKey = JsonConvert.DeserializeObject<ApiKey>(result);
                            return apiKey != null ?
                                (apiKey, "Lisans anahtarı bulundu!.") :
                                (apiKey, "Lisans anahtarı kontrolü başarısız oldu. Lütfen lisans anahtarınızı kontrol ediniz.");
                        }
                        catch (Exception)
                        {
                            return (null, "Maalesef ki lisanslama sunucuları tarafında bir hata oluştu. Lütfen tekrar deneyiniz.\nEğer sorun devam ederse lütfen lisans anahtarınızı aldığınız yerle iletişime geçiniz.");
                        }

                    }
                }
                catch (Exception)
                {
                    return (null, "Maalesef ki lisanslama sunucularına erişilemedi, lütfen internet bağlantınız olduğundan emin olunuz.");
                }
            }
            return (null, "Lütfen doğru bir lisans anahtarı girdiğinizden emin olunuz.");
        }

        public static DateTime GetDateTime()
        {
            DateTime dateTime = DateTime.MinValue;
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string todaysDates = response.Headers["date"];

                    dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                        System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while trying to get the current date and time from the server: " + ex.Message);
            }
            

            return dateTime;
        }
    }
}
