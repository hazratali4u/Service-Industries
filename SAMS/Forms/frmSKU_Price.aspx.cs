using System;
using System.Data;
using System.Web.UI;
using SAMSCommon.Classes;
using SAMSBusinessLayer.Classes;

/// <summary>
/// From To Add, Edit SKU Price
/// </summary>
public partial class SKU_Price : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Populate_drpSKUCompany();
            Populate_drpSKUDivisions();
            Populate_drpSKUCategory();
            Populate_drpSKUBrand();
            Populate_ddlSKUBrandType();
            Populate_ddlMajor();
            Populate_ddlPRRating();
            LoadSKUS();
            LoadGrid();
            LoadDistributor();
            btnSave.Attributes.Add("onclick", "return ValidateForm()");
        }
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void Populate_drpSKUCompany()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUPrincipal, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskuPrincipal, m_dt, 0, 3, true);
    }
        
    /// <summary>
    /// Loads Divisions To Division Combo
    /// </summary>
    private void Populate_drpSKUDivisions()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskuDivision, m_dt, 0, 3, true);
    }
        
    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskuCategory, m_dt, 0, 3, true);
    }
        
    /// <summary>
    /// Loads Brands To Brand Combo
    /// </summary>
    private void Populate_drpSKUBrand()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        string strSKUCategoryID = ddskuCategory.SelectedItem.Value;
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddskuBrand, m_dt, 0, 3, true);
    }
        
    /// <summary>
    /// Loads SKUS To SKU Combo
    /// </summary>
    private void LoadSKUS()
    {
        SkuController mSKUController = new SkuController();
        int compid = Constants.IntNullValue;
        int divid = Constants.IntNullValue;
        int catid = Constants.IntNullValue;
        int barandid = Constants.IntNullValue;
        int varid = Constants.IntNullValue;
        DataTable m_dt = mSKUController.SelectSkuInfo(compid, divid, catid, barandid, varid);
        clsWebFormUtil.FillDropDownList(ddskuName, m_dt, 0, 18, true);
        Session.Add("dtSKU", m_dt);
    }

    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddskuName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();        
     //   ddskuName.Focus();
    }
    
    /// <summary>
    /// Loads Prices To Price Grid
    /// </summary>
    private void LoadGrid()
    {
        SKUPriceDetailController mSKUController = new SKUPriceDetailController();
        if (ddskuName.Items.Count > 0)
        {
            DataTable dtsku = mSKUController.SelectSKuCurrentPrice(Constants.IntNullValue,int.Parse(Session["CompanyId"].ToString()), Constants.IntNullValue,
                Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(ddskuName.SelectedValue.ToString()));
            Grid_pricedetails.DataSource = dtsku.DefaultView;
            Grid_pricedetails.DataBind();            
        }

        DataTable dtSKU = (DataTable)Session["dtSKU"];
        try
        {
            foreach (DataRow dr in dtSKU.Rows)
            {
                if (dr["SKU_ID"].ToString() == ddskuName.SelectedValue)
                {
                    ddskuPrincipal.SelectedValue = dr["Principal_Id"].ToString();
                    ddskuDivision.SelectedValue = dr["Division_Id"].ToString();
                    ddskuCategory.SelectedValue = dr["Category_Id"].ToString();
                    ddskuBrand.SelectedValue = dr["Brand_Id"].ToString();
                    ddlSKUBrandType.SelectedValue = dr["BRAND_TYPE_ID"].ToString();
                    ddlMajor.SelectedValue = dr["MAJOR_ID"].ToString();
                    ddlPRRating.SelectedValue = dr["PRRATING_ID"].ToString();
                    break;
                }
            }
        }
        catch (Exception eee)
        {
          string ss=  eee.StackTrace.ToString();
        }
    }
    
    /// <summary>
    /// Loads Locations To Location ListBox
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(ChbDistributorList, dt, 0, 2, true);
    }

    /// <summary>
    /// Save Or Updates an SKU Price
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SKUPriceDetailController mPriceController = new SKUPriceDetailController();
            DataControl dc = new DataControl();
            for(int i  = 0;i<ChbDistributorList.Items.Count;i++)   
            {
                if(ChbDistributorList.Items[i].Selected == true)
                {
                    mPriceController.InsertSKU_PRICES(int.Parse(ChbDistributorList.Items[i].Value), int.Parse(ddskuName.SelectedValue.ToString()), 1, decimal.Parse(dc.chkNull_0(txtDistributorPrice.Text)), decimal.Parse(dc.chkNull_0(txtTaxPrices.Text)), decimal.Parse(dc.chkNull_0(txtDistributorPrice.Text)), decimal.Parse(dc.chkNull_0(txtTradePrice.Text)), decimal.Parse(dc.chkNull_0(txtRetailPrice.Text)), DateTime.Parse(txtFromdate.Text),decimal.Parse(dc.chkNull_0(txtSADTax.Text)), decimal.Parse(dc.chkNull_0(txtSkuTax.Text)));
                }
            }
            LoadGrid();
            txtRetailPrice.Text = "";
            txtTradePrice.Text = "";
            txtDistributorPrice.Text = "";
            txtTaxPrices.Text = "";
            txtSADTax.Text = "";
            txtSkuTax.Text = "";
        }
    }

    private void Populate_ddlSKUBrandType()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUBrandType, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSKUBrandType, m_dt, 0, 3, true);
    }

    private void Populate_ddlMajor()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUMajor, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlMajor, m_dt, 0, 3, true);
    }

    private void Populate_ddlPRRating()
    {
        SkuHierarchyController mHer_Controller = new SkuHierarchyController();
        DataTable m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUPRRating, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlPRRating, m_dt, 0, 3, true);
    }
}