<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="Dashboard">
    <meta name="keyword" content="Dashboard, Bootstrap, Admin, Template, Theme, Responsive, Fluid, Retina">

    <title>WorQit</title>

    <!-- Bootstrap core CSS -->
    <link href="../dashgum/Theme/assets/css/bootstrap.css" rel="stylesheet">
    <!--external css-->
    <link href="../dashgum/Theme/assets/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="../dashgum/Theme/assets/css/style.css" rel="stylesheet">
    <link href="../dashgum/Theme/assets/css/style-responsive.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
<section id="container" >
    <!-- **********************************************************************************************************************************************************
    TOP BAR CONTENT & NOTIFICATIONS
*********************************************************************************************************************************************************** -->
    <!--header start-->
    <header class="header black-bg">
        <div class="sidebar-toggle-box">
            <div class="fa fa-bars tooltips" data-placement="right" data-original-title="Toggle Navigation"></div>
        </div>
        <!--logo start-->
        <a href="../index.php" class="logo"><b>WorQit</b></a>
        <!--logo end-->
        <div class="nav notify-row" id="top_menu">
            <!--  notification start -->
            <ul class="nav top-menu">
                <!-- inbox dropdown start-->
                <li id="header_inbox_bar" class="dropdown">
                    <a data-toggle="dropdown" class="dropdown-toggle" href="../index.php#">
                        <i class="fa fa-envelope-o"></i>
                        <span class="badge bg-theme">5</span>
                    </a>
                    <ul class="dropdown-menu extended inbox">
                        <div class="notify-arrow notify-arrow-green"></div>
                        <li>
                            <p class="green">You have 5 new messages</p>
                        </li>
                        <li>
                            <a href="../index.php#">
                                <span class="photo"><img alt="avatar" src="../dashgum/Theme/assets/img/ui-zac.jpg"></span>
                                    <span class="subject">
                                    <span class="from">Zac Snider</span>
                                    <span class="time">Just now</span>
                                    </span>
                                    <span class="message">
                                        Hi mate, how is everything?
                                    </span>
                            </a>
                        </li>
                        <li>
                            <a href="../index.php#">
                                <span class="photo"><img alt="avatar" src="../dashgum/Theme/assets/img/ui-divya.jpg"></span>
                                    <span class="subject">
                                    <span class="from">Divya Manian</span>
                                    <span class="time">40 mins.</span>
                                    </span>
                                    <span class="message">
                                        Hi, I need your help with this.
                                    </span>
                            </a>
                        </li>
                        <li>
                            <a href="../index.php#">
                                <span class="photo"><img alt="avatar" src="../dashgum/Theme/assets/img/ui-danro.jpg"></span>
                                    <span class="subject">
                                    <span class="from">Dan Rogers</span>
                                    <span class="time">2 hrs.</span>
                                    </span>
                                    <span class="message">
                                        Love your new Dashboard.
                                    </span>
                            </a>
                        </li>
                        <li>
                            <a href="../index.php#">
                                <span class="photo"><img alt="avatar" src="../dashgum/Theme/assets/img/ui-sherman.jpg"></span>
                                    <span class="subject">
                                    <span class="from">Dj Sherman</span>
                                    <span class="time">4 hrs.</span>
                                    </span>
                                    <span class="message">
                                        Please, answer asap.
                                    </span>
                            </a>
                        </li>
                        <li>
                            <a href="../index.php#">See all messages</a>
                        </li>
                    </ul>
                </li>
                <!-- inbox dropdown end -->
            </ul>
            <!--  notification end -->
        </div>
        <div class="top-menu">
            <ul class="nav pull-right top-menu">
                <li><a class="logout" href="../login.php">Logout</a></li>
            </ul>
        </div>
    </header>
    <!--header end-->

    <!-- **********************************************************************************************************************************************************
    MAIN SIDEBAR MENU
*********************************************************************************************************************************************************** -->
    <!--sidebar start-->
    <aside>
        <div id="sidebar"  class="nav-collapse ">
            <!-- sidebar menu start-->
            <ul class="sidebar-menu" id="nav-accordion">
                <p class="centered"><a href="profiel.php"><img src="../dashgum/Theme/assets/img/ui-sam.jpg" class="img-circle" width="60"></a></p>
                <h5 class="centered"><?php include("getUser.php"); echo $GLOBALS["bedrijfsnaam"] ?></h5>
                <li class="mt">
                    <a href="../index.php">
                        <i class="fa fa-dashboard"></i>
                        <span>Dashboard</span>
                    </a>
                </li>
                <li class="sub-menu">
                    <a class="active" href="profiel.php" >
                        <i class="fa fa-desktop"></i>
                        <span>Profiel</span>
                    </a>
                </li>
                <li class="sub-menu">
                    <a href="vacatures.php" >
                        <i class="fa fa-cogs"></i>
                        <span>Vacatures</span>
                    </a>
                </li>
                <li class="sub-menu">
                    <a href="berichten.php" >
                        <i class="fa fa-book"></i>
                        <span>Berichten</span>
                    </a>
                </li>
            </ul>
            <!-- sidebar menu end-->
        </div>
    </aside>
    <!--sidebar end-->

    <!-- **********************************************************************************************************************************************************
    MAIN CONTENT
*********************************************************************************************************************************************************** -->
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper site-min-height">
            <h3><i class="fa fa-angle-right"></i> Profiel wijzigen </h3>
            <form action="wijzigProfiel.php" method="post">
            <div class="row mt">
                <div class="col-lg-2">
                    <p style="font-weight:bold;font-size: 14pt;";>Naam</p> <input type="text" class="form-control" name="name" placeholder="<?php if($var["Result"] == "successful") { echo $var["User"][0]["name"];} else {} ?>" >
                </div>
                <br/>  <br/>  <br/> <br/>
                <div class="col-lg-2">
                    <p style="font-weight:bold;font-size: 14pt;";>Medewerkers</p> <input type="text" class="form-control" name="employeeCount" placeholder="<?php if($var["Result"] == "successful") { echo $var["User"][0]["employeeCount"];} else {} ?>" >
                </div>
                <br/>  <br/>  <br/> <br/>
                <div class="col-lg-2">
                    <p style="font-weight:bold;font-size: 14pt;";>Bedrijfslocatie</p> <input type="text" class="form-control" name="location" placeholder="<?php if($var["Result"] == "successful") { echo $var["User"][0]["location"];} else {} ?>">
                </div>
                <br/>  <br/>  <br/> <br/>
                <div class="col-lg-4">
                    <p style="font-weight:bold;font-size: 14pt;";>Bedrijfsomschrijving</p> <textarea style="overflow:auto;resize:none" rows="5" cols="300" name="description" class="form-control" placeholder="<?php if($var["Result"] == "successful") { echo $var["User"][0]["description"];} else {} ?>"></textarea>
                </div>
                </form>
                <br/>  <br/>  <br/><br/>  <br/>  <br/><br/>  <br/><br/><br/><br/>
                <div class="col-lg-2">
                    <button class="btn btn-success btn-xs" type="submit" name="submitbutton""><i class="fa fa-pencil"></i></button>
                    <a data-toggle="modal"  class="btn btn-danger btn-xs" href="wijzigProfiel.php.html#deleteAccount"><i class="fa fa-trash-o"></i></a>
                </div>
            </form>
            </div>
            </div>


        </section><! --/wrapper -->
    </section><!-- /MAIN CONTENT -->

    <div aria-hidden="true" aria-labelledby="myModalLabel role="dialog" tabindex="-1" id="deleteAccount" class="modal fade">
    <div class="modal-dialog">"
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Account verwijderen</h4>
            </div>
            <div class="modal-body">
                <p>Weet u zeker dat u het account wilt verwijderen?</p>
            </div>
            <div class="modal-footer">
                <button class="btn btn-theme" type="button" id="bevestigVerwijderen"  onclick="deleteEmployer()">Ja</button>
                <button data-dismiss="modal" class="btn btn-theme" type="button">Nee</button>
                <?php

                function deleteEmployer(){
                $ID = $GLOBALS["user"];

                $deleteVars = array("ID => '$ID', password => 'onzin'");
                $ch = curl_init();
                curl_setopt($ch, CURLOPT_URL, 'http://worqit.azurewebsites.net/api/Employer/deleteEmployer');
                curl_setopt($ch, CURLOPT_CUSTOMREQUEST, 'DELETE');
                curl_setopt($ch, CURLOPT_POSTFIELDS, $deleteVars);
                curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

                $deleteHeaders = array();
                $deleteHeaders[] = 'ID:'.$ID;
                $deleteHeaders[] = 'password: test';
                curl_setopt($ch, CURLOPT_HTTPHEADER, $deleteHeaders);
                $server_output = curl_exec($ch);
                curl_close($ch);
                }

                ?>
                <script>
                    var box = document.getElementById("bevestigVerwijderen");
                    box.onclick = function() {
                        $('#bevestigVerwijderen').attr('data-dismiss', 'modal');
                        location.reload();
                    }
                </script>
            </div>
        </div>
    </div>
    </div>



    <div aria-hidden="true" aria-labelledby="myModalLabel role="dialog" tabindex="-1" id="changeAccount" class="modal fade">
    <div class="modal-dialog">"
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Wijzigingen opslaan</h4>
            </div>
            <div class="modal-body">
                <p>Weet u zeker dat u deze wijzigingen wil opslaan</p>
            </div>
            <div class="modal-footer">
                <button class="btn btn-theme" type="button" id="bevestigWijzig"  onclick="editEmployer()">Ja</button>
                <button data-dismiss="modal" class="btn btn-theme" type="button">Nee</button>

                <?php

                if(isset($_POST['submitbutton'])) {
                    $name = $_POST["name"];
                    $location = $_POST["location"];
                    $description = $_POST["description"];
                    $employeeCount = $_POST["employeeCount"];
                    $id = $var["User"][0]["ID"];

                    $editVars = array("industry  => 'test', username => 'test', password => 'test', id => '$id', firstName => '$name', location => '$location', lastName => '$description', employeeCount => $employeeCount");

                    $ch = curl_init();
                    curl_setopt($ch, CURLOPT_URL, "http://worqit.azurewebsites.net/api/Employer/editEmployer");
                    curl_setopt($ch, CURLOPT_POST, 1);
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $editVars);  //Post Fields
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

                    $editHeaders = array();
                    $editHeaders[] = 'industry: test';
                    $editHeaders[] = 'id:' . $id;
                    $editHeaders[] = 'firstName:' . $name;
                    $editHeaders[] = 'location:' . $location;
                    $editHeaders[] = 'lastName:' . $description;
                    $editHeaders[] = 'username: test';
                    $editHeaders[] = 'password: test';
                    $editHeaders[] = 'employeeCount:' . $employeeCount;
                    curl_setopt($ch, CURLOPT_HTTPHEADER, $editHeaders);
                    $server_output = curl_exec($ch);

                    curl_close($ch);


                    $var = json_decode($server_output, true);

                }
                ?>

                <script>
                    var box = document.getElementById("bevestigWijzig");
                    box.onclick = function() {
                        $('#bevestigWijzig').attr('data-dismiss', 'modal');

                    }
                </script>
            </div>
        </div>
    </div>
    </div>

    <!--main content end-->
    <!--footer start-->
    <footer class="site-footer">
        <div class="text-center">
            <a href="../dashgum/Theme/blank.html#" class="go-top">
                <i class="fa fa-angle-up"></i>
            </a>
        </div>
    </footer>
    <!--footer end-->
</section>
<!-- js placed at the end of the document so the pages load faster -->
<script src="../dashgum/Theme/assets/js/jquery.js"></script>
<script src="../dashgum/Theme/assets/js/bootstrap.min.js"></script>
<script src="../dashgum/Theme/assets/js/jquery-ui-1.9.2.custom.min.js"></script>
<script src="../dashgum/Theme/assets/js/jquery.ui.touch-punch.min.js"></script>
<script class="include" type="text/javascript" src="../dashgum/Theme/assets/js/jquery.dcjqaccordion.2.7.js"></script>
<script src="../dashgum/Theme/assets/js/jquery.scrollTo.min.js"></script>
<script src="../dashgum/Theme/assets/js/jquery.nicescroll.js" type="text/javascript"></script>

<!--common script for all pages-->
<script src="../dashgum/Theme/assets/js/common-scripts.js"></script>



