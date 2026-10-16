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
    // private static int DISTRIBUTOR_ID = 0;
    /// <summary>
    /// Page_Load Function Populates All Combos and ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadEmployee();
            LoadUnAssignedEmployee();
            LoadGrid();
            DrpDesignation.Focus();
        }
    }


    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (lstUnAssignEmployee.Items.Count > 0)
        {
            if (lstUnAssignEmployee.SelectedItem != null)
            {
                this.lstAssignEmployee.Items.Add(new clsListItems(lstUnAssignEmployee.SelectedItem.Text, lstUnAssignEmployee.SelectedValue.ToString()));
                lstUnAssignEmployee.Items.RemoveAt(lstUnAssignEmployee.SelectedIndex);
            }
        }
    }

    protected void btnAssignAll_Click(object sender, EventArgs e)
    {

        int ItemCount = lstUnAssignEmployee.Items.Count;

        for (int i = 0; i < ItemCount; i++)
        {
            this.lstAssignEmployee.Items.Add(new clsListItems(lstUnAssignEmployee.Items[i].Text, lstUnAssignEmployee.Items[i].Value.ToString()));

        }
        lstUnAssignEmployee.Items.Clear();

    }
    
    protected void btnUnAssign_Click(object sender, EventArgs e)
    {

        int ItemCount = lstAssignEmployee.Items.Count;

        for (int i = 0; i < ItemCount; i++)
        {
            this.lstUnAssignEmployee.Items.Add(new clsListItems(lstAssignEmployee.Items[i].Text, lstAssignEmployee.Items[i].Value.ToString()));
        }
        lstAssignEmployee.Items.Clear();

    }
    
    protected void btnUnAssignAll_Click(object sender, EventArgs e)
    {
        if (lstAssignEmployee.Items.Count > 0)
        {
            if (lstAssignEmployee.SelectedItem != null)
            {
                this.lstUnAssignEmployee.Items.Add(new clsListItems(lstAssignEmployee.SelectedItem.Text, lstAssignEmployee.SelectedValue.ToString()));
                lstAssignEmployee.Items.RemoveAt(lstAssignEmployee.SelectedIndex);
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
            for (int i = 0; i < lstAssignEmployee.Items.Count; i++)
            {
                int Employee_ID = int.Parse(lstAssignEmployee.Items[i].Value.ToString());
                bool insert = mTownController.Insert_FieldForceMapping(int.Parse(DrpDesignation.SelectedValue.ToString()), int.Parse(drpEmployee.SelectedValue.ToString()), Employee_ID, int.Parse(this.Session["UserId"].ToString()), DateTime.Now, false, mConnection, mTransaction);
                if (!insert)
                {
                    AllInsert = false;
                }

            }

        }
        if (btnSave.Text == "Update")
        {

            bool Delete = mTownController.Delete_FieldForceMapping(int.Parse(DrpDesignation.SelectedValue), int.Parse(drpEmployee.SelectedValue), mConnection, mTransaction);
            if (Delete)
            {
                for (int i = 0; i < lstAssignEmployee.Items.Count; i++)
                {
                    int Employee_ID = int.Parse(lstAssignEmployee.Items[i].Value.ToString());
                    bool insert = mTownController.Insert_FieldForceMapping(int.Parse(DrpDesignation.SelectedValue.ToString()), int.Parse(drpEmployee.SelectedValue.ToString()), Employee_ID, int.Parse(this.Session["UserId"].ToString()), DateTime.Now, false, mConnection, mTransaction);
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


            this.LoadUnAssignedEmployee();
            LoadGrid();
            btnSave.Text = "Save";
            DrpDesignation.Enabled = true;
            drpEmployee.Enabled = true;

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
        DataTable dt = mTownController.Select_FieldForceMapping(Constants.IntNullValue, Constants.IntNullValue, 1);
        dv = new DataView(dt);

        DataTable dt1 = mTownController.Select_FieldForceMapping(Constants.IntNullValue, Constants.IntNullValue, 0);
        GrdDesignation.EditIndex = -1;
        this.GrdDesignation.DataSource = dt1;
        this.GrdDesignation.DataBind();
        for (int i = 0; i < GrdDesignation.Rows.Count; i++)
        {
            dv.RowFilter = "USER_ID = " + dt1.Rows[i]["USER_ID"].ToString();

            ListBox list = (ListBox)GrdDesignation.Rows[i].Cells[2].FindControl("lstEmployee");
            list.DataSource = dv;
            list.DataTextField = "ASSIGNED_USER";
            list.DataValueField = "ASSIGNED_USER_ID";
            list.DataBind();
        }
    }
  

    protected void DrpDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDesignation.Text = DrpDesignation.SelectedItem.Text;
        LoadEmployee();
        LoadUnAssignedEmployee();
    }
    protected void LoadEmployee()
    {
        drpEmployee.Items.Clear();
        if (DrpDesignation.Items.Count > 0)
        {
            Distributor_UserController UCtl = new Distributor_UserController();
            DataTable dt = UCtl.SelectDistributorUser(int.Parse(DrpDesignation.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillDropDownList(this.drpEmployee, dt, 0, 6, true);
        }
    }
    protected void LoadUnAssignedEmployee()
    {
        lstUnAssignEmployee.Items.Clear();
        lstAssignEmployee.Items.Clear();
        if (DrpDesignation.Items.Count > 0)
        {
            if (int.Parse(DrpDesignation.SelectedValue) == Constants.NSM)// NSM then show all RSM
            {
                Distributor_UserController UCtl = new Distributor_UserController();
                DataTable dt = UCtl.SelectUnAssignedDistributorUser(Constants.RSM, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()),0);
                clsWebFormUtil.FillListBox(this.lstUnAssignEmployee, dt, 0, 6, true);
            }
            else if (int.Parse(DrpDesignation.SelectedValue) == Constants.RSM)// rSM then show all ASM AND ASO
            {
                Distributor_UserController UCtl = new Distributor_UserController();
                DataTable dt = UCtl.SelectUnAssignedDistributorUser(Constants.ASM, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()),0);
                clsWebFormUtil.FillListBox(this.lstUnAssignEmployee, dt, 0, 6, false);

                DataTable dt2 = UCtl.SelectUnAssignedDistributorUser(Constants.ASO, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()),0);
                clsWebFormUtil.FillListBox(this.lstUnAssignEmployee, dt2, 0, 6, false);
            }
            else if (int.Parse(DrpDesignation.SelectedValue) == Constants.ASM || int.Parse(DrpDesignation.SelectedValue) == Constants.ASO)// ASM/ASO then show all TSM AND TSO
            {
                Distributor_UserController UCtl = new Distributor_UserController();
                DataTable dt = UCtl.SelectUnAssignedDistributorUser(Constants.TSE, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()),0);
                clsWebFormUtil.FillListBox(this.lstUnAssignEmployee, dt, 0, 6, false);

                DataTable dt2 = UCtl.SelectUnAssignedDistributorUser(Constants.TSO, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()),0);
                clsWebFormUtil.FillListBox(this.lstUnAssignEmployee, dt2, 0, 6, false);
            }
           
        }
    }


    protected void GrdDesignation_RowEditing(object sender, GridViewEditEventArgs e)
    {

        lstAssignEmployee.Items.Clear();

        GridViewRow gvr = (GridViewRow)this.GrdDesignation.Rows[e.NewEditIndex];
        DrpDesignation.SelectedValue = int.Parse(gvr.Cells[0].Text).ToString();
        LoadEmployee();
        drpEmployee.SelectedValue = int.Parse(gvr.Cells[2].Text).ToString();
        LoadUnAssignedEmployee();
        btnSave.Text = "Update";
        for (int i = 0; i< GrdDesignation.Rows.Count; i++)
        {
            GrdDesignation.Rows[i].Cells[5].Enabled = false;
        }
        //  this.txtAreaName.Text = gvr.Cells[1].Text;
        //drpDistributor.SelectedValue = gvr.Cells[1].Text;

        ListBox list = (ListBox)gvr.Cells[2].FindControl("lstEmployee");
        for (int i = 0; i < list.Items.Count; i++)
        {

            lstAssignEmployee.DataSource = list.DataSource;
            lstAssignEmployee.Items.Add(list.Items[i].Text);
            lstAssignEmployee.Items[i].Value = list.Items[i].Value;
            lstAssignEmployee.DataBind();
        }

        DrpDesignation.Enabled = false;
        drpEmployee.Enabled = false;

    }
}