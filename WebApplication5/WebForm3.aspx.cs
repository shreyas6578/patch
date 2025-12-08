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
            // Do NOT bind anything here because page loads empty
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(patchid.Text))
            {
                label.ForeColor = System.Drawing.Color.Red;
                label.Visible = true;
                label.Text = "Please enter Patch ID!";
                return;
            }

            int patchID;
            if (!int.TryParse(patchid.Text, out patchID))
            {
                label.ForeColor = System.Drawing.Color.Red;
                label.Visible = true;
                label.Text = "Invalid Patch ID!";
                return;
            }

            try
            {
                DataTable dt = GetPatchData(patchID);

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    label.ForeColor = System.Drawing.Color.Green;
                    label.Visible = true;
                    label.Text = "record found!";
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    label.ForeColor = System.Drawing.Color.Red;
                    label.Visible = true;
                    label.Text = "No record found!";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
                label.ForeColor = System.Drawing.Color.Red;
                label.Visible = true;
                label.Text = "Error: "+ex.Message;
            }
        }

        private DataTable GetPatchData(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT * FROM patchmaster WHERE PatchID = @patchID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@patchID", id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            // Re-bind with latest data
            int id = int.Parse(patchid.Text);
            GridView1.DataSource = GetPatchData(id);
            GridView1.DataBind();
        }
    }
}
