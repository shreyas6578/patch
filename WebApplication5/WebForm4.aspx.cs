using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.DropDownList ddlFolders;
        protected global::System.Web.UI.WebControls.FileUpload FileUpload1;
        protected global::System.Web.UI.WebControls.Button btnUpload;
        protected global::System.Web.UI.WebControls.Label lblMessage;

        private string CurrentPath
        {
            get
            {
                return ViewState["CurrentPath"]?.ToString() ?? "";
            }
            set
            {
                ViewState["CurrentPath"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentPath = ""; // root of Uploads
                BindFolders();
            }
        }

        private void BindFolders()
        {
            string basePath = Server.MapPath("~/Uploads");
            string fullPath = Path.Combine(basePath, CurrentPath);

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            ddlFolders.Items.Clear();

            foreach (string dir in Directory.GetDirectories(fullPath))
            {
                ddlFolders.Items.Add(Path.GetFileName(dir));
            }

         lblMessage.Text = "Current Path: /Uploads/" + CurrentPath;
        }

        protected void GO_Click(object sender, EventArgs e)
        {
            if (ddlFolders.SelectedItem == null)
                return;

            string selected = ddlFolders.SelectedValue;

            CurrentPath = string.IsNullOrEmpty(CurrentPath)
                ? selected
                : Path.Combine(CurrentPath, selected);

            BindFolders();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPath))
                return;

            int lastSlash = CurrentPath.LastIndexOf(Path.DirectorySeparatorChar);
            CurrentPath = lastSlash > -1 ? CurrentPath.Substring(0, lastSlash) : "";

            BindFolders();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                lblMessage.Text = "Please select a file.";
                return;
            }

            string uploadsRoot = Server.MapPath("~/Uploads");
            string targetPath = Path.Combine(uploadsRoot, CurrentPath);

            Directory.CreateDirectory(targetPath);

            string fileName = Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Path.Combine(targetPath, fileName));

            lblMessage.Text = "File uploaded to /Uploads/" + CurrentPath;
        }
    }
}
