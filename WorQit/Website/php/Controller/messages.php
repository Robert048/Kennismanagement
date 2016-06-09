<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 7-6-2016
 * Time: 13:11
 */

@session_start();
$function= isset($_GET['function']) ? $_GET['function'] : '';

if($function == 'add'){addMessage();}

function addMessage()
{

//extract data from the post
    
    $vars = array('employerID =>' . urlencode($_SESSION['user']->ID),//employeeID
    'sender=> employer', 'message =>' . urlencode($_GET['message']), 'subject =>' . urlencode($_GET['subject']));

    $headers = array();
    $headers[] = 'employerID:' . urlencode($_SESSION['user']->ID);
    $headers[] = 'employeeID:' . urlencode($_GET['employeeID']);
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
}

function showMessages($ID){

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Message/getOverviewEmployer/'.$ID);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}

function deleteMessage($id){
    
}

function getMessage($id){

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Message/getMessage/'.$id);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}
<<<<<<< HEAD
function unreadMessages(){
    $unread = array();
    $messages= showMessages($_SESSION['user']->ID);
    foreach($messages->Messages as $message){

        if($message->read == false){
            $unread[] = $message;
        }
    }
    return $unread;
=======

function updateMessageRead($messageID)
{
    $messageID = $_GET["ID"];

    $editVars = array("ID => '$messageID");

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Message/messageRead/".$messageID);
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

    curl_setopt($ch, CURLOPT_POSTFIELDS, $editVars);

    $headers = array();
    $headers[] = 'ID:' . $messageID;

    $server_output = curl_exec($ch);
    curl_close($ch);
>>>>>>> refs/remotes/origin/master
}