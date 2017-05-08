<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.Master" AutoEventWireup="true" CodeBehind="AudioUpload.aspx.cs" Inherits="DivineApp.Dashboard.AudioUpload"  Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container">
            <div class="right_col">
                <div class="x_panel">
                    <div class="x_content" style="height:1080px">
                        <h3 style="text-align:center;color:turquoise">File Upload Page</h3>
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
                                                    <h2 class="form-signin-heading">AUDIO UPLOAD</h2>
                                                    <br />
                                                    <asp:TextBox ID="name" runat="server" placeholder="TITLE" ></asp:TextBox>
                                                    <br />
                                                    <asp:TextBox ID="subtitle" runat="server" placeholder="SUBTITLE" ></asp:TextBox>
                                                    <br />
                                                    <asp:TextBox ID="category" runat="server" placeholder="CATEGORY" ></asp:TextBox>
                                                    <br /><asp:TextBox ID="authorName" runat="server" placeholder="AUTHOR" ></asp:TextBox>
                                                    <br /><asp:TextBox ID="email" runat="server" placeholder="EMAIL" ></asp:TextBox>
                                                    <br /><asp:TextBox ID="description" runat="server" TextMode="MultiLine" placeholder="DESCRIPTION" ></asp:TextBox>
                                                    <br />
                                                    <p>Choose Album Art</p>
                                                    <br /><asp:FileUpload ID="album_art" runat="server"  text="Upload"/>
                                                    <br />
                                                    <p>Choose Media File</p>
                                                    <br /><asp:FileUpload ID="upload_file" runat="server"  />
                                                    <br /><asp:Button ID="upload" runat="server" Text="Upload" OnClick="upload_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form col-md-4" style="visibility:hidden"></div>
                                <div class="form col-md-4">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div>
                                                    <h2 class="form-signin-heading">VIDEO UPLOAD</h2>
                                                    <br />
                                                    <asp:TextBox ID="txtVTitle" runat="server" placeholder="TITLE" ></asp:TextBox>
                                                    <br />
                                                    <asp:TextBox ID="txtVSubtitle" runat="server" placeholder="SUBTITLE" ></asp:TextBox>
                                                    <br />
                                                    <asp:TextBox ID="txtVCategory" runat="server" placeholder="CATEGORY" ></asp:TextBox>
                                                    <br /><asp:TextBox ID="txtVAuthor" runat="server" placeholder="AUTHOR" ></asp:TextBox>
                                                    <br /><asp:TextBox ID="txtVEmail" runat="server" placeholder="EMAIL" ></asp:TextBox>
                                                    <br /><asp:TextBox ID="txtVDescription" runat="server" TextMode="MultiLine" placeholder="DESCRIPTION" ></asp:TextBox>
                                                    <br />
                                                    <p>Choose Album Art</p>
                                                    <br /><asp:FileUpload ID="vImageUpload" runat="server"  text="Upload"/>
                                                    <br />
                                                    <p>Choose Media File</p>
                                                    <br /><asp:FileUpload ID="videoUpload" runat="server"  />
                                                    <br /><asp:Button ID="btnVUpload" runat="server" Text="Upload" Onclick="btnVUpload_Click" />
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
