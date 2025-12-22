<%@ Page Language="C#" AutoEventWireup="true"CodeBehind="WebForm4.aspx.cs"Inherits="WebApplication5.WebForm4" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Patch</title>
    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="css/Datepicker.css" />
</head>

<body>
<form id="form1" runat="server">

    <div class="sidebar">
        <h3 class="menu-title">Patch Menu</h3>
        <a href="WebForm1.aspx">Add Patch</a>
        <a href="WebForm2.aspx">View Patch</a>
        <a href="WebForm3.aspx">Update Patch</a>
        <a href="WebForm4.aspx" class="active">Upload Patch</a>
        <a href="WebForm5.aspx">Patch downloadh</a>
    </div>

<div class="content">
    <div class="upload-section">
        <h2>Upload Patch</h2>

        <asp:Label runat="server" Text="Select Upload Folder:" CssClass="form-label" />

        <div class="folder-nav">
            <asp:DropDownList ID="ddlFolders" runat="server" CssClass="dropdown" />
            <asp:Button ID="GO" runat="server" Text="Go" CssClass="nav-btn" OnClick="GO_Click" />
            <asp:Button ID="back" runat="server" Text="Back" CssClass="nav-btn" OnClick="back_Click" />
        </div>

        <div class="file-upload">
            <asp:Label runat="server" Text="Select File:" CssClass="form-label" />
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>

        <asp:Button
            ID="btnUpload"
            runat="server"
            Text="Upload File"
            CssClass="btn-submit upload-btn"
            OnClick="btnUpload_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="upload-path" />
    </div>
</div>


</form>
</body>
</html>
