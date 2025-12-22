<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="WebApplication5.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patch download</title>
    <link rel="stylesheet" type="text/css" href="css/StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="css/Datepicker.css" />
    <link rel="stylesheet"type ="text/css" href="css/gridview.css" />
</head>
<body>
    <form id="form1" runat="server">
          <div class="sidebar">
        <h3 class="menu-title">Patch Menu</h3>
        <a href="WebForm1.aspx">Add Patch</a>
        <a href="WebForm2.aspx">View Patch</a>
        <a href="WebForm3.aspx">Update Patch</a>
        <a href="WebForm4.aspx" >Upload Patch</a>
        <a href="WebForm5.aspx" class="active">Patch downloadh</a>
    </div>
        <div class="content">
    <h2>Patch downloadh</h2>
            <asp:Label runat="server" Text="Select Upload Folder:" CssClass="form-label" />

        <div class="folder-nav">
            <asp:DropDownList ID="ddlFolders" runat="server" CssClass="dropdown" />
            <asp:Button ID="GO" runat="server" Text="Go" CssClass="nav-btn" OnClick="GO_Click" />
            <asp:Button ID="back" runat="server" Text="Back" CssClass="nav-btn" OnClick="back_Click" />
        </div>
        <asp:GridView 
    ID="gvFiles"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="file-grid"
    OnRowCommand="gvFiles_RowCommand">

    <Columns>
        <asp:BoundField 
            DataField="FileName" 
            HeaderText="File Name" />

        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button
                    ID="btnDownload"
                    runat="server"
                    Text="Download"
                    CssClass="btn-submit"
                    CommandName="Download"
                    CommandArgument='<%# Eval("FilePath") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>
        </div>
    </form>
</body>
</html>
