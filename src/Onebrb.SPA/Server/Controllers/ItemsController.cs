﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Onebrb.SPA.Server.Controllers
{
    public class ItemsController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
