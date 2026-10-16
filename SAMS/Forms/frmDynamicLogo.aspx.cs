using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;
using System.IO;

/// <summary>
/// Form To Add, Edit, Delete Busines Type
/// </summary>
public partial class Forms_frmDynamicLogo : System.Web.UI.Page
{
   

    /// <summary>
    /// Page_Load Function Populates All Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadChannelType();
        }
    }

   

    /// <summary>
    /// Loads Channel Types To Channel Type Grid
    /// </summary>
    private void LoadChannelType()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable  dt = mController.SelectLogo(4, Convert.ToInt32(Session["UserID"]));
        if (dt.Rows.Count > 0)
        {
            gvDesignation.EditIndex = -1;
            gvDesignation.DataSource = dt.DefaultView;
            gvDesignation.DataBind();
        }
     }

     /// <summary>
     /// Sets PageIndex Of Channel Type Grid
     /// </summary>
     /// <param name="sender">object</param>
     /// <param name="e">GridViewPageEventArgs</param>
  
    
    protected void gvDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SLASHCodesController mController = new SLASHCodesController();

        int logiD = int.Parse(gvDesignation.Rows[e.RowIndex].Cells[0].Text);
        mController.UpdateLogo(logiD, 3, DateTime.Now);
        this.LoadChannelType();

    }

    /// <summary>
    /// Save Or Updates a Channel Type
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDesignation_Click(object sender, EventArgs e)
    {
        string fileName = null;
        string fullFileName = "";
        string filenametodb = "";
        if (fuImage.HasFile)
        {
            string path = Server.MapPath("~/Logos/");
            string fExtension = "";
            FileInfo oFileInfo = new FileInfo(fuImage.PostedFile.FileName);
            fExtension = Path.GetExtension(fuImage.FileName);
            fileName = "Logo_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fExtension;
            fullFileName = path + fileName;
            filenametodb = "Logos/" + fileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fuImage.PostedFile.SaveAs(fullFileName);
            SLASHCodesController mController = new SLASHCodesController();
            mController.UploadLogo(Convert.ToInt32(Session["UserID"]), filenametodb, DateTime.Now,1);
            this.LoadChannelType();
        }
        else {
            lblErrorMsg.Text = "Select a logo to upload";
            return;
        }
    }

  
}
