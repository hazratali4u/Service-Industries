using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit Customer Claim
/// </summary>
public partial class Forms_frmDemangedClaim : System.Web.UI.Page
{
    DataTable ClaimedSKU;
    private static int RowId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadClaimType();
            this.LoadAccountHead();
            this.LoadSKUDetail();
            this.LoadPrincipal();
            this.CreatTableValue();
            this.CreatTable();
            this.LoadArea();
            this.LoadCustomer();
            this.LoadGrid();
            this.LoadCreditInvoice();
            btnAddNew.Attributes.Add("onclick", "return ValidateValueForm();");
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.SetFocus(drpDistributor);
            lblRowId.Text = "-1";
        }
    }    
    #region Load
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    private void LoadClaimType()
    {
        RbdClaimType.Items.Add(new ListItem("Debit Claim", Constants.DebitClaim.ToString()));
        RbdClaimType.Items.Add(new ListItem("Credit Claim", Constants.CreditClaim.ToString()));
        RbdClaimType.SelectedIndex = 0;         
    }    
    /// <summary>
    /// Loads Account Heads To AccountHead Combo
    /// Modified by: Hazrat Ali
    /// 2012-Feb-16 10:40 AM
    /// Hard coded Account Heads as per Saddruddin Sb Instructions
    /// This is valid only for FDMPL and invalid for BDN and FDM
    /// Commited old code
    /// </summary>
    private void LoadAccountHead()
    {        
        DrpAccountHead.Items.Clear();
        
        #region Commited Code
        /*
        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("SECURITY DEPOSIT BOTTLE", "424"));
        }
        else if (RbdClaimType.SelectedValue == Constants.CreditClaim.ToString())
        {
            DrpAccountHead.Items.Add(new ListItem("Adv. against Empty Bottle", "511"));
            DrpAccountHead.Items.Add(new ListItem("Sales Return (Principal Wise)", "152"));
            DrpAccountHead.Items.Add(new ListItem("Sale Discount - Claimable", "493"));
        }
        */
        AccountHeadController mAccountController = new AccountHeadController();
            DataTable dt = mAccountController.SelectClaimHead(int.Parse(RbdClaimType.SelectedValue));
            clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 2, true);
         
        
        #endregion
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
    private void LoadArea()
    {
        DistributorAreaController mController = new DistributorAreaController();
        DataTable dt = mController.SelectDist_Area(Constants.LongNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, null, null);
        clsWebFormUtil.FillDropDownList(DrpRoute, dt, 0, 6, true);
        DrpRoute.Enabled = true;
    }
    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0 && DrpRoute.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dt = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpRoute.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpPrincipal.SelectedValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dt, 0, 4, true);
        }
        else
        {
            DrpCustomer.Items.Clear();
        }
    }
    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            LedgerController LController = new LedgerController();
            DataTable dt = LController.SelectClaimDetail(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(RbdClaimType.SelectedValue.ToString()),
                DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            GrdOrder.EditIndex = -1;
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }
    private void LoadCreditInvoice()
    {
        ddlInvoice.Items.Clear();
        if (ddlClaimMode.SelectedIndex == 0)
        {
            if (DrpCustomer.Items.Count > 0)
            {
                LedgerController _ledgerCtl = new LedgerController();
                DataTable dtCreditInvoice = _ledgerCtl.SelectCreditPendingInvoice(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpPrincipal.SelectedValue), long.Parse(DrpCustomer.SelectedValue), 0);
                clsWebFormUtil.FillDropDownList(this.ddlInvoice, dtCreditInvoice, "SALE_INVOICE_ID", "MANUAL_INVOICE_ID2", true);
            }
        }        
    }
    #endregion
    private void CreatTableValue()
    {
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();  

    }
    #region Sel/Index Change

    protected void RbdClaimType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAccountHead();
        LoadGrid();
    }
    protected void ddlClaimMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCreditInvoice();
        if (ddlClaimMode.SelectedIndex == 0)
        {
            lblDocumentNo.Visible = false;
            ddlDocumentNo.Visible = false;
            pnlProduct.Visible = false;

            txtAmount.Visible = true;
            lblAmount.Visible = true;
            btnAddNew.Visible = true;
            pnlValue.Visible = true;
        }
        else
        {
            txtAmount.Visible = false;
            lblAmount.Visible = false;

            btnAddNew.Visible = false;
            pnlValue.Visible = false;

            pnlProduct.Visible = true;
            lblDocumentNo.Visible = true;
            ddlDocumentNo.Visible = true;
            this.LoadDocumentNo();
            this.CreatTable();
            this.LoadProductGrid();
        }
    }
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        this.LoadCustomer();
        DrpCustomer_SelectedIndexChanged(null, null);
    }
    protected void DrpRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
        DrpCustomer_SelectedIndexChanged(null, null);
    }
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClaimMode.SelectedIndex == 1)
        {
            this.LoadDocumentNo();            
        }
        else
        {
            this.LoadCreditInvoice();
        }
    }
    protected void ddlDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (long.Parse(ddlDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            DataTable dt = (DataTable)this.Session["dtDocumentNo"];
            DataRow[] foundRows = dt.Select("CLAIM_MASTER_ID  = '" + ddlDocumentNo.SelectedItem.Text + "'");
            if (foundRows.Length > 0)
            {
                DrpAccountHead.SelectedValue = foundRows[0]["ACCOUNT_HEAD_ID"].ToString();
                txtRemarks.Text = foundRows[0]["REMARKS"].ToString();
                txtTotalAmount.Text = foundRows[0]["AMOUNT"].ToString();
                DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();

                this.LoadClaimDetail();
                this.ClearAll();
                ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
            }
        }
        else
        {
            ClearAll();
            this.CreatTable();
            this.LoadProductGrid();
        }
    }
    #endregion    
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LedgerController LController = new LedgerController();
        DataControl dc = new DataControl();

        LController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[6].Text)),Constants.LongNullValue  , decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)));
        this.LoadGrid();
        
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        long DocumentNo = Constants.LongNullValue;
        string ManualDocumentNo = null;
        if (chbInvoiceWise.Checked)
        {
            if (ddlInvoice.Items.Count > 0)
            {
                DocumentNo = Convert.ToInt64(ddlInvoice.SelectedValue);
                ManualDocumentNo = ddlInvoice.SelectedItem.Text.Split('(')[0].ToString().Trim();
            }
        }
        if (IsDayClosed())
        {
            UserController UserCtl = new UserController();
            UserCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            this.Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
        else
        {
            Configuration.GetAccountHead();
            LedgerController LController = new LedgerController();
            string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
            {
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                   DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                    null, int.Parse(this.Session["UserId"].ToString()), DocumentNo, ManualDocumentNo, Constants.Document_SaleInvoice, null, Constants.DateNullValue, Constants.DebitClaim, 0);
                
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(Configuration.AccountReceivable), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                   DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                    null, int.Parse(this.Session["UserId"].ToString()), DocumentNo, ManualDocumentNo, Constants.Document_SaleInvoice, null, Constants.DateNullValue, Constants.DebitClaim, 0);
            }
            else
            {
                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtAmount.Text), 0,
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                     null, int.Parse(this.Session["UserId"].ToString()), DocumentNo, ManualDocumentNo, Constants.Document_SaleInvoice, null, Constants.DateNullValue, Constants.CreditClaim, 0);

                LController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(Configuration.AccountReceivable), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtAmount.Text),
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()),
                     null, int.Parse(this.Session["UserId"].ToString()), DocumentNo, ManualDocumentNo, Constants.Document_SaleInvoice, null, Constants.DateNullValue, Constants.CreditClaim, 0);
            }
            txtAmount.Text = "";
            txtRemarks.Text = "";
            this.LoadGrid();
            this.LoadCreditInvoice();
        }
    }
    #region Product

    private void CreatTable()
    {
        ClaimedSKU = new DataTable();
        ClaimedSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        ClaimedSKU.Columns.Add("CLAIM_MASTER_ID", typeof(long));
        ClaimedSKU.Columns.Add("SKU_ID", typeof(int));
        ClaimedSKU.Columns.Add("SKU_Code", typeof(string));
        ClaimedSKU.Columns.Add("SKU_Name", typeof(string));
        ClaimedSKU.Columns.Add("BATCH_NO", typeof(string));
        ClaimedSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        ClaimedSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        ClaimedSKU.Columns.Add("AMOUNT", typeof(decimal));

        this.Session.Add("ClaimedSKU", ClaimedSKU);
    }
    private void LoadDocumentNo()
    {
        LedgerController LedgrCtl = new LedgerController();

        this.ddlDocumentNo.Items.Clear();

        if ((DrpCustomer.Items.Count > 0) && (DrpPrincipal.Items.Count > 0) && (drpDistributor.Items.Count > 0))
        {
            DataTable dtDocumentNo = LedgrCtl.GetCustomerClaimMaster(int.Parse(DrpPrincipal.SelectedValue), Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToDateTime(this.Session["CurrentWorkDate"]), RbdClaimType.SelectedIndex);
            ddlDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlDocumentNo, dtDocumentNo, "LEDGER_ID", "CLAIM_MASTER_ID");
            this.Session.Add("dtDocumentNo", dtDocumentNo);
        }
    }

    private void LoadClaimDetail()
    {
        LedgerController LegrCtl = new LedgerController();
        ClaimedSKU = LegrCtl.GetCustomerClaimDetail(Convert.ToInt32(ddlDocumentNo.SelectedItem.Text));
        this.Session.Add("ClaimedSKU", ClaimedSKU);
        this.LoadProductGrid();
    }
    private void LoadSKUDetail()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable Dtsku_Price = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, 0, 10, true);
      
        this.Session.Add("Dtsku_Price", Dtsku_Price);
    }

    private bool CheckDublicateSKU()
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        ClaimedSKU = (DataTable)this.Session["ClaimedSKU"];
        DataRow[] foundRows = ClaimedSKU.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        ClaimedSKU = (DataTable)Session["ClaimedSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
      
        decimal mTradePrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));

        if (btnSave.Text == "Add Sku")
        {
            if (CheckDublicateSKU())
            {
                DataRow dr = ClaimedSKU.NewRow();
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                dr["UNIT_PRICE"] = mTradePrice.ToString();

                
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["BATCH_NO"] = "N/A";
                

                ClaimedSKU.Rows.Add(dr);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
            }
        }
        else
        {
            DataRow dr = ClaimedSKU.Rows[RowId];
            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
            dr["BATCH_NO"] = "N/A";
            dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
            dr["UNIT_PRICE"] = mTradePrice.ToString();

          
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
              
            

        }

        decimal TotalProductValue = 0;

        foreach (DataRow dr in ClaimedSKU.Rows)
        {
            TotalProductValue += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));

        }

        txtTotalAmount.Text = Convert.ToString(Math.Round(TotalProductValue, 2));


        this.Session.Add("ClaimedSKU", ClaimedSKU);
        this.LoadProductGrid();
        this.ClearAll();
        ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
    }

    private void LoadProductGrid()
    {
        ClaimedSKU = (DataTable)this.Session["ClaimedSKU"];
        GrdPurchase.EditIndex = -1;
        GrdPurchase.DataSource = ClaimedSKU;
        GrdPurchase.DataBind();
    }
    private void ClearAll()
    {
       
        txtUnitRate.Text = "";
        txtQuantity.Text = "";
        txtProductAmount.Text = "";
      
       
        btnSave.Text = "Add Sku";
        ddlSkus.Enabled = true;
    }

    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowId = e.NewEditIndex;
        ddlSkus.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtProductAmount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        ddlSkus.Enabled = false;

        btnSave.Text = "Update SKU";
        for (int i = 0; i < GrdPurchase.Rows.Count; i++)
        {
            GrdPurchase.Rows[i].Cells[6].Enabled = false;
        }

    }
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ClaimedSKU = (DataTable)this.Session["ClaimedSKU"];
        ClaimedSKU.Rows.RemoveAt(e.RowIndex);
        Session.Add("ClaimedSKU", ClaimedSKU);
        LoadProductGrid();
    }

    protected void btnSaveProductClaim_Click(object sender, EventArgs e)
    {
        LedgerController LController = new LedgerController();
        ClaimedSKU = (DataTable)Session["ClaimedSKU"];

        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
      
        if (RbdClaimType.SelectedValue == Constants.DebitClaim.ToString())
        {
            if (ddlDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                if (LController.InsertCustomerClaim(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 0, decimal.Parse(txtTotalAmount.Text),
                   DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue),
                    null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.DebitClaimProduct, 0, ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    txtRemarks.Text = "";
                    LoadProductGrid();
                    LoadDocumentNo();
                    CreatTable();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
            else
            {
                if (LController.UpdateCustomerClaim(Convert.ToInt64(ddlDocumentNo.SelectedValue), Convert.ToInt64(ddlDocumentNo.SelectedItem.Text), 0, Convert.ToDecimal(txtTotalAmount.Text), Convert.ToInt32(DrpAccountHead.SelectedValue), txtRemarks.Text, Convert.ToInt32(Session["UserID"]), ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    txtRemarks.Text = "";
                    LoadProductGrid();
                    LoadDocumentNo();
                    CreatTable();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim updated successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
        }
        else
        {
            if (ddlDocumentNo.SelectedValue == Constants.LongNullValue.ToString())
            {
                if (LController.InsertCustomerClaim(Constants.Journal_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpAccountHead.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), decimal.Parse(txtTotalAmount.Text), 0,
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), txtRemarks.Text, DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 0,
                     null, int.Parse(this.Session["UserId"].ToString()), Constants.LongNullValue, null, Constants.IntNullValue, null, Constants.DateNullValue, Constants.CreditClaim, 0, ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    LoadProductGrid();
                    LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim Saved Successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
            else
            {
                if (LController.UpdateCustomerClaim(Convert.ToInt64(ddlDocumentNo.SelectedValue), Convert.ToInt64(ddlDocumentNo.SelectedItem.Text), Convert.ToDecimal(txtTotalAmount.Text), 0, Convert.ToInt32(DrpAccountHead.SelectedValue), txtRemarks.Text, Convert.ToInt32(Session["UserID"]), ClaimedSKU))
                {
                    Session.Remove("ClaimedSKU");
                    txtTotalAmount.Text = "";
                    LoadProductGrid();
                    LoadDocumentNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Customer Claim updated successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
                }
            }
        }
    }

    #endregion
    private bool IsDayClosed()
    {
        bool flag = false;
        DistributorController DistrCtl = new DistributorController();
        DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(drpDistributor.SelectedValue), 3);
        if (Convert.ToDateTime(Session["CurrentWorkDate"]) == Convert.ToDateTime(dtDayClose.Rows[0]["DayClose"]))
        {
            flag = false;
        }
        else
        {
            flag = true;
        }

        return flag;
    }

    protected void chbInvoiceWise_CheckedChanged(object sender, EventArgs e)
    {
        if(chbInvoiceWise.Checked)
        {
            lblInvoice.Visible = true;
            ddlInvoice.Visible = true;
        }
        else
        {
            lblInvoice.Visible = false;
            ddlInvoice.Visible = false;
        }
    }
}