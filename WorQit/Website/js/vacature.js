/**
 * Created by maaike on 12-5-2016.
 */

function addVacancy() {

    var request = $.ajax({
        type: "GET",
        url: '../Controller/addVacancy.php',
        data: $('#newVacancy').serializeArray()
    });
    request.done(function(data) {
        $('#newVacModal').attr('aria-hidden', 'true');
        $('#vacancyTable').append('<tr class="vacancyRow">' +
            '<td><a href="vacancieDetails.php?ID=" + data->ID">data->jobfunction?></a></td>' +
            '<td class=hidden-phone>data->description</td>' +
            '<td><button id="delete" class="btn btn-danger btn-xs" data-levelid= data->ID onclick="deleteVacancy()"><i class="fa fa-trash-o "></i></button></td>' +
            '</tr>');
        //location.reload();
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
        $(this).closest('tr').find('td').fadeOut( "slow" );
        //animate({
        //    backgroundColor: '#FF8585'
       // }, 1000, function () {
        //    $(this).fadeOut(1000);
        //});
        //location.reload();
    });
    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });
}