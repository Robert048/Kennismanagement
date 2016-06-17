<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 28-4-2016
 * Time: 13:52
 */
session_start();
if($_SESSION['isloggedin']) {
    
include ('../Controller/vacancies.php');
include_once('../Controller/messages.php');
$messages= unreadMessages();
$likes = getAllLikes($_SESSION['user']->ID);

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
                        <?php $count = count($messages);?>
                        <!-- inbox dropdown start-->
                        <li id="header_inbox_bar" class="dropdown">
                            <a data-toggle="dropdown" class="dropdown-toggle" href="../../index.php#">
                                <i class="fa fa-envelope-o"></i>
                                <span class="badge bg-theme"><?php echo $count ?></span>
                            </a>
                            <ul class="dropdown-menu extended inbox">
                                <div class="notify-arrow notify-arrow-green"></div>
                                <li>
                                    <p class="green">Er zijn <?php echo $count ?> nieuwe berichten</p>
                                </li>
                                <?php foreach($messages as $message){?>
                                    <li>
                                        <a href="bericht.php?<?php echo "ID=".$message->ID."&empID="
                                            .$message->employeeID."&vacID=".$message->vacancyID ?>">
                                <span class="photo"><img alt="avatar"
                                                         src="../../images/email-closed.png"></span>
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
                                    <a href="berichten.php">See all messages</a>
                                </li>
                            </ul>
                        </li>
                        <!-- inbox dropdown end -->
                    </ul>
                <!--  notification end -->
                </div>
                <div class="top-menu">
                    <ul class="nav pull-right top-menu">
                        <li><a class="logout" onclick="LogOut()">Logout</a></li>
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
                        <h5 class="centered"><?php if($_SESSION['user']->name == null){
                                echo $_SESSION['user']->username;
                            }else{
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
                    <h3> Vacature Overzicht</h3>
                    <div class="row mt">
                        <div class="col-md-12">
                            <div class="content-panel">
                                <div id="head">
                                    <div id="nwVac">
                                        <a data-toggle="modal" href="vacancies.php#newVacModal">
                                            <button type="button" class="btn btn-round btn-success">Nieuwe vacature</button>
                                        </a>
                                    </div>
                                </div>
                                <table id="vacancyTable" class="table table-striped table-advance table-hover">
                                    <thead>
                                        <tr>
                                            <th><i class="fa fa-bullhorn"></i> Functie</th>
                                            <th class="hidden-phone"><i class="fa fa-question-circle"></i> Omschrijving</th>
                                            <th><i class="fa fa-heart"></i> Likes</th>
                                            <th>
                                                <?php
                                                $vacancies = showVacancies($_SESSION['user']->ID);
                                                foreach($vacancies as $vacancy) {?>
                                                <tr class="vacancyRow">
                                                    <td>
                                                        <a href="vacancieDetails.php?<?php echo "ID=".$vacancy->ID?>"><?php echo $vacancy->jobfunction?></a>
                                                    </td>
                                                    <td>
                                                        <?php
                                                            if(strlen($vacancy->description)>50){
                                                                echo substr($vacancy->description, 0, 50)."...";
                                                            }
                                                            else{
                                                                echo $vacancy->description;
                                                            }?>
                                                    </td>
                                                    <td>
                                                       <?php
                                                            $likeCount = 0;
                                                            foreach($likes as $like){
                                                                if($like->vacancyID==$vacancy->ID){
                                                                    $likeCount++;
                                                                }
                                                            }
                                                        echo $likeCount;
                                                       ?>
                                                    </td>
                                                    <td>
                                                        <button id="delete" class="btn btn-danger btn-xs" data-levelid="<?php echo $vacancy->ID; ?>" onclick="deleteVacancy()"><i class="fa fa-trash-o "></i></button>
                                                    </td>
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

            <!-- Modal -->
            <!--new vacancy modal-->
            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="newVacModal" class="modal fade">
                <div class="modal-dialog">
                    <form class ="form-newVacancy" id="newVacancy"method="post">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Nieuwe vacature</h4>
                            </div>
                            <div class="modal-body">
                                <p>Voer de volgende gegevens in om een nieuwe vacature aan te maken.</p>
                                <input type="text" name="functie" id="functie" placeholder="Functie" autocomplete="off" class="form-control placeholder-no-fix">
                                <br>
                                <textarea class="form-control placeholder-no-fix" id= "description" name="description" rows="4" cols="50" placeholder="Omschrijving" maxlength="500" style="resize:none;"></textarea>
                                <br>
                                <input type="text" id="branche" name="branche" placeholder="Branche" autocomplete="off" class="form-control placeholder-no-fix">
                                <br>
                                <input type="text" id="level" name="level" placeholder="Opleidingsniveau" autocomplete="off" class="form-control placeholder-no-fix">
                                <br>
                                <input type="text" id="salary" name="salary" placeholder="Salaris" autocomplete="off" class="form-control placeholder-no-fix">
                                <br>
                                <input type="text" id="hours" name="hours" placeholder="Uren" autocomplete="off" class="form-control placeholder-no-fix">
                                <br>
                                <input type="text" id="requirements" name="requirements" placeholder="Eisen" autocomplete="off" class="form-control placeholder-no-fix">
                                <br>
                                <input type="text" id="location" name="location" placeholder="Locatie" autocomplete="off" class="form-control placeholder-no-fix">
                                <br>
                            </div>
                            <div class="modal-footer">
                                <button data-dismiss="modal" class="btn btn-default" type="button">Annuleren</button>
                                <button data-dismiss="modal" class="btn btn-theme" id="newVacancy_button" onclick="addVacancy()" type="button">Verzenden</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <!--footer start-->
            <footer class="site-footer">
                <div class="text-center">
                    2016- WorQit
                    <a href="vacancies.php" class="go-top">
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