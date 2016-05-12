/**
 * Created by maaike on 25-4-2016.
 */

$(document).ready(function() {
    

    $('#login_button').click(function(event) {

        event.preventDefault();

        $.ajax({

            url : '<?php echo BASE_URL; ?>ajax/login.php',
            type : 'post',
            cache: false,
            data : $('.form-login').serialize()

        }).done(function(data) {
            console.log(data);
            if (data.code == 1)
            {
                $('#login').attr('id = "focusedInput"');
               
            }else if(data.code== 2){
                $('#login').attr('id = "focusedInput"');
            }
            else
            {
                window.location.href = 'index.php';
            }

        }).fail(function(data) {
            console.log(data);

        });

    });
    $('#forgotPass_button').click(function(event) {

        event.preventDefault();

        $.ajax({

            url : '<?php echo BASE_URL; ?>ajax/passEmail.php',
            type : 'post',
            cache: false,
            data : $('.form-login').serialize()

        }).done(function(data) {
            console.log(data);
            if (data.code == 0)
            {
                createErrorMessage(data.message);
            }
            else
            {
                createSuccessMessage("Email verstuurd met een link");
            }

        }).fail(function(data) {
            console.log(data);

        });

    });

});

