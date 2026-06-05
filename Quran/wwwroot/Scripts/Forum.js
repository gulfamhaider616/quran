

$(document).ready(function () {
    $('#questionSubmit').click(function () {
        var uname = $('#qname').val();
        var qemail = $('#qemail').val();
        var qcountry = $('#qcountry').val();
        var qsubject = $('#qsubject').val();
        var qexplanation = $('#qexplanation').val();
        $.ajax({
            url: '/Forum/SaveQuestion',
            type: 'GET',
            dataType: 'json',
            data: {
                'uname': uname,
                'qemail': qemail,
                'qcountry': qcountry,
                'qsubject': qsubject,
                'qexplanation': qexplanation
            },
            success: function (result) {
                if(result==true)
                {
                    showPopupModel("Thank you!", "We'll review your question and publish it very soon.", "/Forum")
                }
                else
                {
                    showPopupModel("Sorry!", "We can't publish your question, Please try again.", "/Forum/AskQuestion")
                }
            },
            error: function () {
                showPopupModel("Sorry!", "We can't publish your question due to internal error.", "/Forum/AskQuestion")
            }
        });
    });
});

function showPopupModel(headingText, paragraphText, baseUrl) {
    var modal = document.getElementById('myModal');
    document.getElementById('modelheading').innerHTML = headingText;
    document.getElementById('modelparagraph').innerHTML = paragraphText;
    modal.style.display = "block";
    var span = document.getElementsByClassName("close")[0];
    span.onclick = function () {
        modal.style.display = "none";
        window.location.href = baseUrl;
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            window.location.href = baseUrl;
        }
    }
}

function PublishQuestion(QuestionID) {
    $.ajax({
        url: '/Admin/PublishQuestionByAdmin',
        type: 'GET',
        datatype: 'Json',
        data: { 'QuestionID': QuestionID },
        success: function (result) {
            if (result == true) {
                showPopupModel("Thank you!", "Your Question has been Published.", "/Admin/UnPublish")
            }
            else {
                showPopupModel("Sorry!", "We can't publish your question, Please try again.", "/Admin/UnPublish")
            }
        },
        error: function () {
            showPopupModel("Sorry!", "We can't publish your question due to internal error.", "/Admin/UnPublish")
        }
    });
}

function UnPublishQuestion(QuestionID) {
    $.ajax({
        url: '/Admin/UnPublishQuestionByAdmin',
        type: 'GET',
        datatype: 'Json',
        data: { 'QuestionID': QuestionID },
        success: function (result) {
            if (result == true) {
                showPopupModel("Done!", "The question has been unpublished.", "/Admin/Publish")
            }
            else {
                showPopupModel("Sorry!", "We can't unpublish this question, please try again.", "/Admin/Publish")
            }
        },
        error: function () {
            showPopupModel("Sorry!", "We can't unpublish this question due to internal error.", "/Admin/Publish")
        }
    });
}

function DeleteQuestions(QuestionID) {
    $.ajax({
        url: '/Admin/DeleteQuestions',
        type: 'GET',
        datatype: 'Json',
        data: { 'QuestionID': QuestionID },
        success: function (result) {
            if (result == true) {
                showPopupModel("Thank you!", "Your Question has been Deleted.", "/Admin/UnPublish")
            }
            else {
                showPopupModel("Sorry!", "We can't Deleted your question, Please try again.", "/Admin/UnPublish")
            }
        },
        error: function () {
            showPopupModel("Sorry!", "We can't Deleted your question due to internal error.", "/Admin/UnPublish")
        }
    });
}