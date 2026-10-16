using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Reports;  
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Form For Print Sale Document Report
/// </summary>
public partial class Forms_rptSalesManWiseCustomerSale : System.Web.UI.Page
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
            this.LoadDistributor();
            this.LoadPrincipal();
            this.LoadRoute();
            this.LoadSaleForce();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text =Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");

        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);     
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.GetDistributorWithChild(int.Parse(this.Session["UserId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 1);
    }
    
    /// <summary>
    /// Loads Sale Forces To Sale Force Combo
    /// </summary>
    private void LoadSaleForce()
    {
        this.DrpSaleForce.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            DrpSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpSaleForce, m_dt, 0, 3);
        }
    }
    
    /// <summary>
    /// Loads Routes To Route Combo
    /// </summary>
    private void LoadRoute()
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
    
    /// <summary>
    /// Loads Sale Forces And Routes
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
        this.LoadRoute();
    }

    /// <summary>
    /// Loads Sale Forces
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
    }

    /// <summary>
    /// Loads Sale Forces
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpLedgerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
    }

    /// <summary>
    /// Shows Print Sale Document in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Gets Print Sale Document And Shows Either in Excel or PDF
    /// </summary>
    /// <param name="pReportType">Type</param>
    private void ShowReport(int pReportType)
    {
        RptSaleController RptSaleCtl = new RptSaleController();
        DocumentPrintController DPrint = new DocumentPrintController();
        DataSet ds = null;
        
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        if (dt.Rows.Count > 0)
        {
            DataControl dc = new DataControl();
            ds = RptSaleCtl.GetSalemanWiseCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), Convert.ToInt32(DrpSaleForce.SelectedValue),Convert.ToInt32(DrpRoute.SelectedValue),Convert.ToInt32(rblType.SelectedValue));
            ReportDocument CrpReport = new ReportDocument();
            if (rblType.SelectedValue == "1")
            {
                CrpReport = new CrpSalemanWiseCustomerSummary();
            }
            else
            {
                CrpReport = new CrpSalemanWiseCustomer();
            }
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("FROM_DATE", DateTime.Parse(txtStartDate.Text));
            CrpReport.SetParameterValue("TO_DATE", DateTime.Parse(txtEndDate.Text));
            CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("Route", DrpRoute.SelectedItem.Text);
            CrpReport.SetParameterValue("Salesman", DrpSaleForce.SelectedItem.Text);
            Session.Add("CrpReport", CrpReport);
            Session.Add("ReportType", pReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Print Sale Document in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
}