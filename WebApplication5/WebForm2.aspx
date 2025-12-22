<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication5.WebForm2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="css/gridview.css" />
     <link rel="stylesheet" type="text/css" href="css/Datepicker.css" />
    <script src="JS/JavaScript.js"></script>
    <title>View Patch Detail</title>

</head>
<body>
    <!-- Sidebar -->
    <div class="sidebar">
        <h3 class="menu-title">Patch Menu</h3>
        <a href="WebForm1.aspx">Add Patch</a>
        <a href="WebForm2.aspx" class="active">View Patch</a>
        <a href="WebForm3.aspx">Update Patch</a>
        <a href="WebForm4.aspx">Upload Patch</a>
        <a href="WebForm5.aspx">Patch downloadh</a>
    </div>
<!-- jQuery UI Datepicker CSS -->
<link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">

<!-- jQuery -->
<script src="https://code.jquery.com/jquery-3.6.0.js"></script>

<!-- jQuery UI -->
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>

<script>
    $(function () {
        $("#fromdate").datepicker({ dateFormat: "dd-mm-yy" });
        $("#todate").datepicker({ dateFormat: "dd-mm-yy" });
    });
</script>
<!-- Change main-content to content -->
<div class="content">
    <h2>View Patch Detail</h2>

    <form id="form2" runat="server">
        <div class="form-grid">
            <!-- Row 1 -->
            <div class="grid-item">
                <asp:Label ID="label1" runat="server" Text="Patch ID :"></asp:Label>
                <asp:TextBox ID="patchid" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator 
                    ID="revPatchID" 
                    runat="server" 
                    ControlToValidate="patchid"
                    ErrorMessage="Enter numbers only"
                    ValidationExpression="^\d+$"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>
            
            <div class="grid-item">
                <asp:Label ID="label2" runat="server" Text="Issue ID :"></asp:Label>
                <asp:TextBox ID="issueid" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator 
                    ID="revIssueID" 
                    runat="server" 
                    ControlToValidate="issueid"
                    ErrorMessage="Enter numbers only"
                    ValidationExpression="^\d+$"
                    ForeColor="Red"
                    Display="Dynamic" />
            </div>
            
            <!-- Row 2 -->
            <div class="grid-item">
                <asp:Label ID="label3" runat="server" Text="Patch Name :"></asp:Label>
                <asp:TextBox ID="pacthname" runat="server"></asp:TextBox>
            </div>
            
            <div class="grid-item">
                <asp:Label ID="label4" runat="server" Text="Patch Info :"></asp:Label>
                <asp:TextBox ID="pacthinfo" runat="server"></asp:TextBox>
            </div>
            
            <!-- Row 3 -->
            <div class="grid-item">
                <asp:Label ID="label5" runat="server" Text="From Date :"></asp:Label>
                <input type="text" id="fromdate" runat="server" placeholder="dd-mm-yyyy" />
            </div>
            
            <div class="grid-item">
                <asp:Label ID="label6" runat="server" Text="To Date :"></asp:Label>
                <input type="text" id="todate" runat="server" placeholder="dd-mm-yyyy" />
            </div>
        </div>
  <br />
        <!-- Button -->
        <asp:Button ID="Button1" runat="server" Text="Search Patch" onclick="Button1_Click" CssClass="btn-submit"/>
        <asp:Label ID="errorlabel" runat="server" Text="" CssClass="output-message" Visible="false"></asp:Label>
     <asp:GridView ID="GridView1" runat="server"
        AutoGenerateColumns="False"
        AllowPaging="True"
        PageSize="10"
        CssClass="gridview"
        PagerStyle-CssClass="pager"
        OnPageIndexChanging="GridView1_PageIndexChanging">

    <Columns>
        <asp:BoundField DataField="PatchID" HeaderText="Patch ID" />
        <asp:BoundField DataField="PatchName" HeaderText="Patch Name" />
        <asp:BoundField DataField="PatchInfo" HeaderText="Patch For" />
        <asp:BoundField DataField="Type" HeaderText="Type" />
        <asp:BoundField DataField="IssueID" HeaderText="Issue ID" />
        <asp:BoundField DataField="Client" HeaderText="Client" />
        <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" 
            DataFormatString="{0:dd-MMM-yyyy}" />
        <asp:BoundField DataField="DeploymentDate" HeaderText="Deployment Date" 
            DataFormatString="{0:dd-MMM-yyyy}" />
        <asp:BoundField DataField="Environment" HeaderText="Environment" />
        <asp:BoundField DataField="ReleasedBy" HeaderText="Released By" />
        <asp:BoundField DataField="TestingStatus" HeaderText="Testing Status" />
        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />

    </Columns>

</asp:GridView>

    </form>

</div>

</body>
</html>