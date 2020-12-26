﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Services.Models
{
    public abstract class BaseModel
    {
        public int ItemId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SecurityHash { get; set; }
    }
}
