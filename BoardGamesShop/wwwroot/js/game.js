var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Admin/Game/GetAll"
        },
        "columns": [
        { "data": "name", "width": "15%" },
        { "data": "description", "width": "15%" },
        { "data": "minPlayers", "width": "15%" },
        { "data": "maxPlayers", "width": "15%" },
        { "data": "authors", "width": "15%" },
        { "data": "price", "width": "15%" },
        { "data": "listPrice", "width": "15%" },
        { "data": "price3", "width": "15%" },
        { "data": "price10", "width": "15%" },
        { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return`

                    <div class = "w-75 btn-group" role="group">
                        <a href="/Admin/Game/Upsert?id=${data}" class="btn btn-warning m-3"><i class="bi bi-pencil-square"></i></a>
                        <a onClick=Delete('/Admin/Game/Delete/${data}') class="btn btn-danger m-3"><i class="bi bi-trash"></i></a>
                    </div>`
                },
                "width": "15%"
            },

        ]

      
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
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