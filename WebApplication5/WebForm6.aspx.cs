using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        private static readonly string conStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM patchdeployed ORDER BY refernumber DESC", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Better error handling - log and show user-friendly message
                errorlabel.Visible = true;
                errorlabel.Text = "An error occurred while loading data. Please try again.";
                errorlabel.ForeColor = System.Drawing.Color.Red;

                // Log the actual error (consider using a logging framework)
                System.Diagnostics.Debug.WriteLine($"Error in BindGrid: {ex.Message}");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void Search()
        {
            try
            {
                // Get search parameters
                string patchIdText = patchid.Text.Trim();
                string issueIdText = issueid.Text.Trim();
                string patchNameText = pacthname.Text.Trim();
                string patchInfoText = pacthinfo.Text.Trim();
                string fromDateText = fromdate.Value;
                string toDateText = todate.Value;
                string Deployed = Deployed_By.Text.Trim();
                string project = Project_.Text.Trim(); 
                System.Diagnostics.Debug.WriteLine($"DEBUG - Input values:");
                System.Diagnostics.Debug.WriteLine($"  FromDate: '{fromDateText}'");
                System.Diagnostics.Debug.WriteLine($"  ToDate: '{toDateText}'");

                // Build dynamic query
                string query = "SELECT * FROM patchdeployed WHERE 1=1";
                List<SqlParameter> parameters = new List<SqlParameter>();
                

                // PatchID (integer)
                if (int.TryParse(patchIdText, out int patchId))
                {
                    query += " AND PatchID = @PatchID";
                    parameters.Add(new SqlParameter("@PatchID", patchId));
                }

                // IssueID (integer)
                if (int.TryParse(issueIdText, out int issueId))
                {
                    query += " AND IssueID = @IssueID";
                    parameters.Add(new SqlParameter("@IssueID", issueId));
                }

                // PatchName (string with wildcard search)
                if (!string.IsNullOrWhiteSpace(patchNameText))
                {
                    query += " AND PatchName LIKE @PatchName";
                    parameters.Add(new SqlParameter("@PatchName", "%" + patchNameText + "%"));
                }
                // Deployed (string with wildcard search)
                if (!string.IsNullOrWhiteSpace(Deployed))
                {
                    query += " AND Patchdeployedby LIKE @Deployed";
                    parameters.Add(new SqlParameter("@Deployed", "%" + Deployed + "%"));
                }

                // Project (string with wildcard search)
                if (!string.IsNullOrWhiteSpace(project))
                {
                    query += " AND ProjectName LIKE @ProjectName";
                    parameters.Add(new SqlParameter("@ProjectName", "%" + project + "%"));
                }

                // PatchInfo (string with wildcard search)
                if (!string.IsNullOrWhiteSpace(patchInfoText))
                {
                    query += " AND PatchInfo LIKE @PatchInfo";
                    parameters.Add(new SqlParameter("@PatchInfo", "%" + patchInfoText + "%"));
                }

                // Date range handling - FIXED for jQuery UI format "dd-mm-yy"
                if (!string.IsNullOrEmpty(fromDateText))
                {
                    // jQuery UI sends "dd-mm-yy" (two-digit year)
                    // We need to handle both two-digit and four-digit years
                    if (TryParseDate(fromDateText, out DateTime fromDate))
                    {
                        query += " AND DeploymentDate >= @FromDate";
                        SqlParameter fromParam = new SqlParameter("@FromDate", SqlDbType.Date);
                        fromParam.Value = fromDate;
                        parameters.Add(fromParam);
                        System.Diagnostics.Debug.WriteLine($"  Parsed FromDate: {fromDate:yyyy-MM-dd}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"  ERROR: Could not parse FromDate: '{fromDateText}'");
                    }
                }

                if (!string.IsNullOrEmpty(toDateText))
                {
                    if (TryParseDate(toDateText, out DateTime toDate))
                    {
                        query += " AND DeploymentDate <= @ToDate";
                        SqlParameter toParam = new SqlParameter("@ToDate", SqlDbType.Date);
                        toParam.Value = toDate;
                        parameters.Add(toParam);
                        System.Diagnostics.Debug.WriteLine($"  Parsed ToDate: {toDate:yyyy-MM-dd}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"  ERROR: Could not parse ToDate: '{toDateText}'");
                    }
                }

                // Final ordering
                query += " ORDER BY refernumber DESC";

                // Execute query
                using (SqlConnection con = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        // Show message if no results found
                        if (dt.Rows.Count == 0)
                        {
                            errorlabel.Visible = true;
                            errorlabel.Text = "No records found matching your criteria.";
                            errorlabel.ForeColor = System.Drawing.Color.Blue;
                        }
                        else
                        {
                            errorlabel.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorlabel.Visible = true;
                errorlabel.Text = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorlabel.Text += $"<br/>Inner Exception: {ex.InnerException.Message}";
                }
                errorlabel.ForeColor = System.Drawing.Color.Red;

                System.Diagnostics.Debug.WriteLine($"FINAL ERROR: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        // Helper method to parse dates in various formats
        private bool TryParseDate(string dateText, out DateTime result)
        {
            result = DateTime.MinValue;

            if (string.IsNullOrEmpty(dateText))
                return false;

            // Try multiple date formats
            string[] formats = {
                "dd-MM-yy",      // jQuery UI default with 2-digit year
                "dd-MM-yyyy",    // With 4-digit year
                "d-M-yy",        // Single digit day/month
                "d-M-yyyy",      // Single digit day/month with 4-digit year
                "yyyy-MM-dd"     // Standard format
            };

            return DateTime.TryParseExact(dateText, formats,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }


    }

}