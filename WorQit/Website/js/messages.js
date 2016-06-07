/**
 * Created by maaike on 7-6-2016.
 */

function sentMessage() {

    var request = $.ajax({
        type: "GET",
        url: '../Controller/addVacancy.php',
        data: {subject: $('#onderwerp').val(), message: $('#bericht')}
    });
    request.success(function() {
        //header to messages
    });
    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });

}