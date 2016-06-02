<?php

session_start();
if($_SESSION['isloggedin']) {

    include("../Controller/deleteEmployer.php");
    include("../Controller/editEmployer.php");


    ?>

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
        <link href="../../dashgum/Theme/assets/css/bootstrap.css" rel="stylesheet">
        <!--external css-->
        <link href="../../dashgum/Theme/assets/font-awesome/css/font-awesome.css" rel="stylesheet"/>

        <!-- Custom styles for this template -->
        <link href="../../dashgum/Theme/assets/css/style.css" rel="stylesheet">
        <link href="../../dashgum/Theme/assets/css/style-responsive.css" rel="stylesheet">

        <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
    </head>

    <body>

    <section id="container">
        <!-- **********************************************************************************************************************************************************
        TOP BAR CONTENT & NOTIFICATIONS
  *********************************************************************************************************************************************************** -->
        <!--header start-->
        <header class="header black-bg">
            <div class="sidebar-toggle-box">
                <div class="fa fa-bars tooltips" data-placement="right" data-original-title="Toggle Navigation"></div>
            </div>
            <!--logo start-->
            <a href="../../index.php" class="logo"><b>WorQit</b></a>
            <!--logo end-->
            <div class="nav notify-row" id="top_menu">
                <!--  notification start -->
                <ul class="nav top-menu">
                    <!-- inbox dropdown start-->
                    <li id="header_inbox_bar" class="dropdown">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="../../index.php#">
                            <i class="fa fa-envelope-o"></i>
                            <span class="badge bg-theme">5</span>
                        </a>
                        <ul class="dropdown-menu extended inbox">
                            <div class="notify-arrow notify-arrow-green"></div>
                            <li>
                                <p class="green">You have 5 new messages</p>
                            </li>
                            <li>
                                <a href="../../index.php#">
                                    <span class="photo"><img alt="avatar"
                                                             src="../../dashgum/Theme/assets/img/ui-zac.jpg"></span>
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
                                <a href="../../index.php#">See all messages</a>
                            </li>
                        </ul>
                    </li>
                    <!-- inbox dropdown end -->
                </ul>
                <!--  notification end -->
            </div>
            <div class="top-menu">
            	<ul class="nav pull-right top-menu">
                    <li><a class="logout" onclick="logout()">Logout</a></li>
            	</ul>
            </div>
        </header>
        <!--header end-->
        <!-- **********************************************************************************************************************************************************
        MAIN SIDEBAR MENU
  *********************************************************************************************************************************************************** -->
        <!--sidebar start-->
        <!--sidebar start-->
        <aside>
            <div id="sidebar" class="nav-collapse ">
                <!-- sidebar menu start-->
                <ul class="sidebar-menu" id="nav-accordion">
                    <p class="centered"><a href="profiel.php"><img src="../../dashgum/Theme/assets/img/ui-sam.jpg" class="img-circle" width="60"></a></p>
                    <h5 class="centered"><?php if($_SESSION['user']->name == null){
                            echo $_SESSION['user']->username;
                        }else{
                            echo $_SESSION['user']->name;
                        }
                        ?>
                    </h5>
                    <li class="mt">
                        <a href="../../index.php">
                            <i class="fa fa-dashboard"></i>
                            <span>Dashboard</span>
                        </a>
                    </li>
                    <li class="sub-menu">
                        <a class="active" href="profiel.php">
                            <i class="fa fa-desktop"></i>
                            <span>Profiel</span>
                        </a>
                    </li>
                    <li class="sub-menu">
                        <a href="vacancies.php">
                            <i class="fa fa-cogs"></i>
                            <span>Vacatures</span>
                        </a>
                    </li>
                    <li class="sub-menu">
                        <a href="berichten.php">
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
                <h3><i class="fa fa-angle-right"></i> Profiel van <?php if($_SESSION['user']->name == null){
                        echo $_SESSION['user']->username;
                    }else{
                        echo $_SESSION['user']->name;
                    }
                    ?> </h3>

                <form method="post" action="profiel.php" id="form">
                    <div id="form2">
                        <div class="row mt">
                            <div class="col-lg-2">
                                <p style="font-weight:bold;font-size: 14pt;" ;>Naam</p> <input type="text" class="form-control" name="name" value="<?php echo $_SESSION["user"]->name;?>">
                            </div>
                            <div class="col-lg-2">
                                <p style="font-weight:bold;font-size: 14pt;" ;>Medewerkers</p> <input type="text" class="form-control" name="employeeCount" value="<?php echo $_SESSION["user"]->employeeCount; ?>">
                            </div>
                            <br/> <br/> <br/> <br/>
                            <div class="col-lg-2">
                                <p style="font-weight:bold;font-size: 14pt;" ;>Bedrijfslocatie</p> <input type="text" class="form-control" name="location" value="<?php echo $_SESSION["user"]->location; ?>">
                            </div>
                            <div class="col-lg-2">
                                <p style="font-weight:bold;font-size: 14pt;" ;>Gebruikersnaam</p> <input type="text" class="form-control" name="username" value="<?php echo $_SESSION["user"]->username; ?>">
                            </div>
                            <br/> <br/> <br/> <br/>
                            <div class="col-lg-2">
                                <p style="font-weight:bold;font-size: 14pt;" ;>Wachtwoord</p> <input type="text" class="form-control" name="password" value="<?php echo $_SESSION["user"]->password; ?>">
                            </div>
                            <div class="col-lg-2">
                                <p style="font-weight:bold;font-size: 14pt;" ;>email</p> <input type="text" class="form-control" name="email" value="<?php echo $_SESSION["user"]->email; ?>">
                            </div>
                            <br/> <br/> <br/> <br/>
                            <div class="col-lg-4"><p style="font-weight:bold;font-size: 14pt;" ;>Bedrijfsomschrijving</p> <textarea style="overflow:auto;resize:none" rows="5" cols="300" name="description" class="form-control"><?php echo $_SESSION["user"]->description; ?></textarea></div>
                </form>
                <br/> <br/> <br/><br/> <br/> <br/><br/> <br/></br>
                <div class="col-lg-2">
                    <button class="btn btn-success btn-xs" name="submitbutton" id="submitWijzig""><i class="fa fa-pencil"></i></button>
                    <a data-toggle="modal" class="btn btn-danger btn-xs" href="profiel.php#deleteAccount"><i class="fa fa-trash-o"></i></a>
                </div>
                </div>
                </div>
                </div>
            </section>
            <! --/wrapper -->
        </section>
        <!-- /MAIN CONTENT -->
        <div aria-hidden="true" aria-labelledby="myModalLabel" id="deleteAccount" class="modal fade">
            <div class="modal-dialog">"
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Account verwijderen</h4>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-theme" type="button" id="bevestigVerwijderen" onclick="deleteEmployer()">Ja</button>
                        <button data-dismiss="modal" class="btn btn-theme" type="button">Nee</button>
                        <script>
                            var box = document.getElementById("bevestigVerwijderen");
                            box.onclick = function () {
                                $('#bevestigVerwijderen').attr('data-dismiss', 'modal');
                            }
                        </script>
                    </div>
                    <br/> <br/>

                    <div class="col-lg-4">
                        <p><?php if($_SESSION['user']->employeeCount == null){
                                echo $_SESSION['user']->employeeCount;
                            }else{
                                echo " ";
                            }
                            ?></p>
                    </div>
                    <br/> <br/>

                    <div class="col-lg-4">
                        <p style="font-weight:bold;font-size: 14pt;" ;>Bedrijfslocatie</p>
                    </div>
                    <br/> <br/>

                    <div class="col-lg-1">
                        <p><?php if($_SESSION['user']->location == null){
                                echo $_SESSION['user']->location;
                            }else{
                                echo " ";
                            ?>
                        </p>
                    <br/> <br/>

                    <div class="col-lg-4">
                        <p style="font-weight:bold;font-size: 14pt;" ;>Bedrijfsomschrijving</p>
                    </div>
                    <br/> <br/>

                    <div class="col-lg-6">
                        <p><?php if($_SESSION['user']->description == null){
                                echo $_SESSION['user']->description;
                            }else{
                                echo " ";
                            }
                            ?></p>
                    </div>
                    <br/> <br/> <br/><br/> <br/> <br/><br/> <br/>

                    <div class="col-lg-6">
                        <form method="get" action="wijzigProfiel.php">
                            <button class="btn btn-primary btn-xs" type="submit"><i class="fa fa-pencil"></i></button>
                        </form>
                    </div>
            </section>
            <! --/wrapper -->
        </section>
        <!-- /MAIN CONTENT -->
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
    <script class="include" type="text/javascript"
            src="../dashgum/Theme/assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script src="../dashgum/Theme/assets/js/jquery.scrollTo.min.js"></script>
    <script src="../dashgum/Theme/assets/js/jquery.nicescroll.js" type="text/javascript"></script>


    <!--common script for all pages-->
    <script src="../dashgum/Theme/assets/js/common-scripts.js"></script>

    <!--script for this page-->
  <script src= "../../js/login.js"></script>
    
  <script>
        //custom select box

        $(function () {
            $('select.styled').customSelect();
        });

    </script>

    </body>
    </html>

    <?php
}
?>
    <script src="../../dashgum/Theme/assets/js/jquery.js"></script>
    <script src="../../dashgum/Theme/assets/js/bootstrap.min.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery-ui-1.9.2.custom.min.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery.ui.touch-punch.min.js"></script>
    <script class="include" type="text/javascript"
            src="../../dashgum/Theme/assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery.scrollTo.min.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery.nicescroll.js" type="text/javascript"></script>


    <!--common script for all pages-->
    <script src="../../dashgum/Theme/assets/js/common-scripts.js"></script>

<?php
    }
    else{
        header("location: login.php");
    }
    ?>
