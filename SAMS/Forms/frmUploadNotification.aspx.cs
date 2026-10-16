using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SAMSBusinessLayer.Classes;
using SAMSCommon.Classes;

public partial class Forms_frmUploadNotification : System.Web.UI.Page
{
    GeoHierarchyController gController = new GeoHierarchyController();
    DistributorController DController = new DistributorController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Session.Remove("DocumentID");
            this.LoadRegion();
            this.LoadDistributorRegionWist();
            this.LoadGrid();
            Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }

    protected void LoadRegion()
    {
        DataTable dt = gController.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, false, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.ddlRegion.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlRegion, dt, "GEO_ID", "GEO_NAME");
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDistributorRegionWist();
    }

    private void LoadDistributorRegionWist()
    {
        cblLocation.Items.Clear();
        DataTable dt = DController.GetTownWistDistributor(Constants.IntNullValue, Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(Session["UserID"]));        
        clsWebFormUtil.FillListBox(this.cblLocation, dt, "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (txtSubject.Text.Trim().Length > 0)
        {
            //if (fuNotification.HasFile)
            //{
                DataTable dtLocationIDs = new DataTable();
                dtLocationIDs.Columns.Add("DISTRIBUTOR_ID", typeof(long));

                foreach (ListItem li in cblLocation.Items)
                {
                    if (li.Selected)
                    {
                        DataRow dr = dtLocationIDs.NewRow();
                        dr["DISTRIBUTOR_ID"] = Convert.ToInt32(li.Value);
                        dtLocationIDs.Rows.Add(dr);
                    }
                }

                if (dtLocationIDs.Rows.Count > 0)
                {
                    //string Doc1 = fuNotification.PostedFile.FileName;
                    //string Doc2 = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Doc1;
                    //fuNotification.SaveAs(Server.MapPath("~/Docs/" + Doc2));

                    if (DController.InsertDocument(Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), txtRemarks.Text, null, txtSubject.Text, dtLocationIDs,Convert.ToDateTime(txtStartDate.Text),Convert.ToDateTime(txtEndDate.Text)))
                    {                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Notification uploaded successfully');", true);
                        this.LoadGrid();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Location selected');", true);
                }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No file selected');", true);
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please enter subject');", true);
        }
    }

    private void LoadGrid()
    {        
        DataTable dt = DController.GetDocuemnt(Constants.LongNullValue, Constants.DateNullValue, Constants.IntNullValue,Constants.IntNullValue);
        gvDocument.DataSource = dt;
        gvDocument.DataBind();
    }

    protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnViewDoc = e.Row.FindControl("btnViewDoc") as ImageButton;
            LinkButton lnkViewDoc = e.Row.FindControl("lnkViewDoc") as LinkButton;

            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnViewDoc);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkViewDoc);            
        }
    }

    protected void gvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "download")
        {
            long id = long.Parse(e.CommandArgument.ToString());
            DataTable dtDoc = DController.GetDocuemnt(id, Constants.DateNullValue, Constants.IntNullValue,Constants.IntNullValue);
            if (dtDoc.Rows.Count > 0)
            {
                string FName = dtDoc.Rows[0]["DOCUMENT_NAME2"].ToString();
                DownLoadFile(Server.MapPath("~/Docs/" + FName));
            }
        }
        else if (e.CommandName.ToLower() == "view")
        {
            Session.Add("DocumentID", e.CommandArgument.ToString());
            Response.Redirect("frmUploadNotification2.aspx?LevelType=3&LevelID=" + Request.QueryString["LevelID"].ToString());
        }
        else if (e.CommandName.ToLower() == "del")
        {
            if (DController.UpdateDocument(Convert.ToInt64(e.CommandArgument), Convert.ToInt32(Session["UserID"]), false))
            {
                this.LoadGrid();
            }
        }
    }

    private void DownLoadFile(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
        {
            long sz = fi.Length;
            Response.ClearContent();
            Response.ContentType = MimeType(Path.GetExtension(path));
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(path)));
            Response.AddHeader("Content-Length", sz.ToString("F0"));
            Response.TransmitFile(path);
            Response.End();
        }
    }

    public static string MimeType(string Extension)
    {
        string mime = "application/octetstream";
        if (string.IsNullOrEmpty(Extension))
            return mime;
        string ext = Extension.ToLower();
        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (rk != null && rk.GetValue("Content Type") != null)
            mime = rk.GetValue("Content Type").ToString();
        return mime;
    } 

}