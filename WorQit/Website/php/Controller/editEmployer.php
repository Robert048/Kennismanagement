<?php
if (isset($_POST['submitbutton'])) {
    $name = $_POST["name"];
    $location = $_POST["location"];
    $description = $_POST["description"];
    $employeeCount = $_POST["employeeCount"];
    $username = $_POST["username"];
    $password = $_POST["password"];
    $id = $_SESSION["user"]->ID;
    $email = $_POST["email"];

    $editVars = array("email => '$email', industry  => 'test', username => '$username', password => '$password', id => '$id', name => '$name', location => '$location', description => '$description', employeeCount => $employeeCount");
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Employer/editEmployer");
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $editVars);  //Post Fields
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

    $editHeaders = array();
    $editHeaders[] = 'industry: test';
    $editHeaders[] = 'id:' . $id;
    $editHeaders[] = 'name:' . $name;
    $editHeaders[] = 'location:' . $location;
    $editHeaders[] = 'description:' . $description;
    $editHeaders[] = 'username:' . $username;
    $editHeaders[] = 'password:' . $password;
    $editHeaders[] = 'email:' . $email;
    $editHeaders[] = 'employeeCount:' . $employeeCount;
    curl_setopt($ch, CURLOPT_HTTPHEADER, $editHeaders);
    $server_output = curl_exec($ch);
    curl_close($ch);

    $_SESSION["user"]->name = $name;
    $_SESSION["user"]->location = $location;
    $_SESSION["user"]->description = $description;
    $_SESSION["user"]->employeeCount = $employeeCount;
    $_SESSION["user"]->password = $password;
    $_SESSION["user"]->username = $username;
    $_SESSION["user"]->email = $email;
    ?>
    <script>
        var url = "profiel.php";
        $('#form2').load(url + ' #form2');
        $('#sidebar').load(url + ' #sidebar'); 
    </script>
    <?php
}
