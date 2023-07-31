var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "autoWidth": false,
        "ajax": {
            "url": "/Admin/Subjects/SubjectList"
        },
        "columns": [
            {
                "data": "id",
                "width":"5%",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "subjectId",
                "width": "10%"
            },
            {
                "data": "subjectName",
                "width": "35%"
            },
            {
                "data": "credit",
                "width": "5%"
            },
            {
                "data": "numberOfLesson",
                "width": "5%"
            },
            {
                "data": {
                    id: "subjectId",
                    name:"subjectName"
                },
                "render": function (data) {
                    return ` <a href="/Admin/Subjects/Edit/${data.subjectId}" asp-route-id="${data.subjectId}" class="btn btn-dark"><i class="fa-solid fa-pen-to-square"></i> Edit</a>
                <a onclick="deleteSubject('${data.subjectId}','${data.subjectName}')" class="btn btn-danger"><i class="fa-solid fa-trash"></i> Delete</a>`;
                }, "width": "30%"
            }
        ]
    });
}