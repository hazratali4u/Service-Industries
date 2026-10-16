using System;
using System.Data;
using System.Web.UI;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

/// <summary>
/// From to Assign Towns To Locations.
/// </summary>
public partial class Forms_frmTownClasification : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos and ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //this.LoadDistributor();
            LoadPrincipal();
            this.LoadRegion();
            this.LoadZone();
            this.LoadTerritory();
            this.LoadUnAssignTown();
            this.LoadAssignTown();
            this.LoadClassfication();
           // LoadAssignAllTown();
            // drpDistributor.Focus();
            drpClassification.Focus();

        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    //private void LoadDistributor()
    //{
    //    DistributorController mController = new DistributorController();
    //    DataTable dt = mController.SelectDistributor(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
    //    //clsWebFormUtil.FillDropDownList(drpDistributor, dt, 1, 3, true);

    // //   DistributorController DController = new DistributorController();
    //    //DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
    //    //DataTable dtMasterDistributor = DController.SelectDistributorInfoByTownbyUser(Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserID"].ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, 7);

    //    clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME", true);


    //}

    /// <summary>
    /// Loads Regions To Region Combo
    /// </summary>
    protected void LoadRegion()
    {
        GeoHierarchyController gController = new GeoHierarchyController();
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, false, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
      //  dt.DefaultView.Sort = "GEO_ID";
        ddRegion.DataSource = dt.DefaultView;
        ddRegion.DataTextField = "GEO_NAME";
        ddRegion.DataValueField = "GEO_ID";
        ddRegion.DataBind();
    }
    private void LoadPrincipal()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable m_dt = PController.SelectDataPrice(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 0, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
               clsWebFormUtil.FillDropDownList(chbPrincipal, m_dt, 0, 1);
    }
    //protected void ChkSelectall_CheckedChanged(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < this.chbPrincipal.Items.Count; i++)
    //    {
    //        if (ChkSelectall.Checked)
    //        {
    //            this.chbPrincipal.Items[i].Selected = true;
    //        }
    //        else { this.chbPrincipal.Items[i].Selected = false; }

    //    }
    //}
    /// <summary>
    /// Loads Zones To Area Combo
    /// </summary>
    protected void LoadZone()
    {
        if (ddRegion.Items.Count > 0)
        {
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddRegion.SelectedValue.ToString()), null, null, false, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            ddZone.DataSource = dt;
            ddZone.DataTextField = "GEO_NAME";
            ddZone.DataValueField = "GEO_ID";
            ddZone.DataBind();
        }
    }
    
    /// <summary>
    /// Loads Territories To Territory Combo
    /// </summary>
    protected void LoadTerritory()
    {
        if (ddZone.Items.Count > 0)
        {
            GeoHierarchyController gController = new GeoHierarchyController();
            DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddZone.SelectedValue.ToString()), null, null, false, Constants.IntNullValue, Constants.Territory, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
            ddTerritory.DataSource = dt;
            ddTerritory.DataTextField = "GEO_NAME";
            ddTerritory.DataValueField = "GEO_ID";
            ddTerritory.DataBind();
        }
        else
        {
            this.ddTerritory.Items.Clear();
        }
    }
    
    /// <summary>
    /// Loads UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    private void LoadUnAssignTown()
        
    {
        //string principalIds = "1,";
        
        //for (int i = 0; i < chbPrincipal.Items.Count; i++)
        //{
        //    if (chbPrincipal.Items[i].Selected == true)
        //    {
        //        principalIds += principalIds + chbPrincipal.Items[i].Value + ",";
        //    }
        //}
            if (ddTerritory.Items.Count > 0 && chbPrincipal.Items.Count>0 && drpClassification.Items.Count>0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectTownAsignUnasignForClassificaition(int.Parse(ddTerritory.SelectedValue.ToString()), int.Parse(chbPrincipal.SelectedValue), 0, int.Parse(drpClassification.SelectedValue));
            clsWebFormUtil.FillListBox(lstUnAssignTown, dt, 0, 1, true);
        }
        else
        {
            lstUnAssignTown.Items.Clear();
        }

    }

    private void LoadClassfication()
    {
       
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectTownClassification(Constants.IntNullValue, Constants.IntNullValue, 0);
            clsWebFormUtil.FillDropDownList(drpClassification, dt, 0, 1, true);

        }
       

    }

    /// <summary>
    /// Loads Assigned Towns To Assigned ListBox
    /// </summary>
    private void LoadAssignTown()
    {
        //string principalIds = "";
        //for (int i = 0; i < chbPrincipal.Items.Count; i++)
        //{
        //    if (chbPrincipal.Items[i].Selected == true)
        //    {
        //        principalIds += principalIds + chbPrincipal.Items[i].Value + ",";
        //    }
        //}
        if (ddTerritory.Items.Count > 0 && chbPrincipal.Items.Count>0 &&drpClassification.Items.Count>0)
        {
            DistributorTownController gController = new DistributorTownController();
            DataTable dt = gController.SelectTownAsignUnasignForClassificaition(int.Parse(ddTerritory.SelectedValue.ToString()), int.Parse(chbPrincipal.SelectedValue), 1,int.Parse(drpClassification.SelectedValue));
           clsWebFormUtil.FillListBox(lstAssignTown, dt, 0, 1, true);
        }
        else
        {
            lstAssignTown.Items.Clear();
        }
    }

    //private void LoadAssignAllTown()
    //{

    //    if ( drpDistributor.Items.Count > 0)
    //    {
    //        DistributorTownController gController = new DistributorTownController();
    //        DataTable dt = gController.SelectAssignTown(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), 1);
    //        clsWebFormUtil.FillListBox(lstAssignedallTown, dt, 0, 1, true);
    //    }
    //    else
    //    {
    //        lstAssignedallTown.Items.Clear();
    //    }
    //}

    /// <summary>
    /// Loads Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    //protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.LoadUnAssignTown();
    //    this.LoadAssignTown();
    //    LoadAssignAllTown();
    //}

    /// <summary>
    /// Loads Zones To Area Combo, Territories To Territory Combo, Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadZone();
        this.LoadTerritory();
        this.LoadUnAssignTown();
        this.LoadAssignTown();

    }
    
    /// <summary>
    /// Loads Territories To Territory Combo, Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadTerritory();
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Loads Assigned Towns To Assigned ListBox And UnAssigned Towns To UnAssigned ListBox
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Inserts Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        if (drpClassification.Items.Count > 0) { 
        for (int i = 0; i < lstUnAssignTown.Items.Count; i++)
        {
                if (lstUnAssignTown.Items[i].Selected == true)
                {
                    //for (int p = 0; p < chbPrincipal.Items.Count; p++)
                    //{
                    //    if (chbPrincipal.Items[p].Selected == true)
                    //    {
                            mTownController.InsertTownClassicationAssign(int.Parse(chbPrincipal.SelectedValue),
                               int.Parse( drpClassification.SelectedValue), int.Parse(ddRegion.SelectedValue), int.Parse(ddZone.SelectedValue)
                                , int.Parse(ddTerritory.SelectedValue), int.Parse(lstUnAssignTown.Items[i].Value), true, true, int.Parse(this.Session["UserId"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
                    // }
                    // }
                }
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Inserts All Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignAll_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        if (drpClassification.Items.Count >0)
        {
            for (int i = 0; i < lstUnAssignTown.Items.Count; i++)
            {


                //for (int p = 0; p < chbPrincipal.Items.Count; p++)
                //{
                //    if (chbPrincipal.Items[p].Selected == true)
                //    {
                        mTownController.InsertTownClassicationAssign(int.Parse(chbPrincipal.SelectedValue),
                           int.Parse(drpClassification.SelectedValue), int.Parse(ddRegion.SelectedValue), int.Parse(ddZone.SelectedValue)
                            , int.Parse(ddTerritory.SelectedValue), int.Parse(lstUnAssignTown.Items[i].Value), true, true, int.Parse(this.Session["UserId"].ToString()), Convert.ToDateTime(Session["CurrentWorkDate"]));
                //    }
                //}
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Deletes Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssign_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        if (chbPrincipal.Items.Count > 0 && drpClassification.Items.Count>0)
        {
           // for (int p = 0; p < chbPrincipal.Items.Count; p++)
            {
               // if(chbPrincipal.Items[p].Selected==true)
                {
                    for (int i = 0; i < lstAssignTown.Items.Count; i++)
                    {


                        mTownController.DeleteTwonClassification(int.Parse(chbPrincipal.SelectedValue), int.Parse(lstAssignTown.Items[i].Value),int.Parse(drpClassification.SelectedValue));
                    }
                }
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }
    
    /// <summary>
    /// Deletes All Towns Assignment To Location
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignAll_Click(object sender, EventArgs e)
    {
        DistributorTownController mTownController = new DistributorTownController();
        if (chbPrincipal.Items.Count > 0 && drpClassification.Items.Count>0)
        {
            //for (int p = 0; p < chbPrincipal.Items.Count; p++)
            {
               // if (chbPrincipal.Items[p].Selected == true)
                {
                    for (int i = 0; i < lstAssignTown.Items.Count; i++)
                    {
                        if (lstAssignTown.Items[i].Selected == true)
                        {
                            mTownController.DeleteTwonClassification(int.Parse(chbPrincipal.SelectedValue), int.Parse(lstAssignTown.Items[i].Value),int.Parse(drpClassification.SelectedValue));
                        }
                    }
                }
            }
        }
        this.LoadUnAssignTown();
        this.LoadAssignTown();
    }

    protected void drpClassification_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAssignTown();
        this.LoadUnAssignTown();
    }

    protected void btnNewClassification_Click(object sender, EventArgs e)
    {
        if (btnNewClassification.Text == "New")
        {
            drpClassification.Visible = false;
            txtClassificationName.Text = "";
            txtClassificationName.Visible = true;
            btnNewClassification.Text = "Add";
        }
        else
        if (btnNewClassification.Text == "Add")
        {
            DistributorTownController mTownController = new DistributorTownController();
          
            {
              if(txtClassificationName.Text!="" && txtClassificationName.Text!=null)
                {

                    mTownController.InsertClassificationName(Constants.IntNullValue, true, 0, txtClassificationName.Text, int.Parse(this.Session["UserId"].ToString()) ,Convert.ToDateTime(Session["CurrentWorkDate"]));
                }
            }
            LoadClassfication();
            txtClassificationName.Visible = false;
            drpClassification.Visible = true;
            btnNewClassification.Text = "New";
        }

    }
}