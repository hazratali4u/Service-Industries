using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;    
using SAMSBusinessLayer.Reports;

/// <summary>
/// Form For Sale Person DSR Report
/// </summary>
public partial class Forms_rptCustomerWiseRealization : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadArea();
            this.LoadCustomer();
            this.LoadSaleForce();   
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        DrpPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, 0, 2, true);
    }

    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            ddlRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            ddlRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlRoute, dt, 0, 6);
        }
        else
        {
            ddlRoute.Items.Clear();
        }
    }

    private void LoadCustomer()
    {
        ddlCustomer.Items.Clear();
        if (drpDistributor.Items.Count > 0 && ddlRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(ddlRoute.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            ddlCustomer.Items.Add(new ListItem("All", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlCustomer, dt, 0, 4);
        }
        else
        {
            ddlCustomer.Items.Add(new ListItem("Customer Not Found", Constants.LongNullValue.ToString()));
        }
    }

    private void LoadSaleForce()
    {
        if (drpDistributor.Items.Count > 0)
        {
            ddlSaleForce.Items.Clear();
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(ddlRoute.SelectedValue), int.Parse(Session["CompanyId"].ToString()));
            ddlSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlSaleForce, m_dt, 0, 3);
        }
    }

    /// <summary>
    /// Shows Sale Person DSR in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows Sale Person DSR in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    /// <summary>
    /// Gets Sale Person DSR And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="p_ReportType">Type</param>
    private void ShowReport(int p_ReprotType)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustCtl = new RptCustomerController();
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        DataSet ds = null;
        if (dt.Rows.Count > 0)
        {
            DataControl dc = new DataControl();
            ds = RptCustCtl.GetCustomerWiseRealization(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(DrpPrincipal.SelectedValue),Convert.ToInt32(ddlSaleForce.SelectedValue)
                , int.Parse(Session["UserId"].ToString()),Convert.ToInt32(ddlRoute.SelectedValue),Convert.ToInt64(ddlCustomer.SelectedValue),DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));


            SAMSBusinessLayer.Reports.CrpCustomerWiseRealization2 CrpReport = new SAMSBusinessLayer.Reports.CrpCustomerWiseRealization2();
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("DateFrom", txtStartDate.Text);
            CrpReport.SetParameterValue("DateTo", txtEndDate.Text);
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("SaleForce", ddlSaleForce.SelectedItem.Text);
            CrpReport.SetParameterValue("Company", dt.Rows[0]["COMPANY_NAME"].ToString());

            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", p_ReprotType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadCustomer();
        this.LoadSaleForce();
    }

    protected void Route_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCustomer();
        this.LoadSaleForce();
    }
}