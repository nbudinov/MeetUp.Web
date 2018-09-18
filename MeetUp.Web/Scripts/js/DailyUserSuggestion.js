var POPUP_COOKIE_NAME = "DAILY_USER_SUGGESTION"

$(document).ready(function () {

    var data = {

    }

    var intervalId = setInterval(function () {

        $.ajax({
            type: "POST",
            url: "/dailyUserSuggestion",
            data: {},
            success: function (data) {
                console.log(data)
                if (data.html) {
                    console.log(data.html);
                    $(".modal-body").html(data.html);
                    $('#popupModal').modal("show");
                } else {
                    clearInterval(intervalId);
                }
            }
        });
           
    }, 5000);

})





function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
