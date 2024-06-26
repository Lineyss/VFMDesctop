using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace VFMDesctop.Models.Services
{
    internal class AuthService
    {
        private readonly HttpWorker httpWorker;
        private readonly JsonService jsonService;
        private const string url = "asd";
        public AuthService()
        {
            jsonService = new JsonService();
            httpWorker = new HttpWorker();
        }

        private static User authentedUser { get; set; }

        public async Task<bool> Exit()
        {
            authentedUser = null;

            return false;
        }
        public bool userIsAuth() => authentedUser == null ? false : true;
        public async Task<bool> Auth(string email, string password)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("email", email);
            dict.Add("password", password);
            string response = await httpWorker.sendPostRequest(url, dict);
            dict = jsonService.Deserialize<Dictionary<string, string>>(response);

            return dict == null ? false : true;
        }
    }
}
