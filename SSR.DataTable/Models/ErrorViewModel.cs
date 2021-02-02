// Copyright (c) CuongVD 2021. All rights reserved.
// Licensed under the cuongvd license.
// Email: vuduccuong.ck48@gmail.com.
// Facebook: vuduc.cuong4

using System;

namespace SSR.DataTable.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
