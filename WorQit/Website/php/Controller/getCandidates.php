<?php
/**
 * Created by PhpStorm.
 * User: Thom
 * Date: 02/06/2016
 * Time: 12:17
 */

/*
 * get all candidates by vacancyID, to see which candidates liked the vacancy
 */
function getCandidates($ID){

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Vacancy/getCandidates/'.$ID);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}