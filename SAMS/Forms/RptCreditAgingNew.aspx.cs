using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form For Credit Aging Report
/// </summary>
public partial class Forms_RptCreditAgingNew : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadAssingned();
            this.LoadPrincipal();
            this.LoadChannelType();
            this.LoadArea();
            this.LoadMarket();
            this.LoadCustomer();
            LoadSaleForce();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtDocmentDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            rblReportBehaviour_SelectedIndexChanged(null, null);
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadAssingned()
    {
        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectUserAssignment(int.Parse(this.Session["UserId"].ToString()), 6, 1, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 1);
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Channel Types To ChannelType Combo
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        drpChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2);

    }

    private void LoadSaleForce()
    {
        if (drpDistributor.Items.Count > 0)
        {
            ddlSaleForce.Items.Clear();
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(Session["CompanyId"].ToString()));
            ddlSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlSaleForce, m_dt, 0, 3);
        }
    }

    /// <summary>
    /// Shows Credit Aging Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0)
        {
            ddlCustomer.Items.Clear();
            LedgerController mController = new LedgerController();
            DataTable dtCredit = mController.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.LongNullValue, Constants.IntNullValue);
            ddlCustomer.Items.Add(new ListItem("All", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlCustomer, dtCredit, 0, 1);
        }
    }

    /// <summary>
    /// Shows Credit Aging Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Sets Visibility of Controls as Per Rpeort Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void rbtSotBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRoute.Visible = false;
        ddlRoute.Visible = false;
        lblMarket.Visible = false;
        ddlMarket.Visible = false;
        if (rbtSotBy.SelectedIndex == 2)
        {
            txtDocmentDate.Visible = true;
            ibtnStartDate.Visible = true;
            lblStartDate.Visible = true;            
        }
        else if(rbtSotBy.SelectedIndex == 1)
        {
            txtDocmentDate.Visible = false;
            ibtnStartDate.Visible = false;
            lblStartDate.Visible = false;
            lblRoute.Visible = true;
            ddlRoute.Visible = true;
            lblMarket.Visible = true;
            ddlMarket.Visible = true;
        }
        else
        {
            txtDocmentDate.Visible = false;
            ibtnStartDate.Visible = false;
            lblStartDate.Visible = false;            
        }
    }

    protected void rblReportBehaviour_SelectedIndexChanged(object sender, EventArgs e)
    {
        rbtSotBy.SelectedIndex = 0;
        rbtSotBy_SelectedIndexChanged(null, null);
        rbtSotBy.Visible = true;
        lblEndDate.Text = "End Date";
        lblCustomer.Visible = false;
        ddlCustomer.Visible = false;
        lblSortBy.Visible = false;
        ddlSortBy.Visible = false;
        lblOrderBy.Visible = false;
        ddlOrderBy.Visible = false;
        lblChannelType.Visible = true;
        drpChannelType.Visible = true;
        if (rblReportBehaviour.SelectedValue == "2")
        {
            rbtSotBy.Visible = false;
            lblEndDate.Text = "As On Date";
            lblCustomer.Visible = true;
            ddlCustomer.Visible = true;
            lblSortBy.Visible = true;
            ddlSortBy.Visible = true;
            lblOrderBy.Visible = true;
            ddlOrderBy.Visible = true;
            lblChannelType.Visible = false;
            drpChannelType.Visible = false;
        }
    }

    private void ShowReport(int intReportType)
    {
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        int i0to30, i30to45, i46to75, i76to90, iabove90;
        i0to30 = i30to45 = i46to75 = i76to90 = iabove90 = 0;
        if (cbONetorthirty.Checked == true) { i0to30 = 1; }
        if (cb31to45.Checked == true) { i30to45 = 1; }
        if (cb46to75.Checked == true) { i46to75 = 1; }
        if (cb76to90.Checked == true) { i76to90 = 1; }
        if (cbabove90.Checked == true) { iabove90 = 1; }
        if (rblReportBehaviour.SelectedValue == "1")
        {

            DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            DataSet ds = RptCustomerCtl.GetCreditAgingReportNewCT(6, int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()),
                int.Parse(this.Session["UserId"].ToString()), rbtSotBy.SelectedIndex, DateTime.Parse(txtDocmentDate.Text), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
                int.Parse(drpChannelType.SelectedValue.ToString()),Convert.ToInt32(ddlRoute.SelectedValue),Convert.ToInt32(ddlMarket.SelectedValue),
                Convert.ToInt32(ddlSaleForce.SelectedValue), i0to30, i30to45, i46to75, i76to90, iabove90, int.Parse(this.Session["UserID"].ToString()));
            SAMSBusinessLayer.Reports.CrpCreditAgingReportNewCT CrpReport = new SAMSBusinessLayer.Reports.CrpCreditAgingReportNewCT();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("UptoDate", txtEndDate.Text);
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("ReportType", rbtSotBy.SelectedIndex.ToString());
            CrpReport.SetParameterValue("ChannelType", drpChannelType.SelectedItem.Text);
            if (rbtSotBy.SelectedIndex == 1)
            {
                CrpReport.SetParameterValue("Route", "Route: " + ddlRoute.SelectedItem.Text);
                CrpReport.SetParameterValue("Market", "Market: " + ddlMarket.SelectedItem.Text);
            }
            else
            {
                CrpReport.SetParameterValue("Route", "");
                CrpReport.SetParameterValue("Market", "");
            }
            CrpReport.SetParameterValue("SalePerson", "Sale Force: " + ddlSaleForce.SelectedItem.Text);
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", intReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                DataSet ds = RptCustomerCtl.GetCreditAgingSummary(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), Convert.ToInt64(ddlCustomer.SelectedValue), DateTime.Parse(txtDocmentDate.Text + " 23:59:59")
                    , Convert.ToInt32(ddlSortBy.SelectedValue), Convert.ToInt32(ddlOrderBy.SelectedValue),Convert.ToInt32(ddlSaleForce.SelectedValue), int.Parse(this.Session["UserID"].ToString()));
                SAMSBusinessLayer.Reports.CrpCreditAgingSummary CrpReport = new SAMSBusinessLayer.Reports.CrpCreditAgingSummary();
                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
                CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
                CrpReport.SetParameterValue("Customer", ddlCustomer.SelectedItem.Text);
                CrpReport.SetParameterValue("Date_To", txtDocmentDate.Text);
                CrpReport.SetParameterValue("SalePerson",ddlSaleForce.SelectedItem.Text);
                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", intReportType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadMarket();
        this.LoadCustomer();
        LoadSaleForce();
    }

    private void LoadArea()
    {
        ddlRoute.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {            
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            ddlRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlRoute, dt, 0, 6);
        }
    }

    private void LoadMarket()
    {
        ddlMarket.Items.Clear();
        if (drpDistributor.Items.Count > 0 && ddlRoute.Items.Count > 0)
        {
            DistributorRouteController mController = new DistributorRouteController();
            DataTable dt = mController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(ddlRoute.SelectedValue.ToString()));
            ddlMarket.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlMarket, dt, "Route_ID", "Route_NAME");
        }
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadMarket();
    }
}