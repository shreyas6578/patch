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
        int data = 0;
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
            ClientName.Items.Clear();

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"SELECT ClientID,ClientName + ' (' + ClientAlias + ')' AS NAMES FROM ClientMaster ORDER BY ClientName ASC";

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

            patchIDContainer.Visible = show;
            deployedContainer.Visible = show;

            RequiredFieldValidator9.Enabled = show;
            RequiredFieldValidator11.Enabled = show;
            RegularExpressionValidator1.Enabled = show;

            DeployData = show ? 1 : 0;
        }


        // ------------------------- Submit Button -------------------------
        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if (DeployData == 0)
            {
                pacthmasterinstered();
            }
            else
            {
                output.Text = "feature is not avablie";
                output.ForeColor = System.Drawing.Color.Red;

            }
            
        }
        private int DeployData
        {
            get { return ViewState["DeployData"] == null ? 0 : (int)ViewState["DeployData"]; }
            set { ViewState["DeployData"] = value; }
        }

        protected void pacthmasterinstered()
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

                // ----------- Single Connection -----------
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();

                    // ----------- Insert and get the auto-generated PatchID -----------
                    string query = @"
                INSERT INTO PatchMaster 
                (PatchInfo, Type, IssueID, Client, ReleaseDate,
                 DeploymentDate, Environment, ReleasedBy, TestingStatus, ProjectName) 
                VALUES 
                (@Patch_For, @Type, @IssueID, @Client, @ReleaseDate, 
                 @DeploymentDate, @Environment, @ReleasedBy, @TestingStatus, @ProjectName);
                 
                SELECT SCOPE_IDENTITY();";  // Gets the newly generated PatchID

                    SqlCommand cmd = new SqlCommand(query, con);

                    // Don't include PatchName in the initial insert
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

                    // Execute and get the new auto-generated PatchID
                    int newPatchId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Now create the PatchName using the actual PatchID
                    generatedPatchName = newPatchId + "_" + patch_name;

                    // ----------- Update with the generated PatchName -----------
                    string updateQuery = "UPDATE PatchMaster SET PatchName = @PatchName WHERE PatchID = @PatchID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@PatchName", generatedPatchName);
                    updateCmd.Parameters.AddWithValue("@PatchID", newPatchId);
                    updateCmd.ExecuteNonQuery();
                }

                output.Text = "Patch saved successfully!.<br/>" + generatedPatchName;
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
