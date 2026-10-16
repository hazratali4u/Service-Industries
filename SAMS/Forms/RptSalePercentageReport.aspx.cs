using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;

/// <summary>
/// Form For  Catagory Wise Customer Report
/// </summary>
public partial class RptSalePercentageReport : System.Web.UI.Page
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
            LoadPrincipal();
         
            LoadLocation();
           
            DateTime dt = (DateTime)this.Session["CurrentWorkDate"]; 
            this.txtFromDate.Text = dt.ToString("dd-MMM-yyyy");
            this.txtToDate.Text   = dt.ToString("dd-MMM-yyyy");
        }
    }
    
    protected void LoadPrincipal()
    {
        drpPrincipal.Items.Clear();
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            this.drpPrincipal.Items.Add(new ListItem("All", Constants .IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name",false);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    
    protected void LoadLocation()
    {
        DrpLocation.Items.Clear();
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", false);

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    private void ShowReport(int ReportType)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSalesCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));
            CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrpReport = new SAMSBusinessLayer.Reports.CrpSalePercentage();
            DataSet dts= RptSalesCtl.SalePercentageReport(int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToInt32(this.drpPrincipal.SelectedValue.ToString()), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text + " 23:59:59"), Convert.ToInt32(Session["UserID"]));
            CrpReport.SetDataSource(dts);
            CrpReport.Refresh();
            CrpReport.SetParameterValue("Location", DrpLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtFromDate.Text);
            CrpReport.SetParameterValue("ToDate", txtToDate.Text);
            CrpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());            
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", ReportType);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}