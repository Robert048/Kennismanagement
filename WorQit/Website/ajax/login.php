<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 26-4-2016
 * Time: 11:16
 */

session_start();

// Headers om een JSON response te genereren
header('Cache-Control: no-cache, must-revalidate');
header('Expires: Mon, 26 Jul 1997 05:00:00 GMT');
header('Content-type: application/json');

$message = array();

try{
    $pdo = new PDO(DB_HOST, DB_USER, DB_PASS);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
}
catch (PDOException $e) {
    echo 'Connection failed: ' . $e->getMessage();
}

    // Gegevens van de gebruikersnaam ophalen
    $query = $pdo->prepare("SELECT ID, password FROM employer WHERE email = :email LIMIT 1");
    $query->execute([':email'=> $_POST['username']]);
    $fetch = $query->fetchAll();

    // Checken of er een gebruiker is gevonden
    if ($query->rowCount() == 0)
    {
        $message['code'] = 0;
        $message['message'] = 'Gebruiker is niet gevonden.';
    }
    else
    {
        // Password controleren
        if (password_verify($_POST['password'], $fetch['Password']))
        {

            $message['code'] = 1;
            $message['message'] = '';
        }
        // Oei, wachtwoord klopt niet
        else
        {
            $message['code'] = 2;
            $message['message'] = 'Wachtwoord klopt niet.';
        }
    }

    // JSON bericht uitpringen voor de AJAX request
    echo json_encode($message);

?>