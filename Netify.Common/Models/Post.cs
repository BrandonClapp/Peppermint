﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Netify.Common.Models
{
    public class Post
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
