// Copyright (c) CuongVD 2021. All rights reserved.
// Licensed under the cuongvd license.
// Email: vuduccuong.ck48@gmail.com.
// Facebook: vuduc.cuong4

$(document).ready(function () {
    'use-strics';

    const myTable = $('#my-table').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/v1/customer",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "firstName", "name": "First Name", "autoWidth": true },
            { "data": "lastName", "name": "Last Name", "autoWidth": true },
            { "data": "contact", "name": "Country", "autoWidth": true },
            { "data": "email", "name": "Email", "autoWidth": true },
            { "data": "dateOfBirth", "name": "Date Of Birth", "autoWidth": true },
            {
                "render": function (data, type, row) {return "<a href='#' class='btn btn-danger del-record' data-id='"+row.id+"'>Delete</a>"; }
            },
        ]
    });

    $(document).on('click', '.del-record', function (e) {
        e.preventDefault();
        const id = $(this).attr('data-id');
        if (!id) {
            return;
        }
        const parent = $(this).parent().parent();
        $.ajax({
            url: `/api/v1/customer/${id}`,
            type: 'DELETE',
            success: res => {
                parent.fadeOut('slow', function () { $(this).remove(); });
            },
            error: err => {
                console.log(err);
            }
        });
    });
});

function DeleteCustomer(id){
    
};
