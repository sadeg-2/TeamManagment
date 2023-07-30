function DataTable() {
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
                url: '../GetDataTableSubmission',
                type: "POST",
                data: function (d) {
                    const dataIdValue = document.getElementById("currentID").getAttribute("data-id");
                    d.assignmentId = dataIdValue;
                }
            },
            //bSort: false,
            processing: true,
            serverSide: true,
            filter: true,
            columns: [
                {
                    "data": null, "name": "Created At", "autowidth": true,
                    "sorting": false,
                    "render": function (data, type, row) {
                        return `<td class="text-center">
                                    <span class="badge badge-light-danger fw-semibold me-1">${data.createdAt}</span>
                            </td>`;
                    }
                },
                {
                    "data": null, "name": "Left To Time", "autowidth": true,
                    "sorting": false,
                    "render": function (data, type, row) {
                        return `<td class="text-center">
                                    <span class="badge badge-light-success fw-semibold me-1">${data.timeToLeft}</span>
                            </td>`;
                    }
                },
                {
                    "data": null, "name": "Actions", "autowidth": true,
                    "sorting": false,
                    "render": function (data, type, row) {
                        return `<td class="text-center">
                                    <span class="badge badge-light-danger fw-semibold me-1">${data.createdAt}</span>
                            </td>`;
                    }
                },
                
               
            ],
          
        });
    return dataTable;
}




















