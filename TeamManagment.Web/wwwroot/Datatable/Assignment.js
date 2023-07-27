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
                        return "<div data-EditId-id='" + data.id + "'class='btn updateMoodel' type = 'button' data-bs-toggle='modal' data-bs-target='#userEditModel' >" +
                            "<span class='svg-icon svg-icon-muted svg-icon-2hx'>" +
                            "<svg width='24' height='24' viewBox='0 0 24 24' fill='none' xmlns='http://www.w3.org/2000/svg'>" +
                            "<path opacity='0.3' fill-rule='evenodd' clip-rule='evenodd' d='M2 4.63158C2 3.1782 3.1782 2 4.63158 2H13.47C14.0155 2 14.278 2.66919 13.8778 3.04006L12.4556 4.35821C11.9009 4.87228 11.1726 5.15789 10.4163 5.15789H7.1579C6.05333 5.15789 5.15789 6.05333 5.15789 7.1579V16.8421C5.15789 17.9467 6.05333 18.8421 7.1579 18.8421H16.8421C17.9467 18.8421 18.8421 17.9467 18.8421 16.8421V13.7518C18.8421 12.927 19.1817 12.1387 19.7809 11.572L20.9878 10.4308C21.3703 10.0691 22 10.3403 22 10.8668V19.3684C22 20.8218 20.8218 22 19.3684 22H4.63158C3.1782 22 2 20.8218 2 19.3684V4.63158Z' fill='currentColor'/>" +
                            "<path d='M10.9256 11.1882C10.5351 10.7977 10.5351 10.1645 10.9256 9.77397L18.0669 2.6327C18.8479 1.85165 20.1143 1.85165 20.8953 2.6327L21.3665 3.10391C22.1476 3.88496 22.1476 5.15129 21.3665 5.93234L14.2252 13.0736C13.8347 13.4641 13.2016 13.4641 12.811 13.0736L10.9256 11.1882Z' fill='currentColor'/>" +
                            "<path d='M8.82343 12.0064L8.08852 14.3348C7.8655 15.0414 8.46151 15.7366 9.19388 15.6242L11.8974 15.2092C12.4642 15.1222 12.6916 14.4278 12.2861 14.0223L9.98595 11.7221C9.61452 11.3507 8.98154 11.5055 8.82343 12.0064Z' fill='currentColor'/>" +
                            "</svg></span>" +
                            "</div>" +
                            // href='/User/Delete/" + data.id + "'
                            "<a  href='/TeamMember/ShowAssignment/" + data.id + "'class='btn delete' data-id='" + data.id + "'>" +
                            "<span class='svg-icon svg-icon-muted svg-icon-2hx'>" +
                            "<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' fill='currentColor' class='bi bi-eye' viewBox='0 0 24 24'>"+
                            "<path d='M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z' />"+
                            "<path d='M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z' />"+
                            "</svg></span>" +
                            "</a>";



                    }
                }
            ],
          
        });
    return dataTable;
}




















