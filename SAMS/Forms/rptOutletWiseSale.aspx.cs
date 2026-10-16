using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;

/// <summary>
/// Form For  Catagory Wise Customer Report
/// </summary>
public partial class Forms_rptOutletWiseSale : System.Web.UI.Page
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
            LoadLocation();
            this.LoadRoute();
            this.LoadMarket();
            DateTime dt = (DateTime)this.Session["CurrentWorkDate"]; 
            this.txtFromDate.Text = dt.ToString("dd-MMM-yyyy");
            this.txtToDate.Text   = dt.ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Loads Categories To Category ListBox
    /// </summary>
    protected void LoadCatagory()
    {
        SkuHierarchyController Hierarchy = new SkuHierarchyController();
        DataTable dtCatagory = Hierarchy.SelectSkuHierarchyView(5, int.Parse(this.Session["CompanyId"].ToString()));
        DataView dv = new DataView(dtCatagory);
        dv.RowFilter = "Company_id = " + Convert.ToInt32(drpPrincipal.SelectedValue.ToString());
        dtCatagory = dv.ToTable();
        clsWebFormUtil.FillListBox(this.ListCatagory, dtCatagory , 4, 6);
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

    private void LoadRoute()
    {
        ddlRoute.Items.Clear();
        if (DrpLocation.Items.Count > 0)
        {
            DistributorAreaController mController = new DistributorAreaController();
            DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, null, null);
            ddlRoute.Items.Add(new ListItem("All", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlRoute, dt, 0, 6);
        }
    }

    private void LoadMarket()
    {
        ddlMarket.Items.Clear();
        if (DrpLocation.Items.Count > 0 && ddlRoute.Items.Count > 0)
        {
            DistributorRouteController gController = new DistributorRouteController();
            DataTable dt = gController.SelectDistributorRoute(Constants.LongNullValue, int.Parse(DrpLocation.SelectedValue.ToString()), Constants.IntNullValue, long.Parse(ddlRoute.SelectedValue.ToString()));
            ddlMarket.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(ddlMarket, dt, 0, 8);
        }
    }

    /// <summary>
    /// Loads Categories
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ListCatagory.Items.Clear();
        LoadCatagory();
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

    private void ShowReport(int ReportType)
    {
        try
        {
            if (txtTopCustomer.Text.Length > 0)
            {
                SAMSBusinessLayer.Classes.DocumentPrintController DPrint = new SAMSBusinessLayer.Classes.DocumentPrintController();
                RptCustomerController RptCustomerCtl = new RptCustomerController();
                DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

                DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
                DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
                string FromDate = parsed_date_fromdate.ToShortDateString();
                string ToDate = parsed_date_todate.ToShortDateString();
                string Catagories_IDs = null;
                ReportDocument CrpReport = new ReportDocument();

                if (ddlSortBy.SelectedValue == "1")
                {
                    if (ddlOrderBy.SelectedValue == "1")
                    {
                        CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleQuantityAsc();
                    }
                    else
                    {
                        CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleQuantityDesc();
                    }
                }
                else
                {
                    if (ddlOrderBy.SelectedValue == "1")
                    {
                        CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleValueAsc();
                    }
                    else
                    {
                        CrpReport = new SAMSBusinessLayer.Reports.CrpOutletwiseSaleValueDesc();
                    }
                }

                DataSet ds = null;
                for (int i = 0; i < this.ListCatagory.Items.Count; i++)
                {
                    if (this.ListCatagory.Items[i].Selected == true)
                    {
                        Catagories_IDs += this.ListCatagory.Items[i].Value.ToString() + ",";
                    }
                }

                ds = RptCustomerCtl.OutletWiseSale(int.Parse(drpPrincipal.SelectedValue.ToString()), Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 00:00:00"), Catagories_IDs, Convert.ToInt32(txtTopCustomer.Text), Convert.ToInt64(ddlRoute.SelectedValue), Convert.ToInt32(ddlMarket.SelectedValue), int.Parse(DrpOrderType.SelectedValue));

                CrpReport.SetDataSource(ds);
                CrpReport.Refresh();

                CrpReport.SetParameterValue("Principal", this.drpPrincipal.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Branch", this.DrpLocation.SelectedItem.Text.ToString());
                CrpReport.SetParameterValue("Route", this.ddlRoute.SelectedItem.Text);
                CrpReport.SetParameterValue("Market", this.ddlMarket.SelectedItem.Text);
                CrpReport.SetParameterValue("type", this.DrpOrderType.SelectedItem.Text);
                CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
                CrpReport.SetParameterValue("FromDate", this.txtFromDate.Text);
                CrpReport.SetParameterValue("ToDate", this.txtToDate.Text);

                this.Session.Add("CrpReport", CrpReport);
                this.Session.Add("ReportType", ReportType);
                string url = "'Default.aspx'";
                string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenWindow", script);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enter Top Customer Value');", true);
                ScriptManager.GetCurrent(Page).SetFocus(txtTopCustomer);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadMarket();
    }

    protected void DrpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadRoute();
        this.LoadMarket();
    }
}