var GigsController = function (attendanceService) {

    var button;

    var gigInit = function () {
        $(".js-toggle-attendance").click(function (e) {
            button = $(e.target);
            var gigId = button.attr("data-gig-id");

            if (button.hasClass("btn-default")) {
                attendanceService.createAttendance(done, fail, gigId)
            } else {
                attendanceService.deleteAttendance(done, fail, gigId)
            }
        }
        );
    }

    var done = function () {
        var text = button.text() == "Going" ? "Going?" : "Going";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    const fail = function () {
        alert("Something failed!");
    }

    return { attendToggle: gigInit }
}(AttendanceService);