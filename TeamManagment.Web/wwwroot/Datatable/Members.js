function DataTable() {
    const memberId = document.getElementById('kt_app_content_container').getAttribute('data-id');

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
                url: '/Team/GetDataTableForMember',
                type: "POST",
                data: function (d) {
                //    d.memberId = memberId;
                }
            },
            //bSort: false,
            processing: true,
            serverSide: true,
            filter: true,
            columns: [
                { data: "userName", name: "UserName", className: "dt-center align-middle" },
                { data: "memberPosition", name: "MemberPosition", className: "dt-center align-middle" },
                {
                    "data": null, "name": "DOB", "autowidth": true,
                    "sorting": false,
                    "render": function (data, type, row) {
                        return `<td class="text-end">
                                            <span class="badge badge-light-danger fw-semibold me-1">${data.joinDate}</span>
                                        </td>`;
                    }
                },
                {
                    data: null,
                    name: null,
                    orderable: false,
                    className: "dt-center align-middle",
                    render: function (data) {
                        return "<a id='deleteButton_" + data.id + "'class='btn btn-icon  btn-bg-light btn-active-color-primary btn-sm me-1 delete' data-delete-url='/Team/DeleteMember/'>" +
                            "<span class='svg-icon svg-icon-2'>" +
                            `<i class="fa-solid fa-trash-can"></i>` +
                            "</span>" +
                            "</a>" +
                            "<div data-Memmber-id='" + data.id + "'class='btn btn-icon  btn-bg-light btn-active-color-primary btn-sm me-1 assignButton' type = 'button' data-bs-toggle='modal' data-bs-target='#assignModel' data-bs-trigger='hover' title='Assign Task' >" +
                            "<span class='svg-icon svg-icon-2'>" +
                            `<i class="fa-solid fa-square-plus"></i>` +
                            "</span>" +
                            "</div>";
                    },
                }
            ],
          
        });
    return dataTable;
}
// Function to handle delete confirmation
function DeleteItem() {
    var deleteUrl = $(this).data('delete-url');
    var id = $(this).attr('id').split('_')[1];

    Swal.fire({
        title: 'Are you sure?',
        text: `You are about to delete This Member with ID: ${id}`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            // User clicked "Yes"
            $.ajax({
                url: deleteUrl,
                type: 'Get',
                data: {
                    memberId: id
                },
                success: function (response) {
                    Swal.fire({
                        title: 'Deleted!',
                        text: `This Member with ID: ${id} has been deleted.`,
                        icon: 'success',
                        showConfirmButton: true,
                        confirmButtonText: 'OK'
                    }).then(() => {
                        // Perform any additional action after the confirmation alert is closed
                        console.log('Additional action after OK is pressed');

                        datatable.ajax.reload();
                    });
                },
                error: function (xhr, status, error) {
                    Swal.fire('Error', 'An error occurred while deleting the Task.', 'error');
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            // User clicked "No" or closed the dialog
            Swal.fire('Cancelled', `Deletion of Member with ID: ${id} was cancelled.`, 'info');
        }
    });
}
// Add event listener to delete buttons
$(document).on('click', '[id^="deleteButton_"]', DeleteItem);
// Event delegation to handle click events on elements dynamically added to the DOM
$(document).on("click", ".assignButton", function () {
    let memberId = $(this).closest("div[data-Memmber-id]").attr("data-Memmber-id");
    $.ajax(
        {
            url: "/Team/AssignTask/" + memberId,
            success: function (result) {
                $(".modal-content").html(result);
                $("#addTaskModel").modal("show");
            }
        });
    return false;
});




















