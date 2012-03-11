using System;

public partial class Radiologist_StartNewSeriesSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        Status.Text += Request.QueryString["SeriesID"];
    }
}