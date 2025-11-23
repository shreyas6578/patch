using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void deployStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deployStatus.SelectedValue == "patchdeployed") 
            {
                Label9.Visible = true;
                deployed.Visible = true;
                RequiredFieldValidator9.Visible = true;
                Label12.Visible = true;
                patchID.Visible = true;
                RequiredFieldValidator11.Visible = true;
                RegularExpressionValidator1.Visible = true;
            }
            else
            {
                Label9.Visible = false;
                deployed.Visible = false;
                RequiredFieldValidator9.Visible = false;
                Label12.Visible = false;
                patchID.Visible = false;
                RequiredFieldValidator11.Visible = false;
                RegularExpressionValidator1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string clientname = ClientName.Value;
                string project_name = ProjectList.SelectedValue;
                string patch_for = project_name;        // Patch_For column
                int Issueid = Convert.ToInt32(issueid.Value);
                string enviromnet = EnvironmentList.SelectedValue;
                string patch_name = NameText.Value;
                string patch_info = PatchInfo1.Value;
                string TypeLists = TypeList.SelectedValue;
                string releasedBy = ReleasedBy.Value;
                string testingStatus = "";                   // default if not present
                DateTime deployDateValue = DateTime.ParseExact(deployDate.Value, "dd-MM-yyyy", null);
                DateTime releaseDateValue = DateTime.ParseExact(ReleaseDate.Value, "dd-MM-yyyy", null);

                string conStr = "Data Source=SHREYAS\\SQLEXPRESS;Initial Catalog=Patch;Integrated Security=True;";

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string query = @"INSERT INTO PatchMaster
(PatchName, Patch_For, Type, IssueID, Client, ReleaseDate, DeploymentDate, Environment, ReleasedBy, TestingStatus, ProjectName)
VALUES
(@PatchName, @Patch_For, @Type, @IssueID, @Client, @ReleaseDate, @DeploymentDate, @Environment, @ReleasedBy, @TestingStatus, @ProjectName);";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PatchName", patch_name);
                    cmd.Parameters.AddWithValue("@Patch_For", patch_for);
                    cmd.Parameters.AddWithValue("@Type", TypeLists);
                    cmd.Parameters.AddWithValue("@IssueID", Issueid);
                    cmd.Parameters.AddWithValue("@Client", clientname);
                    cmd.Parameters.AddWithValue("@ReleaseDate", releaseDateValue);
                    cmd.Parameters.AddWithValue("@DeploymentDate", deployDateValue);
                    cmd.Parameters.AddWithValue("@Environment", enviromnet);
                    cmd.Parameters.AddWithValue("@ReleasedBy", releasedBy);
                    cmd.Parameters.AddWithValue("@TestingStatus", testingStatus);
                    cmd.Parameters.AddWithValue("@ProjectName", project_name);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
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