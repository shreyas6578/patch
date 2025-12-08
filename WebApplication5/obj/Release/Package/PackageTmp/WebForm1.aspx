<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication5.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enter Patch Detail</title>

    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <script src="JS/JavaScript.js"></script>
</head>

<body>
<form id="form1" runat="server">

    <!-- Sidebar -->
    <div class="sidebar">
        <h3 class="menu-title">Patch Menu</h3>
        <a href="WebForm1.aspx"class="active">Add Patch</a>
        <a href="WebForm2.aspx">View Patch</a>
        <a href="WebForm3.aspx">Update Patch</a>
        <a href="#">Delete Patch</a>
    </div>

    <!-- Content -->
    <div class="content">
        <h2>Enter Patch Details</h2>

<!-- Date deployed -->
<asp:Label ID="Label1" runat="server" Text="Date Deployed: "></asp:Label>

<input type="text" id="deployDate" runat="server" placeholder="dd-mm-yyyy" />

<link rel="stylesheet"
      href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>

<script>
    $(function () {
        $("#deployDate").datepicker({ dateFormat: "dd-mm-yy" });
    });
</script>

<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator1" 
    runat="server" 
    ControlToValidate="deployDate" 
    ErrorMessage="* Required" 
    ForeColor="Red" />
<br /><br />


<!-- Client -->
<asp:Label ID="Label2" runat="server" Text="Client: "></asp:Label>
<input type="text" id="ClientName" runat="server" />
<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator2" 
    runat="server" 
    ControlToValidate="ClientName" 
    ErrorMessage="* Required" 
    ForeColor="Red" />
<br /><br />

<!-- Project -->
<asp:Label ID="Label3" runat="server" Text="Project: "></asp:Label>
<asp:DropDownList ID="ProjectList" runat="server">
    <asp:ListItem Text="-- Select Project --" Value=""></asp:ListItem>
    <asp:ListItem Text="ADF" Value="ADF"></asp:ListItem>
    <asp:ListItem Text="Cloud Convertor" Value="Cloud Convertor"></asp:ListItem>
    <asp:ListItem Text="JJ CORE" Value="JJ CORE"></asp:ListItem>
    <asp:ListItem Text="MARG" Value="MARG"></asp:ListItem>
    <asp:ListItem Text="PorClaimz" Value="PorClaimz"></asp:ListItem> 
    <asp:ListItem Text="ProClaimz 2.0" Value="ProClaimz 2.0"></asp:ListItem> 
    <asp:ListItem Text="MDM" Value="MDM"></asp:ListItem> 
    <asp:ListItem Text="ZylemMIS" Value="ZylemMIS"></asp:ListItem>
    <asp:ListItem Text="ZylemMIN" Value="ZylemMIN"></asp:ListItem>
    <asp:ListItem Text="OTHER" Value="OTHER"></asp:ListItem>
</asp:DropDownList>

<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator3" 
    runat="server"
    ControlToValidate="ProjectList"
    InitialValue=""
    ErrorMessage="* Select project"
    ForeColor="Red" />
<br /><br />

<!-- Release Date -->
<asp:Label ID="Label4" runat="server" Text="Release Date: "></asp:Label>
<input type="text" id="ReleaseDate" runat="server" placeholder="dd-mm-yyyy" />

<link rel="stylesheet"
      href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
<script src="https://code.jquery.com/jquery-3.6.0.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>

<script>
$(function () {
    $("#ReleaseDate").datepicker({ dateFormat: "dd-mm-yy" });
});
</script>

<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator4" 
    runat="server" 
    ControlToValidate="ReleaseDate" 
    ErrorMessage="* Required" 
    ForeColor="Red" />
<br /><br />

<!-- Released By -->
<asp:Label ID="Label5" runat="server" Text="Released By: "></asp:Label>
<input type="text" id="ReleasedBy" runat="server" />
<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator5" 
    runat="server" 
    ControlToValidate="ReleasedBy" 
    ErrorMessage="* Required" 
    ForeColor="Red" />
<br /><br />

<!-- ISSUE ID -->
<asp:Label ID="Label11" runat="server" Text="ISSUE ID :"></asp:Label>
<input type="text" id="issueid" runat="server" />
<asp:RequiredFieldValidator  
    ID="RequiredFieldValidator10" 
    runat="server" 
    ControlToValidate="issueid" 
    ErrorMessage="* Required" 
    ForeColor="Red" />

<asp:RegularExpressionValidator 
    ID="RegexValidator1"
    runat="server"
    ControlToValidate="issueid"
    ValidationExpression="^\d+$"
    ErrorMessage="* Numbers only"
    ForeColor="Red" />
<br /><br />

<!-- Environment -->
<asp:Label ID="Label6" runat="server" Text="Environment: "></asp:Label>
<asp:DropDownList ID="EnvironmentList" runat="server">
    <asp:ListItem Text="-- Select Environment --" Value=""></asp:ListItem>
    <asp:ListItem Text="UAT" Value="UAT"></asp:ListItem>
    <asp:ListItem Text="LIVE" Value="LIVE"></asp:ListItem>
    <asp:ListItem Text="UAT and LIVE" Value="UAT and LIVE">
    </asp:ListItem> <asp:ListItem Text="OTHER" Value="OTHER"></asp:ListItem>
</asp:DropDownList>

<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator6"
    runat="server"
    ControlToValidate="EnvironmentList"
    InitialValue=""
    ErrorMessage="* Required"
    ForeColor="Red" />
<br /><br />

<!-- Name -->
<asp:Label ID="Label7" runat="server" Text="Name: "></asp:Label>
<input type="text" id="NameText" runat="server" />
<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator7"
    runat="server"
    ControlToValidate="NameText"
    ErrorMessage="* Required"
    ForeColor="Red" />
<br /><br />

<!-- Patch Info -->
<asp:Label ID="Label8" runat="server" Text="Patch For: "></asp:Label>
<input type="text" id="PatchInfo1" runat="server" />
<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator8"
    runat="server"
    ControlToValidate="PatchInfo1"
    ErrorMessage="* Required"
    ForeColor="Red" />
<br /><br />


<!-- Type -->
<asp:Label ID="Label10" runat="server" Text="Type: "></asp:Label>
<asp:DropDownList ID="TypeList" runat="server">
    <asp:ListItem Text="-- Select Type --" Value=""></asp:ListItem>
    <asp:ListItem Text="BUG" Value="BUG"></asp:ListItem>
    <asp:ListItem Text="New Deployment" Value="New Deployment"></asp:ListItem>
</asp:DropDownList>

<asp:ValidationSummary 
    ID="ValidationSummary1" 
    runat="server" 
    ForeColor="Red" 
    HeaderText="Please correct the following:" />
<br />
<asp:Label ID="labelDeployOption" runat="server" Text="Select Deployment Status:" />

<asp:DropDownList ID="deployStatus" 
    runat="server" 
    AutoPostBack="true" 
    OnSelectedIndexChanged="deployStatus_SelectedIndexChanged">
    <asp:ListItem Value="">-- Select --</asp:ListItem>
    <asp:ListItem Value="patchmaster" Selected="True" >patch master</asp:ListItem>
    <asp:ListItem Value="patchdeployed">patch deployed</asp:ListItem>
</asp:DropDownList>

<br />
<!-- patch ID -->
<asp:Label ID="Label12" runat="server" Text="Patch ID :" Visible ="false" ></asp:Label>
<input type="text" id="patchID" runat="server"  Visible ="false"/>
<asp:RequiredFieldValidator  
    ID="RequiredFieldValidator11" 
    runat="server" 
    ControlToValidate="patchID" 
    ErrorMessage="* Required"
     Visible ="false"
    ForeColor="Red" />

<asp:RegularExpressionValidator 
    ID="RegularExpressionValidator1"
    runat="server"
    ControlToValidate="patchID"
    ValidationExpression="^\d+$"
    ErrorMessage="* Numbers only"
     Visible ="false"
    ForeColor="Red" />
<br /><br />
<!-- LABEL (Initially Hidden) -->
<asp:Label ID="Label9" runat="server" Text="Deployed By:" Visible="false"></asp:Label>

<!-- TEXTBOX (Initially Hidden) -->
<input type="text" id="deployed" runat="server" visible="false" />

<!-- VALIDATOR (Initially Hidden) -->
<asp:RequiredFieldValidator 
    ID="RequiredFieldValidator9" 
    runat="server" 
    ControlToValidate="deployed" 
    ErrorMessage="* Required" 
    ForeColor="Red" 
    Visible="false" />

<br />

<!-- Save Button -->
<asp:Button ID="Button1" runat="server"  Text="Save Patch" OnClick="Button1_Click" />
           <br />    <asp:Label ID="output" runat="server" ></asp:Label>
    </div>
</form>
</body>
</html>
