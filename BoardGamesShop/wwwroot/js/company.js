var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "postalCode", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return`

                    <div class = "w-75 btn-group" role="group">
                        <a href="/Admin/Company/Upsert?id=${data}" class="btn btn-warning m-3"><i class="bi bi-pencil-square"></i></a>
                        <a onClick=Delete('/Admin/Company/Delete/${data}') class="btn btn-danger m-3"><i class="bi bi-trash"></i></a>
                    </div>`
                },
                "width": "15%"
            },

        ]

      
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Jetseś pewny?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Tak, usunąć!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}