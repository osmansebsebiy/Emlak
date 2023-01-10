using EmlakProject.Models;
using EmlakProject.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakProject.Services.Bussiness
{
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();
        public int indexLogin = 0;
        public bool Authenticate(UserModel user)
        {
            if (user.userEmail != null && user.userPassword != null)
                return daoService.FindByUser(user);
            
            else
            {
                indexLogin = 1;
                return false;
            }
        }
    }
}