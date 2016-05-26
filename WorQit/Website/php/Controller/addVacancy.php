<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 18-5-2016
 * Time: 16:58
 */


    //extract data from the post
    //set POST variables
    $data = array(
        'employerID' => 9,
        'function' => urlencode($_GET['functie']),
        'description' => urlencode($_GET['description']),
        'salary' => urlencode($_GET['salary']),
        'hours' => urlencode($_GET['hours']),
        'requirements' => urlencode($_GET['requirements']),
        'tags' => urlencode($_GET['tags']));

    $postvars = '';
    $count = 1;
    foreach($data as $key=>$value) {
        if(count($data)== $count){
            $postvars .= $key . "=" . $value;
        }
        else {
            $postvars .= $key . "=" . $value . "&";
        }
        $count++;
    }
    $url = 'http://worqit.azurewebsites.net/api/Vacancy/addVacancy?employerID=9&function='.urlencode($_GET['functie']).'&description='.urlencode($_GET['description']).
            '&salary='.urlencode($_GET['salary']).'&hours='.urlencode($_GET['hours']).'&requirements='.urlencode($_GET['requirements']).'&tags='.urlencode($_GET['tags']);
    $postL = mb_strlen($postvars);

    //open connection
    $ch = curl_init();

    //set the url, number of POST vars, POST data
    curl_setopt($ch, CURLOPT_URL, $url);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_POSTFIELDS,  $postvars);
    curl_setopt($ch, CURLOPT_HEADER, 0);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_HTTPHEADER, array('Content-Length: '.$postL));

    //execute post
    $result = curl_exec($ch);

    //close connection
    curl_close($ch);

