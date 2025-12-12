<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication5.WebForm3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Patch Detail</title>
    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="css/gridview.css" />
    <link rel="stylesheet" type="text/css" href="css/Datepicker.css" />
    <script src="JS/JavaScript.js"></script>

    <!-- jQuery UI Datepicker CSS -->
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <!-- jQuery UI -->
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
</head>

<body>

    <!-- Sidebar -->
    <div class="sidebar">
        <h3 class="menu-title">Patch Menu</h3>
        <a href="WebForm1.aspx">Add Patch</a>
        <a href="WebForm2.aspx">View Patch</a>
        <a href="WebForm3.aspx" class="active">Update Patch</a>
        <a href="#">Delete Patch</a>
    </div>

    <form id="form3" runat="server">
        <div class="content">
           <di+ class="form-grid">
            <!-- Patch ID -->
            <asp:Label ID="label1" runat="server" Text="Patch ID :"></asp:Label>
            <asp:TextBox ID="patchid" runat="server" class="form-control"></asp:TextBox>
            </di+>
            <!-- Search Button -->
            <asp:Button ID="Button1" runat="server" Text="Search Patch" OnClick="Button1_Click" CssClass="btn-submit" />

            <!-- Patch ID validators -->
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

            <!-- Deployment Status -->
          
            <asp:Label ID="labelDeployOption" runat="server" Text="Select Deployment Status:" />
            <asp:DropDownList ID="deployStatus" runat="server" AutoPostBack="true">
                <asp:ListItem Value="">-- Select --</asp:ListItem>
                <asp:ListItem Value="patchmaster" Selected="True">patch master</asp:ListItem>
                <asp:ListItem Value="patchdeployed">patch deployed</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID ="label" runat="server" Text=""
               visible="false"
                ></asp:Label>
      
            <br /><br />
  </div>
            <!-- GridView -->
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
                    <asp:BoundField DataField="Patch_For" HeaderText="Patch For" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="IssueID" HeaderText="Issue ID" />
                    <asp:BoundField DataField="Client" HeaderText="Client" />
                    <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="DeploymentDate" HeaderText="Deployment Date" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="Environment" HeaderText="Environment" />
                    <asp:BoundField DataField="ReleasedBy" HeaderText="Released By" />
                    <asp:BoundField DataField="TestingStatus" HeaderText="Testing Status" />
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                </Columns>

            </asp:GridView>


    </form>

</body>
</html>
