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

function getAllLikes($ID){
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Vacancy/getAllLikes/'.$ID);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}

function getUnreadLikes(){
   @session_start();
    $allLikes= getAllLikes($_SESSION['user']->ID);
    $unread = array();
    if($allLikes != null) {
        foreach ($allLikes as $like) {
            if ($like->seen == false && $like->seen !== null) {
                $unread[] = $like;
            }
        }
    }
    return $unread;
}

function updateLikeSeen($vacancyID, $allCandidates){
    $unreadLikes = getUnreadLikes();

    foreach($unreadLikes as $unreadLike) {
        foreach($allCandidates->Users as $candidate) {
            if ($unreadLike->employeeID == $candidate->ID && $unreadLike->vacancyID == $vacancyID) {
                $matchID = $unreadLike->matchID;
                $editVars = array("ID => '$matchID");

                $ch = curl_init();
                curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Vacancy/reactionSeen/" . $matchID);
                curl_setopt($ch, CURLOPT_POST, 1);
                curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

                curl_setopt($ch, CURLOPT_POSTFIELDS, $editVars);

                $headers = array();
                $headers[] = 'ID:' . $matchID;

                $server_output = curl_exec($ch);
                curl_close($ch);
            }
        }
    }
}