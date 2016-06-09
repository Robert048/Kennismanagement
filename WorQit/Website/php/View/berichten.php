<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 28-4-2016
 * Time: 13:59
 */
session_start();
include_once('../Controller/messages.php');
if($_SESSION['isloggedin']) {
    ?>

    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="description" content="">
        <meta name="author" content="Dashboard">
        <meta name="keyword" content="Dashboard, Bootstrap, Admin, Template, Theme, Responsive, Fluid, Retina">

        <title>Berichten</title>

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
                        <li>
                            <a href="../../index.php#">
                                <span class="photo"><img alt="avatar"
                                                         src="../../dashgum/Theme/assets/img/ui-sherman.jpg"></span>
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
    <aside>
        <div id="sidebar" class="nav-collapse ">
            <!-- sidebar menu start-->
            <ul class="sidebar-menu" id="nav-accordion">

                <p class="centered"><a href="profiel.php"><img src="../../dashgum/Theme/assets/img/ui-sam.jpg"
                                                               class="img-circle" width="60"></a></p>
                <h5 class="centered"><?php if ($_SESSION['user']->name == null) {
                        echo $_SESSION['user']->username;
                    } else {
                        echo $_SESSION['user']->name;
                    }
                    ?></h5>

                <li class="mt">
                    <a href="../../index.php">
                        <i class="fa fa-dashboard"></i>
                        <span>Dashboard</span>
                    </a>
                </li>

                <li class="sub-menu">
                    <a href="profiel.php">
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
                    <a class="active" href="berichten.php">
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
    <h3><i class="fa fa-angle-right"></i> Berichten</h3>
    <div class="row mt">
    <div class="col-lg-12">

    <!-- Berichten overzicht -->
    <div class="row mt">
    <div class="col-md-12">
    <div class="white-panel pn">
    <div class="panel-heading">
        <div class="pull-left"><h5>Berichten</h5></div>
        <br>
    </div>
        <div class="content-panel">
    <div class="custom-check goleft mt">
    <table id="berichten" class="table">
    <thead>
    <tr>
        <td>
        Werknemer
        </td>
        <td>
          Titel
        </td>
        <td>
            Verwijder
        </td>
    </tr>

    </thead>
    <?php
    $messages = showMessages($_SESSION['user']->ID);
    foreach ($messages->Messages as $message) { ?>
        <tr>
            <td>
                <a href="bericht.php?<?php echo "ID=" . $message->ID . "&empID=" .
                    $message->employeeID . "&vacID=" . $message->vacancyID?>">
                    <?php if ($message->read == false)
                    {
                    ?>
                    <b>
                        <?php
                        }
                        echo $message->employeeID;
                        if ($message->read == false){
                        ?>
                    </b>
                <?php
                }
                ?>
                </a>
            </td>
            <td>
                <a href="bericht.php?<?php echo "ID=" . $message->ID . "&empID="
                    . $message->employeeID . "&vacID=" . $message->vacancyID ?>">
                    <?php if ($message->read == false){ ?>
                    <b><?php
                        }
                        echo $message->title;
                        if ($message->read == false){
                        ?>
                    </b>
                <?php
                } ?>
                </a>
            </td>
            <td>
                <button id="delete" class="btn btn-danger btn-xs"
                        data-levelid="<?php echo $message->ID; ?>"
                        onclick="deleteMessage()">
                    <i class="fa fa-trash-o" style="color:white;"></i>
                </button>
            </td>
        </tr>
        <?php
    }

                                        ?>
                                    </table>
                                </div><!-- /table-responsive -->
                            </div><!--/ White-panel -->
                        </div><! --/col-md-12 -->
                    </div><! -- row -->
        </div>
                </div>
            </div>

        </section><! --/wrapper -->
    </section><!-- /MAIN CONTENT -->

    <!--main content end-->
    <!--footer start-->
    <footer class="site-footer">
        <div class="text-center">
            2016- WorQit
            <i class="fa fa-angle-up"></i>
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
<script src= "../../js/login.js"></script>

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
    header("location: ../../login.php");
}

?>

