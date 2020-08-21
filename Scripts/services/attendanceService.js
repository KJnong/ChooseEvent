var AttendanceService = function () {
    var createAttendance = function (done, fail, gigId) {
        $.post("/api/attendances/attend", { gigId: gigId })
            .done(done)
            .fail(fail);
    }

    var deleteAttendance = function (done, fail, gigId) {
        $.ajax({
            url: "/api/attendances/CancelAttendence/" + gigId,
            method: "DELETE"
        }).done(done)
            .fail(fail);
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();