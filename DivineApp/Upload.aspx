<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="DivineApp.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Divine Verities|Upload Files</title>

    <link href="css/style.css" rel="stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- Theme CSS -->
    <link href="css/freelancer.min.css" rel="stylesheet"/>
    
    <!-- Custom Fonts -->
    <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <style>
        .here{
            margin-top:130px;
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
                <a class="navbar-brand" href="#page-top">VerITy Solutions</a>
            </div>

            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-right">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <%--<li>
                        <a href="#page-top">Home</a>
                    </li>                    
                    <li class="page-scroll">
                        <a href="#portfolio">Products</a>
                    </li>
                    <li class="page-scroll">
                        <a href="#about">About</a>
                    </li>
                    <li class="page-scroll">
                        <a href="#contact">Contact Us</a>
                    </li>--%>
                    <li>
                        <%--<a href="#">Username</a>--%><asp:Label ID="Label1" runat="server" Text="Username here" style="color:white"></asp:Label>
                    </li>
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container-fluid -->
    </nav>

    <div class="container">
        <div class="row">
            <div class="form here col-md-4">
        <div class="container-fluid">
                    <div class="row">
                    <div class="col-md-12">
                        <div class="login-form">
                            <p>AUDIO UPLOAD</p>
            <asp:TextBox ID="name" runat="server" placeholder="NAME"></asp:TextBox>
            <asp:TextBox ID="desc" runat="server" placeholder="DESCRIPTION"></asp:TextBox>
            <asp:TextBox ID="genre" runat="server" placeholder="GENRE"></asp:TextBox>

                            <p>Upload File</p>
            <asp:FileUpload ID="audio_file" runat="server" class="button" />

                            <p>Upload Album Art</p>
            <asp:FileUpload ID="album_art" runat="server" class="button" text="Upload"/>

            <asp:Button ID="a_upload" class="button" runat="server" Text="UPLOAD" />

            <%--<p class="message"><%--Not registered? <a href="#">Forgot password?</a></p>--%>
        </div>
                    </div>
                </div>
                </div>
    </div>

            <!--empty-->
            <div class="form here col-md-4" style="visibility:hidden"></div>

            <div class="form here col-md-4">
        <div class="container-fluid">
                    <div class="row">
                    <div class="col-md-12">
                        <div class="login-form">
                            <p>VIDEO UPLOAD</p>
            <asp:TextBox ID="TextBox1" runat="server" placeholder="NAME"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" placeholder="DESCRIPTION"></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" placeholder="GENRE"></asp:TextBox>

                            <p>Upload File</p>
            <asp:FileUpload ID="video_file" runat="server" class="button" />

                            <%--<p>Upload Album Art</p>
            <asp:FileUpload ID="FileUpload2" runat="server" class="button" text="Upload"/>--%>

            <asp:Button ID="v_upload" class="button" runat="server" Text="UPLOAD" />

            <%--<p class="message"><%--Not registered? <a href="#">Forgot password?</a></p>--%>
        </div>
                    </div>
                </div>
                </div>
    </div>
        </div>
    </div>
        
         <!-- Footer -->
    <footer class="text-center">
        <div class="footer-above">
            <div class="container">
                <div class="row">
                    <div class="footer-col col-md-4"></div>
                    <div class="footer-col col-md-4">
                        <h3>Location</h3>
                        <p>Kumasi Business Incubator
                            <br>Kumasi, Ghana</p>
                    </div>
                    <div class="footer-col col-md-4"></div>
                    
                </div>
            </div>
        </div>
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
    

    <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>

    <script src="js/index.js"></script>
</body>
</html>
