using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;

/// <summary>
/// Form For Day Close
/// </summary>
public partial class Forms_frmDayClosePermission : System.Web.UI.Page
{
    DistributorController mController = new DistributorController();

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetLastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()),5);
            if (Grid_Hierarchy.Rows.Count > 1)
            {
                Grid_Hierarchy.UseAccessibleHeader = true;
                Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }

    /// <summary>
    /// Gets Location(s) Last Day Close(s)
    /// </summary>
    /// <param name="UserId">User</param>
    /// <param name="p_Distributor">Location</param>
    //private void LastClosedDay(int UserId, int p_Distributor)
    //{
    //    DistributorController mDayClose = new DistributorController();
    //    DataTable dt = mDayClose.SelectMaxDayClose(UserId, p_Distributor);
    //    if (dt.Rows.Count > 0)
    //    {
    //        this.Session.Add("CurrentWorkDate", DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()));
           
    //    }
    //    else
    //    {
    //       this.Session.Add("CurrentWorkDate", DateTime.Now);
    //      // rblDistributorTypes.Visible = true;
    //    }
    //    GetLastClosedDay(UserId, p_Distributor, 0);       
    //}
    
    /// <summary>
    /// Loads Location(s) Last Day Close(s) To Grid
    /// </summary>
    /// <param name="UserId">User</param>
    /// <param name="p_Distributor">Location</param>
    /// <param name="p_Status">Status</param>
    private void GetLastClosedDay(int UserId, int p_Distributor, int p_Status)
    {
        DataTable dtable = mController.MaxDayClose(int.Parse(this.Session["UserId"].ToString()), p_Status);
        Grid_Hierarchy.DataSource = dtable;
        Grid_Hierarchy.DataBind();
    }
    
    /// <summary>
    /// Loads Locations(Active/InActive/All) Last Day Close(s) To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    //protected void rblDistributorTypes_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetLastClosedDay(int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["Distributor_Id"].ToString()), Convert.ToInt32(rblDistributorTypes.SelectedValue));
    //    if (Grid_Hierarchy.Rows.Count > 1)
    //    {
    //        Grid_Hierarchy.UseAccessibleHeader = true;
    //        Grid_Hierarchy.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    }
    //}

    /// <summary>
    /// Performs Following Tasks And LogOuts
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// Closes Day Transactions
    /// </item>
    /// <item>
    /// Inserts Cash Data To GL
    /// </item>
    /// <item>
    /// Inserts Cheques Data To GL
    /// </item>
    /// <item>
    /// Inserts Expenses Data To GL
    /// </item>
    /// <item>
    /// Inserts Sales And Sales Return TO GL
    /// </item>
    /// <iterm>
    /// Inserts Purchase Data To GL
    /// </iterm>
    /// <item>
    /// Inserts Purchase Return To GL
    /// </item>
    /// <item>
    /// Inserts Rate Difference Data To GL
    /// </item>
    /// <item>
    /// Inserts LogOut Time
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnDayClose_Click(object sender, EventArgs e)
    {
        string distributor_ids="";
        string distributo_unchec_ids = "";
        for (int count = 0; count < Grid_Hierarchy.Rows.Count; count++)
        {
            if (((CheckBox)Grid_Hierarchy.Rows[count].FindControl("chkSelect")).Checked)
            {
                distributor_ids = (Grid_Hierarchy.Rows[count].Cells[0].Text) + ", " + distributor_ids;
            }
            else
            {
                distributo_unchec_ids = (Grid_Hierarchy.Rows[count].Cells[0].Text) + ", " + distributo_unchec_ids;
            }
        }

        DistributorController mDayClose = new DistributorController();

            bool dt = mDayClose.UspClosePermissionChange(distributor_ids,  int.Parse(this.Session["UserID"].ToString()), distributo_unchec_ids);

            if (dt == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Some error occured Contact System Administrator');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' Day Close Permission granted for the select Distributors');", true);

            }
        
    }
}
