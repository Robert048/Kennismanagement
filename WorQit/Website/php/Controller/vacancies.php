<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 18-5-2016
 * Time: 15:09
 */

function showVacancies($ID){

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Vacancy/getVacancies/'.$ID);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}

function getAllVacancies(){
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Vacancy/getAllVacancies');
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);

}