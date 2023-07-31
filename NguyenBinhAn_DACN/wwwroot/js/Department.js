var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "autoWidth": false,
        "ajax": {
            "url": "/Admin/Departments/DepartmentList"
        },
        "columns": [
            {
                "data": "id",
                "width":"10%",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "name",
                "width": "50%"
            },
            {
                "data":
                {
                    "_":null,
                    id: "DepartmentId",
                    name: "name"
                },
      
                "render": function (data) {
                    return ` <a href="/Admin/Departments/Edit/${data.departmentId}" asp-route-id="${data.departmentId}" class="btn btn-dark"><i class="fa-solid fa-pen-to-square"></i> Edit</a>
                <a onclick="deleteDepartment(${data.departmentId},'${data.name}')" class="btn btn-danger"><i class="fa-solid fa-trash"></i> Delete</a>
                `;
                }, "width": "40%"
            }
        ]
    });
}