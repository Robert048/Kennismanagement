<?php
/*
 * function to delete vacancy
 */

$url = 'http://worqit.azurewebsites.net/api/Vacancy/deleteVacancy/'.urlencode($_GET['id']);
$postL = mb_strlen(urlencode($_GET['id']));


$curl = curl_init();
curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
curl_setopt($curl, CURLOPT_POST, true);
curl_setopt($curl, CURLOPT_CUSTOMREQUEST, "DELETE");
curl_setopt($curl, CURLOPT_URL, $url);
$content = curl_exec($curl);
curl_close($curl);