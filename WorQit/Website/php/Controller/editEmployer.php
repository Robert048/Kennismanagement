<?php
if (isset($_POST['submitbutton'])) {
    $name = $_POST["name"];
    $location = $_POST["location"];
    $description = $_POST["description"];
    $employeeCount = $_POST["employeeCount"];
    $username = $_POST["username"];
    $password = $_POST["password"];
    $id = $_SESSION["user"][0]["ID"];
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

    $_SESSION["user"][0]["name"] = $name;
    $_SESSION["user"][0]["location"] = $location;
    $_SESSION["user"][0]["description"] = $description;
    $_SESSION["user"][0]["employeeCount"] = $employeeCount;
    $_SESSION["user"][0]["password"] = $password;
    $_SESSION["user"][0]["username"] = $username;
    $_SESSION["user"][0]["email"] = $email;
    ?>
    <script>
        var url = "profiel.php"; //please insert the url of the your current page here, we are assuming the url is 'index.php'
        $('#form2').load(url + ' #form2'); //note: the space before #div1 is very important
        $('#sidebar').load(url + ' #sidebar'); //note: the space before #div1 is very important
    </script>
    <?php
}
