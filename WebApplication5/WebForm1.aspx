<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication5.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enter Patch Detail</title>
    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="css/Datepicker.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <!-- Sidebar -->
        <div class="sidebar">
            <h3 class="menu-title">Patch Menu</h3>
            <a href="WebForm1.aspx" class="active">Add Patch</a>
            <a href="WebForm2.aspx">View Patch</a>
            <a href="WebForm3.aspx">Update Patch</a>
            <a href="WebForm4.aspx">Upload Patch</a>
             <a href="WebForm5.aspx">Patch downloadh</a>
        </div>

        <!-- Content -->
        <div class="content">
            <h2>Enter Patch Details</h2>
            <div class="form-grid">
                <!-- Row 1 -->
                <div class="grid-item">
                    <asp:Label ID="Label1" runat="server" Text="Date Deployed"></asp:Label>
                    <input type="text" id="deployDate" runat="server" class="datepicker" placeholder="dd-mm-yyyy" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="deployDate" ErrorMessage="* Required" 
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="grid-item">
                    <asp:Label ID="Label2" runat="server" Text="Client:"></asp:Label>

                    <asp:DropDownList ID="ClientName" runat="server" CssClass="dropdown" >
                        <asp:ListItem Text="-- Select Client --" Value="" />
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator2" 
                    runat="server" 
                    ControlToValidate="ClientName"
                    InitialValue=""
                    ErrorMessage="* Required"
                    ForeColor="Red"
                    Display="Dynamic" />

                </div>

                <!-- Row 2 -->
                <div class="grid-item">
                    <asp:Label ID="Label3" runat="server" Text="Project:"></asp:Label>
                    <asp:DropDownList ID="ProjectList" runat="server" CssClass="dropdown">
                        <asp:ListItem Text="-- Select Project---" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="ProjectList" InitialValue=""
                        ErrorMessage="* Select project" ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="grid-item">
                    <asp:Label ID="Label4" runat="server" Text="Release Date:"></asp:Label>
                    <input type="text" id="ReleaseDate" runat="server" class="datepicker" placeholder="dd-mm-yyyy" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="ReleaseDate" ErrorMessage="* Required" 
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Row 3 -->
                <div class="grid-item">
                    <asp:Label ID="Label5" runat="server" Text="Released By:"></asp:Label>
                    <input type="text" id="ReleasedBy" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="ReleasedBy" ErrorMessage="* Required" 
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="grid-item">
                    <asp:Label ID="Label11" runat="server" Text="ISSUE ID:"></asp:Label>
                    <input type="text" id="issueid" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="issueid" ErrorMessage="* Required" 
                        ForeColor="Red" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegexValidator1" runat="server"
                        ControlToValidate="issueid" ValidationExpression="^\d+$"
                        ErrorMessage="* Numbers only" ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Row 4 -->
                <div class="grid-item">
                    <asp:Label ID="Label6" runat="server" Text="Environment:"></asp:Label>
                    <asp:DropDownList ID="EnvironmentList" runat="server" CssClass="dropdown">
                        <asp:ListItem Text="-- Select Environment --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ControlToValidate="EnvironmentList" InitialValue=""
                        ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="grid-item">
                    <asp:Label ID="Label7" runat="server" Text="Name:"></asp:Label>
                    <input type="text" id="Pacth_Name" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                        ControlToValidate="Pacth_Name" ErrorMessage="* Required"
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Row 5 -->
                <div class="grid-item">
                    <asp:Label ID="Label8" runat="server" Text="Patch For:"></asp:Label>
                    <input type="text" id="PatchInfo1" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                        ControlToValidate="PatchInfo1" ErrorMessage="* Required"
                        ForeColor="Red" Display="Dynamic" />
                </div>

                <div class="grid-item">
                    <asp:Label ID="Label10" runat="server" Text="Type:"></asp:Label>
                    <asp:DropDownList ID="TypeList" runat="server" CssClass="dropdown">
                        <asp:ListItem Text="-- Select Type --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                        ControlToValidate="TypeList" InitialValue=""
                        ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" />
                </div>

                <!-- Row 6 -->
                <div class="grid-item.full-width">
                    <asp:Label ID="labelDeployOption" runat="server" Text="Select Deployment Status:" />
                    <asp:DropDownList ID="deployStatus" runat="server" 
                        CssClass="dropdown" AutoPostBack="true" 
                        OnSelectedIndexChanged="deployStatus_SelectedIndexChanged">
                        <asp:ListItem Value="">-- Select --</asp:ListItem>
                        <asp:ListItem Value="patchmaster" Selected ="True">patch master</asp:ListItem>
                        <asp:ListItem Value="patchdeployed">patch deployed</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <!-- Conditional Fields -->
                <div class="grid-item" id="patchIDContainer" runat="server" visible="false">
                    <asp:Label ID="Label12" runat="server" Text="Patch ID:"></asp:Label>
                    <input type="text" id="patchID" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                        ControlToValidate="patchID" ErrorMessage="* Required"
                        ForeColor="Red" Display="Dynamic" Enabled="false" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="patchID" ValidationExpression="^\d+$"
                        ErrorMessage="* Numbers only" ForeColor="Red" Display="Dynamic" Enabled="false" />
                </div>

                <div class="grid-item" id="deployedContainer" runat="server" visible="false">
                    <asp:Label ID="Label9" runat="server" Text="Deployed By:"></asp:Label>
                    <input type="text" id="deployed" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="deployed" ErrorMessage="* Required" 
                        ForeColor="Red" Display="Dynamic" Enabled="false" />
                </div>
            </div>

            <!-- Save Button -->
            <div class="button-container">
                <asp:Button ID="Button1" runat="server" Text="Save Patch" 
                    OnClick="Button1_Click" CssClass="btn-submit" />
                <asp:Label ID="output" runat="server" CssClass="output-message"></asp:Label>
            </div>

        </div>
    </form>

    <script>
        $(function () {
            // Initialize datepickers
            $(".datepicker").datepicker({
                dateFormat: "dd-mm-yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "-5:+5"
            });
        });
    </script>
</body>
</html>