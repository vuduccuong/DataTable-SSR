// Copyright (c) CuongVD 2021. All rights reserved.
// Licensed under the cuongvd license.
// Email: vuduccuong.ck48@gmail.com.
// Facebook: vuduc.cuong4

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSR.DataTable.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
