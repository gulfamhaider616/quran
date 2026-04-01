$(document).ready(function () {
    $('#schedule').click(function () {
        var studentid = $('#studentid').val();
        var totalclasses = $('#totalclasses').val();
        var daysname = $('#daysname').val();
        var classtime = $('#classtime').val();
        var tutorname = $('#tutorname').val();
        var description = $('#description').val();
        debugger;
        $.ajax({
            url: '/Admin/SaveSchedule',
            type: 'GET',
            datatype: 'Json',
            data: { 'studentid': studentid, 'totalclasses': totalclasses, 'daysname': daysname, 'classtime': classtime, 'tutorname': tutorname, 'description': description },
            success: function (result) {
                if(result>0)
                {
                    //$('#contact').parent().parent().hide();
                    swal("Schedule has been saved!");
                }
                else
                {
                    //$('#contact').parent().parent().hide();
                    swal("Sorry, we can't save this schedule. Try again");
                }
            },
            error: function () {
                swal("Sorry, we can't save this schedule due to server error.");
            }

        });
    });

});