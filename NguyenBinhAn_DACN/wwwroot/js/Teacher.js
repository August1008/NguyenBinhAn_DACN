var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "autoWidth": false,
        "ajax": {
            "url": "/Admin/Teachers/TeacherList"
        },
        "columns": [
            {
                "data": "id",
                "width":"3%",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "teacherId",
                "width": "10%"
            },
            {
                "data": "name",
                "width": "20%"
            },
            {
                "data": "educationLevel",
                "width": "10%"
            },
            {
                "data": "phoneNumber",
                "width": "12%"
            },
            {
                "data": "email",
                "width": "15%"
            },
            {
                "data": "teacherId",
                "render": function (data) {
                    return ` <a href="/Admin/Teachers/Edit/${data}" asp-route-id="${data}" class="btn btn-dark"><i class="fa-solid fa-pen-to-square"></i> Edit</a>
                <a onclick="deleteConfirm('${data}')" class="btn btn-danger"><i class="fa-solid fa-trash"></i> Delete</a>
                `;
                }, "width": "30%"
            }
        ]
    });
}