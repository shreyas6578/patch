using System;
using System.Collections;
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
                getClientName();
                BindProjectList();
                Enviroment();
                Type();
            }
        }
        private void getClientName()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"SELECT ClientID, 
                        (ClientName + ' (' + ClientAlias + ')') AS NAMES 
                        FROM ClientMaster";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        ClientName.DataSource = dr;
                        ClientName.DataTextField = "NAMES";
                        ClientName.DataValueField = "ClientID";
                        ClientName.DataBind();
                    }
                }
            }

            // Default item
            ClientName.Items.Insert(0, new ListItem("-- Select Client --", ""));
        }


        // ------------------------- Bind Project Names -------------------------
        private void BindProjectList()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Description FROM SubGroupMaster WHERE GroupType = 'Project' ORDER BY Description";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    ProjectList.DataSource = dr;
                    ProjectList.DataTextField = "Description";   // What user sees
                    ProjectList.DataValueField = "Description";  // What value is stored
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
                string query = "SELECT Description FROM SubGroupMaster WHERE GroupType = 'Environment' ORDER BY Description";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    EnvironmentList.DataSource = dr;
                    EnvironmentList.DataTextField = "Description";   // What user sees
                    EnvironmentList.DataValueField = "Description";  // What value is stored
                    EnvironmentList.DataBind();
                }
            }

            EnvironmentList.Items.Insert(0, new ListItem("-- Environment --", ""));
        }

        private void Type()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Description FROM SubGroupMaster WHERE GroupType = 'Type' ORDER BY Description";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    TypeList.DataSource = dr;
                    TypeList.DataTextField = "Description";   // What user sees
                    TypeList.DataValueField = "Description";  // What value is stored
                    TypeList.DataBind();
                }
            }

            TypeList.Items.Insert(0, new ListItem("-- Select Type --", ""));
        }

        // ------------------------- Deployment Status Change -------------------------
        protected void deployStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool show = deployStatus.SelectedValue == "patchdeployed";

            // Show/hide the containers
            patchIDContainer.Visible = show;
            deployedContainer.Visible = show;

            // Enable/disable validators
            RequiredFieldValidator9.Enabled = show;
            RequiredFieldValidator11.Enabled = show;
            RegularExpressionValidator1.Enabled = show;
        }

        // ------------------------- Submit Button -------------------------
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime deployDateValue = DateTime.ParseExact(deployDate.Value, "dd-MM-yyyy", null);
                string clientname = ClientName.SelectedItem.Text;
                string project_name = ProjectList.SelectedValue;
                DateTime releaseDateValue = DateTime.ParseExact(ReleaseDate.Value, "dd-MM-yyyy", null);
                string releasedBy = ReleasedBy.Value;
                int Issueid = Convert.ToInt32(issueid.Value);
                string environment = EnvironmentList.SelectedItem.Text;
                string patch_name = Pacth_Name.Value;
                string patch_info = PatchInfo1.Value;
                string TypeLists = TypeList.SelectedItem.Text;
                string testingStatus = "";
                string generatedPatchName = "";

                // ----------- Generate PatchName (MaxID + "_" + PatchName) -----------
                using (SqlConnection con = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(PatchID),0) FROM PatchMaster", con);
                    con.Open();
                    int maxId = Convert.ToInt32(cmd.ExecuteScalar());

                    generatedPatchName = (maxId + 1) + "_" + patch_name;
                }

                // ----------- Insert Patch Record -----------
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = @"
                         INSERT INTO PatchMaster 
                    (PatchName, PatchInfo, Type, IssueID, Client, ReleaseDate,
                     DeploymentDate, Environment, ReleasedBy, TestingStatus, ProjectName) 
                    VALUES 
                    (@PatchName, @Patch_For, @Type, @IssueID, @Client, @ReleaseDate, 
                     @DeploymentDate, @Environment, @ReleasedBy, @TestingStatus, @ProjectName)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@PatchName", generatedPatchName);
                    cmd.Parameters.AddWithValue("@Patch_For", patch_info);
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

                output.Text = "Patch saved successfully!.<br/>"+ generatedPatchName;
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
