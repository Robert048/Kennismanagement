<?php
/*
 * get all vacancies by vacancyID
 * @param ID
 */
function showVacancies($ID){

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Vacancy/getVacancies/'.$ID);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}

/*
 * get all vacancies
 */
function getAllVacancies(){
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Vacancy/getAllVacancies');
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);

}

/*
 * get all likes by vacancyID
 */
function getAllLikes($ID){
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Vacancy/getAllLikes/'.$ID);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}

/*
 * get all likes that are not seen yet
 */
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

/*
 * when like is seen, update in the database to seen
 * replace old values
 */
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