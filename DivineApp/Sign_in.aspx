﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sign_in.aspx.cs" Inherits="DivineApp.Sign_in" UnobtrusiveValidationMode="none"  Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Divine Verities|Sign In</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    

    <!-- Bootstrap Core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Theme CSS -->
    <link href="css/freelancer.min.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <link rel="stylesheet" href="css/style.css">

    <style>
        .now{
            margin-top:90px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Navigation -->
    <nav id="mainNav" class="navbar navbar-default navbar-fixed-top navbar-custom">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header page-scroll">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span> Menu <i class="fa fa-bars"></i>
                </button>
                <a class="navbar-brand" href="Default.aspx">VerITy Solutions</a>
            </div>     
            </div>       
    </nav>
    
        <!--Introduction-->
        <div class="login-page">
    <h3 style="text-align:center;color:white">Welcome to Divine Verities! <%--Sign In here!--%></h3>
        <br />
        <p style="text-align:center;color:white">Your Podcasts live here!</p>

        <!--main content-->
        <div class="form now">
            <div class="login-form">
      
            <asp:TextBox ID="u_name" runat="server" placeholder="Username"></asp:TextBox>
                <asp:RequiredFieldValidator ID="unameValidator" runat="server" Text="Username Required!" ControlToValidate="u_name" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
      
            <asp:TextBox ID="pass" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="passValidator" runat="server" Text="Password Required!" ControlToValidate="pass" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
      
            <asp:Button ID="Button1" runat="server" Text="LOGIN" style="background: #00597d;color:white;" OnClick="Button1_Click"/>
                <asp:Label ID="StatusText" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
      <p class="message">Not registered? <a href="Sign_up.aspx">Create an account</a></p>
    </div>
        </div>
    </div>
         <!-- Footer -->
    <footer class="text-center">
        <%--<div class="footer-above">
            <div class="container">
                <div class="row">
                    <div class="footer-col col-md-4"></div>
                    <div class="footer-col col-md-4">
                        <h3>Location</h3>
                        <p>Kumasi Business Incubator
                            <br>Kumasi, Ghana</p>
                    </div>
                    <div class="footer-col col-md-4"></div>
                    <div class="footer-col col-md-4">
                        <h3>Location</h3>
                        <p>Kumasi Business Incubator
                            <br>Kumasi, Ghana</p>
                    </div>
                    <div class="footer-col col-md-4">
                        <h3>Around the Web</h3>
                        <ul class="list-inline">
                            <li>
                                <a href="#" class="btn-social btn-outline"><span class="sr-only">Facebook</span><i class="fa fa-fw fa-facebook"></i></a>
                            </li>
                            <li>
                                <a href="#" class="btn-social btn-outline"><span class="sr-only">Google Plus</span><i class="fa fa-fw fa-google-plus"></i></a>
                            </li>
                            <li>
                                <a href="#" class="btn-social btn-outline"><span class="sr-only">Twitter</span><i class="fa fa-fw fa-twitter"></i></a>
                            </li>
                            <li>
                                <a href="#" class="btn-social btn-outline"><span class="sr-only">Linked In</span><i class="fa fa-fw fa-linkedin"></i></a>
                            </li>
                            <li>
                                <a href="#" class="btn-social btn-outline"><span class="sr-only">Dribble</span><i class="fa fa-fw fa-dribbble"></i></a>
                            </li>
                        </ul>
                    </div>
                    <div class="footer-col col-md-4">
                        <h3>Divine Verities</h3>
                        <p>Divine Verities is our flagship product. Easy to use and available on all mobile platforms <a href="#">Download apk here</a>.</p>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="footer-below">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <%--Copyright--%> &copy; VerITy Solutions 2017
                    </div>
                </div>
            </div>
        </div>
    </footer>
    </form>

    <!-- jQuery -->
    <script src="vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>

    <!-- Contact Form JavaScript -->
    <script src="js/jqBootstrapValidation.js"></script>
    <script src="js/contact_me.js"></script>

    <!-- Theme JavaScript -->
    <script src="js/freelancer.min.js"></script>

    <!--Toggle Form-->
    <script src="js/index.js"></script>
</body>
</html>
