using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Collections;
using System.Web.UI.WebControls;
using SAMSDataAccessLayer.Classes;

/// <summary>
/// From to Assign Towns To Locations.
/// </summary>
public partial class Forms_frmBankAssignment : System.Web.UI.Page
{

    DistributorTownController mTownController = new DistributorTownController();
    GeoHierarchyController GControler = new GeoHierarchyController();
    DataView dv;
    DistributorController ds = new DistributorController();
        protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadNSM();
            this.LoadRSM();
            this.LoadASM_ASO();
            this.LoadSO_SR();
            this.LoadDistributor();
            this.LoadUnAssignedDSR();
            this.LoadGrid();
            DrpNSM.Focus();
        }
    }
        protected void LoadDistributor()
    {
        DrpDistributor.Items.Clear();
       
       // DistributorController mController = new DistributorController();
       // DataTable dtMasterDistributor = mController.SelectDistributorInfoByTownbyUser(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 7);
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        clsWebFormUtil.FillDropDownList(DrpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
    }
    protected void DrpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadUnAssignedDSR();
    }
  
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (lstUnAssignDSR.Items.Count > 0)
        {
            if (lstUnAssignDSR.SelectedItem != null)
            {
                this.lstAssignDSR.Items.Add(new clsListItems(lstUnAssignDSR.SelectedItem.Text, lstUnAssignDSR.SelectedValue.ToString()));
                lstUnAssignDSR.Items.RemoveAt(lstUnAssignDSR.SelectedIndex);
            }
        }
    }

    protected void btnAssignAll_Click(object sender, EventArgs e)
    {

        int ItemCount = lstUnAssignDSR.Items.Count;

        for (int i = 0; i < ItemCount; i++)
        {
            this.lstAssignDSR.Items.Add(new clsListItems(lstUnAssignDSR.Items[i].Text, lstUnAssignDSR.Items[i].Value.ToString()));

        }
        lstUnAssignDSR.Items.Clear();

    }

    protected void btnUnAssign_Click(object sender, EventArgs e)
    {

        int ItemCount = lstAssignDSR.Items.Count;

        for (int i = 0; i < ItemCount; i++)
        {
            this.lstUnAssignDSR.Items.Add(new clsListItems(lstAssignDSR.Items[i].Text, lstAssignDSR.Items[i].Value.ToString()));
        }
        lstAssignDSR.Items.Clear();

    }

    protected void btnUnAssignAll_Click(object sender, EventArgs e)
    {
        if (lstAssignDSR.Items.Count > 0)
        {
            if (lstAssignDSR.SelectedItem != null)
            {
                this.lstUnAssignDSR.Items.Add(new clsListItems(lstAssignDSR.SelectedItem.Text, lstAssignDSR.SelectedValue.ToString()));
                lstAssignDSR.Items.RemoveAt(lstAssignDSR.SelectedIndex);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        bool AllInsert = true;
        IDbConnection mConnection = null;
        IDbTransaction mTransaction = null;

        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        mConnection.Open();
        mTransaction = ProviderFactory.GetTransaction(mConnection);


        if (btnSave.Text == "Save")
        {
            for (int i = 0; i < lstAssignDSR.Items.Count; i++)
            {
                int DSR_ID = int.Parse(lstAssignDSR.Items[i].Value.ToString());
                bool insert = mTownController.Insert_DSR_MAPPING(int.Parse(DrpNSM.SelectedValue.ToString()), int.Parse(DrpRSM.SelectedValue.ToString()), int.Parse(DrpASM_ASO.SelectedValue.ToString()), int.Parse(DrpSO_SR.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()),DSR_ID, int.Parse(this.Session["UserId"].ToString()), DateTime.Now,false, mConnection, mTransaction);
                if (!insert)
                {
                    AllInsert = false;
                }

            }

        }
        if (btnSave.Text == "Update")
        {

            bool Delete = mTownController.Delete_DSRMapping(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(DrpSO_SR.SelectedValue), int.Parse(DrpDistributor.SelectedValue), Constants.IntNullValue, mConnection, mTransaction);
            if (Delete)
            {
                for (int i = 0; i < lstAssignDSR.Items.Count; i++)
                {
                    int DSR_ID = int.Parse(lstAssignDSR.Items[i].Value.ToString());
                    bool insert = mTownController.Insert_DSR_MAPPING(int.Parse(DrpNSM.SelectedValue.ToString()), int.Parse(DrpRSM.SelectedValue.ToString()), int.Parse(DrpASM_ASO.SelectedValue.ToString()), int.Parse(DrpSO_SR.SelectedValue.ToString()), int.Parse(DrpDistributor.SelectedValue.ToString()), DSR_ID, int.Parse(this.Session["UserId"].ToString()), DateTime.Now, false, mConnection, mTransaction);
                    if (!insert)
                    {
                        AllInsert = false;
                    }
                }
            }
            else
            {
                AllInsert = false;
            }
        }

        if (AllInsert)
        {
            mTransaction.Commit();
            if (mConnection != null && mConnection.State == ConnectionState.Open)
            {
                mConnection.Close();
            }


               this.LoadUnAssignedDSR();
                LoadGrid();
                btnSave.Text = "Save";
                DrpNSM.Enabled = true;
                DrpRSM.Enabled = true;
                DrpASM_ASO.Enabled = true;
                DrpSO_SR.Enabled = true;
                DrpDistributor.Enabled = true;
              
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Updated !')", true);
        }
        else
        {
            mTransaction.Rollback();
            if (mConnection != null && mConnection.State == ConnectionState.Open)
            {
                mConnection.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Try Again !')", true);
        }


    }
    protected void LoadGrid()
    {
        DataTable dt = mTownController.Select_DSR_Mapping(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 1);
        dv = new DataView(dt);

        DataTable dt1 = mTownController.Select_DSR_Mapping(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 0);
        GrdDSR_Assignment.EditIndex = -1;
        this.GrdDSR_Assignment.DataSource = dt1;
        this.GrdDSR_Assignment.DataBind();
        for (int i = 0; i < GrdDSR_Assignment.Rows.Count; i++)
        {
            dv.RowFilter = "TSO_TSE_ID = " + dt1.Rows[i]["TSO_TSE_ID"].ToString() + " AND DISTRIBUTOR_ID = " + dt1.Rows[i]["distributor_id"].ToString();

            ListBox list = (ListBox)GrdDSR_Assignment.Rows[i].Cells[2].FindControl("lstDSR");
            list.DataSource = dv;
            list.DataTextField = "DSR";
            list.DataValueField = "ASSIGNED_DSR_ID";
            list.DataBind();
        }
    }
    protected void BankGroup_Grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        

    }

    protected void LoadNSM()
    {
        DrpNSM.Items.Clear();
        Distributor_UserController UCtl = new Distributor_UserController();
        DataTable dt = UCtl.SelectDistributorUser(Constants.NSM, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpNSM, dt, 0, 6, true);
    }

    protected void LoadRSM()
    {
        DrpRSM.Items.Clear();
        if (DrpNSM.Items.Count > 0)
        {
            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectAssignedDistributorUser(Constants.RSM, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), int.Parse(DrpNSM.SelectedValue), 0);
            clsWebFormUtil.FillDropDownList(this.DrpRSM, dt, 0, 6, true);
        }
    }

    protected void LoadASM_ASO()
    {
        DrpASM_ASO.Items.Clear();
        if (DrpRSM.Items.Count > 0)
        {

            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectAssignedDistributorUser(Constants.ASM, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), int.Parse(DrpRSM.SelectedValue), 0);
            clsWebFormUtil.FillDropDownList(this.DrpASM_ASO, dt, 0, 6, false);

            DataTable dt2 = UCtl.SelectAssignedDistributorUser(Constants.ASO, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), int.Parse(DrpRSM.SelectedValue), 0);
            clsWebFormUtil.FillDropDownList(this.DrpASM_ASO, dt2, 0, 6, false);
        }
    }


    protected void LoadSO_SR()
    {
        DrpSO_SR.Items.Clear();
        if (DrpASM_ASO.Items.Count > 0)
        {

            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectAssignedDistributorUser(Constants.TSE, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), int.Parse(DrpASM_ASO.SelectedValue), 0);
            clsWebFormUtil.FillDropDownList(this.DrpSO_SR, dt, 0, 6, false);

            DataTable dt2 = UCtl.SelectAssignedDistributorUser(Constants.TSO, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), int.Parse(DrpASM_ASO.SelectedValue), 0);
            clsWebFormUtil.FillDropDownList(this.DrpSO_SR, dt2, 0, 6, false);
        }
    }

    protected void LoadUnAssignedDSR()
    {
        lstUnAssignDSR.Items.Clear();
        lstAssignDSR.Items.Clear();
        if (DrpDistributor.Items.Count > 0)
        {

            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectUnAssignedDistributorUser(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(DrpDistributor.SelectedValue), Constants.IntNullValue, 1);
            clsWebFormUtil.FillListBox(this.lstUnAssignDSR, dt, 0, 6, false);
        }

    }


    protected void DrpNSM_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.LoadRSM();
        this.LoadASM_ASO();
        this.LoadSO_SR();
    }

    protected void DrpRSM_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadASM_ASO();
        this.LoadSO_SR();
    }

    protected void DrpASM_ASO_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSO_SR();
    }

   



    protected void GrdDSR_Assignment_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lstAssignDSR.Items.Clear();
        btnSave.Text = "Update";
        GridViewRow gvr = (GridViewRow)this.GrdDSR_Assignment.Rows[e.NewEditIndex];
        DrpNSM.SelectedValue = int.Parse(gvr.Cells[0].Text).ToString();
        LoadRSM();
        DrpRSM.SelectedValue = int.Parse(gvr.Cells[2].Text).ToString();
        LoadASM_ASO();
        DrpASM_ASO.SelectedValue = int.Parse(gvr.Cells[4].Text).ToString();
        LoadSO_SR();
        DrpSO_SR.SelectedValue = int.Parse(gvr.Cells[6].Text).ToString();

        DrpDistributor.SelectedValue = int.Parse(gvr.Cells[8].Text).ToString();
        for (int i = 0; i < GrdDSR_Assignment.Rows.Count; i++)
        {
            GrdDSR_Assignment.Rows[i].Cells[11].Enabled = false;
        }
        this.LoadUnAssignedDSR();

        ListBox list = (ListBox)gvr.Cells[2].FindControl("lstDSR");
        for (int i = 0; i < list.Items.Count; i++)
        {
            lstAssignDSR.DataSource = list.DataSource;
            lstAssignDSR.Items.Add(list.Items[i].Text);
            lstAssignDSR.Items[i].Value = list.Items[i].Value;
            lstAssignDSR.DataBind();
        }
        DrpNSM.Enabled = false;
        DrpRSM.Enabled = false;
        DrpASM_ASO.Enabled = false;
        DrpSO_SR.Enabled = false;
    }
}