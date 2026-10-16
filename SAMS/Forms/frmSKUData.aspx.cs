using System;
using System.Data;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmSKUData : System.Web.UI.Page
{

    SkuController mController = new SkuController();
    DataControl dc = new DataControl(); 
    SkuHierarchyController mHer_Controller = new SkuHierarchyController();
    private  DataTable m_dt,m_SKUDt;
    private static int m_sku_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Populate_drpSKUCompany();
            Populate_drpSKUDivisions();
            Populate_drpSKUCategory();
            Populate_drpSKUBrand();
            Populate_ddlSKUBrandType();
            Populate_ddlMajor();
            Populate_ddlPRRating();
            LoadData();
            LoadGrid();
        }
    }

    /// <summary>
    /// Loads Principal To Principal Combo
    /// </summary>
    private void Populate_drpSKUCompany()
    {
        m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUPrincipal, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddskuPrincipal, m_dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Divisions To Division Combo, Categories To Category Combo, Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskuPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUDivisions();
        Populate_drpSKUCategory();
        Populate_drpSKUBrand();
        LoadData();
        LoadGrid();
        ddskuPrincipal.Focus();
    }

    /// <summary>
    /// Loads Divisions To Division Combo
    /// </summary>
    private void Populate_drpSKUDivisions()
    {
        if (ddskuPrincipal.Items.Count > 0)
        {
            if (ddskuPrincipal.Items.Count > 0)
            {
                m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, int.Parse(ddskuPrincipal.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.ddskudivision, m_dt, 0, 3, true);
            }
        }
        else
        {
            ddskudivision.Items.Clear();   
        }
    }

    /// <summary>
    /// Loads Categories To Category Combo, Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskudivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUCategory();
        Populate_drpSKUBrand();
        this.LoadData();
        this.LoadGrid();
        ddskudivision.Focus();
    }

    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        if (ddskudivision.Items.Count > 0)
        {
            if (ddskudivision.Items.Count   > 0)
            {
                m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(ddskudivision.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.ddskucategory, m_dt, 0, 3, true);
            }
        }
        else
        {
            ddskucategory.Items.Clear();   
        }
    }

    /// <summary>
    /// Loads Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate_drpSKUBrand();
        this.LoadData();
        this.LoadGrid();
        ddskucategory.Focus();
    }

    /// <summary>
    /// Loads Brands To Brand Combo
    /// </summary>
    private void Populate_drpSKUBrand()
    {
        if (ddskucategory.Items.Count > 0)
        {
            string strSKUCategoryID = this.ddskucategory.SelectedItem.Value;

            if (ddskucategory.Items.Count   > 0)
            {
                m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, int.Parse(ddskucategory.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
                clsWebFormUtil.FillDropDownList(this.ddskuBrand, m_dt, 0, 3, true);
            }
        }
        else
        {
            ddskuBrand.Items.Clear();   
        }
    }

    /// <summary>
    /// Loads SKU Data To Session
    /// </summary>
    private void LoadData()
    {
        SkuController mSKUController = new SkuController();
        m_SKUDt = mSKUController.SelectSkuInfo(int.Parse(ddskuPrincipal.SelectedValue.ToString()), int.Parse(ddskudivision.SelectedValue.ToString()), int.Parse(ddskucategory.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.Session.Add("m_SKUDt", m_SKUDt);
    }

    /// <summary>
    /// Loads Data From Session To Grid
    /// </summary>
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)this.Session["m_SKUDt"];

        switch (ddSearchType.SelectedIndex)
        {
            
            case 1:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 3:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 4:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                m_SKUDt.DefaultView.RowFilter = "SKU_CODE" + " like '%" + "" + "%'";
                break; 
        }
        grdSKUData.EditIndex = -1;
        grdSKUData.DataSource = m_SKUDt.DefaultView;   
        grdSKUData.DataBind();
       }

    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
   {
        try
        {
            ddskuPrincipal.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[0].Text;
            this.Populate_drpSKUDivisions();
            ddskudivision.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[1].Text;
            this.Populate_drpSKUCategory();
            ddskucategory.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[2].Text;
            this.Populate_drpSKUBrand();
            ddskuBrand.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[3].Text;
            m_sku_id = int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[4].Text);
            txtskucode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[9].Text;
            txtskuname.Text = grdSKUData.Rows[e.NewEditIndex].Cells[10].Text;
            txtpacksize.Text = grdSKUData.Rows[e.NewEditIndex].Cells[11].Text;
            txtunitincase.Text = grdSKUData.Rows[e.NewEditIndex].Cells[12].Text;
            ddlSKUBrandType.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[14].Text;
           
            ddlPRRating.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[16].Text;
            txtPrincipalCode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[17].Text;
            ddskuPrincipal.Enabled = false;
            btnSave.Text = "Update";
            for (int i = 0; i < grdSKUData.Rows.Count; i++)
            {
                grdSKUData.Rows[i].Cells[18].Enabled = false;
            }
            ddlMajor.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[15].Text;
        }
        catch (Exception ee)
        {
            string sss = ee.StackTrace;
        }
    }

    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {

        bool IsExemted = false;
        if(grdSKUData.Rows[e.RowIndex].Cells[13].Text == "E")
        {
            IsExemted = true;
        }
       string result = mController.UpdateSKUS(IsExemted, false, Constants.CharNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
       Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.ShortNullValue, int.Parse(grdSKUData.Rows[e.RowIndex].Cells[4].Text), null, null,null, null, null, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()),Constants.IntNullValue,Constants.IntNullValue,Constants.IntNullValue);
       this.LoadData();
       this.LoadGrid();
   }

    protected void btnSave_Click(object sender, EventArgs e)
    {
       bool IsExemted = true;
      // char Gst_On = 'E';
       if (txtpacksize.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter SKU Packsize";
           return; 
       }
       if (txtPrincipalCode.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter Principal Code";
           return;
       }
       if (txtskucode.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter SKU Code";
           return;
       }
       if (txtskuname.Text.Length <= 0)
       {
           lblErrorMsg.Text = "Must Enter SKU Name";
           return;
       }
       if(btnSave.Text == "Save") 
       {
           mController.InsertSKUS(IsExemted, true, char.Parse(DrpSKUTaxType.SelectedValue.ToString()),int.Parse(ddskuPrincipal.SelectedValue.ToString()),int.Parse(ddskudivision.SelectedValue.ToString()), int.Parse(ddskucategory.SelectedValue.ToString()),
           int.Parse(ddskuBrand.SelectedValue.ToString()), Constants.IntNullValue, 0,0,short.Parse(dc.chkNull_0(txtunitincase.Text)),
           txtskucode.Text.ToUpper(),txtPrincipalCode.Text.ToUpper(), txtskuname.Text, null, txtpacksize.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()),Convert.ToInt32(ddlSKUBrandType.SelectedValue),Convert.ToInt32(ddlMajor.SelectedValue),Convert.ToInt32(ddlPRRating.SelectedValue));
           this.CLearAll();  
       }
       else if (btnSave.Text == "Update")
       {
           mController.UpdateSKUS(IsExemted, true, char.Parse(DrpSKUTaxType.SelectedValue.ToString()), int.Parse(ddskuPrincipal.SelectedValue.ToString()), int.Parse(ddskudivision.SelectedValue.ToString()),
           int.Parse(ddskucategory.SelectedValue.ToString()), int.Parse(ddskuBrand.SelectedValue.ToString()),Constants.IntNullValue, 0
           , 0, short.Parse(dc.chkNull_0(txtunitincase.Text)), m_sku_id, txtskucode.Text.ToUpper(),txtPrincipalCode.Text.ToUpper(), txtskuname.Text, null, txtpacksize.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(ddlSKUBrandType.SelectedValue), Convert.ToInt32(ddlMajor.SelectedValue), Convert.ToInt32(ddlPRRating.SelectedValue));
           this.CLearAll();
       }
       this.LoadData(); 
        
    }
  
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadGrid();
    }

    private void CLearAll()
    {
        txtpacksize.Text = "";
        txtskucode.Text = "";
        txtPrincipalCode.Text = string.Empty;
        txtunitincase.Text = "";
        txtskuname.Text = "";
        btnSave.Text = "Save";
        ddskuPrincipal.Enabled = true;
        this.LoadData();
        this.LoadGrid();
    }

    private void Populate_ddlSKUBrandType()
    {
        m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUBrandType, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlSKUBrandType, m_dt, 0, 3, true);
    }

    private void Populate_ddlMajor()
    {
        m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUMajor, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlMajor, m_dt, 0, 3, true);
    }

    private void Populate_ddlPRRating()
    {
        m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUPRRating, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlPRRating, m_dt, 0, 3, true);
    }
}