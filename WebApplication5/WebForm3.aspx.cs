using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        static string conStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Do not bind anything here
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(patchid.Text))
            {
                output.Text = "Please enter Patch ID!";
                output.ForeColor = System.Drawing.Color.Red;
                output.Visible = true;
                return;
            }

            if (!int.TryParse(patchid.Text, out int patchID))
            {
                output.Text = "Invalid Patch ID!";
                output.ForeColor = System.Drawing.Color.Red;
                output.Visible = true;
                return;
            }

            try
            {
                DataTable dt = GetPatchData(patchID);

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    output.Text = "Record found!";
                    output.ForeColor = System.Drawing.Color.Green;
                    output.Visible = true;

                    using (SqlConnection con = new SqlConnection(conStr))
                    {
                        string query = "SELECT TestingStatus FROM PatchMaster WHERE PatchID = @patchID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.Add("@patchID", SqlDbType.Int).Value = patchID;
                            con.Open();

                            object result = cmd.ExecuteScalar();
                            Testing_Status.Text = result != null ? result.ToString() : string.Empty;
                        }
                    }
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    output.Text = "No record found!";
                    output.ForeColor = System.Drawing.Color.Red;
                    output.Visible = true;
                }
            }
            catch (Exception ex)
            {
                output.Text = "Error: " + ex.Message;
                output.ForeColor = System.Drawing.Color.Red;
                output.Visible = true;
            }
        }

        private DataTable GetPatchData(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT * FROM PatchMaster WHERE PatchID = @patchID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@patchID", SqlDbType.Int).Value = id;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            if (int.TryParse(patchid.Text, out int id))
            {
                GridView1.DataSource = GetPatchData(id);
                GridView1.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Save();
            }
        }

        private void Save()
        {
            if (!int.TryParse(patchid.Text.Trim(), out int patchID))
            {
                output.Text = "Invalid Patch ID!";
                output.ForeColor = System.Drawing.Color.Red;
                output.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(Testing_Status.Text))
            {
                output.Text = "Testing Status cannot be empty!";
                output.ForeColor = System.Drawing.Color.Red;
                output.Visible = true;
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string query = @"UPDATE PatchMaster
                                     SET TestingStatus = @status
                                     WHERE PatchID = @patchID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@status", SqlDbType.VarChar, 50)
                                      .Value = Testing_Status.Text.Trim();
                        cmd.Parameters.Add("@patchID", SqlDbType.Int)
                                      .Value = patchID;

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            output.Text = "Testing Status updated successfully!";
                            output.ForeColor = System.Drawing.Color.Green;
                            output.Visible = true;
                        }
                        else
                        {
                            output.Text = "Update failed. Patch not found.";
                            output.ForeColor = System.Drawing.Color.Red;
                            output.Visible = true;
                        }
                    }
                }
            }
            catch
            {
                output.Text = "An error occurred while updating data.";
                output.ForeColor = System.Drawing.Color.Red;
                output.Visible = true;
            }
        }
    }
}
