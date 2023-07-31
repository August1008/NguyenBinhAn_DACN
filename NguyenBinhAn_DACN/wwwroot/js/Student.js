var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "autoWidth": false,
        "ajax": {
            "url": "/Admin/Student/StudentList"
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
                "data": "studentId",
                "width": "8%"
            },
            {
                "data": "name",
                "width": "15%"
            },
            {
                "data": "class",
                "width": "8%"
            },
            {
                "data": "department",
                "width": "20%"
            },
            {
                "data": "email",
                "width": "13%"
            },
            {
                "data": {
                    id: "studentId",
                    name:"name"
                },
                "render": function (data) {
                    return ` <a href="/Admin/Student/Edit/${data.studentId}" asp-route-id="${data.studentId}" class="btn btn-dark"><i class="fa-solid fa-pen-to-square"></i> Edit</a>
                <a onclick="deleteStudent('${data.studentId}','${data.name}')" class="btn btn-danger"><i class="fa-solid fa-trash"></i> Delete</a>
                `;
                }, "width": "20%"
            },
            {
                "data":
                {
                    studentId: "studentId",
                    imagesCount: "imagesCount"
                },
                "render": function (data) {
                    return `<a href="/Admin/Student/StudentImage?studentId=${data.studentId}" class="btn btn-warning"><i class="fa-solid fa-camera-retro"></i> ${data.imagesCount} images</a>`;
                },
                "width":"13%"
            }
        ]
    });
}