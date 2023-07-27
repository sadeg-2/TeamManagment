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
                url: 'GetDataTableColleagues',
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
                {
                    data: null, name: "Member", sorting: false,
                    render: function (data, type, row) {
                        return `<div class="d-flex align-items-center">
                                    <div class="symbol symbol-45px me-5">
                                        <img src="../../Images/${data.imageUrl}" alt="" />
                                    </div>
                                    <div class="d-flex justify-content-start flex-column">
                                        <a href="#" class="text-dark fw-bold text-hover-primary fs-6">${data.fullName}</a>
                                    </div>
                                </div>`;
                    }
                },
                {
                    data: null, name: "Position",className: "dt-center align-middle",
                    render: function (data) {
                        var badgeClass = '';

                        if (data.position === 'TeamLeader') {
                            badgeClass = 'badge-light-success';
                        } else if (data.position === 'TeamMember') {
                            badgeClass = 'badge-light-warning';
                        } else {
                            badgeClass = 'badge-light-error';
                        }
                        return "<td class='text-end'>" +
                            "<span class='badge " + badgeClass + "'>" + data.position + "</span>" +
                            "</td>";
                    }
                },
                {
                    data: null, name: "Rating", sorting: false,
                    render: function (data, type, row) {
                        return `<div class="d-flex flex-column w-100 me-2">
				                    <div class="d-flex flex-stack mb-2">
					                    <span class="text-muted me-2 fs-7 fw-bold">${data.myRating}%</span>
				                    </div>
				                    <div class="progress h-6px w-100">
					                    <div class="progress-bar bg-primary" role="progressbar" style="width:${data.myRating}%" aria-valuenow="${data.myRating}" aria-valuemin="0" aria-valuemax="100"></div>
				                    </div>
			                    </div>`;
                    }
                },
                {
                    data: null, name: "Actions", orderable: false, className: "dt-center align-middle",
                    render: function (data) {
                        return `<div class="d-flex justify-content-center flex-shrink-0">
				                    <a href="#" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
					                    <!--begin::Svg Icon | path: icons/duotune/general/gen019.svg-->
					                    <span class="svg-icon svg-icon-3">
						                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
							                    <path d="M17.5 11H6.5C4 11 2 9 2 6.5C2 4 4 2 6.5 2H17.5C20 2 22 4 22 6.5C22 9 20 11 17.5 11ZM15 6.5C15 7.9 16.1 9 17.5 9C18.9 9 20 7.9 20 6.5C20 5.1 18.9 4 17.5 4C16.1 4 15 5.1 15 6.5Z" fill="currentColor" />
							                    <path opacity="0.3" d="M17.5 22H6.5C4 22 2 20 2 17.5C2 15 4 13 6.5 13H17.5C20 13 22 15 22 17.5C22 20 20 22 17.5 22ZM4 17.5C4 18.9 5.1 20 6.5 20C7.9 20 9 18.9 9 17.5C9 16.1 7.9 15 6.5 15C5.1 15 4 16.1 4 17.5Z" fill="currentColor" />
						                    </svg>
					                    </span>
					                    <!--end::Svg Icon-->
				                    </a>
				                    <a href="/TeamMember/Profile/${data.id}" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1" data-bs-trigger='hover' title='Show Profile'>
					                    <!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
					                    <span class="svg-icon svg-icon-2">
											<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
												<path d="M20 14H18V10H20C20.6 10 21 10.4 21 11V13C21 13.6 20.6 14 20 14ZM21 19V17C21 16.4 20.6 16 20 16H18V20H20C20.6 20 21 19.6 21 19ZM21 7V5C21 4.4 20.6 4 20 4H18V8H20C20.6 8 21 7.6 21 7Z" fill="currentColor"></path>
												<path opacity="0.3" d="M17 22H3C2.4 22 2 21.6 2 21V3C2 2.4 2.4 2 3 2H17C17.6 2 18 2.4 18 3V21C18 21.6 17.6 22 17 22ZM10 7C8.9 7 8 7.9 8 9C8 10.1 8.9 11 10 11C11.1 11 12 10.1 12 9C12 7.9 11.1 7 10 7ZM13.3 16C14 16 14.5 15.3 14.3 14.7C13.7 13.2 12 12 10.1 12C8.10001 12 6.49999 13.1 5.89999 14.7C5.59999 15.3 6.19999 16 7.39999 16H13.3Z" fill="currentColor"></path>
											</svg>
										</span>
					                    <!--end::Svg Icon-->
				                    </a>
				                    <a href="#" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm">
					                    <!--begin::Svg Icon | path: icons/duotune/general/gen027.svg-->
					                    <span class="svg-icon svg-icon-3">
						                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
							                    <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="currentColor" />
							                    <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="currentColor" />
							                    <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="currentColor" />
						                    </svg>
					                    </span>
					                    <!--end::Svg Icon-->
				                    </a>
			                    </div>`;
                    }
                }
            ],
          
        });
    return dataTable;
}




















