﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Dtos.Common
{
    public class Pagination
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int? PageSize { get; set; }
        public int? Page { get; set; }
    }
}
