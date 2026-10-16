using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Customer Credit Limit And Principal Assignment
/// </summary>
public partial class frmDistributotShareAssignment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
        }
    }

    
   
    //private void LoadChannelType()
    //{
    //    SLASHCodesController mController = new SLASHCodesController();
    //    DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
    //    foreach (GridViewRow dr in GrdCreditLimit.Rows)
    //    {
    //        DropDownList DrpChannel = (DropDownList)dr.FindControl("drpChannelType");

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            DrpChannel.Items.Add(new ListItem(dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString()));
    //        }
    //    }
    //}
    

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDistributorSharePrincipal(Constants.IntNullValue, int.Parse(DrpDistributor.SelectedValue), int.Parse(this.Session["UserId"].ToString()), 4, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        GrdCreditLimit.DataSource = m_dt;
        GrdCreditLimit.DataBind();
    }
    
    private void LoadDistributor()
    {
        DistributorController mController = new DistributorController();
        DataTable dt = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, 0, 2, true);
    }
        
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomerDataController mCustomer = new CustomerDataController();
        DataControl dc = new DataControl();

        for (int i = 0; i < GrdCreditLimit.Rows.Count; i++)
        {
            TextBox txtSHARE_VALUE = (TextBox)GrdCreditLimit.Rows[i].FindControl("txtSHARE_VALUE");
            TextBox txtSHARE_PER = (TextBox)GrdCreditLimit.Rows[i].FindControl("txtSHARE_PER");
            int p_principal_Id = int.Parse(GrdCreditLimit.Rows[i].Cells[0].Text.ToString());
            mCustomer.InsertDistributorShare(int.Parse(DrpDistributor.SelectedValue.ToString()), p_principal_Id, decimal.Parse(dc.chkNull_0(txtSHARE_VALUE.Text)), decimal.Parse(txtSHARE_PER.Text), int.Parse(this.Session["UserID"].ToString()));
        }
          ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Updated !');", true);
        LoadDistributor();
        LoadPrincipal();
    }

    



    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPrincipal();
    }
}
