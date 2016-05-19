<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 25-4-2016
 * Time: 16:58
 */

//require_once('functions.php');
//require_once('globals.php');
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
    <link href="dashgum/Theme/assets/css/bootstrap.css" rel="stylesheet">
    <!--external css-->
    <link href="dashgum/Theme/assets/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="dashgum/Theme/assets/css/style.css" rel="stylesheet">
    <link href="dashgum/Theme/assets/css/style-responsive.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>

	  <!--js file voor login-->
	  <script src= "js/login.js"></script>
    <![endif]-->
  </head>

  <body>

      <!-- **********************************************************************************************************************************************************
      MAIN CONTENT
*********************************************************************************************************************************************************** -->

	  <div id="login-page">
	  	<div class="container" id="login">

		      <form class="form-login" action="index.php">
		        <h2 class="form-login-heading">log in</h2>
		        <div class="login-wrap">
		            <input type="text" class="form-control" placeholder="Username">
		            <br>
		            <input type="password" class="form-control" placeholder="Password">
		            <label class="checkbox">
		                <span class="pull-right">
		                    <a data-toggle="modal" href="login.php#forgotPassModal"> Wachtwoord vergeten?</a>

		                </span>
		            </label>
		            <button class="btn btn-theme btn-block" id="login_button" type="submit"><i class="fa fa-lock"></i> LOG IN</button>
		            <hr>
		            <div class="registration">
						Nog geen account?<br/>
						<a data-toggle="modal" href="login.php#signupModal"> Maak een account</a>
		            </div>

		        </div>

		          <!-- Modal -->
				  <!--forgot password modal-->
		          <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="forgotPassModal" class="modal fade">
		              <div class="modal-dialog">
						  <form class ="form-forgotPass">
							  <div class="modal-content">
								  <div class="modal-header">
									  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
									  <h4 class="modal-title">Wachtwoord vergeten?</h4>
								  </div>
								  <div class="modal-body">
									  <p>Voer emailadres in om je wachtwoord te resetten.</p>
									  <input type="text" name="email" placeholder="Email" autocomplete="off" class="form-control placeholder-no-fix">

								  </div>
								  <div class="modal-footer">
									  <button data-dismiss="modal" class="btn btn-default" type="button">Annuleren</button>
									  <button class="btn btn-theme" id="forgotPass_button" type="button">Verzenden</button>
								  </div>
							  </div>
						  </form>
		              </div>
		          </div>

					<!--signup modal-->
				  <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="signupModal" class="modal fade">
					  <div class="modal-dialog">
						  <form class = "form-signup">
							  <div class="modal-content">
								  <div class="modal-header">
									  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
									  <h4 class="modal-title">Maak een acount aan</h4>
								  </div>
								  <div class="modal-body">
									  <div class="login-wrap">
										  <input type="text" class="form-control" placeholder="username">
										  <br>
										  <input type="text" class="form-control" placeholder="email">
										  <br>
										  <input type="password" class="form-control" placeholder="Password">
										  <br>
										  <button class="btn btn-theme btn-block" id="signup_button" type="submit"><i class="fa fa-lock"></i> Maak account</button>
									  </div>
									</div>
								  <div class="modal-footer">
									  <button data-dismiss="modal" class="btn btn-default" type="button">Annuleren</button>
									  <button class="btn btn-theme" type="button">Maak account</button>
								  </div>
							  </div>
						  </form>
					  </div>
				  </div>
		          <!-- modal -->

		      </form>

	  	</div>
	  </div>

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="dashgum/Theme/assets/js/jquery.js"></script>
    <script src="dashgum/Theme/assets/js/bootstrap.min.js"></script>

    <!--BACKSTRETCH-->
    <!-- You can use an image of whatever size. This script will stretch to fit in any screen size.-->
    <script type="text/javascript" src="dashgum/Theme/assets/js/jquery.backstretch.min.js"></script>
    <script>
$.backstretch("dashgum/Theme/assets/img/login-bg.jpg", {speed: 500});
    </script>


  </body>
</html>