var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "autoWidth": false,
        "ajax": {
            "url": "/Admin/Courses/CourseList"
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
                "data": "className",
                "width": "7%"
            },
            {
                "data": "subjectName",
                "width": "15%"
            },
            {
                "data": "teacherId",
                "width": "5%"
            },
            {
                "data": {
                    startTime: "startTime",
                    endTime: "endTime"
                },
                "render": function (data) {
                    return data.startTime + " to " + data.endTime;
                },
                "width": "10%"
            },
            {
                "data": {
                    startDate: "startDate",
                    endDate: "endDate"
                },
                "render": function (data) {
                    return  data.startDate + "  --  " + data.endDate;
                },
                "width": "30%"
            },
            {
                "data":  "courseOrder",
                "render": function (data) {
                    return ` <a href="/Admin/Courses/Edit/${data}" asp-route-id="${data}" class="btn btn-dark"><i class="fa-solid fa-pen-to-square"></i> Edit</a>
                <a onclick="deleteCourse(${data})" class="btn btn-danger"><i class="fa-solid fa-trash"></i> Delete</a>`;
                }, "width": "30%"
            }
        ]
    });
}