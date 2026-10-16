using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;

public partial class Forms_RptRouteWiseMonthlySales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadArea();
            this.LoadSalesForce();
            this.LoadChannelType();
            this.LoadCategory();
            this.LoadSKU();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("MMM-yyyy");
        }
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
        else
        {
            DrpRoute.Items.Clear();
        }
    }

    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.CustomerChannelType, null, Constants.IntNullValue, bool.Parse("True"));
        drpChannelType.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(drpChannelType, dt, 0, 2);

    }

    private void LoadCategory()
    {
        DrpCategory.Items.Clear();

        SkuHierarchyController mSkuHieController = new SkuHierarchyController();
        DataTable dtDist = mSkuHieController.SelectDTSkuHierarchy01(2, int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        DrpCategory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpCategory, dtDist, "Category_Id", "Category_Name");
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadSalesForce();
    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSalesForce();
        this.LoadCategory();
        this.LoadSKU();
    }

    protected void LoadSalesForce()
    {
        SaleForceController ds = new SaleForceController();
        DataTable dt = ds.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpRoute.SelectedValue), int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(DrpPrincipal.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            this.drpSaleForce.Items.Clear();
            this.drpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.drpSaleForce, dt, "USER_ID", "USER_NAME");
        }
        else
        {
            this.drpSaleForce.Items.Clear();
        }
    }

    private void LoadSKU()
    {
        SkuController mSKUController = new SkuController();
        DataTable m_SKUDt = mSKUController.SelectSkuInfo(int.Parse(DrpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpCategory.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));

        this.DrpSKU.Items.Clear();
        this.DrpSKU.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpSKU, m_SKUDt, "SKU_ID", "SKUDETAIL");
    }

    protected void DrpCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKU();
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSalesForce();
    }

    private void ShowReport(int p_Report_Type)
    {

        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        DateTime dtFromMonth = DateTime.Parse(txtStartDate.Text);
        dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

        DateTime dtToMonth = DateTime.Parse(txtEndDate.Text);
        dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
        dtTo = dtTo.AddMonths(1).AddDays(-1);

        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataControl dc = new DataControl();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        DataSet ds = RptCustomerCtl.SelectRouteWiseMonthlySales(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), dtFrom, dtTo, Constants.IntNullValue, int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, DrpReportType.SelectedIndex, Constants.IntNullValue, Constants.IntNullValue);

        //SAMSBusinessLayer.Reports.CrpRouteWiseMonthlySales CrpReport = new SAMSBusinessLayer.Reports.CrpRouteWiseMonthlySales();
        SAMSBusinessLayer.Reports.CrpRouteWiseMonthlySalesNewWithMarkets CrpReport = new SAMSBusinessLayer.Reports.CrpRouteWiseMonthlySalesNewWithMarkets();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Distributor_Name", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("Area", DrpRoute.SelectedItem.Text);
        CrpReport.SetParameterValue("ChannelType", drpChannelType.SelectedItem.Text);
        CrpReport.SetParameterValue("ReportType", "Customer Monthly " + DrpReportType.SelectedItem.Text + " Report");
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("OrderBooker", drpSaleForce.SelectedItem.Text);
        CrpReport.SetParameterValue("SKU", DrpSKU.SelectedItem.Text);
        CrpReport.SetParameterValue("Category", DrpCategory.SelectedItem.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", p_Report_Type);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}