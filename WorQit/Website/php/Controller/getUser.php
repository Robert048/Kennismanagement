<?php
/**
 * Created by PhpStorm.
 * User: Thom
 * Date: 26/05/2016
 * Time: 11:25

**/

$vars = array("username => 'test', password => 'test'");

$ch = curl_init();
curl_setopt($ch, CURLOPT_URL,"http://worqit.azurewebsites.net/api/Employer/logIn");
curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLOPT_POSTFIELDS,$vars);  //Post Fields
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

$headers = array();
$headers[] = 'username: test';
$headers[] = 'password: test';
curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
$server_output = curl_exec ($ch);
curl_close ($ch);
$var = json_decode($server_output, true);

$GLOBALS["bedrijfsnaam"] = $var["User"][0]["name"];
$GLOBALS["userId"] = $var["User"][0]["ID"];