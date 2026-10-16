using SAMSBusinessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            loadLogo();
        }
    }
    private void loadLogo()
    {

        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectLogo(2, Convert.ToInt32(Session["UserID"]));
        if (dt.Rows.Count > 0)
        {
            System.Text.StringBuilder logotext = new System.Text.StringBuilder();
            logotext.Append("<table><tr>");
            foreach (DataRow dr in dt.Rows)
            {

                logotext.Append("<td>");
                logotext.Append("<img src='" + "/" + dr["LGOG_NAME"].ToString() + "' alt=''  />");
                logotext.Append("</td>");
            }
            logotext.Append("</tr></table>");
            ltrlLogo.Text = logotext.ToString();
        }
    }
}
