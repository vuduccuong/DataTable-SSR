// Copyright (c) CuongVD 2021. All rights reserved.
// Licensed under the cuongvd license.
// Email: vuduccuong.ck48@gmail.com.
// Facebook: vuduc.cuong4

using Microsoft.AspNetCore.Mvc;
using SSR.DataTable.Data;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;

namespace SSR.DataTable.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly DataTableSSRDbContext _dbContext;
        public CustomerController(DataTableSSRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Customers()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData =  (from tempcustomer in _dbContext.Customers select tempcustomer);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.FirstName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.Contact.Contains(searchValue)
                                                || m.Email.Contains(searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("{customerId:int}", Name = "DeleteCustomer")]
        public IActionResult RemoveCustomers(int customerId)
        {
            dynamic customerData = _dbContext.Customers.SingleOrDefault(m => m.Id == customerId);
            if (customerData != null)
            {
                _dbContext.Customers.Attach(customerData);
                _dbContext.Customers.Remove(customerData);
                _dbContext.SaveChanges();
            }
            return Ok(customerData);
        }
    }
}
