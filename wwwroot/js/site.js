//Site Specific Works
$(document).ready(function () {

    /////////////////////////////////Datatable for Showing self previous application
    if ($('#self-previous-applications-datatable').length > 0) {
        //Datatable with ID exists
        $('#self-previous-applications-datatable').DataTable({
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            "processing": true,
            "serverSide": true,
            "filter": true,
            "ajax": {
                "url": $('meta[name=datatable-url]').attr('content'),
                "type": "POST",
                "datatype": "json"
            },
            //"columnDefs": [{
            //    "targets": [4],
            //    "visible": true,
            //    "searchable": false,
            //    "orderable": false
            //}],
            "columns": [
                { "data": "leaveStart", "name": "Leave Start", "autoWidth": true },
                { "data": "leaveEnd", "name": "Leave End", "autoWidth": true },
                { "data": "leaveType", "name": "Leave Type", "autoWidth": true },
                { "data": "applicationStatus", "name": "Application Status", "autoWidth": true },
                {
                    "data": "id",
                    "name": "Id",
                    "render": function (data, row) {
                        var viewUrl = $('meta[name=view-url]').attr('content').slice(0, -1) + data;
                        var editUrl = $('meta[name=edit-url]').attr('content').slice(0, -1) + data;
                        var deleteUrl = $('meta[name=delete-url]').attr('content').slice(0, -1) + data;
                        var html = '<div class="btn-group" role="group" aria-label="Action"> <a href = "' + viewUrl + '" class="btn btn-primary" >View</a> <a href="' + editUrl + '" class="btn btn-warning">Edit</a> <a href="' + deleteUrl + '" class="btn btn-danger">Delete</a></div >';
                        //return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + data + "'); >Delete</a>";
                        return html;
                    },
                    "searchable": false,
                    "orderable": false
                }
            ]
        });
    }
    /////////////////////////////////////////
});