/**
 * Created by maaike on 12-5-2016.
 */

function addVacancy() {

    var request = $.ajax({
        type: "GET",
        url: '../Controller/addVacancy.php',
        data: $('#newVacancy').serializeArray()
    });
    request.success(function() {
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
        $('#').live('click', function() {
            var here = this;
            $(this).closest('tr').find('td').fadeOut('fast',
                function(here){
                    $(here).parents('tr:first').remove();
                });

            return false;
        });
     location.reload();
    });
    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });
}