<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 26-5-2016
 * Time: 12:05
 */

$vars = array('username =>' . urlencode($_GET['username']), 'password =>' . urlencode($_GET['password']),
                'email=>' . urlencode($_GET['email']));

$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Employer/addEmployer");
curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLOPT_POSTFIELDS, $vars);  //Post Fields
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);


$headers = array();
$headers[] = 'username:' . urlencode($_GET['username']);
$headers[] = 'password:' . urlencode($_GET['password']);
$headers[] = 'email:' . urlencode($_GET['email']);

curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);

$server_output = curl_exec($ch);

curl_close($ch);
//$data= unserialize($server_output);
//echo $server_output;
$data = json_decode($server_output);
session_start();
$_SESSION['user']= $data->employer;
$_SESSION['isloggedin']= true;
return $server_output;