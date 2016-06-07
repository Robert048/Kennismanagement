<?php

function deleteEmployer()
{
    $ID = $_SESSION["user"][0]["ID"];

    $deleteVars = array("ID => '$ID', password => 'onzin'");
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Employer/deleteEmployer');
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, 'DELETE');
    curl_setopt($ch, CURLOPT_POSTFIELDS, $deleteVars);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

    $deleteHeaders = array();
    $deleteHeaders[] = 'ID:' . $ID;
    $deleteHeaders[] = 'password: test';
    curl_setopt($ch, CURLOPT_HTTPHEADER, $deleteHeaders);
    $server_output = curl_exec($ch);
    curl_close($ch);
}
