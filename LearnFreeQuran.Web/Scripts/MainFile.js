$(document).ready(function () {
    $('#days').hide();
    $('#incorrect').hide();
    $('#correct').hide();

    $('#classes').change(function () {
        if ($('#classes').val() != '7') {
            $('#days').show();
        }
        else {
            $('#days').hide();
        }
    });

    $('#email').change(function () {
        var email = $('#email').val();
        if(isValidEmailAddress(email)){
        $.ajax({
            url: '/Home/VarifyEmail',
            type: 'GET',
            dataType: 'json',
            data: { 'email': email },
            success: function (data) {
                debugger;
                if(data == true)
                {
                    $('#correct').show();
                    $('#incorrect').hide();
                    $("#email").css("background", "white");
                }
                else {
                    $('#incorrect').show();
                    $('#correct').hide();
                    $("#email").css("background", "red");
                }
            },
            error: function () {
                $('#incorrect').show();
                $('#correct').hide();
                $("#email").css("background", "red");
            }
        });
        } else {
            $('#incorrect').hide();
            $('#correct').hide();
            alert('Please enter valid email address.');
            $("#email").css("background", "red");
        }

    });

    $('#editemail').blur(function () {
        var email = $('#editemail').val();
        if (isValidEmailAddress(email)) {
            $.ajax({
                url: '/Home/VarifyEmail',
                type: 'GET',
                dataType: 'json',
                data: { 'email': email },
                success: function (data) {
                    $("#editemail").css("background", "white");
                    //debugger;
                    //if (data == true) {
                    //    $('#correct').show();
                    //    $('#incorrect').hide();
                    //}
                    //else {
                    //    $('#incorrect').show();
                    //    $('#correct').hide();
                    //}
                },
                error: function () {

                    alert('Please enter valid email address.');
                    $("#editemail").css("background", "red");
                }
            });
        } else {
            alert('Please enter valid email address.');
            $("#editemail").css("background", "red");
        }

    });

    function isValidEmailAddress(emailAddress) {
        var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
        return pattern.test(emailAddress);
    };

    //$('.confirm').click(function () {
    //    alert('clicked');
    //    window.location.href = "/";
    //});
    $('#missingdays').hide();
    $('#termserror').hide();
    $('#registrationSubmit').click(function () {
        debugger;
        var studentname = $('#studentname').val();
        var fathername = $('#fathername').val();
        var phonenumber = $('#phonenumber').val();
        var email = $('#email').val();
        var skypeid = $('#skypeid').val();
        var gender = $('#gender').val();
        var date = $('#date').val();
        var month = $('#month').val();
        var year = $('#year').val();
        var country = $('#country').val();
        var city = $('#city').val();
        var classes = $('#classes').val();
        var days = $('#days').val();
        var feasibletime = $('#feasibletime').val();
        var language = $('#language').val();

        if ($('#CHKBOX1').is(":checked") && studentname != "" && fathername != "" && phonenumber != "" && email != "" && skypeid != "" && gender != "" && year != "" && country != "" && city != "" && classes != "" && language!="")
        {
            if (classes != 7 && days == "")
            {
                $('#missingdays').show();
            }
            else
            {
                $('#missingdays').hide();
                $('#termserror').hide();
                $.ajax({
                    url: '/Home/SaveRegistration',
                    type: 'GET',
                    dataType: 'json',
                    data: {
                        'studentname': studentname,
                        'fathername': fathername,
                        'phonenumber': phonenumber,
                        'email': email,
                        'skypeid': skypeid,
                        'gender': gender,
                        'date': date,
                        'month': month,
                        'year': year,
                        'country': country,
                        'city': city,
                        'classes': classes,
                        'days': days,
                        'feasibletime': feasibletime,
                        'language':language
                    },
                    success: function (result) {
                        if (result != false)
                        {
                            var StudentID = result.StudentID;
                            var Name = result.StudentName;
                            swal({
                                title: "Hi " + Name + "!  Your Record has been saved successfully.",
                                text: "Please remember this Student ID : <span style='color:red;font-weight: bold;'>" + StudentID + "</span>.  You can check your schedule by using this ID.",
                                type: "success",
                                html: true
                            });
                        }
                        else
                        {
                            swal({
                                title: "Sorry " + Name + "!  We can not save your record.",
                                text: "Please try again.",
                                type: "success",
                                html: true
                            });
                        }
                    },
                    error: function () {
                        alert('Sorry! We cannt save your record due to server error. Try again');
                    }
                });
            }
        }
        else
        {
            alert('Please fill all required fields.');
            $('#termserror').show();
        }
    });

 
    $('#scheduletable').hide();
    $('#edittableinformation').hide();
    $('#recordnotfound').hide();
    $('#recordnotfoundduetoserver').hide();
    $('#studentshedulelogin').click(function () {
        debugger;
        var studentid = $('#studentscheduleid').val();
        $('#schedulebody').html('');
        $.ajax({
            url: '/Home/GetStudentScheduleDataByID',
            type: 'GET',
            datatype: 'Json',
            data: { 'studentid': studentid },
            success: function (data) {
                if (data.StudentID != null && data.StudentID !="")
                {
                    $('#schedulebody').append("<tr><td><span><a href='/Home/StudentDetails?StudentID=" + data.StudentID + "'>" + data.StudentID + "</a></span></td><td>" + data.StudentName + "</td><td>" + data.Classes + "</td>"

                        + "<td>" + data.Days + "</td><td>" + data.ClassTime + "</td><td>" + data.TutorName + "</td></tr>");

                    $('#scheduletable').show();
                    $('#recordnotfoundduetoserver').hide();
                    $('#recordnotfound').hide();
                }
                else
                {
                    $('#recordnotfound').show();
                    $('#recordnotfoundduetoserver').hide();
                }
            },
            error: function () {
                $('#recordnotfoundduetoserver').show();
            }
        });
    });

    $('#studentforgetbutton').click(function () {
        var email = $('#forgetemailid').val();
        $.ajax({
            url: '/Home/GetForgetStudentIDBy',
            type: 'GET',
            datatype: 'Json',
            data: { 'email': email },
            success: function (data) {
                if (data.StudentID != null && data.StudentID!="")
                {
                    swal({
                        title: "Hi " + data.StudentName + "!  Your Student ID : <span style='color:red;font-weight: bold;'>" + data.StudentID + "</span>.",
                        text: "Please remember your Student ID. We have your record on the basis of this ID.",
                        type: "success",
                        html: true
                    });
                }
                else
                {
                    swal("Sorry, We can't find your Record!");
                }
            },
            error: function () {
                swal("Schedule has been saved!");
            }
        });
    });

    $('#contactbutton').click(function () {
        debugger;
        var name = $('#User_Name').val();
        var phone = $('#User_Phone').val();
        var email = $('#User_email').val();
        var subjct = $('#subject').val();
        var message = $('#message').val();
        if (name != "" && phone != "" && email != "" && subjct!="" && message != "")
        {
            $.ajax({
                url: '/Home/SaveContactUs',
                type: 'GET',
                datatype: 'Json',
                data: { 'User_Name': name, 'User_Phone': phone, 'User_email': email, 'subject': subjct, 'message': message },
                success: function (data) {
                    if (data > 0) {
                        swal({
                            title: "Thanks!",
                            text: "Your Message has been sent.",
                            type: "success",
                            html: true
                        });
                    }
                    else {
                        alert("Sorry, Please try again.");
                    }
                },
                error: function () {
                    swal({
                        title: "Sorry!",
                        text: "We are facing some internal error.",
                        type: "error",
                        html: true
                    });
                }
            });
        }
        else {
            swal({
                title: "Sorry!",
                text: "Please Enter all required values.",
                type: "error",
                html: true
            });
        }
    })

    $('#feedbackbutton').click(function () {
        var feedbackname = $('#feedbackname').val();
        var feedbackcountry = $('#feedbackcountry').val();
        var feedbackmessage = $('#feedbackmessage').val();
        $.ajax({
            url: '/Home/SaveFeedback',
            type: 'GET',
            datatype: 'Json',
            data: { 'name': feedbackname, 'country': feedbackcountry, 'message': feedbackmessage },
            success: function (result) {
                if (result > 0) {
                    swal({
                        title: "Thanks!",
                        text: "Your Feedback has been sent.",
                        type: "success",
                        html: true
                    });
                }
                else {
                    alert("Sorry, Please try again.");
                }
            },
            error: function () {
                alert("Sorry! We can't send your feedback due to some internal error.");
            }
        });
    });

    //Student Edit Portal
    $('#editstudentbutton').click(function () {
        debugger;
        var studentid = $('#editstudentid').val();
        //$('#schedulebody').html('');
        $.ajax({
            url: '/Home/GetEditInformation',
            type: 'GET',
            datatype: 'Json',
            data: { 'StudentID': studentid },
            success: function (data) {
                if (data.StudentID != null && data.StudentID != "") {
                    $('#studentidinformation').html(data.StudentID);
                    $('#editstudentname').val(data.StudentName);
                    $('#editfathername').val(data.FatherName);
                    $('#editphonenumber').val(data.PhoneNumber);
                    $('#editemail').val(data.Email);
                    $('#editskypeid').val(data.SkypeID);
                    $('#editgender').val(data.Gender);
                    $('#editbirth').val(data.DateOfBirth);
                    $('#editcountry').val(data.Country);
                    $('#editcity').val(data.City);
                    $('#editlanguage').val(data.FirstLanguage);
                    $('#editclass').val(data.Classes);
                    $('#editdays').val(data.Days);
                    $('#editfeasibletime').val(data.FeasibleTime);
                    $('#recordnotfoundduetoserver').hide();
                    $('#recordnotfound').hide();
                    $('#edittableinformation').show();
                }
                else {
                    $('#recordnotfound').show();
                    $('#edittableinformation').hide();
                    $('#recordnotfoundduetoserver').hide();
                }
            },
            error: function () {
                $('#recordnotfoundduetoserver').show();
                $('#edittableinformation').hide();
            }
        });
    });

    $('#editrecordbutton').click(function () {
        var studentid= $('#studentidinformation').text();
        var studentname=$('#editstudentname').val();
        var fathername=$('#editfathername').val();
        var phonenumber=$('#editphonenumber').val();
        var email=$('#editemail').val();
        var skypeid=$('#editskypeid').val();
        var gender=$('#editgender').val();
        var dateofbirth=$('#editbirth').val();
        var country=$('#editcountry').val();
        var city=$('#editcity').val();
        var language=$('#editlanguage').val();
        var classes=$('#editclass').val();
        var days=$('#editdays').val();
        var feasibletime = $('#editfeasibletime').val();
        $.ajax({
            url: '/Home/SaveUpdatedRecord',
            datatype: 'json',
            type: 'GET',
            data: {
                'studentid': studentid, 'studentname': studentname, 'fathername': fathername, 'phonenumber': phonenumber, 'email': email,
                'skypeid': skypeid, 'gender': gender, 'dateofbirth': dateofbirth, 'country': country, 'city': city, 'language': language, 'classes': classes, 'days': days, 'feasibletime': feasibletime
            },
            success: function (data) {
                if (data.StudentID != null && data.StudentID != "") {
                    swal({
                        title: "Hi " + data.StudentName + "! ",
                        text: "Your information has been updated.",
                        type: "success",
                        html: true
                    });
                }
                else {
                    swal("Sorry, We can't update your Record!");
                }
            },
            error: function () {
                swal("Sorry! We can't update your rocord due to server error.");
            }
        });
    });

  
});
function changeLesson(lessonid) {
    $.ajax({
        url: '/Home/SectionLesson',
        type:'GET',
        datatype: 'Json',
        data: {'LessonID':lessonid},
        success: function (data) {
            //console.log(data);
            var lessonID = data.LessonID;        
            var LessonName = data.LessonName;
            var Lessonurl = "~/assets/VideoLessons/" + LessonName + ".mp4";
            $('#lessonname').html("Listen Carefully Qurani "+LessonName);
            $('#lessonurl').attr("src", "~/assets/VideoLessons/" + LessonName + ".mp4");
            var html = "<video id='video' width='640' height='480' poster='sample-video.jpg' controls autoplay preload style='width: 100%;height:100%;border: 1px solid black;'><source id='lessonurl' src='/assets/VideoLessons/" + LessonName.toString() + ".mp4' type='video/mp4'></video>"
            removeElement("innerdiv");
            addElement("videodiv", "div", "innerdiv", html);
        },
        error: function()
        {
            alert("Server Error");
        }
    });
}
function addElement(parentId, elementTag, elementId, html) {
    // Adds an element to the document
    var p = document.getElementById(parentId);
    var newElement = document.createElement(elementTag);
    newElement.setAttribute('id', elementId);
    newElement.innerHTML = html;
    p.appendChild(newElement);
}
function removeElement(elementId) {
    // Removes an element from the document
    var element = document.getElementById(elementId);
    element.parentNode.removeChild(element);
}
function Addookmark(id) {
    if (id != "") {
        $.ajax({
            url: '/User/Addookmark',
            type: 'GET',
            datatype: 'Json',
            data: { 'id': id },
            success: function (data) {
                if (data != null) {
                    //swal({
                    //    title: "Thanks!",
                    //    text: "Your BookMark has been recorded",
                    //    type: "success",
                    //    html: true
                    //});
                    if (data.includes("UserLogin")) {
                        window.location.href = data;
                    }
                }
                else {
                    swal({
                        title: "Sorry!",
                        text: "Please login first to record your Bookmark",
                        type: "Warning",
                        html: true
                    });
                }
            },
            error: function () {
                swal({
                    title: "Sorry!",
                    text: "We are facing some internal error.",
                    type: "error",
                    html: true
                });
            }
        });
    }
    else {
        swal({
            title: "Sorry!",
            text: "Please report this Error to support team",
            type: "error",
            html: true
        });
    }
}