/**
 * Created by maaike on 12-5-2016.
 */

function addVacancy() {

    var request = $.ajax({
        type: "GET",
        url: '../Controller/addVacancy.php',
        data: $('#newVacancy').serializeArray()
    });
    request.done(function () {
        $('#newVacModal').attr('aria-hidden', 'true');
        location.reload();
    });
    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });

}

function deleteVacancy(){
    
    var parameter = 
        $('#delete').data('levelid');
    

    var request = $.ajax({
        type: "GET",
        url: '../Controller/deleteVacancy.php',
        data: "id="+parameter
    });
    request.done(function () {
        $('#parameter').animate({
            backgroundColor: '#FF8585'
        }, 1000, function () {
            $(this).fadeOut(1000);
        });
        //location.reload();
    });
    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });
}