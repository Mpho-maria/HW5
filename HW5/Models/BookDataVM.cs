﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW5.Models
{
    public class BookDataVM
    {
        public Book Book { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }
}