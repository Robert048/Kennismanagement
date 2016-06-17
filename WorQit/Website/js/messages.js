/**
 * Created by maaike on 7-6-2016.
 */

function sentMessage(id) {

    var request = $.ajax({
        type: "GET",
        url: '../Controller/messages.php',
        data: {function: 'add', subject: $('#onderwerp').val(), message: $('#bericht').val(), employeeID: $('#employeeID').val()}
    });
    request.success(function() {
        window.location.href = '../View/berichten.php';
    });
    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });

}