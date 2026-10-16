using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// Form For SKU Wise Branch Sales Report
/// </summary>
public partial class Forms_RptDistributorReconcilation : System.Web.UI.Page
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
            LoadMTown();
           LoadArea();
            LoadMarket();
            LoadTransporter();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
          //  DrpPrincipal_SelectedIndexChanged(null,null);
         //   DrpMTown_SelectedIndexChanged(null,null);

            LoadDeliveryman();
        }
    }
    private void LoadDeliveryman()
    {
        this.ddlSaleForce.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
            ddlSaleForce.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlSaleForce, m_dt, 0, 3);
        }
    }

    /// <summary>
    /// Loads User Assigned Locations To Location Combo
    /// </summary>
    private void LoadAssingned()
    {
        drpDistributor.Items.Clear();
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
    /// Shows SKU Wise Branch Sales in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        string trans = "All";
        if(DrpTransporter.SelectedIndex!=0)
        {
            trans = DrpTransporter.SelectedItem.Text.Trim();
        }
        DataSet ds = RptSaleCtl.GetDistributorReconcilation(int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(this.txtStartDate.Text + " 00:00:00"), DateTime.Parse(this.txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(rblType.SelectedValue), trans, int.Parse(DrpTransporter.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpMarket.SelectedValue), int.Parse(DrpOrderType.SelectedValue),int.Parse(ddlSaleForce.SelectedValue),int.Parse(DrpMTown.SelectedValue));
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        SAMSBusinessLayer.Reports.CrpDistributorReconcilation CrpReport = new SAMSBusinessLayer.Reports.CrpDistributorReconcilation();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Transporter", DrpTransporter.SelectedItem.Text);
        CrpReport.SetParameterValue("Route", DrpRoute.SelectedItem.Text);
        CrpReport.SetParameterValue("market", DrpMarket.SelectedItem.Text);
        CrpReport.SetParameterValue("type", DrpOrderType.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportName", "SKU Wise Branch Sales (" + rblType.SelectedItem.Text + ")");
        CrpReport.SetParameterValue("SaleForce", ddlSaleForce.SelectedItem.Text);
        CrpReport.SetParameterValue("CityName",DrpMTown.SelectedItem.Text);

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script); 
    }

    /// <summary>
    /// Shows SKU Wise Branch Sales in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExce_Click(object sender, EventArgs e)
    {

        DocumentPrintController mDocumentPrntControl = new DocumentPrintController();
        RptSaleController RptSaleCtl = new RptSaleController();
        string trans = "All";
        if (DrpTransporter.SelectedIndex != 0)
        {
            trans = DrpTransporter.SelectedItem.Text.Trim();
        }
        DataSet ds = RptSaleCtl.GetDistributorReconcilation(int.Parse(DrpPrincipal.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), DateTime.Parse(this.txtStartDate.Text + " 00:00:00"), DateTime.Parse(this.txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(rblType.SelectedValue), trans, int.Parse(DrpTransporter.SelectedValue), int.Parse(DrpRoute.SelectedValue), int.Parse(DrpMarket.SelectedValue), int.Parse(DrpOrderType.SelectedValue),int.Parse(ddlSaleForce.SelectedValue), int.Parse(DrpMTown.SelectedValue));
        DataTable dt = mDocumentPrntControl.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));


        SAMSBusinessLayer.Reports.CrpDistributorReconcilation CrpReport = new SAMSBusinessLayer.Reports.CrpDistributorReconcilation();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        CrpReport.SetParameterValue("FromDate", txtStartDate.Text);
        CrpReport.SetParameterValue("ToDate", txtEndDate.Text);
        CrpReport.SetParameterValue("Principal", DrpPrincipal.SelectedItem.Text);
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("Transporter", DrpTransporter.SelectedItem.Text);
        CrpReport.SetParameterValue("Route", DrpRoute.SelectedItem.Text);
        CrpReport.SetParameterValue("market", DrpMarket.SelectedItem.Text);
        CrpReport.SetParameterValue("type", DrpOrderType.SelectedItem.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportName", "SKU Wise Branch Sales (" + rblType.SelectedItem.Text + ")");
        CrpReport.SetParameterValue("SaleForce", ddlSaleForce.SelectedItem.Text);
        CrpReport.SetParameterValue("CityName", DrpMTown.SelectedItem.Text);

        string path = SAMSCommon.Classes.Configuration.GetAppInstallationPath() + "\\ExportedFile.xls";

        CrpReport.SetDatabaseLogon("sa", "Laislabonitamac2065");

        CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, path);

        System.IO.FileInfo file = new System.IO.FileInfo(path);

        if (file.Exists)
        {
            Response.Clear();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);

            Response.AddHeader("Content-Length", file.Length.ToString());

            Response.ContentType = "application/octet-stream";

            Response.WriteFile(file.FullName);

            Response.End();

        }
        else
        {
            Response.Write("This file does not exist.");
        }        
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadArea();

        LoadDeliveryman();
        LoadMTown();
        LoadMarket();
        LoadArea();
        LoadTransporter();
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMarket();
    }
    private void LoadArea()
    {
        DrpRoute.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpMTown.SelectedValue.ToString()), null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
    }

    private void LoadMarket()
    {
        DrpMarket.Items.Clear();
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            DistributorRouteController gController = new DistributorRouteController();
            DataTable dt = gController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpRoute.SelectedValue.ToString()));
            DrpMarket.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDropDownList(DrpMarket, dt, 0, 8, false);
        }
        else
        {
            DrpMarket.Items.Clear();
        }
    }
    private void LoadTransporter()
    {
        DrpTransporter.Items.Clear();
        DistributorController DistCtl = new DistributorController();
        DataTable dtTransporter = DistCtl.SelectTranspoter(int.Parse(drpDistributor.SelectedValue), Convert.ToInt32(Session["UserID"]));
        DrpTransporter.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpTransporter, dtTransporter, "TRASNPOTER_ID", "NAME", false);
    }

    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
       // this.LoadMTown();
    }

    protected void DrpMTown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadArea();
        LoadMarket();
        LoadArea();
        LoadTransporter();
    }
    private void LoadMTown()
    {
        DrpMTown.Items.Clear();
        if (drpDistributor.Items.Count > 0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectAssignTown(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
            DrpMTown.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpMTown, dt, 0, 1, false);
        }
    }
}