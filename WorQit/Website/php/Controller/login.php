<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 24-5-2016
 * Time: 11:29
 */

/*
 * function to decide if login is successed
 */

    header('Content-Type: application/json');

    $aResult = array();

    if( !isset($_GET['functionname']) ) { $aResult['error'] = 'No function name!'; }

    if( !isset($_GET['arguments']) ) { $aResult['error'] = 'No function arguments!'; }

    if( !isset($aResult['error']) ) {

        switch($_GET['functionname']) {
            case 'login':
                if( !is_array($_GET['arguments']) || (count($_GET['arguments']) < 2) ) {
                    $aResult['error'] = 'Error in arguments!';
                }
                else {
                    $aResult = login($_GET['arguments']);
                }
                break;
            case 'loggedin':
                if((count($_GET['arguments']) < 1) ) {
                    $aResult['error'] = 'Error in arguments!';
                }
                else {
                    $aResult['result'] = loggedin($_GET['arguments']);
                }
                break;
            case 'logout':
                    $aResult['result'] = logout();
                break;
            default:
                $aResult['error'] = 'Not found function '.$_GET['functionname'].'!';
                break;
        }

    }

    echo json_encode($aResult);
    return json_encode($aResult);


/*
 *  function to login to te application, send user to index page when successful
*/
function login($data)
{

    $vars = array("username => " . urlencode($data[0]), "password => " . urlencode($data[1]));

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Employer/logIn");
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $vars);  //Post Fields
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

    $headers = array();
    $headers[] = 'username:' . urlencode($data[0]);
    $headers[] = 'password:' . urlencode($data[1]);

    curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);

    $server_output = curl_exec($ch);

    curl_close($ch);
    //echo serialize($server_output);
    //echo $server_output;
    return $server_output;

}

/*
 * determine if the login was succesfull or not
 */
function loggedin($result){
    $data = json_decode($result);
    session_start();
    $_SESSION['user']= $data->User[0];
    $_SESSION['isloggedin']= true;
    return "Succes";
}

/*
 * sign logged in user out
 */
function logout(){
    session_start();
    session_unset();
    session_destroy();
    return "logged out!";
}


