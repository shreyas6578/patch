using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
namespace WebApplication5
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        static string conStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
   
                BindGrid();   // Reloads grid on new page
            }

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();   // reload 20 records for that page
        }
        private void BindGrid()
        {
           

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM patchmaster order by PatchID desc", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            save();
        }
        protected void save()
        {
            int Pachid;
            int Issueid;

            // Fields (make sure these IDs exist as server controls)
            string pacthNameText = pacthname.Text.Trim();
            string patchInfoText = pacthinfo.Text.Trim();

            DateTime fromDateValue, toDateValue;

            bool hasPatchId = int.TryParse(patchid.Text, out Pachid);
            bool hasIssueId = int.TryParse(issueid.Text, out Issueid);
            bool hasPatchName = !string.IsNullOrWhiteSpace(pacthNameText);
            bool hasPatchFor = !string.IsNullOrWhiteSpace(patchInfoText);

            // Try flexible date parsing — HTML date inputs give yyyy-MM-dd; allow common formats
            bool hasFromDate = DateTime.TryParse(fromdate.Value, out fromDateValue);
            bool hasToDate = DateTime.TryParse(todate.Value, out toDateValue);
            errorlabel.Visible = false;
            // If no inputs → show alert
            if (!hasPatchId && !hasIssueId && !hasPatchName && !hasPatchFor && !hasFromDate && !hasToDate)
            {
                errorlabel.Visible = true;
                errorlabel.Text = "Please enter at least one input";
                errorlabel.ForeColor = System.Drawing.Color.Red;
    
            }

            // ---------- BUILD DYNAMIC QUERY ----------
            string query = "SELECT * FROM patchmaster WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (hasPatchId)
            {
                query += " AND PatchID = @Pachid";
                parameters.Add(new SqlParameter("@Pachid", Pachid));
            }

            if (hasIssueId)
            {
                query += " AND IssueID = @Issueid";
                parameters.Add(new SqlParameter("@Issueid", Issueid));
            }

            if (hasPatchName)
            {
                query += " AND PatchName LIKE @pacthname";
                parameters.Add(new SqlParameter("@pacthname", "%" + pacthNameText + "%"));
            }

            if (hasPatchFor)
            {
                query += " AND PatchInfo LIKE @patchinfos";
                parameters.Add(new SqlParameter("@patchinfos", "%" + patchInfoText + "%"));
            }

            if (hasFromDate && hasToDate)
            {
                // Use BETWEEN when both provided
                query += " AND ReleaseDate BETWEEN @fromdate AND @todate";
                parameters.Add(new SqlParameter("@fromdate", fromDateValue.Date));
                parameters.Add(new SqlParameter("@todate", toDateValue.Date));
            }
            else if (hasFromDate) // single-sided range
            {
                query += " AND ReleaseDate >= @fromdate";
                parameters.Add(new SqlParameter("@fromdate", fromDateValue.Date));
            }
            else if (hasToDate)
            {
                query += " AND ReleaseDate <= @todate";
                parameters.Add(new SqlParameter("@todate", toDateValue.Date));
            }

            // final ordering
            query += " ORDER BY PatchID DESC";

            // ---------- RUN QUERY ----------
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }



    }
}
