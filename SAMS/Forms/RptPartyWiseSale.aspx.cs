using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Web.UI.WebControls;

/// <summary>
/// Form For  Catagory Wise Customer Report
/// </summary>
public partial class RptPartyWiseSale : System.Web.UI.Page
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
            LoadCatagory();
            LoadSKUDetail();
            LoadLocation();
            LoadArea();
            LoadMarket();
            DateTime dt = (DateTime)this.Session["CurrentWorkDate"]; 
            this.txtFromDate.Text = dt.ToString("dd-MMM-yyyy");
            this.txtToDate.Text   = dt.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Categories To Category ListBox
    /// </summary>
    /// 
    private void LoadArea()
    {
        DrpRoute.Items.Clear();
        if (DrpLocation.Items.Count > 0)
        {
            DrpRoute.Items.Clear();
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            DrpRoute.Items.Add(new ListItem("All", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6);
        }
    }

    private void LoadMarket()
    {
        DrpMarket.Items.Clear();
        if (DrpLocation.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            DistributorRouteController gController = new DistributorRouteController();
            DataTable dt = gController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(DrpRoute.SelectedValue.ToString()));
            DrpMarket.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));

            clsWebFormUtil.FillDropDownList(DrpMarket, dt, 0, 8, false);
        }
        else
        {
            DrpMarket.Items.Clear();
        }
    }
    private void LoadSKUDetail()
    {
        DrpSkus.Items.Clear();
        if (drpCategory.Items.Count > 0)
        {
            DrpSkus.Items.Clear();
            SkuController mContoller = new SkuController();
            DataTable dt = mContoller.SelectSkuInfo(int.Parse(drpPrincipal.SelectedValue), Constants.IntNullValue, int.Parse(drpCategory.SelectedValue), Constants.IntNullValue, Constants.IntNullValue);
            DrpSkus.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpSkus, dt, 0, 18, false);
        }
    }
    protected void LoadCatagory()
    {
        drpCategory.Items.Clear();
        SkuHierarchyController Hierarchy = new SkuHierarchyController();
        DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
        DataView dv = new DataView(dtCatagory);
        dv.RowFilter = "Company_id = " + Convert.ToInt32(drpPrincipal.SelectedValue.ToString());
        dtCatagory = dv.ToTable();
        drpCategory.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
         clsWebFormUtil.FillDropDownList(this.drpCategory, dtCatagory , 4, 6,false);
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    protected void LoadPrincipal()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            //this.drpPrincipal.Items.Add(new ListItem("All", "0"));
            clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, "Company_Id", "Company_Name");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpLocation.DataSource = dt;
            this.DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
            this.DrpLocation.DataValueField = "DISTRIBUTOR_ID";
            this.DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    /// <summary>
    /// Loads Categories
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        LoadCatagory();
        LoadSKUDetail();
    }
        
    /// <summary>
    /// Shows  Catagory Wise Customer Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    /// <summary>
    /// Shows  Catagory Wise Customer Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

  

    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadArea();
        LoadMarket();
    }

    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMarket();
    }

    protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }


    private void ShowReport(int ReportType)
    {
        try
        {
            SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
            RptSaleController RptSalesCtl = new RptSaleController();
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

            CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrpReport = new SAMSBusinessLayer.Reports.CrpPartyWiseSale();

            DataTable dtopening = RptSalesCtl.PartyWiseSaleSkuWiseTotal(int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToInt32(this.drpPrincipal.SelectedValue.ToString()), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text + " 23:59:59"), Convert.ToInt32(Session["UserID"]), null, long.Parse(DrpRoute.SelectedValue), int.Parse(DrpMarket.SelectedValue), int.Parse(drpCategory.SelectedValue), int.Parse(DrpSkus.SelectedValue));
            int TotalShops = 0;
            int TotalQty = 0;
            if (dtopening != null && dtopening.Rows.Count > 0)
            {
                foreach (DataRow dr in dtopening.Rows)
                {
                    TotalShops++;
                    TotalQty += int.Parse(dr["QUANTITY_UNIT"].ToString());
                }
            }

            CrpReport.SetDataSource(dtopening);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Location", DrpLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("FromDate", txtFromDate.Text);
            CrpReport.SetParameterValue("ToDate", txtToDate.Text);
            CrpReport.SetParameterValue("Principal", drpPrincipal.SelectedItem.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            CrpReport.SetParameterValue("Route", DrpRoute.SelectedItem.Text);
            CrpReport.SetParameterValue("Matket", DrpMarket.SelectedItem.Text);
            CrpReport.SetParameterValue("Category", drpCategory.SelectedItem.Text);
            CrpReport.SetParameterValue("Skus", DrpSkus.SelectedItem.Text);
            CrpReport.SetParameterValue("TotalShops", TotalShops);
            CrpReport.SetParameterValue("TotalQty", TotalQty);
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