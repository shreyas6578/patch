using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.DropDownList ddlFolders;
        protected global::System.Web.UI.WebControls.FileUpload FileUpload1;
        protected global::System.Web.UI.WebControls.Button btnUpload;
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
                CurrentPath = ""; // Root of Uploads
                BindFolders();
                BindFiles();
            }
        }

        // ===============================
        // Load Subfolders
        // ===============================
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
        }

        // ===============================
        // Load Files into GridView
        // ===============================
        private void BindFiles()
        {
            string basePath = Server.MapPath("~/Uploads");
            string fullPath = Path.Combine(basePath, CurrentPath);

            if (!Directory.Exists(fullPath))
            {
                gvFiles.DataSource = null;
                gvFiles.DataBind();
                return;
            }

            var files = Directory.GetFiles(fullPath)
                .Select(f => new
                {
                    FileName = Path.GetFileName(f),
                    FilePath = f
                })
                .ToList();

            gvFiles.DataSource = files;
            gvFiles.DataBind();
        }

        // ===============================
        // Go to Selected Folder
        // ===============================
        protected void GO_Click(object sender, EventArgs e)
        {
            if (ddlFolders.SelectedItem == null)
                return;

            string selectedFolder = ddlFolders.SelectedValue;

            CurrentPath = string.IsNullOrEmpty(CurrentPath)
                ? selectedFolder
                : Path.Combine(CurrentPath, selectedFolder);

            BindFolders();
            BindFiles();
        }

        // ===============================
        // Go Back One Level
        // ===============================
        protected void back_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentPath))
                return;

            int lastSlash = CurrentPath.LastIndexOf(Path.DirectorySeparatorChar);
            CurrentPath = lastSlash > -1
                ? CurrentPath.Substring(0, lastSlash)
                : "";

            BindFolders();
            BindFiles();
        }

        // ===============================
        // Download File
        // ===============================
        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string filePath = e.CommandArgument.ToString();

                if (!File.Exists(filePath))
                    return;

                FileInfo file = new FileInfo(filePath);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition",
                    "attachment; filename=" + file.Name);
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }
    }
}
