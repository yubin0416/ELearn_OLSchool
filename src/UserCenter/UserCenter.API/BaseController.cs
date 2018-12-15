using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserCenter.API
{
    public class BaseController : Controller
    {
        public string UserID => User.Claims.FirstOrDefault(v=>v.Type=="sub").Value;
    }
}
