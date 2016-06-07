<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 7-6-2016
 * Time: 13:11
 */

require_once("../../globals.php");
session_start();

//extract data from the post

$password= password_hash($_GET['password'], PASSWORD_DEFAULT);
$vars = array('employerID =>' . urlencode($_SESSION['user']->ID), 'employeeID =>' . ,
    'sender=> employer', 'message =>'. urlencode($_GET['message']), 'subject =>'. urlencode($_GET['subject']));

$headers = array();
$headers[] = 'employerID:' . urlencode($_SESSION['user']->ID);
$headers[] = 'employeeID:' . urlencode();
$headers[] = 'sender:' . urlencode('employer');
$headers[] = 'message:' . urlencode($_GET['message']);
$headers[] = 'subject:' . urlencode($_GET['subject']);

$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Message/sendMessage");
curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLOPT_POSTFIELDS, $vars);  //Post Fields
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);

$server_output = curl_exec($ch);

curl_close($ch);
//$data= unserialize($server_output);
//echo $server_output;
$data = json_decode($server_output);
return $server_output;