
function DataTable(url, columns)
{
    if (url == undefined || url == null || url == "")
    {
        url = "GetDataTableData";
    }

    var dataTable = $("#kt_datatable_dom_positioning").DataTable(
        {
            "language": {
                "lengthMenu": "عرض _MENU_",
            },
            "dom":
                "<'row'" +
                "<'col-sm-6 d-flex align-items-center justify-conten-start'l>" +
                "<'col-sm-6 d-flex align-items-center justify-content-end'f>" +
                ">" +

                "<'table-responsive'tr>" +

                "<'row'" +
                "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
                "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
                ">",
            ajax: {
                url: url,
                type: "POST",
                data: function (d) {
                    // Add the selected value from the form-select to the AJAX request data
                    var selectedValue = $('#TaskFilter').val();
                    d.filter = selectedValue; // Assuming the server expects the filter parameter as 'filter'
                }

            },
            //bSort: false,
            processing: true,
            serverSide: true,
            filter: true,
            columns: columns,
            fnDrawCallback: function (data)
            {
                if (typeof _funAfterGetData == 'function')
                {
                    _funAfterGetData();
                }
            }
        });
    return dataTable;

}

