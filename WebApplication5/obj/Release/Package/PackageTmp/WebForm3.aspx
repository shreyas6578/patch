<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication5.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enter Patch Detail</title>

    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <script src="JS/JavaScript.js"></script>
</head>

<body>
        <!-- Sidebar -->
    <div class="sidebar">
        <h3 class="menu-title">Patch Menu</h3>
        <a href="WebForm1.aspx">Add Patch</a>
        <a href="WebForm2.aspx" >View Patch</a>
        <a href="WebForm3.aspx" class="active">Update Patch</a>
        <a href="#">Delete Patch</a>
    </div>
           <!-- jQuery UI Datepicker CSS -->
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
<!-- jQuery -->
<script src="https://code.jquery.com/jquery-3.6.0.js"></script>
<!-- jQuery UI -->
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
    <form id="form3" runat="server">
        <div class="main-content">
<!-- Patch ID -->
<asp:Label ID="label1" runat="server" Text="Patch ID :"></asp:Label>
<asp:TextBox ID="patchid" runat="server"></asp:TextBox>
     <!-- Button -->
     <asp:Button ID="Button1" runat="server" Text="Search Patch" OnClick="Button1_Click" />
 
        <asp:RegularExpressionValidator 
            ID="revPatchID" 
            runat="server" 
            ControlToValidate="patchid"
            ErrorMessage="Enter numbers only"
            ValidationExpression="^\d+$"
            ForeColor="Red"
            Display="Dynamic" />

           <asp:RequiredFieldValidator 
            ID="RequiredFieldValidator6"
            runat="server"
            ControlToValidate="patchid"
            InitialValue=""
            ErrorMessage="* Required"
            ForeColor="Red" />
        </div>
    </form>
</body>
</html>
