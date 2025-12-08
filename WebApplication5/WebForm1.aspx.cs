using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjectList();
                Enviroment();
                Type();
            }
        }

        // ------------------------- Bind Project Names -------------------------
        private void BindProjectList()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Name FROM Settings WHERE id = 'project_names'";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                object rawValue = cmd.ExecuteScalar();

                if (rawValue != null)
                {
                    string names = rawValue.ToString();   // Example: "ADF,Cloud Convertor,JJ CORE"
                    string[] items = names.Split(',')
                                          .Select(x => x.Trim())
                                          .Where(x => x.Length > 0)
                                          .ToArray();

                    ProjectList.DataSource = items;
                    ProjectList.DataBind();
                }
            }

            ProjectList.Items.Insert(0, new ListItem("-- Select Project --", ""));
        }

        // ------------------------- Bind Environments -------------------------
        private void Enviroment()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Name FROM Settings WHERE id = 'Environment'";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                object rawValue = cmd.ExecuteScalar();

                if (rawValue != null)
                {
                    string names = rawValue.ToString();
                    string[] items = names.Split(',')
                                          .Select(x => x.Trim())
                                          .Where(x => x.Length > 0)
                                          .ToArray();

                    EnvironmentList.DataSource = items;
                    EnvironmentList.DataBind();
                }
            }

            EnvironmentList.Items.Insert(0, new ListItem("-- Environment --", ""));
        }

        private void Type()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Name FROM Settings WHERE id = 'Type'";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                object rawValue = cmd.ExecuteScalar();

                if (rawValue != null)
                {
                    string names = rawValue.ToString();
                    string[] items = names.Split(',')
                                          .Select(x => x.Trim())
                                          .Where(x => x.Length > 0)
                                          .ToArray();

                    TypeList.DataSource = items;
                    TypeList.DataBind();
                }
            }

            TypeList.Items.Insert(0, new ListItem("-- Select Type --", ""));
        }

        // ------------------------- Deployment Status Change -------------------------
        protected void deployStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool show = deployStatus.SelectedValue == "patchdeployed";

            Label9.Visible = show;
            deployed.Visible = show;
            RequiredFieldValidator9.Visible = show;

            Label12.Visible = show;
            patchID.Visible = show;
            RequiredFieldValidator11.Visible = show;
            RegularExpressionValidator1.Visible = show;
        }


        // ------------------------- Submit Button -------------------------
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string clientname = ClientName.Value;
                string project_name = ProjectList.SelectedValue;
                string patch_for = project_name;

                int Issueid = Convert.ToInt32(issueid.Value);
                string environment = EnvironmentList.SelectedValue;
                string patch_name = NameText.Value;
                string patch_info = PatchInfo1.Value;
                string TypeLists = TypeList.SelectedValue;
                string releasedBy = ReleasedBy.Value;

                string testingStatus = ""; // default

                DateTime deployDateValue = DateTime.ParseExact(deployDate.Value, "dd-MM-yyyy", null);
                DateTime releaseDateValue = DateTime.ParseExact(ReleaseDate.Value, "dd-MM-yyyy", null);

                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = @"
                    INSERT INTO PatchMaster 
                    (PatchName, Patch_For, Type, IssueID, Client, ReleaseDate,
                     DeploymentDate, Environment, ReleasedBy, TestingStatus, ProjectName) 
                    VALUES 
                    (@PatchName, @Patch_For, @Type, @IssueID, @Client, @ReleaseDate, 
                     @DeploymentDate, @Environment, @ReleasedBy, @TestingStatus, @ProjectName);";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@PatchName", patch_name);
                    cmd.Parameters.AddWithValue("@Patch_For", patch_for);
                    cmd.Parameters.AddWithValue("@Type", TypeLists);
                    cmd.Parameters.AddWithValue("@IssueID", Issueid);
                    cmd.Parameters.AddWithValue("@Client", clientname);
                    cmd.Parameters.AddWithValue("@ReleaseDate", releaseDateValue);
                    cmd.Parameters.AddWithValue("@DeploymentDate", deployDateValue);
                    cmd.Parameters.AddWithValue("@Environment", environment);
                    cmd.Parameters.AddWithValue("@ReleasedBy", releasedBy);
                    cmd.Parameters.AddWithValue("@TestingStatus", testingStatus);
                    cmd.Parameters.AddWithValue("@ProjectName", project_name);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                output.Text = "Patch saved successfully!";
                output.ForeColor = System.Drawing.Color.Green;
            }
            catch (FormatException ex)
            {
                output.Text = "Error: Issue ID or Date format is invalid.<br/>" + ex.Message;
                output.ForeColor = System.Drawing.Color.Red;
            }
            catch (SqlException ex)
            {
                output.Text = "Database Error: " + ex.Message;
                output.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                output.Text = "Unexpected Error: " + ex.Message;
                output.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
