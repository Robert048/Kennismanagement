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
    $data = json_decode($server_output);
    return $server_output;
}

/*
 * show messages for the employer
 * fill ID to get messages for that employer
 */
function showMessages($ID){

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Message/getOverviewEmployer/'.$ID);
    $content = curl_exec($curl);
    curl_close($curl);

    $allMessages = json_decode($content);
    $receivedMessages= array();
    if(!empty($allMessages->Messages)){
        foreach($allMessages->Messages as $allMessage){
            if($allMessage->sender== 'employee'){
                $receivedMessages[] = $allMessage;
            }
        }
    }
    return $receivedMessages;
}

/*
 * get Message by message ID
 */
function getMessage($id){

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Message/getMessage/'.$id);
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}

/*
 * get messages that are not read, for user signed in
 */
function unreadMessages()
{
    $unread = array();
    if ($_SESSION['user'] === null) {
        return $unread;
    } else {
        $messages = showMessages($_SESSION['user']->ID);
        if (!empty($messages)) {
            foreach ($messages as $message) {

                if ($message->read == false) {
                    $unread[] = $message;
                }
            }
        }
        return $unread;
    }
}

/*
 * when a message is read, update not read to read
 */
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
}

/*
 * get all messages based on employerID, employeeID
 */
function getAllMessages($employerID, $employeeID, $count)
{
     $curl = curl_init();
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($curl, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Message/getLast?employerID='.$employerID.'&employeeID='.$employeeID.'&count='.$count.'');
    $content = curl_exec($curl);
    curl_close($curl);

    return json_decode($content);
}

/*
 *  get last messages between employer and employee and give in how many you want to get back
 */
function getLastMessages($employerID, $employeeID, $count, $title){

    $allMessages = getAllMessages($employerID, $employeeID, $count);
    $messages = array();
    foreach($allMessages->Messages as $message){

        if($message->title == $title){
            $messages[] = $message;
        }
    }
    $reverseArray = array_reverse($messages);
    $lastMessages = array_slice($reverseArray,0,3);
    return $lastMessages;
}