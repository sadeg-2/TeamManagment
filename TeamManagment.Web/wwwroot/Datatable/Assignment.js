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
                url: 'GetDataTableAssignment',
                type: "POST",
                data: function (d) {
                    // Add the selected value from the form-select to the AJAX request data
                    var teamId = $('#teamSelect').val();
                    d.teamId = teamId; // Assuming the server expects the filter parameter as 'filter'
                }

            },
            //bSort: false,
            processing: true,
            serverSide: true,
            filter: true,
            columns: [
                { data: "title", name: "Title", className: "dt-right align-middle" },
                { data: "teamName", name: "TeamName", className: "dt-center align-middle" },
                {
                    "data": null, "name": "DeadLine", "autowidth": true,
                    "sorting": false,
                    "render": function (data, type, row) {
                        return `<td class="text-end">
                                    <span class="badge badge-light-danger fw-semibold me-1">${data.deadLine}</span>
                            </td>`;
                    }
                },
                {
                    data: null,
                    name: "Status",
                    className: "dt-center align-middle",
                    render: function (data) {
                        var badgeClass = '';

                        if (data.status === 'IsCompleted') {
                            badgeClass = 'badge-light-success';
                        } else if (data.status === 'UnCompleted') {
                            badgeClass = 'badge-light-warning';
                        } else {
                            badgeClass = 'badge-light-error';
                        }
                        return "<td class='text-end'>" +
                            "<span class='badge " + badgeClass + "'>" + data.status + "</span>" +
                            "</td>";
                    }
                },
                {
                    data: null, name: "Actions", orderable: false, className: "dt-right align-middle",
                    render: function (data) {
                        return "<div data-EditId-id='" + data.id + "'class='btn btn-icon  btn-bg-light btn-active-color-primary btn-sm me-1 updateMoodel' type = 'button' data-bs-toggle='modal' data-bs-target='#userEditModel' >" +
                            "<span class='svg-icon svg-icon-muted svg-icon-2hx'>" +
                            `<i class="fa-solid fa-square-plus"></i>` +
                            "</span>" +
                            "</div>" +
                            // href='/User/Delete/" + data.id + "'
                            "<a  href='/TeamMember/ShowAssignment/" + data.id + "'class='btn btn-icon  btn-bg-light btn-active-color-primary btn-sm me-1 delete' data-id='" + data.id + "'>" +
                            "<span class='svg-icon svg-icon-muted svg-icon-2hx'>" +
                            `<i class="fa-regular fa-eye"></i>`+
                            "</span>" +
                            "</a>";
                    }
                }
            ],
          
        });
    return dataTable;
}




















