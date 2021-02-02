// Copyright (c) CuongVD 2021. All rights reserved.
// Licensed under the cuongvd license.
// Email: vuduccuong.ck48@gmail.com.
// Facebook: vuduc.cuong4

using Microsoft.EntityFrameworkCore;
using SSR.DataTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSR.DataTable.Data
{
    public class DataTableSSRDbContext : DbContext
    {
        public DataTableSSRDbContext(DbContextOptions<DataTableSSRDbContext> options) : base(options)
        {
        }

      public DbSet<Customer> Customers { get; set; }
    }
}
