/**
 * Created by maaike on 25-4-2016.
 */
$(document).ready(function() {
    $('#login_button').click(function(event) {

        event.preventDefault();
    

        var request= $.ajax({
    
            url: 'php/Controller/login.php',
            type: "GET",
            cache: false,
            data: {"userName" : $('#username').val(), "password" : $('#password').val()}
    
        });
            request.success(function(data) {
               login(data);
        });
            request.error(function (data) {
            console.log(data);
    
        });
    });
});

function login(result){

    var data =  JSON.parse(result);

    if(data["Result"] == "successful") {

        var bool = true;
        $.post({
            url: '../../globals.php',
            type: "POST",
            data: {"bool":bool},
            cache: false

        })
        .success(function() {
        });
        window.location.href = 'index.php';
    }
    else{
        if(data["Error"]== "Verkeerd wachtwoord") {
            $('#password').css('border-color', '#FA0505').css('border-width', '2px');
        }
        else{
            $('#username').css('border-color', '#FA0505').css('border-width', '2px');
        }
    }
}
function forgotPass() {


    $('#forgotPass_button').click(function (event) {

        event.preventDefault();

        $.ajax({

            url: '<?php echo BASE_URL; ?>ajax/passEmail.php',
            type: 'post',
            cache: false,
            data: $('.form-login').serialize()

        }).done(function (data) {
            console.log(data);
            if (data.code == 0) {
                createErrorMessage(data.message);
            }
            else {
                createSuccessMessage("Email verstuurd met een link");
            }

        }).error(function (data) {
            console.log(data);

        });
    });
}

