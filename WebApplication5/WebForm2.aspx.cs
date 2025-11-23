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
            string conStr = "Data Source=SHREYAS\\SQLEXPRESS;Initial Catalog=Patch;Integrated Security=True";

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM patchmaster", con);
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
            string conStr = "Data Source=SHREYAS\\SQLEXPRESS;Initial Catalog=Patch;Integrated Security=True";

            int Pachid;
            int Issueid;

            // Fields
            string pacthnames = pacthname.Text.Trim();
            string patchinfos = pacthinfo.Text.Trim();

            DateTime fromdates, todates;

            bool hasPatchId = int.TryParse(patchid.Text, out Pachid);
            bool hasIssueId = int.TryParse(issueid.Text, out Issueid);
            bool hasPatchName = !string.IsNullOrWhiteSpace(pacthnames);
            bool hasPatchFor = !string.IsNullOrWhiteSpace(patchinfos);

            bool hasFromDate = DateTime.TryParseExact(fromdate.Value, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out fromdates);
            bool hasToDate = DateTime.TryParseExact(todate.Value, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out todates);

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
                parameters.Add(new SqlParameter("@pacthname", "%" + pacthnames + "%"));
            }

            if (hasPatchFor)
            {
                query += " AND Patch_For LIKE @patchinfos";
                parameters.Add(new SqlParameter("@patchinfos", "%" + patchinfos + "%"));
            }

            if (hasFromDate && hasToDate)
            {
                query += " AND ReleaseDate BETWEEN @fromdate AND @todate";
                parameters.Add(new SqlParameter("@fromdate", fromdates));
                parameters.Add(new SqlParameter("@todate", todates));
            }

            // If no inputs → show alert
            if (!hasPatchId && !hasIssueId && !hasPatchName && !hasPatchFor && !hasFromDate && !hasToDate)
            {
                Response.Write("<script>alert('Please enter at least one input');</script>");
                return;
            }

            // ---------- RUN QUERY ----------
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand(query, con);

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
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }



    }
}
