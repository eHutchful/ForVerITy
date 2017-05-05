<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.Master" AutoEventWireup="true" CodeBehind="AudioUpload.aspx.cs" Inherits="DivineApp.Dashboard.AudioUpload"  Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container">
            <div class="right_col">
                <div class="x_panel">
                    <div class="x_content" style="height:800px">
                    <h3 style="text-align:center;color:turquoise">File Upload Page</h3>
        <br />
        <p style="text-align:center;color:white">Upload your audio or video files here</p>
        <br />
        <h2 class="form-signin-heading">AUDIO UPLOAD</h2>
        <br />
        <asp:TextBox ID="name" runat="server" placeholder="NAME" ></asp:TextBox>
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
        <br /><asp:Button ID="upload" runat="server" Text="Upload" />
                </div>
                </div>                
            </div>
        </div>
    </div>
</asp:Content>
