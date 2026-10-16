using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmMobileDevice : System.Web.UI.Page
{
    DistributorController mController = new DistributorController();
    private static int rId;
    //  private static int DistributorId;
    //  private static int CompanyId;

    /// <summary>
    /// Page_Load Function Populates All Combos and Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadDistributor();
            this.LoadOrderBooker();
            this.LoadGird();
            this.SetTableSorter();
  btnSave.Attributes.Add("onclick", "return ValidateForm()");
  }

    }
    private void LoadDistributor()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

           // DistributorController mController = new DistributorController();
           // DataTable dtMasterDistributor = mController.SelectDistributorInfoByTownbyUser(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 7);
            clsWebFormUtil.FillDropDownList(drpLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);
        }
        catch (Exception ex)
        {
            ExceptionPublisher.PublishException(ex);
        }
    }
    private void LoadOrderBooker()
    {
        drpOrderBooker.Items.Clear();
        if (drpLocation.Items.Count > 0 )
        {
            SaleForceController mDController = new SaleForceController();
            DataTable m_dt = mDController.SelectSaleForceAssignedArea(Constants.SALES_FORCE_ORDERBOOKER, int.Parse(drpLocation.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Constants.IntNullValue);
            
            clsWebFormUtil.FillDropDownList(this.drpOrderBooker, m_dt, 0, 3);
        }
    }


    private void SetTableSorter()
    {
        if (GridDistributor.Rows.Count > 1)
        {
            GridDistributor.UseAccessibleHeader = true;
            GridDistributor.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    /// <summary>
    /// Loads Locations To Location Grid
    /// </summary>
    private void LoadGird()
    {
        GridDistributor.DataSource = null;

        GridDistributor.DataBind();

        if (drpLocation.Items.Count > 0 )
        {
            DataTable dtDistributor = mController.getMobileDevice(int.Parse(drpLocation.SelectedValue),
Constants.IntNullValue
                ,null,
           null
            , null, null, null,
            Constants.IntNullValue, 0, 1);
            if (dtDistributor.Rows.Count > 0)
            {
                switch (ddSearchType.SelectedIndex)
                {
                    case 1:
                        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 2:
                        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 3:
                        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 4:
                        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    case 5:
                        dtDistributor.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                        break;
                    default:
                        dtDistributor.DefaultView.RowFilter = "MOBILE_DEVICE_NAME" + " like '%" + "" + "%'";
                        break;
                }
                GridDistributor.EditIndex = -1;
                GridDistributor.DataSource = dtDistributor;

                GridDistributor.DataBind();
            }
          
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        int Insert = 1;

        if (btnSave.Text == "Save")
        {


           
            Insert = mController.setMobileDevice(int.Parse(drpLocation.SelectedValue), int.Parse(drpOrderBooker.SelectedValue)
                            , txtDevicName.Text,
                        txtMake.Text
                        , txtModel.Text, txtMac.Text, txtIMEI.Text,
                        int.Parse(Session["UserID"].ToString()), 0, 2);
        }
        else
        {
            Insert = mController.setMobileDevice(int.Parse(drpLocation.SelectedValue), int.Parse(drpOrderBooker.SelectedValue)
                           , txtDevicName.Text,
                       txtMake.Text
                       , txtModel.Text, txtMac.Text, txtIMEI.Text,
                       int.Parse(Session["UserID"].ToString()), rId, 3);
  }
        if (Insert == -5)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('MAC address Or IMEI already exist !')", true);
        }else if (Insert>0)
        { 

            GridDistributor.EditIndex = -1;
            ClearAll();
            this.LoadGird();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Updated !')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Try Again !')", true);
        }
    }


    /// <summary>
    /// Clears All The Fields Through ClearAll() Function.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    /// <summary>
    /// Loads Locations To Location Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {

        this.LoadGird();
        this.SetTableSorter();
    }

    /// <summary>
    /// Sets PageIndex Of Location Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void GridDistributor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridDistributor.PageIndex = e.NewPageIndex;
        this.LoadGird();
        this.SetTableSorter();
    }

    /// <summary>
    /// Actives/DeActives A Location.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GridDistributor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int distributorId = int.Parse(GridDistributor.Rows[e.RowIndex].Cells[0].Text);
        int mobileDeviceId = int.Parse(GridDistributor.Rows[e.RowIndex].Cells[1].Text);
        int Insert = mController.setMobileDevice(distributorId, int.Parse(drpOrderBooker.SelectedValue)
                             , txtDevicName.Text,
                         txtMake.Text
                         , txtModel.Text, txtMac.Text, txtIMEI.Text,
                         int.Parse(Session["UserID"].ToString()), mobileDeviceId, 4);
        if (Insert>0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Record Deleted !')", true);
            GridDistributor.EditIndex = -1;
            ClearAll();
            this.LoadGird();

         
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Try Again !')", true);
        }
    }

    /// <summary>
    /// Sets Location Data For Edit. This Function Runs When An Existing Location Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GridDistributor_RowEditing(object sender, GridViewEditEventArgs e)
    {
      rId= int.Parse(GridDistributor.Rows[e.NewEditIndex].Cells[1].Text);
        btnSave.Text = "Update";
        drpLocation.SelectedValue = GridDistributor.Rows[e.NewEditIndex].Cells[0].Text;
        drpOrderBooker.SelectedValue = GridDistributor.Rows[e.NewEditIndex].Cells[2].Text;
        txtDevicName.Text = GridDistributor.Rows[e.NewEditIndex].Cells[3].Text;
        txtMake.Text = GridDistributor.Rows[e.NewEditIndex].Cells[4].Text;
        txtModel.Text = GridDistributor.Rows[e.NewEditIndex].Cells[5].Text;
        txtMac.Text = GridDistributor.Rows[e.NewEditIndex].Cells[6].Text;
        txtIMEI.Text = GridDistributor.Rows[e.NewEditIndex].Cells[7].Text;
        for (int i = 0; i < GridDistributor.Rows.Count; i++)
        {
            GridDistributor.Rows[i].Cells[9].Enabled=false;
            GridDistributor.Rows[i].Cells[10].Enabled=false;
        }



    }

    /// <summary>
    /// Clears All The Fields.
    /// </summary>
    private void ClearAll()
    {
        txtDevicName.Text = "";
        txtIMEI.Text = "";
        txtMac.Text = "";
        txtMake.Text = "";
        txtModel.Text = "";
        
        btnSave.Text = "Save";

        LoadGird();

    }
    protected void drpLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOrderBooker();
        LoadGird();
    }

   
}
