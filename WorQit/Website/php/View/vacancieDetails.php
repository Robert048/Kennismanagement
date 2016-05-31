<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 28-4-2016
 * Time: 13:52
 */
session_start();
if($_SESSION['isloggedin']) {


    include  ('../Controller/getVacancie.php');


    $allVacances = showVacancie($_SESSION["user"][0]["ID"]);



    ?>
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="description" content="">
        <meta name="author" content="Dashboard">
        <meta name="keyword" content="Dashboard, Bootstrap, Admin, Template, Theme, Responsive, Fluid, Retina">

        <title>Vacatures</title>

        <!-- Bootstrap core CSS -->
        <link href="../../dashgum/Theme/assets/css/bootstrap.css" rel="stylesheet">
        <!--external css-->
        <link href="../../dashgum/Theme/assets/font-awesome/css/font-awesome.css" rel="stylesheet" />

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
                        </ul>
                    </li>
                    <!-- inbox dropdown end -->
                </ul>
                <!--  notification end -->
            </div>
            <div class="top-menu">
                <ul class="nav pull-right top-menu">
                    <li><a class="logout" href="../../login.php">Logout</a></li>
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

                    <p class="centered"><a href="profiel.php"><img src="../../dashgum/Theme/assets/img/ui-sam.jpg" class="img-circle" width="60"></a></p>
                    <h5 class="centered"><?php echo $_SESSION['user'][0]['name']?></h5>

                    <li class="mt">
                        <a href="../../index.php">
                            <i class="fa fa-dashboard"></i>
                            <span>Dashboard</span>
                        </a>
                    </li>

                    <li class="sub-menu">
                        <a href="profiel.php" >
                            <i class="fa fa-desktop"></i>
                            <span>Profiel</span>
                        </a>
                    </li>

                    <li class="sub-menu">
                        <a class="active" href="vacancies.php" >
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
                <h3><i class="fa fa-angle-right"></i> Vacature details</h3>
                <div class="row mt">
                    <div class="col-md-12">
                        <div class="content-panel">
                            <div id="head">
                                <div id="title">
                                    <h4>Vacatures overzicht</h4>
                                </div>
                            </div>
                            <table id="vacancyTable" class="table table-striped table-advance table-hover">
                                <thead>
                                <tr>


                                    <th>
                                        <?php
                                        foreach($allVacances as $vacancy)  if ($vacancy->ID == $_GET["ID"] && $vacancy->employerID == $_SESSION["user"][0]["ID"])
                                           {?>
                                <tr class="vacancyRow">
                                    <th></i> Functie</th>
                                    <th class=hidden-phone><?php echo $vacancy->jobfunction?></th>
                                    </tr>
                                    <tr>
                                    <th></i> Omschrijving</th>
                                    <th class=hidden-phone><?php echo $vacancy->description?></th>
                                    </tr>
                                <tr>
                                    <th></i> Salaris</th>
                                    <th class=hidden-phone>&euro;<?php echo $vacancy->salary?>,-</th>
                                </tr>
                                <tr>
                                    <th></i> Uren</th>
                                    <th class=hidden-phone><?php echo $vacancy->hours ?> uren</th>
                                </tr>
                                <tr>
                                    <th></i> Eisen</th>
                                    <th class=hidden-phone><?php echo $vacancy->requirements?></th>
                                </tr>
                                <tr>
                                    <th></i> Tags</th>
                                    <th class=hidden-phone><?php echo $vacancy->tags?></th>
                                </tr>
                                </tr>
                                <?php
                                }
                                ?>
                                </thead>
                            </table>
                        </div><!-- /content-panel -->
                    </div><!-- /col-md-12 -->
                </div>
            </section><! --/wrapper -->
        </section><!-- /MAIN CONTENT -->
        <!--main content end-->
        <!--footer start-->
        <footer class="site-footer">
            <div class="text-center">
                <a href="../../dashgum/Theme/blank.html#" class="go-top">
                    <i class="fa fa-angle-up"></i>
                </a>
            </div>
        </footer>
        <!--footer end-->
    </section>

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="../../dashgum/Theme/assets/js/jquery.js"></script>
    <script src="../../dashgum/Theme/assets/js/bootstrap.min.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery-ui-1.9.2.custom.min.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery.ui.touch-punch.min.js"></script>
    <script class="include" type="text/javascript" src="../../dashgum/Theme/assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery.scrollTo.min.js"></script>
    <script src="../../dashgum/Theme/assets/js/jquery.nicescroll.js" type="text/javascript"></script>


    <!--common script for all pages-->
    <script src="../../dashgum/Theme/assets/js/common-scripts.js"></script>

    <!--script for this page-->
    <script src= "../../js/vacature.js"></script>

    <script>var base_url = "<?php echo BASE_URL; ?>"</script>
    <script>
        //custom select box

        $(function(){
            $('select.styled').customSelect();
        });

    </script>
    </body>
    </html>
    <?php
}
else{
    header("location: login.php");
}
?>