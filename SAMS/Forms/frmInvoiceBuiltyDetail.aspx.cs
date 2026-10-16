using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// Form To Add, Edit Freight
/// </summary>
public partial class Forms_frmInvoiceBuiltyDetail : Page
{
    readonly LedgerController _lController = new LedgerController();
    readonly DistributorController _dController = new DistributorController();
    readonly UserController _userCtl = new UserController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDistributor();
            LoadPrincipal();
            LoadCustomer();
            LoadTransporter();
            LoadGrid();
            DateTime pOrderDate = DateTime.Parse(Session["CurrentWorkDate"].ToString());
            txtBuiltyDate.Text = pOrderDate.ToString("dd/MM/yyyy");
            
        }
    }

    #region Load

    /// <summary>
    /// Loads Transporter To Transporter Combo
    /// </summary>
    private void LoadTransporter()
    {
        DataTable dt = _dController.SelectTranspoter();
        clsWebFormUtil.FillDropDownList(DrpTransporter, dt, 0, 1);
          
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
       
        DataTable dt = _dController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
    }
    
    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpPrincipal, m_dt, 0, 1);
    }
    
    /// <summary>
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadCustomer()
    {
        if (drpDistributor.Items.Count > 0)
        {
            CustomerDataController mController = new CustomerDataController();
            DataTable dtCustomer = mController.SelectPrincipalCustomer(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpPrincipal.SelectedValue.ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            string[] colums = new string[] { "CUSTOMER_ID", "CUSTOMER_DETAIL"};
            DataTable dtCustomer2 = dtCustomer.DefaultView.ToTable("dtCustomer2", true, colums);
            
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, dtCustomer2, 0, 1, true);
            
            if (dtCustomer.Rows.Count > 0)
            {
                DrpInvoiceNo.Items.Clear();   
                DataRow[] foundRows = dtCustomer.Select("Customer_id  = " + DrpCustomer.SelectedValue);
                for (int i = 0; i < foundRows.Length; i++)
                {
                    DrpInvoiceNo.Items.Add(new ListItem(foundRows[i]["MANUAL_INVOICE_ID"].ToString(), foundRows[i]["Sale_Invoice_Id"].ToString()));

                }
                Session.Add("dtCustomer", dtCustomer);
            }
        }
        else
        {
            DrpCustomer.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads Freight Data To Grid
    /// </summary>
    private void LoadGrid()
    {
        if (drpDistributor.Items.Count > 0)
        {
            
            DataTable dt = _lController.SelectBankCashTransction(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.Builty_Exp ,
                DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()),Constants.IntNullValue);
            GrdOrder.EditIndex = -1;
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
    }

    #endregion

    #region Sel/Index Change

    /// <summary>
    /// Loads Invoices To Invoice Combo
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCustomer = (DataTable)this.Session["dtCustomer"];

        DrpInvoiceNo.Items.Clear();

        DataRow[] foundRows = dtCustomer.Select("Customer_id  = " + DrpCustomer.SelectedValue);

        for (int i = 0; i < foundRows.Length; i++)
        {
            DrpInvoiceNo.Items.Add(new ListItem(foundRows[i]["MANUAL_INVOICE_ID"].ToString(), foundRows[i]["Sale_Invoice_Id"].ToString()));
        }
    }

    /// <summary>
    /// Loads Customers And Freight Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
        LoadGrid();
    }

    /// <summary>
    /// Loads Customers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }

    #endregion

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        if (IsDayClosed())
        {

            _userCtl.InsertUserLogoutTime(Convert.ToInt32(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (btnAddNew.Text == "Save")
            {
                //

                DataTable dt = _lController.SelectBankCashTransction(
                    int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, Constants.Builty_Exp,
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()),
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()),Constants.IntNullValue);

                DataRow[] foundRows = dt.Select("Document_no  = " + DrpInvoiceNo.SelectedValue);
                if (foundRows.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg",
                                                        "alert('This Invoice No already exist,Kindly select different Invoice No');",
                                                        true);
                    return;
                }
                //
                DateTime builtyDate = new DateTime();

                if (txtBuiltyDate.Text.Length == 10)
                {
                    builtyDate = DateTime.Parse(ConvertDate.British_To_American(txtBuiltyDate.Text));

                }

                Configuration.GetAccountHead();

                string maxDocumentId = _lController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher,
                                                                              int.Parse(drpDistributor.SelectedValue));

                _lController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(maxDocumentId),
                                                      long.Parse(Configuration.BuiltyExp),
                                                      int.Parse(drpDistributor.SelectedValue),
                                                      decimal.Parse(txtAmount.Text), 0,
                                                      DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                      "Builty : " + txtRemarks.Text, builtyDate,
                                                      int.Parse(DrpCustomer.SelectedValue),
                                                      int.Parse(DrpPrincipal.SelectedValue),
                                                      null, int.Parse(Session["UserId"].ToString()),
                                                      int.Parse(DrpInvoiceNo.SelectedValue),
                                                      DrpInvoiceNo.SelectedItem.Text, Constants.Document_Invoice, null,
                                                      Constants.DateNullValue, Constants.Builty_Exp,
                                                      Convert.ToInt32(DrpTransporter.SelectedValue));

                Clear();
            }
            else if(btnAddNew .Text =="Update")
            {
                DataControl dc = new DataControl();
                _lController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue), int.Parse(dc.chkNull_0(hfDocument_type.Value)),
                  int.Parse(dc.chkNull_0(hfVoucherNo.Value)), Constants.LongNullValue, decimal.Parse(dc.chkNull_0(hfTotalAmount.Value)));

                DateTime builtyDate = new DateTime();

                if (txtBuiltyDate.Text.Length == 10)
                {
                    builtyDate = DateTime.Parse(ConvertDate.British_To_American(txtBuiltyDate.Text));

                }

                Configuration.GetAccountHead();

                string maxDocumentId = _lController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher,
                                                                              int.Parse(drpDistributor.SelectedValue));

                _lController.PostingCash_Bank_Account(Constants.Journal_Voucher, long.Parse(maxDocumentId),
                                                      long.Parse(Configuration.BuiltyExp),
                                                      int.Parse(drpDistributor.SelectedValue),
                                                      decimal.Parse(txtAmount.Text), 0,
                                                      DateTime.Parse(Session["CurrentWorkDate"].ToString()),
                                                      txtRemarks.Text, builtyDate,
                                                      int.Parse(DrpCustomer.SelectedValue),
                                                      int.Parse(DrpPrincipal.SelectedValue),
                                                      null, int.Parse(Session["UserId"].ToString()),
                                                      int.Parse(DrpInvoiceNo.SelectedValue),
                                                      DrpInvoiceNo.SelectedItem.Text, Constants.Document_Invoice, null,
                                                      Constants.DateNullValue, Constants.Builty_Exp,
                                                      Convert.ToInt32(DrpTransporter.SelectedValue));

                Clear();
            }
        }
    }

    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        DataControl dc = new DataControl();

        _lController.DeleteWareHouseLedger(int.Parse(drpDistributor.SelectedValue), int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[2].Text)),
        int.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[7].Text)), Constants.LongNullValue, decimal.Parse(dc.chkNull_0(GrdOrder.Rows[e.RowIndex].Cells[8].Text)));
        LoadGrid();
        
    }
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DrpPrincipal.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;
        DrpPrincipal.Enabled = false;
        drpDistributor.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;
        drpDistributor.Enabled = false;
        DrpTransporter.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[11].Text;
        DrpCustomer.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
        txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
        hfdocument_No.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        hfDocument_type.Value = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
        hfVoucherNo.Value = GrdOrder.Rows[e.NewEditIndex].Cells[7].Text;
        hfTotalAmount .Value= GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
        DrpInvoiceNo.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[12].Text;

       // DateTime pOrderDate = DateTime.Parse(GrdOrder.Rows[e.NewEditIndex].Cells[6].Text);
       // txtBuiltyDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[6].Text; 
        btnAddNew.Text = "Update";
        for (int i = 0; i < GrdOrder.Rows.Count; i++)
        {
            GrdOrder.Rows[i].Cells[14].Enabled = false;
        }

    }

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

    private void Clear()
    {
        LoadGrid();
        txtAmount.Text = "";
        txtRemarks.Text = "";
        DateTime pOrderDate = DateTime.Parse(Session["CurrentWorkDate"].ToString());
        txtBuiltyDate.Text = pOrderDate.ToString("dd/MM/yyyy");
        btnAddNew.Text = "Save";
        DrpPrincipal.Enabled = true;
        drpDistributor.Enabled = true;
    }
}
