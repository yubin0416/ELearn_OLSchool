using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderCenter.API
{
    public class BaseController : Controller
    {
        public string UserID => User.Claims.Where(v => v.Type == "sub").FirstOrDefault().Value;
    }
}
