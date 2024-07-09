using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.Interfaces;

namespace VFMDesctop.Models.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpService httpService;
        public AuthorizationService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public void Login(string Email, string Password)
        {

        }
    }
}
