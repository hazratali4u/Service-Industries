using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU Hierarchy
/// </summary>
public partial class frmSKUHierarchyData : System.Web.UI.Page
{
    SkuHierarchyController mController = new SkuHierarchyController();
    static int PrincipalId;
    static int DivisionId;
    static int CategoryId;
    static int BrandId;
    static int BrandTypeId;
    static int MajorId;
    static int PRRatingId;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadPrincipal();
            this.LoadDivision();
            this.LoadCategoryDivision();
            this.LoadBrandDivision();
            this.LoadCategroy();
            this.LoadBrandCategory();
            this.LoadBrand();
            this.LoadBrandType();
            this.LoadMajor();
            this.LoadPRRating();
            TabContainer1.ActiveTabIndex = 0;
        }
    }

    #region Principal Tab

    /// <summary>
    /// Loads Principals To Principal Grid On Principal Tab And To All Principal Combos On Form
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable dt = mController.SelectPrincipal(Constants.SKUPrincipal, int.Parse(this.Session["CompanyId"].ToString()));
        GrdPrincipal.EditIndex = -1;
        GrdPrincipal.DataSource = dt;
        GrdPrincipal.Columns[0].Visible = true;
        GrdPrincipal.Columns[4].Visible = true;
        GrdPrincipal.Columns[5].Visible = true;
        GrdPrincipal.Columns[6].Visible = true;
        GrdPrincipal.DataBind();
        GrdPrincipal.Columns[0].Visible = false;
        GrdPrincipal.Columns[4].Visible = false;
        GrdPrincipal.Columns[5].Visible = false;
        GrdPrincipal.Columns[6].Visible = false;
        clsWebFormUtil.FillDropDownList(this.dddivisonPrincipal, dt, 0, 3, true);
        clsWebFormUtil.FillDropDownList(this.DrpCategoryPrincipal, dt, 0, 3, true);
        clsWebFormUtil.FillDropDownList(this.DrpBrandPrincipal, dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Divisions To Division Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void dddivisonPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDivision();
    }

    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCategoryPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCategoryDivision();
        this.LoadCategroy();
    }

    /// <summary>
    /// Loads Brands To Brand Grid On Brand Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpBrandPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadBrandDivision();
        this.LoadBrandCategory();
        this.LoadBrand();
    }

    /// <summary>
    /// Sets Principal Data For Edit. This Function Runs When An Existing Principal Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PrincipalId = int.Parse(GrdPrincipal.Rows[e.NewEditIndex].Cells[0].Text);
        txtPrincipalCode.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[1].Text;
        txtPrincipalName.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[2].Text;
        if (GrdPrincipal.Rows[e.NewEditIndex].Cells[3].Text == "True")
        {
            ChIsMunalDiscount.Checked = true;
        }
        else
        {
            ChIsMunalDiscount.Checked = false;
        }
        txtAddress.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtNTN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[5].Text;
        txtSTRN.Text = GrdPrincipal.Rows[e.NewEditIndex].Cells[6].Text;
        txtPrincipalName.Enabled = true;
        txtAddress.Enabled = true;
        txtNTN.Enabled = true;
        txtSTRN.Enabled = true;
        btnSavePrincipal.Text = "Update";
        for (int i = 0; i < GrdPrincipal.Rows.Count; i++)
        {
            GrdPrincipal.Rows[i].Cells[7].Enabled = false;
        }
    }

    /// <summary>
    /// Deletes A Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPrincipal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PrincipalId = int.Parse(GrdPrincipal.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUDivision, PrincipalId);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsg.Text = "Wrong Command: first delete associated division";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUPrincipal, PrincipalId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadPrincipal();
            lblErrorMsg.Text = "";
        }
    }

    /// <summary>
    /// Sets PageIndex Of Principal Grid On Principal Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdPrincipal.PageIndex = e.NewPageIndex;
        this.LoadPrincipal();

    }

    /// <summary>
    /// Save Or Updates a Principal
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSavePrincipal_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        if (btnSavePrincipal.Text == "New")
        {
            txtPrincipalCode.Text = this.GetAutoCode("SC", 0);
            txtPrincipalName.Enabled = true;
            txtAddress.Enabled = true;
            txtNTN.Enabled = true;
            txtSTRN.Enabled = true;
            txtPrincipalName.Focus();
            btnSavePrincipal.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtPrincipalName);
        }
        else if (btnSavePrincipal.Text == "Save")
        {
            if (txtPrincipalName.Text.Length == 0)
            {
                lblErrorMsg.Text = "Must Enter Principal Name";
                return;
            }
            mController.InsertPrincipal(Constants.SKUPrincipal, Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), ChIsMunalDiscount.Checked, txtAddress.Text, txtNTN.Text, txtSTRN.Text);
            this.SetAutoCode("SC", long.Parse(txtPrincipalCode.Text.Substring(2)));
            btnSavePrincipal.Text = "New";
            txtPrincipalCode.Text = "";
            txtPrincipalName.Text = "";
            txtAddress.Text = "";
            txtNTN.Text = "";
            txtSTRN.Text = "";
            txtPrincipalName.Enabled = false;
            txtAddress.Enabled = false;
            txtNTN.Enabled = false;
            txtSTRN.Enabled = false;
            this.LoadPrincipal();

        }
        else if (btnSavePrincipal.Text == "Update")
        {
            if (txtPrincipalName.Text.Length == 0)
            {
                lblErrorMsg.Text = "Must Enter Principal Name";
                return;
            }
            mController.UpdatePrincipal(Constants.SKUPrincipal, PrincipalId, Constants.IntNullValue, txtPrincipalCode.Text, txtPrincipalName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()), ChIsMunalDiscount.Checked, txtAddress.Text, txtNTN.Text, txtSTRN.Text);
            btnSavePrincipal.Text = "New";
            txtPrincipalCode.Text = "";
            txtPrincipalName.Text = "";
            txtAddress.Text = "";
            txtNTN.Text = "";
            txtSTRN.Text = "";
            txtPrincipalName.Enabled = false;
            txtAddress.Enabled = false;
            txtNTN.Enabled = false;
            txtSTRN.Enabled = false;
            this.LoadPrincipal();
        }

    }

    #endregion

    #region Division Tab

    /// <summary>
    /// Loads Divisions To Division Grid On Division Tab
    /// </summary>
    private void LoadDivision()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, int.Parse(dddivisonPrincipal.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        GrdDivision.EditIndex = -1;
        GrdDivision.DataSource = dt;
        GrdDivision.Columns[0].Visible = true;
        GrdDivision.DataBind();
        GrdDivision.Columns[0].Visible = false;

    }

    /// <summary>
    /// Sets Division Data For Edit. This Function Runs When An Existing Division Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdDivision_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DivisionId = int.Parse(GrdDivision.Rows[e.NewEditIndex].Cells[0].Text);
        txtDivisionCode.Text = GrdDivision.Rows[e.NewEditIndex].Cells[1].Text;
        txtDivisionName.Text = GrdDivision.Rows[e.NewEditIndex].Cells[2].Text;
        txtDivisionName.Enabled = true;
        btnSaveDivison.Text = "Update";
        for (int i = 0; i < GrdDivision.Rows.Count; i++)
        {
            GrdDivision.Rows[i].Cells[3].Enabled = false;
        }
    }

    /// <summary>
    /// Deletes A Division
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdDivision_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DivisionId = int.Parse(GrdDivision.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, DivisionId);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsgDivsion.Text = "Wrong Command: first delete associated category";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUDivision, DivisionId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadDivision();
            lblErrorMsgDivsion.Text = "";
        }
    }

    /// <summary>
    /// Sets PageIndex Of Division Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdDivision.PageIndex = e.NewPageIndex;
        this.LoadDivision();
    }

    /// <summary>
    /// Save Or Updates a Division
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDivison_Click(object sender, EventArgs e)
    {
        if (btnSaveDivison.Text == "New")
        {
            txtDivisionCode.Text = this.GetAutoCode("DV", 0);
            txtDivisionName.Enabled = true;
            txtDivisionName.Focus();
            btnSaveDivison.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(dddivisonPrincipal);
        }
        else if (btnSaveDivison.Text == "Save")
        {
            if (txtDivisionName.Text.Length == 0)
            {
                lblErrorMsgDivsion.Text = "Must Entry Division Name";
                return;
            }
            mController.InsertHierarchy(Constants.SKUDivision, int.Parse(dddivisonPrincipal.SelectedValue.ToString()), txtDivisionCode.Text, txtDivisionName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("DV", long.Parse(txtDivisionCode.Text.Substring(2)));
            btnSaveDivison.Text = "New";
            txtDivisionCode.Text = "";
            txtDivisionName.Text = "";
            txtDivisionName.Enabled = false;
            this.LoadDivision();
            lblErrorMsgDivsion.Text = "";

        }
        else if (btnSaveDivison.Text == "Update")
        {
            if (txtDivisionName.Text.Length == 0)
            {
                lblErrorMsgDivsion.Text = "Must Enter Division Name";
                return;
            }
            mController.UpdateHierarchy(Constants.SKUDivision, DivisionId, int.Parse(dddivisonPrincipal.SelectedValue.ToString()), txtDivisionCode.Text, txtDivisionName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveDivison.Text = "New";
            txtDivisionName.Text = "";
            txtDivisionName.Text = "";
            txtDivisionName.Enabled = false;
            this.LoadDivision();
        }

    }

    #endregion

    #region Category Tab

    /// <summary>
    /// Loads Divisions To Division Combo On Category Tab
    /// </summary>
    private void LoadCategoryDivision()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, int.Parse(DrpCategoryPrincipal.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddCategoryDivision, dt, 0, 3, true);
    }

    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddCategoryDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCategroy();
    }

    /// <summary>
    /// Loads Categories To Category Grid On Category Tab
    /// </summary>
    private void LoadCategroy()
    {
        if (ddCategoryDivision.Items.Count > 0)
        {
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(ddCategoryDivision.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            GrdCategory.EditIndex = -1;
            GrdCategory.DataSource = dt;
            GrdCategory.Columns[0].Visible = true;
            GrdCategory.DataBind();
            GrdCategory.Columns[0].Visible = false;
        }
    }

    /// <summary>
    /// Sets Category Data For Edit. This Function Runs When An Existing Category Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ddCategoryDivision.SelectedValue = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        CategoryId = int.Parse(GrdCategory.Rows[e.NewEditIndex].Cells[0].Text);
        txtCategoryCode.Text = GrdCategory.Rows[e.NewEditIndex].Cells[1].Text;
        txtCategoryName.Text = GrdCategory.Rows[e.NewEditIndex].Cells[2].Text;
        txtCategoryName.Enabled = true;
        btnSaveCategory.Text = "Update";
        for (int i = 0; i < GrdCategory.Rows.Count; i++)
        {
            GrdCategory.Rows[i].Cells[3].Enabled = false;
        }
    }

    /// <summary>
    /// Deletes A Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CategoryId = int.Parse(GrdCategory.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrand, CategoryId);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsgCategory.Text = "Wrong Command: first delete associated Brand";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadCategroy();
        }
    }

    /// <summary>
    /// Sets PageIndex Of Category Grid On Division Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdCategory.PageIndex = e.NewPageIndex;
        this.LoadCategroy();
    }

    /// <summary>
    /// Save Or Updates a Category
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        lblErrorMsgCategory.Text = "";
        if (btnSaveCategory.Text == "New")
        {
            txtCategoryCode.Text = this.GetAutoCode("CA", 0);
            txtCategoryName.Enabled = true;
            txtCategoryName.Focus();
            btnSaveCategory.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(ddCategoryDivision);
        }
        else if (btnSaveCategory.Text == "Save")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                lblErrorMsgCategory.Text = "Must Enter Category";
                return;
            }
            mController.InsertHierarchy(Constants.SKUCategory, int.Parse(ddCategoryDivision.SelectedValue.ToString()), txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("CA", long.Parse(txtCategoryCode.Text.Substring(2)));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
            lblErrorMsgCategory.Text = "";

        }
        else if (btnSaveCategory.Text == "Update")
        {
            if (txtCategoryName.Text.Length <= 0)
            {
                lblErrorMsgCategory.Text = "Must Enter Category";
                return;
            }
            mController.UpdateHierarchy(Constants.SKUCategory, CategoryId, int.Parse(ddCategoryDivision.SelectedValue.ToString()), txtCategoryCode.Text, txtCategoryName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveCategory.Text = "New";
            txtCategoryCode.Text = "";
            txtCategoryName.Text = "";
            txtCategoryName.Enabled = false;
            this.LoadCategroy();
        }
    }

    #endregion

    #region Brand Tab

    /// <summary>
    /// Loads Divisions To Division Combo On Brand Tab
    /// </summary>
    private void LoadBrandDivision()
    {
        if (DrpBrandPrincipal.Items.Count > 0)
        {
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUDivision, Constants.IntNullValue, int.Parse(DrpBrandPrincipal.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpBrandDivision, dt, 0, 3, true);
        }
    }

    /// <summary>
    /// Loads Categories To Category Combo And Brands To Brand Grid On Brand Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpBrandDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadBrandCategory();
        this.LoadBrand();
    }

    /// <summary>
    /// Loads Categories To Category Combo On Brand Tab
    /// </summary>
    private void LoadBrandCategory()
    {
        if (DrpBrandDivision.Items.Count > 0)
        {
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(DrpBrandDivision.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.ddBrandCategory, dt, 0, 3, true);
        }
    }

    /// <summary>
    /// Loads Brands To Brand Grid On Brand Tab
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddBrandCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadBrand();
    }

    /// <summary>
    /// Loads Brands To Brand Grid On Brand Tab
    /// </summary>
    private void LoadBrand()
    {
        if (ddBrandCategory.Items.Count > 0)
        {
            DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrand, Constants.IntNullValue, int.Parse(ddBrandCategory.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            GrdBrand.EditIndex = -1;
            GrdBrand.DataSource = dt;
            GrdBrand.Columns[0].Visible = true;
            GrdBrand.DataBind();
            GrdBrand.Columns[0].Visible = false;
        }
    }

    /// <summary>
    /// Sets Brand Data For Edit. This Function Runs When An Existing Brand Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdBrand_RowEditing(object sender, GridViewEditEventArgs e)
    {
        BrandId = int.Parse(GrdBrand.Rows[e.NewEditIndex].Cells[0].Text);
        txtBrandCode.Text = GrdBrand.Rows[e.NewEditIndex].Cells[1].Text;
        txtBrandName.Text = GrdBrand.Rows[e.NewEditIndex].Cells[2].Text;
        txtBrandName.Enabled = true;
        btnSaveBrand.Text = "Update";
        for (int i = 0; i < GrdBrand.Rows.Count; i++)
        {
            GrdBrand.Rows[i].Cells[3].Enabled = false;
        }
    }

    /// <summary>
    /// Deletes A Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SkuController msku = new SkuController();
        BrandId = int.Parse(GrdBrand.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, BrandId, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsgBrand.Text = "Wrong Command: first delete associated SKUS";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUBrand, BrandId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadBrand();
        }
    }

    /// <summary>
    /// Sets PageIndex Of Brand Grid On Brand Tab
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GrdBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdBrand.PageIndex = e.NewPageIndex;
        this.LoadBrand();
    }

    /// <summary>
    /// Save Or Updates a Brand
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveBrand_Click(object sender, EventArgs e)
    {
        lblErrorMsgBrand.Text = "";
        if (btnSaveBrand.Text == "New")
        {
            txtBrandCode.Text = this.GetAutoCode("BN", 0);
            txtBrandName.Enabled = true;
            txtBrandName.Focus();
            btnSaveBrand.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(ddBrandCategory);
        }
        else if (btnSaveBrand.Text == "Save")
        {
            if (txtBrandName.Text.Length == 0)
            {
                lblErrorMsgBrand.Text = "Must Enter Brand";
                return;
            }
            mController.InsertHierarchy(Constants.SKUBrand, int.Parse(ddBrandCategory.SelectedValue.ToString()), txtBrandCode.Text, txtBrandName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("BN", long.Parse(txtBrandCode.Text.Substring(2)));
            btnSaveBrand.Text = "New";
            txtBrandCode.Text = "";
            txtBrandName.Text = "";
            txtBrandName.Enabled = false;
            this.LoadBrand();
            lblErrorMsgBrand.Text = "";
        }
        else if (btnSaveBrand.Text == "Update")
        {
            if (txtBrandName.Text.Length == 0)
            {
                lblErrorMsgBrand.Text = "Must Enter Brand";
                return;
            }
            mController.UpdateHierarchy(Constants.SKUBrand, BrandId, int.Parse(ddBrandCategory.SelectedValue.ToString()), txtBrandCode.Text, txtBrandName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveBrand.Text = "New";
            txtBrandCode.Text = "";
            txtBrandName.Text = "";
            txtBrandName.Enabled = false;
            this.LoadBrand();
        }
    }

    #endregion

    #region Brand Type Tab

    protected void btnSaveBrandType_Click(object sender, EventArgs e)
    {
        lblErrorMsgBrandType.Text = "";
        if (btnSaveBrandType.Text == "New")
        {
            txtBrandTypeCode.Text = this.GetAutoCode("BNT", 0);
            txtBrandTypeName.Enabled = true;
            btnSaveBrandType.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtBrandTypeName);
        }
        else if (btnSaveBrandType.Text == "Save")
        {
            if (txtBrandTypeName.Text.Length == 0)
            {
                lblErrorMsgBrandType.Text = "Must Enter Brand Type";
                return;
            }
            mController.InsertHierarchy(Constants.SKUBrandType, Constants.IntNullValue, txtBrandTypeCode.Text, txtBrandTypeName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("BNT", long.Parse(txtBrandTypeCode.Text.Substring(3)));
            btnSaveBrandType.Text = "New";
            txtBrandTypeCode.Text = "";
            txtBrandTypeName.Text = "";
            txtBrandTypeName.Enabled = false;
            this.LoadBrandType();
            lblErrorMsgBrandType.Text = "";
        }
        else if (btnSaveBrandType.Text == "Update")
        {
            if (txtBrandTypeName.Text.Length == 0)
            {
                lblErrorMsgBrandType.Text = "Must Enter Brand Type";
                return;
            }
            mController.UpdateHierarchy(Constants.SKUBrandType, BrandTypeId, Constants.IntNullValue, txtBrandTypeCode.Text, txtBrandTypeName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveBrandType.Text = "New";
            txtBrandTypeCode.Text = "";
            txtBrandTypeName.Text = "";
            txtBrandTypeName.Enabled = false;
            this.LoadBrandType();
        }
    }

    private void LoadBrandType()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUBrandType, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        GrdBrand.EditIndex = -1;
        GrdBrandType.DataSource = dt;
        GrdBrandType.Columns[0].Visible = true;
        GrdBrandType.DataBind();
        GrdBrandType.Columns[0].Visible = false;
    }

    protected void GrdBrandType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        BrandTypeId = int.Parse(GrdBrandType.Rows[e.NewEditIndex].Cells[0].Text);
        txtBrandTypeCode.Text = GrdBrandType.Rows[e.NewEditIndex].Cells[1].Text;
        txtBrandTypeName.Text = GrdBrandType.Rows[e.NewEditIndex].Cells[2].Text;
        txtBrandTypeName.Enabled = true;
        btnSaveBrandType.Text = "Update";
        for (int i = 0; i < GrdBrandType.Rows.Count; i++)
        {
            GrdBrandType.Rows[i].Cells[3].Enabled = false;
        }
    }

    protected void GrdBrandType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SkuController msku = new SkuController();
        BrandTypeId = int.Parse(GrdBrandType.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, BrandTypeId, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsgBrandType.Text = "Wrong Command: first delete associated SKUS";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUBrandType, BrandTypeId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadBrandType();
        }
    }

    #endregion

    #region Major

    protected void btnSaveMajor_Click(object sender, EventArgs e)
    {
        lblErrorMsgMajor.Text = "";
        if (btnSaveMajor.Text == "New")
        {
            txtMajorCode.Text = this.GetAutoCode("MJ", 0);
            txtMajorName.Enabled = true;
            btnSaveMajor.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtMajorName);
        }
        else if (btnSaveMajor.Text == "Save")
        {
            if (txtMajorName.Text.Length == 0)
            {
                lblErrorMsgMajor.Text = "Must Enter Major";
                return;
            }
            mController.InsertHierarchy(Constants.SKUMajor, Constants.IntNullValue, txtMajorCode.Text, txtMajorName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("MJ", long.Parse(txtMajorCode.Text.Substring(2)));
            btnSaveMajor.Text = "New";
            txtMajorCode.Text = "";
            txtMajorName.Text = "";
            txtMajorName.Enabled = false;
            this.LoadMajor();
            lblErrorMsgMajor.Text = "";
        }
        else if (btnSaveMajor.Text == "Update")
        {
            if (txtMajorName.Text.Length == 0)
            {
                lblErrorMsgMajor.Text = "Must Enter Major";
                return;
            }
            mController.UpdateHierarchy(Constants.SKUMajor, MajorId, Constants.IntNullValue, txtMajorCode.Text, txtMajorName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSaveMajor.Text = "New";
            txtMajorCode.Text = "";
            txtMajorName.Text = "";
            txtMajorName.Enabled = false;
            this.LoadMajor();
        }
    }

    private void LoadMajor()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUMajor, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        GrdMajor.EditIndex = -1;
        GrdMajor.DataSource = dt;
        GrdMajor.Columns[0].Visible = true;
        GrdMajor.DataBind();
        GrdMajor.Columns[0].Visible = false;
    }

    protected void GrdMajor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        MajorId = int.Parse(GrdMajor.Rows[e.NewEditIndex].Cells[0].Text);
        txtMajorCode.Text = GrdMajor.Rows[e.NewEditIndex].Cells[1].Text;
        txtMajorName.Text = GrdMajor.Rows[e.NewEditIndex].Cells[2].Text;
        txtMajorName.Enabled = true;
        btnSaveMajor.Text = "Update";
        for (int i = 0; i < GrdMajor.Rows.Count; i++)
        {
            GrdMajor.Rows[i].Cells[3].Enabled = false;
        }
    }

    protected void GrdMajor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SkuController msku = new SkuController();
        MajorId = int.Parse(GrdMajor.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, MajorId, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsgMajor.Text = "Wrong Command: first delete associated SKUS";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUMajor, MajorId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadMajor();
        }
    }

    #endregion

    #region PR Rating

    protected void btnSavePRRating_Click(object sender, EventArgs e)
    {
        lblErrorMsgPRRating.Text = "";
        if (btnSavePRRating.Text == "New")
        {
            txtPRRatingCode.Text = this.GetAutoCode("PR", 0);
            txtPRRatingName.Enabled = true;
            btnSavePRRating.Text = "Save";
            ScriptManager.GetCurrent(Page).SetFocus(txtPRRatingName);
        }
        else if (btnSavePRRating.Text == "Save")
        {
            if (txtPRRatingName.Text.Length == 0)
            {
                lblErrorMsgPRRating.Text = "Must Enter PR Rating";
                return;
            }
            mController.InsertHierarchy(Constants.SKUPRRating, Constants.IntNullValue, txtPRRatingCode.Text, txtPRRatingName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            this.SetAutoCode("PR", long.Parse(txtPRRatingCode.Text.Substring(2)));
            btnSavePRRating.Text = "New";
            txtPRRatingCode.Text = "";
            txtPRRatingName.Text = "";
            txtPRRatingName.Enabled = false;
            this.LoadPRRating();
            lblErrorMsgPRRating.Text = "";
        }
        else if (btnSavePRRating.Text == "Update")
        {
            if (txtPRRatingName.Text.Length == 0)
            {
                lblErrorMsgPRRating.Text = "Must Enter PR Rating";
                return;
            }
            mController.UpdateHierarchy(Constants.SKUPRRating, PRRatingId, Constants.IntNullValue, txtPRRatingCode.Text, txtPRRatingName.Text, null, true, int.Parse(this.Session["CompanyId"].ToString()));
            btnSavePRRating.Text = "New";
            txtPRRatingCode.Text = "";
            txtPRRatingName.Text = "";
            txtPRRatingName.Enabled = false;
            this.LoadPRRating();
        }
    }

    private void LoadPRRating()
    {
        DataTable dt = mController.SelectSkuHierarchy(Constants.SKUPRRating, Constants.IntNullValue, Constants.IntNullValue, null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        GrdPRRating.EditIndex = -1;
        GrdPRRating.DataSource = dt;
        GrdPRRating.Columns[0].Visible = true;
        GrdPRRating.DataBind();
        GrdPRRating.Columns[0].Visible = false;
    }

    protected void GrdPRRating_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PRRatingId = int.Parse(GrdPRRating.Rows[e.NewEditIndex].Cells[0].Text);
        txtPRRatingCode.Text = GrdPRRating.Rows[e.NewEditIndex].Cells[1].Text;
        txtPRRatingName.Text = GrdPRRating.Rows[e.NewEditIndex].Cells[2].Text;
        txtPRRatingName.Enabled = true;
        btnSavePRRating.Text = "Update";
        for (int i = 0; i < GrdPRRating.Rows.Count; i++)
        {
            GrdPRRating.Rows[i].Cells[3].Enabled = false;
        }
    }

    protected void GrdPRRating_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SkuController msku = new SkuController();
        PRRatingId = int.Parse(GrdPRRating.Rows[e.RowIndex].Cells[0].Text);
        DataTable dt = msku.SelectSkuInfo(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, PRRatingId, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            lblErrorMsgPRRating.Text = "Wrong Command: first delete associated SKUS";
        }
        else
        {
            mController.UpdateHierarchy(Constants.SKUPRRating, PRRatingId, Constants.IntNullValue, null, null, null, false, int.Parse(this.Session["CompanyId"].ToString()));
            this.LoadPRRating();
        }
    }

    #endregion

    /// <summary>
    /// Gets Code For New Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CodeType">Type</param>
    /// <returns>Code As String</returns>
    private string GetAutoCode(string PreeFix,int CodeType)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        return AutoCode.GetAutoCode(PreeFix, CodeType, Constants.LongNullValue);
    }
    
    /// <summary>
    /// Sets Code For Principal, Division, Category And Brand
    /// </summary>
    /// <param name="PreeFix">Prefix</param>
    /// <param name="CValue">Value</param>
    private void SetAutoCode(string PreeFix, long CValue)
    {
        SETTINGS_TABLE_Controller AutoCode = new SETTINGS_TABLE_Controller();
        string result = AutoCode.GetAutoCode(PreeFix, 1, CValue);
    }    
}