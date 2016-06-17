<?php
/*
 * Send message from employer to employee
 */
if (isset($_POST["submitbutton"])) {
    $employeeID = $_POST["employeeID"];
    $employerID = $_SESSION["user"]->ID;
    $text = $_POST["text"];
    $sender = "employer";
    $title = $_POST["titel"];

    $editVars = array("employeeID => '$employeeID', employerID  => '$employerID', text => '$text', sender => '$sender', title => '$title''");

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Message/sendMessage");
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $editVars);  //Post Fields
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

    $headers = array();
    $headers[] = 'employeeID:' . $employeeID;
    $headers[] = 'employerID:' . $employerID;
    $headers[] = 'text:' . $text;
    $headers[] = 'sender:' . $sender;
    $headers[] = 'title:' . $title;

  curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);

   $server_output = curl_exec($ch);

   curl_close($ch);

    return $server_output;
    ?>
    <Script>
        document.getElementById("sendMessage").innerHTML = "Wijzigingen opgeslagen!";
    </Script>
    <?php
}
