using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;

public partial class Forms_frmTransporter : Page
{
    readonly DistributorController _dController = new DistributorController();   
     static int _userId = Constants.IntNullValue;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadGrid();            
        }
    }

    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlDistributor, dt, 0, 2, true);        
    }

    protected void LoadGrid()
    {        
        var dt = _dController.SelectTranspoter(Constants.IntNullValue,Convert.ToInt32(Session["UserID"]));
        Grid_users.EditIndex = -1;
        Grid_users.DataSource = dt;
        Grid_users.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (_userId <= 0)
            {
                _userId = Constants.IntNullValue;
            }
            if (_dController.InsertTransporter(Convert.ToInt32(ddlDistributor.SelectedValue), _userId, txtName.Text, txtEmail.Text, txtPhoneNo.Text, txtAddress.Text,Convert.ToInt32(rblType.SelectedValue)))
            {
                LoadGrid();
                ClearControls();
                btnSave.Text = "Save";
            }
            else
            {
               
                lblErrorMsg.Text = "Plz Try Again!!!";
            }
        }

        catch (Exception ex)
        {
            lblErrorMsg.Text = "Plz Try Again!!!";
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadGrid();
        ClearControls();
        btnSave.Text = "Save";
    }

    protected void Grid_users_RowEditing(object sender, GridViewEditEventArgs e)
    {
        var gvr = Grid_users.Rows[e.NewEditIndex];
        _userId = int.Parse(gvr.Cells[0].Text);
        ddlDistributor.SelectedValue = gvr.Cells[1].Text;
        rblType.SelectedValue = gvr.Cells[2].Text;
        txtName.Text = gvr.Cells[5].Text;
        txtPhoneNo.Text = Convert.ToString(gvr.Cells[6].Text.Replace("&nbsp;", ""));
        txtEmail.Text = Convert.ToString(gvr.Cells[7].Text.Replace("&nbsp;", ""));
        txtAddress.Text = Convert.ToString(gvr.Cells[8].Text.Replace("&nbsp;", ""));     
        btnSave.Text = "Update";
        for (int i = 0; i < Grid_users.Rows.Count; i++)
        {
            Grid_users.Rows[i].Cells[9].Enabled = false;
        }
    }

    protected void ClearControls()
    {

        txtName.Text = null;
        txtPhoneNo.Text = null;
        txtAddress.Text = null;
        txtEmail.Text = null;
        _userId = Constants.IntNullValue;
        lblErrorMsg.Text = null;
    }
}