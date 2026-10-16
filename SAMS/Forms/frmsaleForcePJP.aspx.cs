using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From To Assign/UnAssign Rotes And Principals To Sale Force
/// </summary>
public partial class Forms_frmsaleForcePJP : System.Web.UI.Page
{
    SaleForceController UController = new SaleForceController();
    SLASHCodesController mController = new SLASHCodesController();
    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDISTRIBUTOR();
            this.LoadSaleForce();
            this.LoadArea();
            this.loadPJPGrid();
            drpDistributor.Focus();
            SAMSCommon.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = SAMSCommon.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            cbAlternate_CheckedChanged(null,null);
            this.LoadDays();
        }
    }

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDISTRIBUTOR()
    {
        //    DistributorController mController = new DistributorController();
        //    DataTable dtDistributor = mController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        //    clsWebFormUtil.FillDropDownList(drpDistributor, dtDistributor, 0, 2, true);
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        //DistributorController mController = new DistributorController();
       // DataTable dtMasterDistributor = mController.SelectDistributorInfoByTownbyUser(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 7);
        clsWebFormUtil.FillDropDownList(drpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);

    }

    /// <summary>
    /// Loads Designations To Designation Combo
    /// </summary>

    /// <summary>
    /// Loads Sale Forces To Sale Force Combo
    /// </summary>
    protected void LoadSaleForce()
    {
        if (drpDistributor.Items.Count > 0)
        {
            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectDistributorUser((Constants.SALES_FORCE_ORDERBOOKER), int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.DrpSaleForce, dt, 0, 6, true);
        }
    }

    /// <summary>
    /// Loads Routes To Section ListBox
    /// </summary>
    private void LoadArea()
    {
        if (drpDistributor.Items.Count > 0)
        {
            DataTable dtArea = mController.SelectSalesForceAreaPJP(int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(DrpSaleForce.SelectedValue.ToString()), Constants.LongNullValue);
            DistributorAreaController gController = new DistributorAreaController();
            clsWebFormUtil.FillDropDownList(ddlSection, dtArea, "AREA_ID", "AREA_NAME", true);
        }
    }

    /// <summary>
    /// Checks/UnChecks Section ListBox And Principal ListBox According To Assinged/UnAssinged
    /// </summary>


    /// <summary>
    /// Loads Sale Forces To Sale Force Combo, Routes To Section Combo And Checks/UnChecks Section ListBox And Principal ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSaleForce();
        this.LoadArea();
        loadPJPGrid();
        this.UncheckSelectAll();
    }


    /// <summary>
    /// Checks/UnChecks Section ListBox And Principal ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpSaleForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadArea();
        loadPJPGrid();
        this.UncheckSelectAll();
    }

    /// <summary>
    /// Inserts Routes And Principal Assignment
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int datetype = 1;
        int countchk = 0;
        DateTime dtFrom = Convert.ToDateTime(txtStartDate.Text);
        DateTime dtTo = Convert.ToDateTime(txtEndDate.Text);
        if (cbAlternate.Checked)
        {
            if (rblAlternate.SelectedValue == "1")
            {
                datetype = 2;
            }
            else
            {
                datetype = 3;
            }
        }

        for (int j = 0; j < cblDate.Items.Count; j++)
        {
            if (cblDate.Items[j].Selected == true)
            {
                long retchk = mController.setPJP(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpSaleForce.SelectedValue), datetype, dtFrom, dtTo, int.Parse(Session["UserId"].ToString()), true, 2);
                if (retchk > 0)
                {
                    if (mController.setPJPDetail(retchk, Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(cblDate.Items[j].Value)) > 0)
                    {
                        countchk++;
                    }
                }
            }
        }

        if (countchk > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Inserted !')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error Occured !')", true);
        }
        LoadArea();
        loadPJPGrid();
        UncheckSelectAll();
    }

    /// <summary>
    /// Uncheks "Select All" CheckBoxes
    /// </summary>
    private void UncheckSelectAll()
    {
        ChPrincipal.Checked = false;
    }
    protected void grdPJP_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grdPJP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int msterId = int.Parse(grdPJP.Rows[e.RowIndex].Cells[0].Text);
        long chk = mController.setPJP(int.Parse(drpDistributor.SelectedValue), msterId,
           Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"),
             DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(Session["UserId"].ToString())
             , false, 3);
        if (chk > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Deleted !')", true);
        }
        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error Occured !')", true); }
        LoadArea();
        loadPJPGrid();
        UncheckSelectAll();
    }
    private void loadPJPGrid()
    {

        int datetype = 1;
        if (cbAlternate.Checked)
        {
            if (rblAlternate.SelectedValue == "1")
            {
                datetype = 2;
            }
            else
            {
                datetype = 3;
            }
        }
        DataTable dt = mController.getPJP(int.Parse(drpDistributor.SelectedValue), int.Parse(DrpSaleForce.SelectedValue),
            datetype, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"),
            int.Parse(Session["UserId"].ToString()), true, Constants.IntNullValue, Constants.IntNullValue, 1);
        grdPJP.EditIndex = -1;
        grdPJP.DataSource = dt;
        grdPJP.DataBind();
    }

    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        LoadDays();
    }

    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        LoadDays();
    }

    protected void cbAlternate_CheckedChanged(object sender, EventArgs e)
    {
        rblAlternate.Visible = false;
        txtStartDate.Enabled = true;
        txtEndDate.Enabled = true;
        if (cbAlternate.Checked)
        {
            rblAlternate.Visible = true;
            txtStartDate.Enabled = false;
            txtEndDate.Enabled = false;
        }
        LoadDays();
    }

    private void LoadDays()
    {
        cblDate.Items.Clear();
        if (cbAlternate.Checked)
        {
            if (rblAlternate.SelectedValue == "1")
            {
                cblDate.Items.Add(new ListItem("Monday", "2"));
                cblDate.Items.Add(new ListItem("Tuesday", "3"));
                cblDate.Items.Add(new ListItem("Wednesday", "4"));
                cblDate.Items.Add(new ListItem("Thrusday", "5"));
                cblDate.Items.Add(new ListItem("Friday", "6"));
                cblDate.Items.Add(new ListItem("Saturday", "7"));
                cblDate.Items.Add(new ListItem("Sunday", "1"));
            }
            else
            {
                cblDate.Items.Add(new ListItem("1", "1"));
                cblDate.Items.Add(new ListItem("2", "2"));
                cblDate.Items.Add(new ListItem("3", "3"));
                cblDate.Items.Add(new ListItem("4", "4"));
                cblDate.Items.Add(new ListItem("5", "5"));
                cblDate.Items.Add(new ListItem("6", "6"));
                cblDate.Items.Add(new ListItem("7", "7"));
                cblDate.Items.Add(new ListItem("8", "8"));
                cblDate.Items.Add(new ListItem("9", "9"));
                cblDate.Items.Add(new ListItem("10", "10"));
                cblDate.Items.Add(new ListItem("11", "11"));
                cblDate.Items.Add(new ListItem("12", "12"));
                cblDate.Items.Add(new ListItem("13", "13"));
                cblDate.Items.Add(new ListItem("14", "14"));
                cblDate.Items.Add(new ListItem("15", "15"));
                cblDate.Items.Add(new ListItem("16", "16"));
                cblDate.Items.Add(new ListItem("17", "17"));
                cblDate.Items.Add(new ListItem("18", "18"));
                cblDate.Items.Add(new ListItem("19", "19"));
                cblDate.Items.Add(new ListItem("20", "20"));
                cblDate.Items.Add(new ListItem("21", "21"));
                cblDate.Items.Add(new ListItem("22", "22"));
                cblDate.Items.Add(new ListItem("23", "23"));
                cblDate.Items.Add(new ListItem("24", "24"));
                cblDate.Items.Add(new ListItem("25", "25"));
                cblDate.Items.Add(new ListItem("26", "26"));
                cblDate.Items.Add(new ListItem("27", "27"));
                cblDate.Items.Add(new ListItem("28", "28"));
                cblDate.Items.Add(new ListItem("29", "29"));
                cblDate.Items.Add(new ListItem("30", "30"));
                cblDate.Items.Add(new ListItem("31", "31"));
            }
        }
        else
        {
            int i = 1;
            for (DateTime counter = Convert.ToDateTime(txtStartDate.Text); counter <= Convert.ToDateTime(txtEndDate.Text); counter = counter.AddDays(1))
            {
                cblDate.Items.Add(new ListItem(counter.ToString("dd-MMM-yyyy"), i.ToString()));
                i++;
            }
        }
    }

    protected void rblAlternate_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDays();
    }
}