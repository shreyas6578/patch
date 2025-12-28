<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication5.WebForm3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Patch Detail</title>

    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="css/gridview.css" />
    <link rel="stylesheet" type="text/css" href="css/Datepicker.css" />

    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
</head>

<body>

    <!-- Sidebar -->
    <div class="sidebar">
        <h3 class="menu-title">Patch Menu</h3>
        <a href="WebForm1.aspx">Add Patch</a>
        <a href="WebForm2.aspx">View Patch(master)</a>
        <a href="WebForm6.aspx" >View Patch(deployed) </a>
        <a href="WebForm3.aspx" class="active">Update Patch</a>
        <a href="WebForm4.aspx">Upload Patch</a>
         <a href="WebForm5.aspx">Patch download</a>
    </div>

    <!-- ONLY ONE SERVER FORM -->
    <form id="form1" runat="server">

        <div class="content">
            <h2>Update Patch Details</h2>

            <div class="form-grid">

                <!-- Patch ID -->
                <div class="grid-item">
                    <asp:Label ID="label1" runat="server" Text="Patch ID :" />
                    <asp:TextBox ID="patchid" runat="server" CssClass="form-control" />

                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator6"
                        runat="server"
                        ControlToValidate="patchid"
                        ErrorMessage="* Required"
                        ForeColor="Red" />

                    <asp:RegularExpressionValidator 
                        ID="revPatchID"
                        runat="server"
                        ControlToValidate="patchid"
                        ValidationExpression="^\d+$"
                        ErrorMessage="Enter numbers only"
                        ForeColor="Red" />
                </div>

                <!-- Deployment Status -->
                <div class="grid-item">
                    <asp:Label ID="labelDeployOption" runat="server" Text="Select Deployment Status:" />
                    <asp:DropDownList ID="deployStatus" runat="server" CssClass="dropdown">
                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                        <asp:ListItem Value="patchmaster" Selected="True">Patch Master</asp:ListItem>
<%--                        <asp:ListItem Value="patchdeployed">Patch Deployed</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>

            </div>

            <!-- Search Button -->
            <asp:Button ID="Button1" runat="server" Text="Search Patch"
                CssClass="btn-submit" OnClick="Button1_Click" />

            <asp:Label ID="label" runat="server" Visible="false" />

            <!-- GridView -->
            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                AllowPaging="True"
                PageSize="10"
                CssClass="gridview"
                OnPageIndexChanging="GridView1_PageIndexChanging">

                <Columns>
                    <asp:BoundField DataField="PatchID" HeaderText="Patch ID" />
                    <asp:BoundField DataField="PatchName" HeaderText="Patch Name" />
                    <asp:BoundField DataField="PatchInfo" HeaderText="Patch For" />
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

            <!-- Testing Status -->
            <div class="grid-item">
                <asp:Label ID="Label5" runat="server" Text="Testing Status" />
                <asp:TextBox ID="Testing_Status" runat="server" CssClass="form-control" />

   <%--             <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator5"
                    runat="server"
                    ControlToValidate="Testing_Status"
                    ErrorMessage="* Required"
                    ForeColor="Red" />--%>
            </div>
                        <div class="button-container">
                <asp:Button ID="Button2" runat="server" Text="Save Patch" 
                    onClick="Button2_Click" CssClass="btn-submit" />
                <asp:Label ID="output" runat="server" CssClass="output-message"></asp:Label>
            </div>
        </div>

    </form>

</body>
</html>
