// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.btn.btn-leftside').click(function () {
        $(this).toggleClass("click");
        $('.sidebar').toggleClass("show");
        $('.row.row-main').toggleClass("right-move");

    });



    $('.sidebar ul li a').click(function () {
        var id = $(this).attr('id');
        $('nav ul li ul.item-show-' + id).toggleClass("show");
        $('nav ul li #' + id + ' span').toggleClass("rotate");

    });

    $('nav ul li').click(function () {
        $(this).addClass("active").siblings().removeClass("active");
    });

});

var closesIcon = document.querySelectorAll('.xd-message .close-icon');

closesIcon.forEach(function (closeIcon) {
    closeIcon.addEventListener('click', function () {
        this.parentNode.parentNode.classList.add('hide');
    });
});




$(function () {
    $("#ddlProvince").change(function () {
        $.getJSON("/api/Address/get-district-list", { ProvinceId: $("#ddlProvince").val() }, function (d) {
            var row = "";
            $("#ddlDistrict").empty();
            $.each(d, function (i, v) {
                row += "<option value=" + v.value + " >" + v.text + "</option>"
            });
            $("#ddlDistrict").html(row);
        })
    })
    $("#ddlDistrict").change(function () {
        $.getJSON("/api/Address/get-ward-list", { DistrictId: $("#ddlDistrict").val() }, function (d) {
            var row = "";
            $("#ddlWard").empty();
            $.each(d, function (i, v) {
                row += "<option value=" + v.value + " >" + v.text + "</option>"
            });
            $("#ddlWard").html(row);
        })
    })

    //show class list in department
    $("#ddlDepartment").change(function () {
        $.getJSON("/Admin/Courses/GetClassList", { DepartmentId: $("#ddlDepartment").val() }, function (d) {
            var row = "";
            $("#ddlClass").empty();
            $.each(d, function (i, v) {
                row += "<option value=" + v.value + " >" + v.text + "</option>"
            });
            $("#ddlClass").html(row);
        })
    })
})

function deleteConfirm(id) {
    if (confirm("Are you want to delete " + id)) {
        $.ajax({
            url: "/Admin/Teachers/Delete",
            type: 'DELETE',
            data: { 'id': id },
            success: function (data, message) {
                successNotify();
                $('#tblData').DataTable().ajax.reload();
            },
            error: function () {
                failedNotify();
            }
        });
    }
}
function deleteStudent(id,name) {
    if (confirm("Are you want to delete " + name)) {
        $.ajax({
            url: "/Admin/Student/Delete",
            type: 'DELETE',
            data: { 'id': id },
            success: function (data, message) {
                successNotify();
                $('#tblData').DataTable().ajax.reload();
            },
            error: function () {
                failedNotify();
            }
        });
    }
}
function deleteDepartment(id,name) {
    if (confirm("Are you want to delete " + name)) {
        $.ajax({
            url: "/Admin/Departments/Delete",
            type: 'DELETE',
            data: { 'id': id },
            success: function (data, message) {
                successNotify();
                $('#tblData').DataTable().ajax.reload();
            },
            error: function () {
                failedNotify();
            }
        });
    }
}
function deleteClass(id, name) {
    if (confirm("Are you want to delete " + name)) {
        $.ajax({
            url: "/Admin/Classes/Delete",
            type: 'DELETE',
            data: { 'id': id },
            success: function (data, message) {
                successNotify();
                $('#tblData').DataTable().ajax.reload();
            },
            error: function () {
                failedNotify();
            }
        });
    }
}
function deleteSubject(id, name) {
    if (confirm("Are you want to delete " + name)) {
        $.ajax({
            url: "/Admin/Subjects/Delete",
            type: 'DELETE',
            data: { 'id': id },
            success: function (data, message) {
                successNotify();
                $('#tblData').DataTable().ajax.reload();
            },
            error: function () {
                failedNotify();
            }
        });
    }
}
//delete course
function deleteCourse(data) {
    if (confirm("Are you want to delete " )) {
        $.ajax({
            url: "/Admin/Courses/Delete",
            type: 'DELETE',
            data: { 'id': data },
            success: function (data, message) {
                successNotify();
                $('#tblData').DataTable().ajax.reload();
            },
            error: function () {
                failedNotify();
            }
        });
    }
}

function DeleteImage(filepath, imgid) {
    $.ajax({
        url: "/Admin/Student/DeleteImage",
        type: 'POST',
        data: { 'filepath': filepath },
        success: function (data, message) {
            let x = parseInt($('#ImageCount').text()) - 1;
            $('#ImageCount').html(x);
            $(`#${imgid}`).remove();
            successNotify();
        },
        error: function () {
            failedNotify();
        }
    })
}


const successNotify = () => {
    var messageBox = $('#message_box');
    messageBox.html('<b><h3>Success!</h3></b>');
    messageBox.css("box-shadow", "0px 5px 15px #04ED4C");

    messageBox.fadeIn('fast');
    setTimeout(function () {
        messageBox.hide();
    }, 1000);
}

const failedNotify = () => {
    var messageBox = $('#message_box');
    messageBox.html('<b><h3>Failed!</h3></b>');
    messageBox.css("box-shadow", "0px 5px 15px red");

    messageBox.fadeIn('fast');
    setTimeout(function () {
        messageBox.hide();
    }, 1000);
}

function ExportExcel(courseOrder) {
    $.ajax({
        url: "/api/TeacherAPIs/export-excel-file",
        type: "GET",
        data: { 'id': courseOrder },
        success: function () {
            successNotify();
        },
        error: function () {
            failedNotify();
        }
    })
}