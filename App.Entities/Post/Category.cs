﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Post
{
    public class Category :EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
