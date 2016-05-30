/**
 * Created by maaike on 26-5-2016.
 */

function addEmployer() {

    var request = $.ajax({
        type: "GET",
        url: '../Controller/addVacancy.php',
        data: $('#newEmployer').serializeArray()
    });
    request.success(function () {
        $('#signupModal').attr('aria-hidden', 'true');
        location.reload();
    });
    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });

}