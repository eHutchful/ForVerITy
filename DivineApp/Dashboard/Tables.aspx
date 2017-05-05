<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.Master" AutoEventWireup="true" CodeBehind="Tables.aspx.cs" Inherits="DivineApp.Dashboard.Tables" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class ="container" data-ng-controller="studentsCtrl" data-ng-app="app">
        <div class="right_col" role="main">
            <div class="">
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="x_panel">
                            <div class="x_title">
                                <h2>Media Files</h2>
                                <div class="clearfix"></div>
                            </div>
                            <div class="x_content" style="height:800px">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="card-box table-responsive">
                                            <table class ="table table-striped table-hover table-condensed" >
                                                <thead>
                                                <tr>
                                                    <th>Id</th>
                                                    <th>Name</th>
                                                    <th>Credit</th>
                                                    <th>Semester</th>
                                                </tr>
                                                </thead>
                                                <tbody>
                                                     <tr data-ng-repeat="student in studentInfo">
                                                         <td>{{student.id}}</td>
                                                         <td>{{student.name}}</td>
                                                         <td>{{student.credit}}</td>
                                                         <td>{{student.semester}}</td>

                                                     </tr>

                                                </tbody>

                                            </table>
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
