using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Take Order, Invoice And Sale Return(Step2)
/// </summary>
public partial class Forms_frmOrderEntry : System.Web.UI.Page
{
    #region Variables

    DataTable PurchaseSKU;
    DataTable dtFreeSKU;
    private static int mCustomerTypeId;
    private static int mCustomerVolClassId;
    private int mTownId;
    private static int RowId;
    private static int UnitType;
    private static int OrderNo;

    #endregion

    /// <summary>
    /// Page_Load Function Populates All Combos, Grids And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadPrincipal();
            LoadPrincipalWisediscount();
            this.LoadCategory();
            this.LoadTransporter();
            OrderNo = (int)this.Session["OrderNo"];
            UnitType = (int)this.Session["UnitType"];
            txtprincipal.Text = this.Session["Route"].ToString();
            txtDeliveryMan.Text = this.Session["SaleMan"].ToString();
            this.Session.Remove("Route");
            this.Session.Remove("SaleMan");
            ScriptManager.GetCurrent(Page).SetFocus(txtBuiltyNo);
            if (OrderNo == -1)
            {
                btnSaveOrder.Text = "Save Invoice";
                this.LoadInvoiceToEdit();
            }
            else if (OrderNo == -2)
            {
                drpDocumentNo.Enabled = false;
                ChbDiscount.Visible = true;
                btnSaveOrder.Text = "Sale Return";
                lblBuiltyNo.Visible = false;
                txtBuiltyNo.Visible = false;
                txtBuiltyAmount.Visible = false;
                lblTransporter.Visible = false;
                ddlTransporter.Visible = false;
                ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
            }
            else
            {
                this.LoadPendingOrder();
            }
            this.LoadCustomerData();
            this.CustomerDetail();
            this.LoadSkuDetail();
            this.CreatTable();
            LoadPromotion();
            this.CreateFreeSKU();
            this.LoadFreeGrid();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtPriceGroup.Attributes.Add("Readonly", "Readonly");
        }
    }
    
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("SALE_ORDER_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("DistributorId", typeof(int));
        PurchaseSKU.Columns.Add("SALE_ORDER_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Code", typeof(string));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_UNIT", typeof(int));
        PurchaseSKU.Columns.Add("AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("EXTRA_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("DISTRIBUTOR_SHARE", typeof(decimal));
        PurchaseSKU.Columns.Add("STANDARD_DISCOUNT_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("RETAIL_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_RATE", typeof(decimal));
        PurchaseSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_EXTRA_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_STANDARD_DISCOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("CLAIM_PER", typeof(decimal));
        PurchaseSKU.Columns.Add("SED_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("NET_AMOUNT", typeof(decimal));
        PurchaseSKU.Columns.Add("QUANTITY_CTN", typeof(decimal));
        PurchaseSKU.Columns.Add("IS_DELETED", typeof(bool));
        PurchaseSKU.Columns.Add("CATEGORY_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_GST", typeof(decimal));
        PurchaseSKU.Columns.Add("SKU_GST_AMOUNT", typeof(decimal));
        this.Session.Add("PurchaseSKU", PurchaseSKU);
    }

    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1, true);
        this.Session.Add("PrincipalId", int.Parse(DrpPrincipal.SelectedValue.ToString()));
    }
    private void LoadPrincipalWisediscount()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDistributorSharePrincipal(int.Parse(DrpPrincipal.SelectedValue), int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["UserId"].ToString()), 4, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        if (m_dt != null && m_dt.Rows.Count > 0)
        {
                lblDistributorSh.Text = "Max Discount in Value: ";
                lblShareValue.Text = m_dt.Rows[0]["SHARE_VALUE"].ToString();
           
                lblDistrSharePer.Text = "Max Discount in Perc..: ";
                lblSharePer.Text = m_dt.Rows[0]["SHARE_PER"].ToString();
            
        }
        else
        {
            lblShareValue.Text = "0";
            lblSharePer.Text = "0";
        }
    }
    
    private void CreateFreeSKU()
    {
        dtFreeSKU = new DataTable();
        dtFreeSKU.Columns.Add("SKU_ID", typeof(int));
        dtFreeSKU.Columns.Add("SKU_Code", typeof(string));
        dtFreeSKU.Columns.Add("SKU_Name", typeof(string));
        dtFreeSKU.Columns.Add("UNIT_PRICE", typeof(decimal));
        dtFreeSKU.Columns.Add("Quantity", typeof(int));
        dtFreeSKU.Columns.Add("AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_RATE", typeof(decimal));
        dtFreeSKU.Columns.Add("GST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("TST_AMOUNT", typeof(decimal));
        dtFreeSKU.Columns.Add("PROMOTION_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_ID", typeof(int));
        dtFreeSKU.Columns.Add("BASKET_DETAIL_ID", typeof(int));
        dtFreeSKU.Columns.Add("PROMOTION_OFFER_ID", typeof(int));
        Session.Add("dtFreeSKU", dtFreeSKU);
    }
    
    private void LoadPendingOrder()
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtOrder = or.SelectPendingOrder(int.Parse(Session["DistributorId"].ToString()), int.Parse(Session["AreaId"].ToString()), Constants.IntNullValue, int.Parse(Session["OrderBookerId"].ToString()), int.Parse(Session["DeliveryManId"].ToString()), Constants.Order_Pending_Id, Constants.IntNullValue, int.Parse(Session["UserId"].ToString()), Convert.ToDateTime(Session["OrderDate"]));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtOrder, "SALE_ORDER_ID", "SALE_ORDER_ID2");
        this.Session.Add("dtOrder", dtOrder);
    }

    private void LoadInvoiceToEdit()
    {
        OrderEntryController or = new OrderEntryController();
        this.drpDocumentNo.Items.Clear();
        DataTable dtDoc = or.SelectPendingInvoices(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["AreaId"].ToString()), Constants.IntNullValue, int.Parse(this.Session["DeliveryManId"].ToString()), Convert.ToDateTime(this.Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), 0, int.Parse(this.Session["OrderBookerId"].ToString()));
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dtDoc, "DocID", "DocNo");
        this.Session.Add("dtDoc", dtDoc);
    }
    
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (long.Parse(drpDocumentNo.SelectedValue.ToString()) != Constants.LongNullValue)
        {
            if (OrderNo == -1)
            {
                if (btnSaveOrder.Text == "Save Invoice" || btnSaveOrder.Text == "Update Invoice")
                {
                    if (drpDocumentNo.Items.Count > 0)
                    {
                        decimal ObjGrossSales = 0;
                        decimal ObjStandardDiscount = 0;
                        decimal ObjExtraDiscount = 0;
                        decimal ObjSchemeDiscount = 0;
                        decimal ObjTotalGST = 0;
                        decimal ObjTotalSED = 0;
                        decimal ObjTotalTST = 0;
                        decimal ObjNetAmount = 0;
                        decimal ObjClaimableDiscount = 0;

                        DataControl dc = new DataControl();
                        DataTable dt = (DataTable)this.Session["dtDoc"];
                        DataRow[] foundRows = dt.Select("DocID  = '" + drpDocumentNo.SelectedValue + "'");

                        if (foundRows.Length > 0)
                        {
                            RblPayMode.SelectedValue = dt.Rows[0]["PayMode"].ToString();
                            btnSaveOrder.Text = "Update Invoice";
                            ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
                            numTxtTotalStndrdDiscnt.Text = foundRows[0]["DISCOUNT_AMOUNT"].ToString();
                            numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                            //numtxtSpecialDiscValue.Text = foundRows[0]["SPECIAL_DISCOUNT"].ToString();
                            numtxtTotalDistShare.Text = foundRows[0]["DISTRIBUTOR_SHARE"].ToString();
                            txtRemarks.Text = foundRows[0]["REMARKS"].ToString();
                            txtBuiltyNo.Text = foundRows[0]["BUILTY_NO"].ToString();
                            if (foundRows[0]["TRANSPORTER"].ToString() != "")
                            {
                                ddlTransporter.SelectedItem.Text = foundRows[0]["TRANSPORTER"].ToString();
                            }
                            txtBuiltyAmount.Text = foundRows[0]["BUILTY_AMOUNT"].ToString();
                            this.LoadDocumentDetail(Convert.ToInt64(drpDocumentNo.SelectedValue));

                            DataTable dt2 = (DataTable)this.Session["dtDoc2"];

                            foreach (DataRow dr in dt2.Rows)
                            {
                                ObjStandardDiscount = Convert.ToDecimal(dc.chkNull_0(numTxtTotalStndrdDiscnt.Text)) + Convert.ToDecimal(dc.chkNull_0(numtxtTotalExtraDiscnt.Text));
                                ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                                ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                                ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                                ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                                ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                                ObjNetAmount += decimal.Parse(dc.chkNull_0(dr["NET_AMOUNT"].ToString()));
                                ObjClaimableDiscount += decimal.Parse(dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString()));

                            }

                            txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
                           /// numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST, 2));
                           // numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalTST, 2));
                            numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjNetAmount - ObjStandardDiscount, 2));
                            //if (decimal.Parse(numtxtSpecialDiscValue.Text) > 0)
                            //{
                            //    numtxtSpecialDiscPr.Text = Convert.ToString(Math.Round((decimal.Parse(numtxtSpecialDiscValue.Text) / (ObjGrossSales - Convert.ToDecimal(numTxtTotalStndrdDiscnt.Text) - ObjTotalSED - ObjTotalGST - ObjTotalTST)) * 100, 2));
                            //}
                        }
                    }

                }
            }
            else
            {
                DataTable dt = (DataTable)this.Session["dtOrder"];
                DataRow[] foundRows = dt.Select("SALE_ORDER_ID  = '" + drpDocumentNo.SelectedValue + "'");
                if (foundRows.Length > 0)
                {
                    txtGrossAmount.Text = foundRows[0]["TOTAL_AMOUNT"].ToString();
                    numTxtTotalStndrdDiscnt.Text = foundRows[0]["STANDARD_DISCOUNT_AMOUNT"].ToString();
                //    numTxtTotalGST.Text = foundRows[0]["GST_AMOUNT"].ToString();
                    numTxtTotlAmnt.Text = foundRows[0]["TOTAL_NET_AMOUNT"].ToString();
                  //  numTxtTotalTST.Text = foundRows[0]["TST_AMOUNT"].ToString();
                    ddlCustomer.SelectedValue = foundRows[0]["CUSTOMER_ID"].ToString();
                    numtxtTotalExtraDiscnt.Text = foundRows[0]["EXTRA_DISCOUNT_AMOUNT"].ToString();
                    //numtxtSpecialDiscValue.Text = foundRows[0]["SPECIAL_DISCOUNT"].ToString();
                    numtxtTotalDistShare.Text = foundRows[0]["DISTRIBUTOR_SHARE"].ToString();
                    txtRemarks.Text = foundRows[0]["REMARKS"].ToString();
                    txtBuiltyNo.Text = foundRows[0]["BUILTY_NO"].ToString();
                    if (foundRows[0]["TRANSPORTER"].ToString() != "")
                    {
                        ddlTransporter.SelectedItem.Text = foundRows[0]["TRANSPORTER"].ToString();
                    }
                    txtBuiltyAmount.Text = foundRows[0]["BUILTY_AMOUNT"].ToString();
                    //if (decimal.Parse(numtxtSpecialDiscValue.Text) > 0)
                    //{
                    //    numtxtSpecialDiscPr.Text = Convert.ToString(Math.Round((decimal.Parse(numtxtSpecialDiscValue.Text) / (Convert.ToDecimal(txtGrossAmount.Text) - Convert.ToDecimal(numtxtTotalExtraDiscnt.Text) - Convert.ToDecimal(numTxtTotalStndrdDiscnt.Text))) * 100, 2));
                    //}                    
                    this.ExistenOrderDetail(long.Parse(drpDocumentNo.SelectedValue.ToString()));
                    EnableDisableController(false);
                    drpDocumentNo.Enabled = true;
                    btnSaveOrder.Text = "Save Invoice";
                    btnSaveOrder.Enabled = false;
                    btnCalculate.Enabled = false;
                    this.ClearAll();
                    ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
                    RblPayMode.SelectedValue = foundRows[0]["ORDER_TYPE_ID"].ToString();
                    DrpPrincipal.SelectedValue = foundRows[0]["PRINCIPAL_ID"].ToString();
                    DrpPrincipal_SelectedIndexChanged(null, null);
                }
                else
                {
                    this.CreateFreeSKU();
                    this.CreatTable();
                    this.LoadGird();
                    this.LoadFreeGrid();
                    ClearMasterALL();
                    btnSaveOrder.Text = "Save Order";
                }
            }
        }
        else
        {
            this.CreateFreeSKU();
            this.CreatTable();
            ClearMasterALL();
            if (OrderNo != -1)
            {
                btnSaveOrder.Text = "Save Order";
            }
            else
            {
                btnSaveOrder.Text = "Save Invoice";
            }
        }
    }
    
    private void LoadCustomerData()
    {
        if (int.Parse(this.Session["DistributorId"].ToString()) > 0 && long.Parse(this.Session["AreaId"].ToString()) > 0)
        {
            ddlCustomer.Items.Clear();
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["AreaId"].ToString()), Constants.IntNullValue, int.Parse(this.Session["PrincipalId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.ddlCustomer, dtCustomer, "CUSTOMER_ID", "CUSTOMER_DETAIL");
            this.Session.Add("dtCustomer", dtCustomer);
        }
        else
        {
            ddlCustomer.Items.Clear();
        }
    }
    
    private void LoadSkuDetail()
    {
        try
        {
            SKUPriceDetailController PController = new SKUPriceDetailController();
            ddlSkus.Items.Clear();
            if (txtPriceGroup.Text.Trim() == "Default")
            {
                if (int.Parse(this.Session["PrincipalId"].ToString()) > 0)
                {

                    DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(this.Session["PrincipalId"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                    ddlSkus.Items.Add(new ListItem("Select Product", Constants.IntNullValue.ToString()));
                    clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, "SKU_ID", "SkuPriceDetail2");
                    this.Session.Add("Dtsku_Price", Dtsku_Price);

                }
            }
            else if (txtPriceGroup.Text.Trim() != "")
            {
                OrderEntryController orderc = new OrderEntryController();
                DataTable Dtsku_Price = orderc.GetCustomerSKU(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(ddlCustomer.SelectedValue));
                ddlSkus.Items.Add(new ListItem("Select Product", Constants.IntNullValue.ToString()));
                clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, "SKU_ID", "SkuPriceDetail2");
                this.Session.Add("Dtsku_Price", Dtsku_Price);

            }
            else
            {
                if (int.Parse(this.Session["PrincipalId"].ToString()) > 0)
                {

                    DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(this.Session["PrincipalId"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
                    ddlSkus.Items.Add(new ListItem("Select Product", Constants.IntNullValue.ToString()));
                    clsWebFormUtil.FillDropDownList(ddlSkus, Dtsku_Price, "SKU_ID", "SkuPriceDetail2");
                    this.Session.Add("Dtsku_Price", Dtsku_Price);

                }
            }
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }

    private void LoadCategory()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();

        ddlSkus.Items.Clear();
        DataTable dtCategoryAll = PController.GetAllCatgory();
        this.Session.Add("dtCategoryAll", dtCategoryAll);
    }
    
    private void LoadPromotion()
    {
        try
        {
            if (int.Parse(this.Session["PrincipalId"].ToString()) > 0)
            {

                SKUPriceDetailController PController = new SKUPriceDetailController();
                DataTable dt = PController.SelectDataPrice(int.Parse(this.Session["PrincipalId"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["OrderDate"].ToString()));
                if (bool.Parse(dt.Rows[0]["Is_ManualDiscount"].ToString()) == false)
                {
                    OrderEntryController orderController = new OrderEntryController();
                    PromotionCollections_Controller Arpc = orderController.LoadSchemes(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), Convert.ToDateTime(this.Session["OrderDate"]));
                    this.Session.Add("Arpc", Arpc);
                    //  txtDiscountType.Text = "Auto Discount";
                    txtDiscount.Text = "0";
                    ChbDiscount.Checked = true;
                }
                else
                {
                    // txtDiscountType.Text = "Manual Discount";
                    numtxtTotalExtraDiscnt.ReadOnly = false;
                    //numtxtSpecialDiscValue.ReadOnly = false;
                    txtDiscount.Enabled = true;
                    ChbDiscount.Checked = false;
                }
            }
        }catch(Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
    
    private void LoadGird()
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.EditIndex = -1;
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();

        DataTable dtCategoryAll = (DataTable)this.Session["dtCategoryAll"];
        DataTable dtCategory = new DataTable();
        dtCategory.Columns.Add("SKU_HIE_ID", typeof(int));
        dtCategory.Columns.Add("SKU_HIE_NAME", typeof(string));
        dtCategory.Columns.Add("Quantity", typeof(int));

        DataRow drCat;
        int Quantity = 0;
        foreach (DataRow drCatAll in dtCategoryAll.Rows)
        {
            Quantity = 0;
            drCat = dtCategory.NewRow();
            foreach (DataRow drSKU in PurchaseSKU.Rows)
            {
                if (drSKU["CATEGORY_ID"].ToString() == drCatAll["SKU_HIE_ID"].ToString())
                {
                    Quantity += Convert.ToInt32(drSKU["QUANTITY_UNIT"]);
                    drCat["SKU_HIE_ID"] = drCatAll["SKU_HIE_ID"].ToString();
                    drCat["SKU_HIE_NAME"] = drCatAll["SKU_HIE_NAME"].ToString();
                }
            }
            if (Quantity > 0)
            {
                drCat["Quantity"] = Quantity;
                dtCategory.Rows.Add(drCat);
            }
        }
        gvCategory.EditIndex = -1;
        gvCategory.DataSource = dtCategory;
        gvCategory.DataBind();
    }


    private void LoadTransporter()
    {
        ddlTransporter.Items.Clear();
        DistributorController DistCtl = new DistributorController();
        DataTable dtTransporter = DistCtl.SelectTranspoter(Convert.ToInt32(Session["DistributorId"]), Convert.ToInt32(Session["UserID"]));
        clsWebFormUtil.FillDropDownList(this.ddlTransporter, dtTransporter, "TRASNPOTER_ID", "NAME", true);
    }
    
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataControl dc = new DataControl();
        RowId = e.NewEditIndex;
        ddlSkus.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        txtDiscount.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtUnitRate.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
        txtDistShare.Text = Math.Round((Convert.ToDecimal(dc.chkNull_0(GrdPurchase.Rows[e.NewEditIndex].Cells[6].Text)) / Convert.ToDecimal(dc.chkNull_0(txtQuantity.Text))), 2).ToString();

        if (UnitType == 0)
        {
            txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text;
        }
        else
        {
            txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[8].Text;
        }
        ddlSkus.Enabled = false;
        btnSave.Text = "Update";
        for (int i = 0; i < GrdPurchase.Rows.Count; i++)
        {
            GrdPurchase.Rows[i].Cells[10].Enabled = false;
        }

    }
    
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        PurchaseSKU.Rows.RemoveAt(e.RowIndex);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.LoadGird();
        btnCalculate.Enabled = true;
        btnSaveOrder.Enabled = false;
        ddlSkus.Focus();
        this.EnableDisableController(false);
    }
    
    private void LoadFreeGrid()
    {
        dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
        GrdFreeSKU.DataSource = dtFreeSKU;
        GrdFreeSKU.DataBind();
    }

    private void LoadDocumentDetail(long DocID)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.SelectInvoiceDetail(DocID, 0);
        dtFreeSKU = ord.SelectInvoicePromotion(int.Parse(this.Session["DistributorId"].ToString()), DocID);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.Session.Add("dtDoc2", PurchaseSKU);
        this.Session.Add("dtFreeSKU", dtFreeSKU);
        this.LoadGird();
        this.LoadFreeGrid();
    }
    
    private bool CheckDublicateSKU()
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Checks Free SKU in Free SKU Grid
    /// </summary>
    /// <returns>True On Success And False On Failure</returns>
    private int FreeSKUExist(DataTable dt, int Sku_id)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (int.Parse(dt.Rows[i]["SKU_ID"].ToString()) == Sku_id)
            {
                return i;
            }

        }
        return -1;
    }

    /// <summary>
    /// Loads Order And Free SKUS Grids
    /// </summary>
    /// <param name="OrderId">Order</param>
    private void ExistenOrderDetail(long OrderId)
    {
        OrderEntryController ord = new OrderEntryController();
        PurchaseSKU = ord.SelectOrderDetail(int.Parse(this.Session["DistributorId"].ToString()), OrderId);
        dtFreeSKU = ord.SelectOrderPromotion(int.Parse(this.Session["DistributorId"].ToString()), OrderId);
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.Session.Add("dtFreeSKU", dtFreeSKU);
        this.LoadGird();
        this.LoadFreeGrid();
    }

    /// <summary>
    /// Enables/Disables Controls
    /// </summary>
    /// <param name="CValue">Value</param>
    private void EnableDisableController(bool CValue)
    {

        if (CValue == true)
        {
            if (decimal.Parse(txtGrossAmount.Text) > 0)
            {
                ddlCustomer.Enabled = true;
                DrpPrincipal.Enabled = true;
                RblPayMode.Enabled = true;
                drpDocumentNo.Enabled = true;
            }
        }
        else
        {
            RblPayMode.Enabled = false;
            DrpPrincipal.Enabled = false;
            ddlCustomer.Enabled = false;
            drpDocumentNo.Enabled = false;
        }
    }

    /// <summary>
    /// Clears Session Variables And Redircts To Step1
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Session.Remove("PrincipalId");
        this.Session.Remove("AreaId");
        this.Session.Remove("OrderBookerId");
        this.Session.Remove("DeliveryManId");
        Response.Redirect("~/Forms/frmOrderEntryStep1.aspx?Status=" + false + "&LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
    }

    /// <summary>
    /// Calcualtes Discount And Free SKU For Order
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        decimal ObjGrossSales = 0;
        decimal ObjStandardDiscount = 0;
        decimal ObjExtraDiscount = 0;
        decimal ObjDistShare = 0;
        decimal ObjTotalGST = 0;
        decimal ObjTotalSED = 0;
        decimal ObjTotalTST = 0;
        decimal ObjFreeGSTAmount = 0;
        decimal ObjFreeGrossAmt = 0;
        decimal ObjFreeTST = 0;
        decimal ProductExtDist = 0;
        decimal ProductStdDist = 0;
        decimal ObjTotalSKU_GST = 0;
      

        DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];
        DataRow[] foundCustomerRows = dtCustomer.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");
        if (foundCustomerRows.Length > 0)
        {
            mCustomerTypeId = int.Parse(foundCustomerRows[0]["CHANNEL_TYPE_ID"].ToString());
            mCustomerVolClassId = int.Parse(foundCustomerRows[0]["VOLUME_CLASS_ID"].ToString());
            mTownId = int.Parse(foundCustomerRows[0]["TOWN_ID"].ToString());
        }

        DataControl dc = new DataControl();
        OrderEntryController or = new OrderEntryController();
        SKUGroupController GroupCtl = new SKUGroupController();
        DataTable dt = (DataTable)this.Session["PurchaseSKU"];
        foreach (DataRow dr in dt.Rows)
        {
            dr["STANDARD_DISCOUNT_PER"] = 0;
            dr["EXTRA_DISCOUNT"] = 0;
            dr["STANDARD_DISCOUNT"] = 0;
            dr["CLAIM_EXTRA_AMOUNT"] = 0;
            dr["CLAIM_STANDARD_DISCOUNT"] = 0;
            dr["CLAIM_PER"] = 0;

        }
        PromotionCollections_Controller Arpc = (PromotionCollections_Controller)this.Session["Arpc"];
        this.CreateFreeSKU();
        this.LoadFreeGrid();

        if (dt.Rows.Count > 0)
        {
            if (ChbDiscount.Checked == true)
            {
                ArrayList ARlistOffer = or.GetPromotionOffers(Arpc, mCustomerVolClassId, mCustomerTypeId, dt, false);

                for (int i = 0; i < ARlistOffer.Count; i++)
                {
                    PromoOffers_Collection pofferCol = (PromoOffers_Collection)ARlistOffer[i];

                    if (pofferCol.Is_Claimable)
                    {
                        if (pofferCol.SKU_ID > 0)
                        {
                            #region slab on SKU
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                                {

                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + pofferCol.Offer_Value;

                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString()))) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString()))) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                            //dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString())));
                                            dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString())));
                                        }
                                    }
                                    if (pofferCol.Free_SKU_ID > 0)
                                    {
                                        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                        int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                        if (foundRows.Length > 0)
                                        {
                                            if (RowId < 0)
                                            {
                                                DataRow dr1 = dtFreeSKU.NewRow();
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];

                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }

                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                dtFreeSKU.Rows.Add(dr1);
                                            }
                                            else
                                            {

                                                DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;

                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            }

                                        }
                                    }
                                    this.Session.Add("dtFreeSKU", dtFreeSKU);
                                    this.LoadFreeGrid();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region slab on SKU Group

                            #region Free SKU
                            if (pofferCol.Free_SKU_ID > 0)
                            {
                                DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                if (foundRows.Length > 0)
                                {
                                    if (RowId < 0)
                                    {
                                        DataRow dr1 = dtFreeSKU.NewRow();
                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                        {

                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                            dr1["TST_AMOUNT"] = 0;

                                        }
                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                        }
                                        else
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                            dr1["TST_AMOUNT"] = 0;
                                        }
                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                        dtFreeSKU.Rows.Add(dr1);
                                    }
                                    else
                                    {

                                        DataRow dr1 = dtFreeSKU.Rows[RowId];
                                        ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                        ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                        dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                        {

                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                            dr1["TST_AMOUNT"] = 0;

                                        }
                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                        }
                                        else
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                            dr1["TST_AMOUNT"] = 0;
                                        }
                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                    }
                                    this.Session.Add("dtFreeSKU", dtFreeSKU);
                                    this.LoadFreeGrid();

                                }
                            }
                            #endregion

                            foreach (DataRow dr in dt.Rows)
                            {
                                if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                                {
                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (pofferCol.Offer_Value * Convert.ToInt32(dr["QUANTITY_UNIT"]));
                                            dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (pofferCol.Offer_Value * Convert.ToInt32(dr["QUANTITY_UNIT"]));
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString()))) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                            dr["CLAIM_EXTRA_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString()))) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + (pofferCol.Offer_Value * Convert.ToInt32(dr["QUANTITY_UNIT"]));
                                            //dr["CLAIM_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_AMOUNT"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString())));
                                            dr["CLAIM_PER"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString())));
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (pofferCol.SKU_ID > 0)
                        {
                            #region slab on SKU
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (int.Parse(dr["SKU_ID"].ToString()) == pofferCol.SKU_ID)
                                {

                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + pofferCol.Offer_Value;


                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString()))) * (decimal.Parse(dr["AMOUNT"].ToString()));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + pofferCol.Offer_Value;
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString())));
                                        }
                                    }
                                    if (pofferCol.Free_SKU_ID > 0)
                                    {
                                        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                        int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                        if (foundRows.Length > 0)
                                        {
                                            if (RowId < 0)
                                            {
                                                DataRow dr1 = dtFreeSKU.NewRow();
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];

                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                    dr1["TST_AMOUNT"] = 0;
                                                }

                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                                dtFreeSKU.Rows.Add(dr1);
                                            }
                                            else
                                            {

                                                DataRow dr1 = dtFreeSKU.Rows[RowId];
                                                ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                                dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                                dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                                dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                                dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;

                                                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                                {

                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                                    dr1["TST_AMOUNT"] = 0;

                                                }
                                                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                    dr1["GST_AMOUNT"] = 0;
                                                }
                                                else
                                                {
                                                    dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                                    dr1["Quantity"] = pofferCol.Quantity.ToString();
                                                    dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                                    dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                                }
                                                dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                                dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                                dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                                dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                                ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                                ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                                ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                            }

                                        }
                                    }
                                    this.Session.Add("dtFreeSKU", dtFreeSKU);
                                    this.LoadFreeGrid();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region slab on SKU Group

                            #region Free SKU
                            if (pofferCol.Free_SKU_ID > 0)
                            {
                                DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
                                DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + pofferCol.Free_SKU_ID.ToString() + "'");
                                int RowId = FreeSKUExist(dtFreeSKU, pofferCol.Free_SKU_ID);
                                if (foundRows.Length > 0)
                                {
                                    if (RowId < 0)
                                    {
                                        DataRow dr1 = dtFreeSKU.NewRow();
                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                        dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                        dr1["Quantity"] = pofferCol.Quantity.ToString();
                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                        {

                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                            dr1["TST_AMOUNT"] = 0;

                                        }
                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                        }
                                        else
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                            dr1["TST_AMOUNT"] = 0;
                                        }
                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                        dtFreeSKU.Rows.Add(dr1);
                                    }
                                    else
                                    {

                                        DataRow dr1 = dtFreeSKU.Rows[RowId];
                                        ObjFreeGrossAmt = ObjFreeGrossAmt - decimal.Parse(dr1["AMOUNT"].ToString());
                                        ObjFreeGSTAmount = ObjFreeGSTAmount - decimal.Parse(dr1["GST_AMOUNT"].ToString());
                                        dr1["SKU_ID"] = foundRows[0]["SKU_ID"];
                                        dr1["SKU_Code"] = foundRows[0]["SKU_CODE"];
                                        dr1["SKU_Name"] = foundRows[0]["SKU_NAME"];
                                        dr1["Quantity"] = int.Parse(dr1["Quantity"].ToString()) + pofferCol.Quantity;
                                        if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                                        {

                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = (decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) / 100) * decimal.Parse(dr1["AMOUNT"].ToString());
                                            dr1["TST_AMOUNT"] = 0;

                                        }
                                        else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["TST_AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["GST_RATE_TP"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                        }
                                        else
                                        {
                                            dr1["UNIT_PRICE"] = foundRows[0]["TRADE_PRICE"];
                                            dr1["Quantity"] = pofferCol.Quantity.ToString();
                                            dr1["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                                            dr1["AMOUNT"] = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString())) * decimal.Parse(pofferCol.Quantity.ToString());
                                            dr1["GST_AMOUNT"] = 0;
                                            dr1["TST_AMOUNT"] = 0;
                                        }
                                        dr1["PROMOTION_ID"] = pofferCol.Promotion_ID.ToString();
                                        dr1["BASKET_ID"] = pofferCol.Basket_ID.ToString();
                                        dr1["BASKET_DETAIL_ID"] = pofferCol.BasketDetail_ID.ToString();
                                        dr1["PROMOTION_OFFER_ID"] = pofferCol.PromoOffer_ID.ToString();
                                        ObjFreeGrossAmt += decimal.Parse(dr1["AMOUNT"].ToString());
                                        ObjFreeGSTAmount += decimal.Parse(dc.chkNull_0(dr1["GST_AMOUNT"].ToString()));
                                        ObjFreeTST += decimal.Parse(dc.chkNull_0(dr1["TST_AMOUNT"].ToString()));
                                    }
                                    this.Session.Add("dtFreeSKU", dtFreeSKU);
                                    this.LoadFreeGrid();

                                }
                            }
                            #endregion

                            foreach (DataRow dr in dt.Rows)
                            {
                                if (GroupCtl.ExistsInGroup(Constants.IntNullValue, pofferCol.Group_ID, int.Parse(dr["SKU_ID"].ToString())))
                                {
                                    if (pofferCol.Is_And == true)
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + (pofferCol.Offer_Value * Convert.ToInt32(dr["QUANTITY_UNIT"]));
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["EXTRA_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())) + ((decimal.Parse(pofferCol.Discount.ToString())) / (100 + decimal.Parse(pofferCol.Discount.ToString()))) * (decimal.Parse(dr["AMOUNT"].ToString()) - decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString())));
                                        }
                                    }
                                    else
                                    {
                                        if (pofferCol.Offer_Value > 0)
                                        {
                                            dr["STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString())) + (pofferCol.Offer_Value * Convert.ToInt32(dr["QUANTITY_UNIT"]));
                                        }
                                        if (pofferCol.Discount > 0)
                                        {
                                            dr["STANDARD_DISCOUNT_PER"] = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) + (decimal.Parse(pofferCol.Discount.ToString()) / (100 + decimal.Parse(pofferCol.Discount.ToString())));
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }

                #region Recalulate Products

                foreach (DataRow dr in dt.Rows)
                {
                    decimal TempAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));

                    #region GST Calculation
                    // dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                    // dr["SKU_GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                    dr["GST_AMOUNT"] = (TempAmount) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                    dr["SKU_GST_AMOUNT"] = ((TempAmount) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100) + decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                    ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                    ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                    ObjTotalSKU_GST += decimal.Parse(dc.chkNull_0(dr["SKU_GST_AMOUNT"].ToString()));
                    #endregion

                    dr["STANDARD_DISCOUNT"] = Convert.ToDecimal(dr["STANDARD_DISCOUNT"]) + (decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()))));
                    dr["CLAIM_STANDARD_DISCOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_PER"].ToString())) * (decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString())) - decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString())));
                    dr["SED_AMOUNT"] = decimal.Parse(dc.chkNull_0(dr["CLAIM_STANDARD_DISCOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["CLAIM_EXTRA_AMOUNT"].ToString()));
                    ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    ObjStandardDiscount += decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                    ObjDistShare += decimal.Parse(dc.chkNull_0(dr["DISTRIBUTOR_SHARE"].ToString()));
                    ObjExtraDiscount += decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                    ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));

                    

                    decimal TempDiscount = decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                    decimal TempExtraDisAmt = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                    dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt - decimal.Parse(dc.chkNull_0(dr["DISTRIBUTOR_SHARE"].ToString()))) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));

                }

                #endregion
            }
            else
            {
                #region Reversecalulate Products

                //decimal ObjGrossSales = 0;
                decimal ObjTaxableAmount = 0;
                decimal ObjClaimAblePer = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                }

                if (decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) > 0)
                {
                    ProductExtDist = decimal.Parse(dc.chkNull_0(numtxtTotalExtraDiscnt.Text)) / (ObjGrossSales - ObjTaxableAmount);
                }

                ObjGrossSales = 0;


                foreach (DataRow dr in dt.Rows)
                {
                    decimal TempAmount = 0;
                    decimal TempDiscount = 0;
                    decimal TempExtraDisAmt = 0;
                    dr["EXTRA_DISCOUNT"] = ProductExtDist * decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    dr["STANDARD_DISCOUNT"] = ProductStdDist * decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    dr["SED_AMOUNT"] = ObjClaimAblePer * decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    ObjDistShare += decimal.Parse(dc.chkNull_0(dr["DISTRIBUTOR_SHARE"].ToString()));
                    #region GST Calculation

                    TempAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    TempDiscount = ProductStdDist;//decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));
                    TempExtraDisAmt = decimal.Parse(dc.chkNull_0(dr["EXTRA_DISCOUNT"].ToString()));
                    //dr["SKU_GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                   // dr["GST_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;
                    dr["SKU_GST_AMOUNT"] =( (TempAmount ) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100)+ decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                    dr["GST_AMOUNT"] = (TempAmount ) * decimal.Parse(dc.chkNull_0(dr["GST_RATE"].ToString())) / 100;

                    dr["NET_AMOUNT"] = (TempAmount - TempDiscount - TempExtraDisAmt - decimal.Parse(dc.chkNull_0(dr["DISTRIBUTOR_SHARE"].ToString()))) + decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString())) + decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                    ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));
                    ObjTotalGST += decimal.Parse(dc.chkNull_0(dr["GST_AMOUNT"].ToString()));
                    ObjTotalTST += decimal.Parse(dc.chkNull_0(dr["TST_AMOUNT"].ToString()));
                    ObjTotalSED += decimal.Parse(dc.chkNull_0(dr["SED_AMOUNT"].ToString()));
                    ObjTotalSKU_GST += decimal.Parse(dc.chkNull_0(dr["SKU_GST_AMOUNT"].ToString()));
                      ObjStandardDiscount += ProductStdDist;//decimal.Parse(dc.chkNull_0(dr["STANDARD_DISCOUNT"].ToString()));                                                
                    #endregion
                }
                #endregion
            }

            #region Set Total Values

            txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
            txtSKuGst.Text = Convert.ToString(Math.Round(ObjTotalSKU_GST, 2));
            txtAmountINclSKuGST.Text = Convert.ToString(Math.Round((ObjGrossSales + ObjTotalSKU_GST), 2));
            numtxtTotalExtraDiscnt.Text = Convert.ToString(Math.Round(ObjExtraDiscount, 2));
            numTxtTotalStndrdDiscnt.Text = Convert.ToString(Math.Round(ObjStandardDiscount, 2));
            numtxtTotalDistShare.Text = Convert.ToString(Math.Round(ObjDistShare, 2));
           // numTxtTotalGST.Text = Convert.ToString(Math.Round(ObjTotalGST + ObjFreeGSTAmount, 2));
           // numTxtTotalTST.Text = Convert.ToString(Math.Round(ObjTotalTST + ObjFreeTST, 2));
            decimal dNemtAmount = Math.Round(ObjGrossSales - ObjStandardDiscount - ObjExtraDiscount + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST + ObjFreeTST, 2);
            //numtxtSpecialDiscValue.Text = Convert.ToString(dNemtAmount * Convert.ToDecimal(dc.chkNull_0(numtxtSpecialDiscPr.Text)) / 100);
            numtxtBuiltyAmount.Text = txtBuiltyAmount.Text;
            numTxtTotlAmnt.Text = Convert.ToString(Math.Round(ObjGrossSales  - ObjStandardDiscount - ObjExtraDiscount - ObjDistShare + ObjTotalGST + ObjFreeGSTAmount + ObjTotalTST + ObjFreeTST, 2) - Convert.ToDecimal(dc.chkNull_0(numtxtBuiltyAmount.Text)));

            #endregion

            btnSaveOrder.Enabled = true;
            if (RblPayMode.SelectedIndex == 1)
            {
                txtCashReceived.Enabled = true;
                ScriptManager.GetCurrent(Page).SetFocus(txtCashReceived);
            }
            else
            {
                txtCashReceived.Enabled = false;
            }
        }
    }

    /// <summary>
    /// Adds/Updates Row To Order Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal discount = 0;
        DataControl dc = new DataControl();
        if (ddlSkus.SelectedValue == Constants.IntNullValue.ToString())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please select Product')", true);
            return;
        }
        if (btnSaveOrder.Text != "Sale Return")
        {
            if (int.Parse(dc.chkNull_0(txtQuantity.Text)) > int.Parse(txtStock.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Quantity is greater then available stock')", true);
                return;
            }
        }
        if (txtDistShare.Text.Length > 0)
        {

            if (drpdisType.SelectedValue == "0")
            {
                if (Convert.ToDecimal(dc.chkNull_0(txtDistShare.Text)) > Convert.ToDecimal(lblShareValue.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Distributor Share Value can not be greater than " + lblShareValue.Text + "')", true);
                    return;
                }
            }
            else
            {
                if (Convert.ToDecimal(dc.chkNull_0(txtDistShare.Text)) > Convert.ToDecimal(lblSharePer.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Distributor Share Per can not be greater than " + lblSharePer.Text + "')", true);
                    return;
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter Distributor Share')", true);
            txtDistShare.Focus();
            return;
        }
        DataTable dtskuPrice = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = dtskuPrice.Select("SKU_ID  = '" + ddlSkus.SelectedValue + "'");        
        decimal mTradePrice = decimal.Parse(dc.chkNull_0(foundRows[0]["TRADE_PRICE"].ToString()));
        if (txtDiscount.Text != "")
        {
            discount = Convert.ToDecimal(dc.chkNull_0(txtDiscount.Text)) / 100;
        }
        else
        {
            txtDiscount.Text = "0";
        }
        int mPackSize = int.Parse(dc.chkNull_0(foundRows[0]["UNITS_IN_CASE"].ToString()));
        if (btnSave.Text == "Add")
        {
            if (CheckDublicateSKU())
            {
                DataRow dr = PurchaseSKU.NewRow();
                dr["CATEGORY_ID"] = foundRows[0]["CATEGORY_ID"];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                if (UnitType == 0)
                {
                    dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["QUANTITY_CTN"] = 0;
                }
                else
                {
                    dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;
                    dr["QUANTITY_CTN"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                }
                dr["UNIT_PRICE"] = mTradePrice.ToString();
                discount = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * discount;
                if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());// -discount;
                    dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                    dr["TST_AMOUNT"] = 0;
                    dr["SKU_GST"] = foundRows[0]["SKU_GST"];
                    dr["BATCH_NO"] = "T";
                }
                else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());//- discount;
                    dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                    dr["GST_RATE"] = 0;
                    dr["SKU_GST"] = foundRows[0]["SKU_GST"];
                    dr["BATCH_NO"] = "R";
                }
                else
                {
                    dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());//- discount;
                    dr["TST_AMOUNT"] = 0;
                    dr["GST_RATE"] = 0;
                    dr["BATCH_NO"] = "E";
                    dr["SKU_GST"] = foundRows[0]["SKU_GST"];
                }
                dr["STANDARD_DISCOUNT"] = decimal.Parse(txtDiscount.Text);
                if (drpdisType.SelectedValue == "0")
                {
                    dr["DISTRIBUTOR_SHARE"] = decimal.Parse(dc.chkNull_0(txtDistShare.Text)) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                }
                else
                {
                    dr["DISTRIBUTOR_SHARE"] = (decimal.Parse(dc.chkNull_0(txtDistShare.Text)) * decimal.Parse(dr["AMOUNT"].ToString())) / 100;
                }
                dr["EXTRA_DISCOUNT"] = 0;
                dr["GST_AMOUNT"] = 0;
                dr["NET_AMOUNT"] = 0;

                PurchaseSKU.Rows.InsertAt(dr,0);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('SKU Already Exists')", true);
            }
        }
        else
        {
            DataRow dr = PurchaseSKU.Rows[RowId];
            dr["CATEGORY_ID"] = foundRows[0]["CATEGORY_ID"];
            dr["SKU_ID"] = foundRows[0]["SKU_ID"];
            dr["SKU_Code"] = foundRows[0]["SKU_CODE"];
            dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
            dr["BATCH_NO"] = "";
            if (UnitType == 0)
            {
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
            }
            else
            {
                dr["QUANTITY_UNIT"] = int.Parse(dc.chkNull_0(txtQuantity.Text)) * mPackSize;
            }
            dr["UNIT_PRICE"] = mTradePrice.ToString();
            discount = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * discount;
            dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());// -discount;
            if (foundRows[0]["GST_ON"].ToString().Trim() == "T")
            {
                dr["GST_RATE"] = foundRows[0]["GST_RATE_TP"];
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());//- discount;
                dr["TST_AMOUNT"] = 0;
                dr["BATCH_NO"] = "T";
            }
            else if (foundRows[0]["GST_ON"].ToString().Trim() == "R")
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());//- discount;
                dr["TST_AMOUNT"] = decimal.Parse(foundRows[0]["GST_RATE_TP"].ToString()) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                dr["GST_RATE"] = 0;
                dr["BATCH_NO"] = "R";
            }
            else
            {
                dr["AMOUNT"] = mTradePrice * decimal.Parse(dr["QUANTITY_UNIT"].ToString());// - discount;
                dr["TST_AMOUNT"] = 0;
                dr["GST_RATE"] = 0;
                dr["BATCH_NO"] = "E";
            }
            dr["STANDARD_DISCOUNT"] = decimal.Parse(txtDiscount.Text);
            if (drpdisType.SelectedValue == "0")
            {
                dr["DISTRIBUTOR_SHARE"] = decimal.Parse(dc.chkNull_0(txtDistShare.Text)) * decimal.Parse(dr["QUANTITY_UNIT"].ToString());
            }
            else
            {
                dr["DISTRIBUTOR_SHARE"] =( decimal.Parse(dc.chkNull_0(txtDistShare.Text)) * decimal.Parse(dr["AMOUNT"].ToString()))/100;
            }

            dr["EXTRA_DISCOUNT"] = 0;
            dr["GST_AMOUNT"] = 0;
            dr["NET_AMOUNT"] = 0;
            dr["STANDARD_DISCOUNT_PER"] = 0;
            dr["SKU_GST"] = foundRows[0]["SKU_GST"];
            dr["SKU_GST_AMOUNT"] = (decimal.Parse(dr["AMOUNT"].ToString()) * decimal.Parse(dr["SKU_GST"].ToString())) / 100;

        }
        this.Session.Add("PurchaseSKU", PurchaseSKU);
        this.EnableDisableController(false);
        this.LoadGird();
        this.ClearAll();
        ScriptManager.GetCurrent(Page).SetFocus(ddlSkus);
        if (ChbDiscount.Checked == false)
        {
            decimal ObjGrossSales = 0;
            decimal ObjTaxableAmount = 0;
            foreach (DataRow dr in PurchaseSKU.Rows)
            {
                ObjGrossSales += decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));//-discount;
                if (dr["BATCH_NO"].ToString() == "T")
                {
                    ObjTaxableAmount = decimal.Parse(dc.chkNull_0(dr["AMOUNT"].ToString()));//-discount;
                }
            }
            txtGrossAmount.Text = Convert.ToString(Math.Round(ObjGrossSales, 2));
            numTxtTotalSED.Text = Convert.ToString(Math.Round(ObjTaxableAmount, 2));
        }
    }

    /// <summary>
    /// Saves/Updates Order, Invoice And Sale Return
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
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
            if (ddlTransporter.Items.Count > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Transporter not Exists !');", true);
                return;
            }
            string ManualID = null;
            DataControl DC = new DataControl();
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            dtFreeSKU = (DataTable)this.Session["dtFreeSKU"];
            OrderEntryController mOrderController = new OrderEntryController();
            string Transporter = null;
            if (ddlTransporter.Items.Count > 0)
            {
                if (ddlTransporter.SelectedValue != Constants.IntNullValue.ToString())
                {
                    Transporter = ddlTransporter.SelectedItem.Text;
                }
            }
            if (btnSaveOrder.Text == "Save Order")
            {
                bool IsValidInsert = mOrderController.Add_Order(int.Parse(this.Session["DistributorId"].ToString()), ManualID, mTownId, (long)this.Session["AreaId"], int.Parse(this.Session["PrincipalId"].ToString()), long.Parse(ddlCustomer.SelectedValue), long.Parse(ddlCustomer.SelectedValue), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()),
                    decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)),0, decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, Constants.Order_Pending_Id, PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), DateTime.Parse(this.Session["OrderDate"].ToString()), 0,
                    0, 0, txtRemarks.Text, txtBuiltyNo.Text, Convert.ToDecimal(DC.chkNull_0(txtBuiltyAmount.Text)), Transporter, Convert.ToDecimal(DC.chkNull_0(numtxtTotalDistShare.Text)), int.Parse(ddlTransporter.SelectedValue), decimal.Parse(DC.chkNull_0(txtSKuGst.Text)));
                if (IsValidInsert)
                {
                    this.LoadPendingOrder();
                    this.ClearMasterALL();
                    LoadSkuDetail();
                }
            }
            else if (btnSaveOrder.Text == "Save Invoice")
            {
                if (Convert.ToDateTime(this.Session["CurrentWorkDate"]) >= Convert.ToDateTime(Session["OrderDate"].ToString()))
                {
                    if (int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Credit_Order_Id || int.Parse(RblPayMode.SelectedValue.ToString()) == Constants.Advance_PaymentOrder_id)
                    {
                        CustomerDataController CDC = new CustomerDataController();
                        DataTable dt = CDC.SelectCustomerCreditBalance(long.Parse(ddlCustomer.SelectedValue), int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(this.Session["DistributorId"].ToString()), int.Parse(RblPayMode.SelectedValue.ToString()));
                        decimal NetCashSale = decimal.Parse(numTxtTotlAmnt.Text) - decimal.Parse(DC.chkNull_0(txtCashReceived.Text));
                    }

                    foreach (DataRow dr in PurchaseSKU.Rows)
                    {
                        PhaysicalStockController mController = new PhaysicalStockController();
                        DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(dr["SKU_ID"].ToString()), null, DateTime.Parse(this.Session["OrderDate"].ToString()));
                        if (dtstock.Rows.Count > 0)
                        {
                            if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dr["QUANTITY_UNIT"].ToString()))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                                return;
                            }
                            //else if (Convert.ToDecimal(dtstock.Rows[0][1]) != Convert.ToDecimal(dr["UNIT_PRICE"]))
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Order Rate is " + dr["UNIT_PRICE"].ToString() + " and Current Rate is " + dtstock.Rows[0][1].ToString() + "');", true);
                            //    return;
                            //}
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + "No Stock Found');", true);
                            return;
                        }
                    }

                    bool IsValidInsert = mOrderController.Add_Invoice(int.Parse(this.Session["DistributorId"].ToString()), ManualID, mTownId, long.Parse(this.Session["AreaId"].ToString()), int.Parse(this.Session["PrincipalId"].ToString()), long.Parse(ddlCustomer.SelectedValue), long.Parse(ddlCustomer.SelectedValue),
                        int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), long.Parse(drpDocumentNo.SelectedValue.ToString()), decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)),
                       0, decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()), PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()), decimal.Parse(DC.chkNull_0(txtCashReceived.Text)), DateTime.Parse(this.Session["OrderDate"].ToString()),
                        0, 0, 0, txtRemarks.Text, txtBuiltyNo.Text, Convert.ToDecimal(DC.chkNull_0(txtBuiltyAmount.Text)), Transporter, Convert.ToDecimal(DC.chkNull_0(numtxtTotalDistShare.Text)), int.Parse(ddlTransporter.SelectedValue), decimal.Parse(DC.chkNull_0(txtSKuGst.Text)));
                    if (IsValidInsert)
                    {
                        if (drpDocumentNo.Enabled == true)
                        {
                            btnSaveOrder.Text = "Save Invoice";
                        }
                        this.ClearMasterALL();
                        LoadSkuDetail();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Order Date Can not be greater than current working date');", true);
                }
            }
            else if (btnSaveOrder.Text == "Update Invoice")
            {
                foreach (DataRow dr in PurchaseSKU.Rows)
                {

                    PhaysicalStockController mController = new PhaysicalStockController();

                    DataTable dtstock = mController.SelectSKUClosingStock(int.Parse(this.Session["DistributorId"].ToString()), int.Parse(dr["SKU_ID"].ToString()), null, DateTime.Parse(this.Session["OrderDate"].ToString()));
                    if (dtstock.Rows.Count > 0)
                    {
                        if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dr["QUANTITY_UNIT"].ToString()))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                            return;
                        }
                        //else if (Convert.ToDecimal(dtstock.Rows[0][1]) != Convert.ToDecimal(dr["UNIT_PRICE"]))
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Code"].ToString() + " Order Rate is " + dr["UNIT_PRICE"].ToString() + " and Current Rate is " + dtstock.Rows[0][1].ToString() + "');", true);
                        //    return;
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Code"].ToString() + "No Stock Found');", true);
                        return;
                    }
                }
                bool IsValidInsert = mOrderController.Update_Invoice(Convert.ToInt64(drpDocumentNo.SelectedValue), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(this.Session["DistributorId"].ToString()), ManualID, long.Parse(ddlCustomer.SelectedValue), 0,
                   decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)), 0, decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)),
                   Constants.DecimalNullValue, int.Parse(RblPayMode.SelectedValue.ToString()), PurchaseSKU, int.Parse(this.Session["UserId"].ToString()), 0, 0, 0, 0, 0, Constants.DateNullValue, 0
                   , txtRemarks.Text, txtBuiltyNo.Text, Convert.ToDecimal(DC.chkNull_0(txtBuiltyAmount.Text)), Transporter, Convert.ToDecimal(DC.chkNull_0(numtxtTotalDistShare.Text)), int.Parse(this.Session["PrincipalId"].ToString()), int.Parse(ddlTransporter.SelectedValue));
                if (IsValidInsert)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invoice updated successfully.');", true);
                    this.LoadInvoiceToEdit();
                    this.ClearMasterALL();
                    btnSaveOrder.Text = "Save Invoice";
                }
            }
            else if (btnSaveOrder.Text == "Sale Return")
            {
                bool IsValidInsert = mOrderController.Add_SaleReturn(int.Parse(this.Session["DistributorId"].ToString()), mTownId, (long)this.Session["AreaId"], int.Parse(this.Session["PrincipalId"].ToString()), long.Parse(ddlCustomer.SelectedValue), long.Parse(ddlCustomer.SelectedValue), int.Parse(this.Session["OrderBookerId"].ToString()), int.Parse(this.Session["DeliveryManId"].ToString()), long.Parse("0"),
                decimal.Parse(DC.chkNull_0(txtGrossAmount.Text)), decimal.Parse(DC.chkNull_0(numtxtTotalExtraDiscnt.Text)), decimal.Parse(DC.chkNull_0(numTxtTotalStndrdDiscnt.Text)),0, decimal.Parse(DC.chkNull_0(numTxtTotlAmnt.Text)), 0, int.Parse(RblPayMode.SelectedValue.ToString()), PurchaseSKU, dtFreeSKU, int.Parse(this.Session["UserId"].ToString()),
                DateTime.Parse(this.Session["CurrentWorkDate"].ToString()),0, 0, txtRemarks.Text);
                if (IsValidInsert)
                {
                    this.LoadPendingOrder();
                    this.ClearMasterALL();
                    LoadSkuDetail();
                    btnSaveOrder.Text = "Sale Return";
                }
            }
        }
    }

    /// <summary>
    /// Clears Some Of Controls
    /// </summary>
    private void ClearAll()
    {

        txtUnitRate.Text = "";
        txtQuantity.Text = "";
        txtDiscount.Text = "0";
        btnSave.Text = "Add";
        btnSaveOrder.Enabled = false;
        btnCalculate.Enabled = true;
        ddlSkus.Enabled = true;

    }

    /// <summary>
    /// Clears All Controls
    /// </summary>
    private void ClearMasterALL()
    {
        this.EnableDisableController(true);
        this.Session.Remove("PurchaseSKU");
        this.Session.Remove("dtFreeSKU");
        btnSaveOrder.Enabled = false;
        this.CreatTable();
        this.CreateFreeSKU();
        ddlCustomer.SelectedIndex = 0;
        this.LoadGird();
        this.LoadFreeGrid();
        txtGrossAmount.Text = "";
        numtxtTotalExtraDiscnt.Text = "";
        numtxtTotalDistShare.Text = string.Empty;
        numTxtTotalStndrdDiscnt.Text = "";
       // numTxtTotalGST.Text = "";
        numTxtTotlAmnt.Text = "";
        numTxtTotalSED.Text = "";
     //   numTxtTotalTST.Text = "";
        numTxtTotalSED.Text = "";
        txtStock.Text = "";
        txtDistShare.Text = string.Empty;
        RowId = 0;
        mCustomerTypeId = 0;
        mCustomerVolClassId = 0;
        drpDocumentNo.SelectedIndex = 0;
        txtCashReceived.Text = "";
        txtRemarks.Text = string.Empty;
        txtBuiltyNo.Text = string.Empty;
        txtBuiltyAmount.Text = string.Empty;
        txtSKuGst.Text = string.Empty;
        txtAmountINclSKuGST.Text = string.Empty;
        ddlTransporter.SelectedIndex = 0;
        if (btnSaveOrder.Text != "Sale Return")
        {
            ScriptManager.GetCurrent(Page).SetFocus(txtBuiltyNo);
        }
        else
        {
            ScriptManager.GetCurrent(Page).SetFocus(ddlCustomer);
        }
    }
    private bool IsDayClosed()
    {
        bool flag = false;
        DistributorController DistrCtl = new DistributorController();
        DataTable dtDayClose = DistrCtl.MaxDayClose(Convert.ToInt32(Session["DistributorId"]), 3);
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
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session.Add("PrincipalId", int.Parse(DrpPrincipal.SelectedValue.ToString()));
        this.LoadSkuDetail();
        this.LoadPromotion();
        LoadPrincipalWisediscount();
    }

    protected void CustomerDetail()
    {
        var dtCustomer = (DataTable)this.Session["dtCustomer"];
        if (dtCustomer != null && dtCustomer.Rows.Count > 0)
        {
            DataRow[] foundRows = dtCustomer.Select("CUSTOMER_ID  = '" + ddlCustomer.SelectedValue + "'");
            txtDiscountType.Text = foundRows[0]["CHANNEL"].ToString();
            txtPriceGroup.Text = foundRows[0]["PRICE_GROUP"].ToString();
        }
        else
        {
            txtDiscountType.Text = "";
            txtPriceGroup.Text = "";
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomerDetail();
        LoadSkuDetail();
    }
}