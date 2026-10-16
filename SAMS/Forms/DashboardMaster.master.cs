using System;
using System.Web.UI.WebControls;
using System.Data;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Text;

public partial class DashboardMaster : System.Web.UI.MasterPage
{
    MenuController MenuCtl = new MenuController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblWorkingDate.Text = ((DateTime)this.Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
           // ltrlMenu.Text = GetTopMenu();
        }
    }

    private string GetTopMenu()
    {
        //DataTable dtBreadCrumb = MenuCtl.GetMenu(Constants.IntNullValue, Constants.IntNullValue, 12, (int)Session["RoleID"]);
        //StringBuilder strBreadCrumb = new StringBuilder();
        //if (dtBreadCrumb != null && dtBreadCrumb.Rows.Count > 0)
        //{
        //    foreach (DataRow row in dtBreadCrumb.Rows)
        //    {
        //        if (row["FirstLink"].ToString() != "")
        //        {
        //            strBreadCrumb.Append("<a href='" + row["FirstLink"].ToString() + "?LevelType=3&LevelID=" + row["FirstLinkID"] + "&TopID=" + row["MENU_ID"] + "'>" + row["MENU_DESCRIPTION"].ToString() + "</a>");
        //        }
        //    }
        //}
        //return strBreadCrumb.ToString();
        return "";
    }
}
