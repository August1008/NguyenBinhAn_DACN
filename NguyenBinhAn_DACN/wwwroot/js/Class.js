var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "autoWidth": false,
        "ajax": {
            "url": "/Admin/Classes/ClassList"
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
                    id: "classId",
                    name: "name"
                },
                "render": function (data) {
                    return ` <a href="/Admin/Classes/Edit/${data.classId}" asp-route-id="${data.classId}" class="btn btn-dark"><i class="fa-solid fa-pen-to-square"></i> Edit</a>
                <a onclick="deleteClass(${data.classId},'${data.name}')" class="btn btn-danger"><i class="fa-solid fa-trash"></i> Delete</a>
                `;
                }, "width": "40%"
            }
        ]
    });
}