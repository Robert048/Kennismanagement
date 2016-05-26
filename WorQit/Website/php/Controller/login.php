<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 24-5-2016
 * Time: 11:29
 */


$vars = array("username => ".urlencode($_GET['userName']), "password => ".urlencode($_GET['password']));

$ch = curl_init();
curl_setopt($ch, CURLOPT_URL,"http://worqit.azurewebsites.net/api/Employer/logIn");
curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLOPT_POSTFIELDS,$vars);  //Post Fields
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

$headers = array();
$headers[] = 'username:'.urlencode($_GET['userName']);
$headers[] = 'password:'.urlencode($_GET['password']);

curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);

$server_output = curl_exec ($ch);

curl_close ($ch);
//echo serialize($server_output);
echo $server_output;


