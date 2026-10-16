using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmUploadNotification2 : System.Web.UI.Page
{

    DistributorController DController = new DistributorController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadGrid();
        }
    }

    protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            HiddenField hfSTATUS_ID = (HiddenField)e.Row.FindControl("hfSTATUS_ID");
            if (hfSTATUS_ID.Value == "1")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FEF0C9");
            }
        }
    }

    private void LoadGrid()
    {
        DataTable dt = DController.GetDocuemnt(Convert.ToInt64(Session["DocumentID"]), Constants.DateNullValue, Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            lblSubject2.Text = dt.Rows[0]["SUBJECT"].ToString();
            lblDocumentName2.Text = dt.Rows[0]["DOCUMENT_NAME"].ToString();
            lblUploadedBy2.Text = dt.Rows[0]["USER_NAME"].ToString();
            lblUploadDateTime2.Text = dt.Rows[0]["TIME_STAMP"].ToString();
        }

        gvDocument.DataSource = dt;
        gvDocument.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session.Remove("DocumentID");
        Response.Redirect("frmUploadNotification.aspx?LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
    }
}