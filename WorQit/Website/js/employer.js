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
        request.success(function(result) {
            var data= JSON.parse(result);

            if(data["Result"] == "successful") {
                $('#signupModal').attr('aria-hidden', 'true');
                window.location.href = 'index.php';
            }
            else{
                console.log(data);
                alert(data["Error"]);
            }

        });
        request.fail(function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });

    });
});
