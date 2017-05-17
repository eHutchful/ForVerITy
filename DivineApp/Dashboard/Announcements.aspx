<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.Master" AutoEventWireup="true" CodeBehind="Announcements.aspx.cs" Inherits="DivineApp.Dashboard.Announcements" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container">
            <div class="right_col">
                <div class="x_panel">
                    <div class="x_content" style="height:1080px">
                        <h3 style="text-align:center;color:turquoise">Announcements Page</h3>
                        <br />
                        <p style="text-align:center;color:#ffd800">Upload your audio or video files here</p>
                        <br />
                        <div>
                            <div class="row">
                                <div class="form col-md-4">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div>
                                                    <h2 class="form-signin-heading">Announcements</h2>
                                                    <br />
                                                    <asp:TextBox ID="name" runat="server" placeholder="TITLE" ></asp:TextBox>
                                                    <br />
                                                    <br /><asp:TextBox ID="description" runat="server" TextMode="MultiLine" placeholder="DESCRIPTION" ></asp:TextBox>
                                                    <br />
                                                    <p>Image</p>
                                                    <br /><asp:FileUpload ID="image" runat="server"  text="Upload"/>
                                                    <br />
                                                    <br /><asp:Button ID="upload" runat="server" Text="Upload" OnClick="upload_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>                            
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </div>
</asp:Content>
