using System;
using System.Data;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmDashboard : System.Web.UI.Page
{
    GeoHierarchyController gController = new GeoHierarchyController();
    DistributorController DController = new DistributorController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).AddDays(-1).ToString("dd-MMM-yyyy");
            LoadMarqueeText();
            this.LoadRegion();
            this.LoadZone();
            this.LoadTerritory();
            this.LoadAssingned();
            this.LoadPrincipal();
            this.LoadDashboardData();
            this.LoadCurrentMonthPrimarySale();
            this.LoadCurrentMonthRecovery();
            this.LoadCurrentMonthExpense();
            this.LoadCurrentMonthSecondarySale();
            this.LoadTopDistributorsSale();
            this.LoadTopDSRSale();
            this.LoadMonthWiseSale();
            this.LoadTopSKUSales();
            this.LoadMarketCredit();
            this.LoadSkusCounter();
        }
    }

    #region Load

    protected void LoadRegion()
    {
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, false, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.ddlRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlRegion, dt, "GEO_ID", "GEO_NAME");
    }

    private void LoadAssingned()
    {
        ddlLocation.Items.Clear();
        UserController mUserController = new UserController();
        DataTable dt = DController.GetTownWistUnAssignDistributor(Constants.IntNullValue, Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlZone.SelectedValue), Convert.ToInt32(ddlTerritory.SelectedValue), Convert.ToInt32(Session["UserID"]), 0);
        this.ddlLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlLocation, dt, 0, 1);

    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(Session["CurrentWorkDate"].ToString()));
        ddlPrincipal.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlPrincipal, m_dt, 0, 1);
    }

    protected void LoadZone()
    {
        if (ddlRegion.Items.Count > 0)
        {
            ddlZone.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            ddlZone.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlRegion.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlZone, dt, "GEO_ID", "GEO_NAME");
        }
    }


    protected void LoadTerritory()
    {
        if (ddlZone.Items.Count > 0)
        {
            ddlTerritory.Items.Clear();
            GeoHierarchyController gController = new GeoHierarchyController();
            ddlTerritory.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlZone.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(ddlTerritory, dt, "GEO_ID", "GEO_NAME");
        }
    }


    #endregion

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadZone();
        this.LoadTerritory();
        this.LoadAssingned();
        this.LoadDashboardData();
        this.LoadTopDistributorsSale();
        this.LoadTopDSRSale();
        this.LoadTopSKUSales();
        this.LoadMonthWiseSale();
        this.LoadCurrentMonthPrimarySale();
        this.LoadCurrentMonthRecovery();
        this.LoadCurrentMonthExpense();
        this.LoadCurrentMonthSecondarySale();
        this.LoadMarketCredit();
        this.LoadSkusCounter();
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritory();
        this.LoadAssingned();
        this.LoadDashboardData();
        this.LoadTopDistributorsSale();
        this.LoadTopDSRSale();
        this.LoadTopSKUSales();
        this.LoadMonthWiseSale();
        this.LoadCurrentMonthPrimarySale();
        this.LoadCurrentMonthRecovery();
        this.LoadCurrentMonthExpense();
        this.LoadCurrentMonthSecondarySale();
        this.LoadMarketCredit();
        this.LoadSkusCounter();
    }

    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAssingned();
        this.LoadDashboardData();
        this.LoadTopDistributorsSale();
        this.LoadTopDSRSale();
        this.LoadTopSKUSales();
        this.LoadMonthWiseSale();
        this.LoadCurrentMonthPrimarySale();
        this.LoadCurrentMonthRecovery();
        this.LoadCurrentMonthExpense();
        this.LoadCurrentMonthSecondarySale();
        this.LoadMarketCredit();
        this.LoadSkusCounter();
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDashboardData();
        this.LoadTopDistributorsSale();
        this.LoadTopDSRSale();
        this.LoadTopSKUSales();
        this.LoadMonthWiseSale();
        this.LoadCurrentMonthPrimarySale();
        this.LoadCurrentMonthRecovery();
        this.LoadCurrentMonthExpense();
        this.LoadCurrentMonthSecondarySale();
        this.LoadMarketCredit();
        this.LoadSkusCounter();
    }

  


    protected void timerDashboard_Tick(object sender, EventArgs e)
    {
        this.LoadCurrentMonthPrimarySale();
        this.LoadCurrentMonthRecovery();
        this.LoadCurrentMonthExpense();
        this.LoadCurrentMonthSecondarySale();
        this.LoadDashboardData();
        this.LoadTopDistributorsSale();
        this.LoadTopDSRSale();
        this.LoadMonthWiseSale();
        this.LoadTopSKUSales();
        this.LoadMonthWiseSale();
        this.LoadMarketCredit();
        this.LoadSkusCounter();
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        this.LoadCurrentMonthPrimarySale();
        this.LoadCurrentMonthRecovery();
        this.LoadCurrentMonthExpense();
        this.LoadCurrentMonthSecondarySale();
        this.LoadDashboardData();
        this.LoadTopDistributorsSale();
        this.LoadTopDSRSale();
        this.LoadTopSKUSales();
        this.LoadMonthWiseSale();
        this.LoadMarketCredit();
        this.LoadSkusCounter();
    }

    private void LoadDashboardData()
    {
        RptSaleController rptCtl = new RptSaleController();
        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();
      

        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }
        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataSet dsDashboardData = rptCtl.GetDashboardSecondarySales(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text + " 23:59:59"), sbDistIDs.ToString(), sbDivisionIDs.ToString());

        DataTable dtPrimarySales = dsDashboardData.Tables[0];
        DataTable dtSecondarySales = dsDashboardData.Tables[1];
        DataTable dtInventory = dsDashboardData.Tables[2];
        DataTable dtBooking = dsDashboardData.Tables[3];
        
        lblGrossSale2.Text = "00";        
        lblPrimarySales2.Text = "00";
        lblInventory2.Text = "00";
        lblBooking2.Text = "00";

        if (dtPrimarySales.Rows.Count > 0)
        {
            lblPrimarySales2.Text = String.Format("{0:0,0}", dtPrimarySales.Rows[0]["PrimarySales"]);
        }

        if (dtSecondarySales.Rows.Count > 0)
        {
            lblGrossSale2.Text = String.Format("{0:0,0}", dtSecondarySales.Rows[0]["GrossSale"]);
        }

        if (dtInventory.Rows.Count > 0)
        {
            decimal Inventory = 0;
            Inventory = Convert.ToDecimal(dtInventory.Rows[0]["OpeningStock"])
                + Convert.ToDecimal(dtInventory.Rows[0]["Purchased"])
                + Convert.ToDecimal(dtInventory.Rows[0]["SaleReturn"])
            + Convert.ToDecimal(dtInventory.Rows[0]["ExcessStock"])
            + Convert.ToDecimal(dtInventory.Rows[0]["TransferIn"])
            + Convert.ToDecimal(dtInventory.Rows[0]["FreeStockIn"])
            - Convert.ToDecimal(dtInventory.Rows[0]["Sales"])
            - Convert.ToDecimal(dtInventory.Rows[0]["PurchaseReturn"])
            - Convert.ToDecimal(dtInventory.Rows[0]["DamageStock"])
            - Convert.ToDecimal(dtInventory.Rows[0]["TransferOut"])
            - Convert.ToDecimal(dtInventory.Rows[0]["FreeStockOut"])
            - Convert.ToDecimal(dtInventory.Rows[0]["ShortStock"]);
            lblInventory2.Text = String.Format("{0:0,00}", Inventory);
        }

        if (dtBooking.Rows.Count > 0)
        {
            lblBooking2.Text = String.Format("{0:0,0}", dtBooking.Rows[0]["OrderedQuantity"]);
        }
    }

    private void LoadTopDistributorsSale()
    {
        RptSaleController rptCtl = new RptSaleController();
        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();
        

        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        DateTime dtFrom = new DateTime();
        dtFrom = new DateTime(dtTo.Year, dtTo.Month, 1);

        
        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtTopDistributorsSale = rptCtl.GetTopDistributorSales(dtFrom, Convert.ToDateTime(dtTo.ToShortDateString() + " 23:59:59"), sbDistIDs.ToString(), sbDivisionIDs.ToString(), 5);

        rDistributors.DataSource = dtTopDistributorsSale;
        rDistributors.DataBind();
        decimal current = 0, currentton = 0, previous = 0, previouston = 0, valueperdsr = 0;
        foreach (DataRow dr in dtTopDistributorsSale.Rows)
        {
            current += Convert.ToDecimal(dr["CurrentSaelesValue"]);
            currentton += Convert.ToDecimal(dr["CurrentSaelesTon"]);
            previous += Convert.ToDecimal(dr["PreviousSaelesValue"]);
            previouston += Convert.ToDecimal(dr["PreviousSaelesTon"]);
            valueperdsr += Convert.ToDecimal(dr["ValuePerDSR"]);
        }

        lblTotalCurrentValue.Text = String.Format("{0:0,00}", current);
        lblTotalCurrentTon.Text = String.Format("{0:0,00}", currentton);
        lblTotalPreviousValue.Text = String.Format("{0:0,00}", previous);
        lblTotalPreviousTon.Text = String.Format("{0:0,00}", previouston);
        lblValuePerDSR.Text = String.Format("{0:0,00}", valueperdsr);
    }

    private void LoadTopDSRSale()
    {
        RptSaleController rptCtl = new RptSaleController();
        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();
        
        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtTopDSRSale = rptCtl.GetTopDSRSales(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text + " 23:59:59"), sbDistIDs.ToString(), sbDivisionIDs.ToString(), 5);

        rDSR.DataSource = dtTopDSRSale;
        rDSR.DataBind();

        decimal value = 0, ton = 0;
        int bill = 0;
        foreach (DataRow dr in dtTopDSRSale.Rows)
        {
            value += Convert.ToDecimal(dr["SalesValue"]);
            ton += Convert.ToDecimal(dr["SalesTon"]);
            bill += Convert.ToInt32(dr["NoOfBill"]);
        }

        lblTotalDSRValue.Text = String.Format("{0:0,00}", value);
        lblTotalDSRTon.Text = String.Format("{0:0}", ton);
        lblTotalBill.Text = String.Format("{0:0}", bill);
    }

    private void LoadMonthWiseSale()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        DateTime dtFrom = dtTo.AddMonths(-4);
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();
        

        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetMonthWiseSales(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        rSales.DataSource = dtMonthWiseSale;
        rSales.DataBind();
        decimal value = 0;
        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            value += Convert.ToDecimal(dr["GrossSale"]);
        }

        lblTotalPakValue.Text = String.Format("{0:0,00}", value);        
    }

    private void LoadCurrentMonthPrimarySale()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        // DateTime dtFrom = DateTime.Now.AddMonths(-4);
        DateTime dtFrom = dtTo;
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();


        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetMonthPrimarySales(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        rPrimarySales.DataSource = dtMonthWiseSale;
        rPrimarySales.DataBind();
        decimal value = 0;
        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            value += Convert.ToDecimal(dr["GrossSale"]);
        }

        lblTotalPrimarySales.Text = String.Format("{0:0,00}", value);
    }

    private void LoadCurrentMonthRecovery()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        // DateTime dtFrom = DateTime.Now.AddMonths(-4);
        DateTime dtFrom = dtTo;
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();


        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetCurrentMonthRecovery(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        rRecovery.DataSource = dtMonthWiseSale;
        rRecovery.DataBind();
        decimal value = 0;
        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            value += Convert.ToDecimal(dr["value"]);
        }

        lbltotalRecovery.Text = String.Format("{0:0,00}", value);
    }
    private void LoadCurrentMonthExpense()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        // DateTime dtFrom = DateTime.Now.AddMonths(-4);
        DateTime dtFrom = dtTo;
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();


        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetCurrentMonthExpense(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        rExpense.DataSource = dtMonthWiseSale;
        rExpense.DataBind();
        decimal value = 0;
        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            value += Convert.ToDecimal(dr["value"]);
        }

        lblTotalExpense.Text = String.Format("{0:0,00}", value);
    }

    private void LoadCurrentMonthSecondarySale()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        // DateTime dtFrom = DateTime.Now.AddMonths(-4);
        DateTime dtFrom = dtTo;
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();


        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetCurrentMonthSecondarySales(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        rSecondarySales.DataSource = dtMonthWiseSale;
        rSecondarySales.DataBind();
        decimal value = 0;
        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            value += Convert.ToDecimal(dr["GrossSale"]);
        }

        lblTotalSecondarysales.Text = String.Format("{0:0,00}", value);
    }

    private void LoadMarketCredit()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        // DateTime dtFrom = DateTime.Now.AddMonths(-4);
        DateTime dtFrom = dtTo;
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();


        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetMarketCredit(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        rMarketCredit.DataSource = dtMonthWiseSale;
        rMarketCredit.DataBind();
        decimal value = 0;
        
        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            value += Convert.ToDecimal(dr["value"]);
        }

        lblTotalMarketCredit.Text = String.Format("{0:0,00}", value);
    }

    private void LoadSkusCounter()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        // DateTime dtFrom = DateTime.Now.AddMonths(-4);
        DateTime dtFrom = dtTo;
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();


        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetSkusCouter(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        rSkusCounter.DataSource = dtMonthWiseSale;
        rSkusCounter.DataBind();
        decimal value = 0;

        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            value += Convert.ToDecimal(dr["value"]);
        }

        lblTotalSkuscounter.Text = String.Format("{0:0,00}", value);
    }

    private void LoadMarqueeText()
    {
        RptSaleController rptCtl = new RptSaleController();
        DateTime dtTo = Convert.ToDateTime(txtDate.Text);

        // DateTime dtFrom = DateTime.Now.AddMonths(-4);
        DateTime dtFrom = dtTo;
        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();


        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }

        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtMonthWiseSale = rptCtl.GetMarqueeText(dtFrom, dtTo, sbDistIDs.ToString(), sbDivisionIDs.ToString());

        System.Text.StringBuilder marquee = new System.Text.StringBuilder();

        foreach (DataRow dr in dtMonthWiseSale.Rows)
        {
            marquee.Append(dr["Text"]);
            marquee.Append(" , ");
        }
        lblMarqueeText.Text = marquee.ToString();


    }


    private void LoadTopSKUSales()
    {
        RptSaleController rptCtl = new RptSaleController();
        System.Text.StringBuilder sbDistIDs = new System.Text.StringBuilder();
        
        if (ddlLocation.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlLocation.Items)
            {
                sbDistIDs.Append(li.Value);
                sbDistIDs.Append(",");
            }
        }
        else
        {
            sbDistIDs.Append(ddlLocation.SelectedValue);
        }


        System.Text.StringBuilder sbDivisionIDs = new System.Text.StringBuilder();
        if (ddlPrincipal.SelectedValue == Constants.IntNullValue.ToString())
        {
            foreach (ListItem li in ddlPrincipal.Items)
            {
                sbDivisionIDs.Append(li.Value);
                sbDivisionIDs.Append(",");
            }
        }
        else
        {
            sbDivisionIDs.Append(ddlPrincipal.SelectedValue);
        }

        DataTable dtTopSKU = rptCtl.GetTopSKUSales(Convert.ToDateTime(txtDate.Text), Convert.ToDateTime(txtDate.Text + " 23:59:59"), sbDistIDs.ToString(), sbDivisionIDs.ToString(), 5);

        rSKU.DataSource = dtTopSKU;
        rSKU.DataBind();

        rSKU1.DataSource = dtTopSKU;
        rSKU1.DataBind();

        rSKU2.DataSource = dtTopSKU;
        rSKU2.DataBind();

        rSKU3.DataSource = dtTopSKU;
        rSKU3.DataBind();

        decimal qty = 0, value = 0, ton = 0;
        foreach (DataRow dr in dtTopSKU.Rows)
        {
            qty += Convert.ToDecimal(dr["SalesQty"]);
            value += Convert.ToDecimal(dr["SalesValue"]);
            ton += Convert.ToDecimal(dr["SalesTon"]);
        }

        lblTotalSKUQty.Text = String.Format("{0:0,00}", qty);        
        lblTotalSKUValue.Text = String.Format("{0:0,00}", value);
        lblTotalSKUTon.Text = String.Format("{0:0,00}", ton);        
    }

    protected void ddlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCurrentMonthSecondarySale();
        this.LoadCurrentMonthPrimarySale();
        this.LoadCurrentMonthRecovery();
        this.LoadCurrentMonthExpense();
        this.LoadDashboardData();
        this.LoadTopDistributorsSale();
        this.LoadTopDSRSale();
        this.LoadTopSKUSales();
        this.LoadMonthWiseSale();
        this.LoadMarketCredit();
        this.LoadSkusCounter();

    }
}