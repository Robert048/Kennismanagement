<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 26-4-2016
 * Time: 09:40
 */
session_start();

if($_SESSION['isloggedin']) {
    include_once('php/Controller/messages.php');
    include_once('php/Controller/vacancies.php');
    $messages= unreadMessages();
    $unreadLikes= getUnreadLikes();
?>

    <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="Dashboard">

    <title>Dashboard WorQit</title>

    <!-- Bootstrap core CSS -->
    <link href="dashgum/Theme/assets/css/bootstrap.css" rel="stylesheet">
    <!--external css-->
    <link href="dashgum/Theme/assets/font-awesome/css/font-awesome.css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="dashgum/Theme/assets/css/zabuto_calendar.css">
    <link rel="stylesheet" type="text/css" href="dashgum/Theme/assets/js/gritter/css/jquery.gritter.css"/>
    <link rel="stylesheet" type="text/css" href="dashgum/Theme/assets/lineicons/style.css">

    <!-- Custom styles for this template -->
    <link href="dashgum/Theme/assets/css/style.css" rel="stylesheet">
    <link href="dashgum/Theme/assets/css/style-responsive.css" rel="stylesheet">

    <script src="dashgum/Theme/assets/js/chart-master/Chart.js"></script>

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
        <a href="index.php" class="logo"><b>WorQit</b></a>
        <!--logo end-->
        <div class="nav notify-row" id="top_menu">
            <!--  notification start -->
            <ul class="nav top-menu">
                <?php $count = count($messages);?>
                <!-- inbox dropdown start-->
                <li id="header_inbox_bar" class="dropdown">
                    <a data-toggle="dropdown" class="dropdown-toggle" href="index.php#">
                        <i class="fa fa-envelope-o"></i>
                        <span class="badge bg-theme"><?php echo $count ?></span>
                    </a>
                    <ul class="dropdown-menu extended inbox">
                        <div class="notify-arrow notify-arrow-green"></div>
                        <li>
                            <p class="green">Er zijn <?php echo $count ?> nieuwe berichten</p>
                        </li>
                        <!-- loop through the messages and set as notifications -->
                        <?php foreach($messages as $message){?>
                        <li>
                            <a href="php/View/bericht.php?<?php echo "ID=".$message->ID."&empID="
                                .$message->employeeID."&vacID=".$message->vacancyID ?>">
                                <span class="photo"><img alt="avatar"
                                                         src="images/email-closed.png"></span>
                                        <span class="subject">
                                        <span class="from"><?php echo $message->employeeID?></span>
                                        <span class="time"><?php echo $message->date ?></span>
                                        </span>
                                        <span class="message">
                                            <?php echo $message->title?>
                                        </span>
                            </a>
                        </li>
                        <?php } ?>
                        <li>
                            <a href="php/View/berichten.php">See all messages</a>
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
    <aside>
        <div id="sidebar" class="nav-collapse ">
            <!-- sidebar menu start-->
            <ul class="sidebar-menu" id="nav-accordion">
                <p class="centered"><a href="php/View/profiel.php"><img src="dashgum/Theme/assets/img/ui-sam.jpg"
                                                                        class="img-circle" width="60"></a></p>
                <h5 class="centered">
                    <?php if ($_SESSION['user']->name == null) {
                        echo $_SESSION['user']->username;
                    } else {
                        echo $_SESSION['user']->name;
                    }
                    ?>
                </h5>

                <li class="mt">
                    <a class="active" href="index.php">
                        <i class="fa fa-dashboard"></i>
                        <span>Dashboard</span>
                    </a>
                </li>

                <li class="sub-menu">
                    <a href="php/View/profiel.php">
                        <i class="fa fa-desktop"></i>
                        <span>Profiel</span>
                    </a>
                </li>

                <li class="sub-menu">
                    <a href="php/View/vacancies.php">
                        <i class="fa fa-cogs"></i>
                        <span>Vacatures</span>
                    </a>
                </li>
                <li class="sub-menu">
                    <a href="php/View/berichten.php">
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
        <?php $likeCount= count($unreadLikes); ?>
        <section class="wrapper">
            <div class="row">
                <div class="col-lg-9 main-chart">
                    <div class="row mtbox">
                        <a href="php/View/vacancies.php">
                            <div class="col-md-2 col-sm-2 col-md-offset-1 box0">
                                <div class="box1">
                                    <span class="li_heart"></span>
                                    <h3><?php echo $likeCount ?></h3>
                                </div>
                                <p><?php echo $likeCount ?> Nieuwe mensen hebben een vacature geliked. Whoohoo!</p>
                            </div>
                        </a>
                        <a href="php/View/berichten.php">
                            <div class="col-md-2 col-sm-2 col-md-offset-1 box0">
                                <div class="box1">
                                    <span class="li_mail"></span>

                                    <h3><?php echo $count ?></h3>
                                </div>
                                <p><?php echo $count ?> nieuwe berichten!</p>
                            </div>
                        </a>
                    </div>
                    <!-- /row mt -->

                    <div class="row mt">
                        <!-- Profiel status Panel -->
                        <a href="php/View/profiel.php">
                            <div class="col-md-4 col-sm-4 mb">
                                <div class="darkblue-panel pn donut-chart">
                                    <div class="darkblue-header">
                                        <h5>PROFIEL</h5>
                                    </div>
                                    <?php
                                    $fieldsFilled = 0;
                                    foreach ($_SESSION['user'] as $s) {
                                        if ($s === NULL || is_array($s) || $s == "") {
                                        } else {
                                            $fieldsFilled++;
                                        }

                                        $percFilled = 14.3 * $fieldsFilled;
                                        $percEmpty = 100 - $percFilled;

                                    }
                                    $fieldsFilled = $fieldsFilled - 1;
                                    $percFilled = 14.3 * $fieldsFilled;
                                    if ($percFilled > 90)
                                    {
                                        $percFilled = 100;
                                    }
                                        $percEmpty = 100 - $percFilled;
                                        ?>

                                        <div class="row">
                                            <div class="col-sm-6 col-xs-6 goleft">
                                                <p style="color: white"><?php echo $percFilled ?>% voltooid</p>
                                            </div>
                                        </div>
                                        <canvas id="serverstatus01" height="120" width="120"></canvas>
                                        <script>
                                            var profielData = [
                                                {
                                                    value: <?php echo $percFilled ?>,
                                                    color: "#68dff0"
                                                },
                                                {
                                                    value: <?php echo $percEmpty ?>,
                                                    color: "#fdfdfd"
                                                }
                                            ];
                                            var profielChart = new Chart(document.getElementById("serverstatus01").getContext("2d")).Doughnut(profielData);
                                        </script>
                                    </div>
                                    <! --/grey-panel -->
                                </div><!-- /col-md-4-->
                            </a>
                        </div><!-- /row -->
                    </div>
                    <div class="col-lg-3 ds">
                        <div id="calendar" class="mb">
                            <div class="panel green-panel no-margin">
                                <div class="panel-body">
                                    <div id="date-popover" class="popover top" style="cursor: pointer; disadding: block; margin-left: 33%; margin-top: -50px; width: 175px;">
                                        <div class="arrow"></div>
                                        <h3 class="popover-title" style="disadding: none;"></h3>
                                        <div id="date-popover-content" class="popover-content"></div>
                                    </div>
                                    <div id="my-calendar"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <! --/row -->
            </section>
        </section>
        <!--main content end-->
        <!--footer start-->
    <footer class="site-footer">
        <div class="text-center">
            2016- WorQit
            <a href="index.php" class="go-top">
                <i class="fa fa-angle-up"></i>
            </a>
        </div>
    </footer>
        <!--footer end-->
    </section>

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="dashgum/Theme/assets/js/jquery.js"></script>
    <script src="dashgum/Theme/assets/js/jquery-1.8.3.min.js"></script>
    <script src="dashgum/Theme/assets/js/bootstrap.min.js"></script>
    <script class="include" type="text/javascript" src="dashgum/Theme/assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script src="dashgum/Theme/assets/js/jquery.scrollTo.min.js"></script>
    <script src="dashgum/Theme/assets/js/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="dashgum/Theme/assets/js/jquery.sparkline.js"></script>


    <!--common script for all pages-->
    <script src="dashgum/Theme/assets/js/common-scripts.js"></script>

    <script type="text/javascript" src="dashgum/Theme/assets/js/gritter/js/jquery.gritter.js"></script>
    <script type="text/javascript" src="dashgum/Theme/assets/js/gritter-conf.js"></script>

    <!--script for this page-->
    <script src="dashgum/Theme/assets/js/sparkline-chart.js"></script>
    <script src="dashgum/Theme/assets/js/zabuto_calendar.js"></script>
    <script src= "js/login.js"></script>


    <script type="application/javascript">
        $(document).ready(function () {
            $("#date-popover").popover({html: true, trigger: "manual"});
            $("#date-popover").hide();
            $("#date-popover").click(function (e) {
                $(this).hide();
            });

            $("#my-calendar").zabuto_calendar({
                action: function () {
                    return myDateFunction(this.id, false);
                },
                action_nav: function () {
                    return myNavFunction(this.id);
                },
                ajax: {
                    url: "show_data.php?action=1",
                    modal: true
                },
                legend: [
                    {type: "text", label: "Special event", badge: "00"},
                    {type: "block", label: "Regular event",}
                ]
            });
        });


        function myNavFunction(id) {
            $("#date-popover").hide();
            var nav = $("#" + id).data("navigation");
            var to = $("#" + id).data("to");
            console.log('nav ' + nav + ' to: ' + to.month + '/' + to.year);
        }
    </script>


    </body>
    </html>
<?php
}
else{
    header("location: login.php");
}
?>
