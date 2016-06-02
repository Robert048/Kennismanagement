/**
 * Created by maaike on 26-5-2016.
 */

$(document).ready(function() {
    $('#signup_button').click(function (event) {

        event.preventDefault();

        var request = $.ajax({
            type: "GET",
            url: 'php/Controller/addEmployer.php',
            data: {username: $('#newusername').val(), password: $('#newpassword').val(), email: $('#newemail').val()}
        });
        request.success(function() {
            $('#signupModal').attr('aria-hidden', 'true');
            window.location.href = 'index.php';

        });
        request.fail(function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });

    });
});
