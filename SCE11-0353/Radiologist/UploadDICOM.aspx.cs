using System;
using System.Web;

public partial class Radiologist_UploadDICOM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void RetrieveButtonClick(object sender, EventArgs e)
    {
        Validate();
        if (!IsValid)
            return;

        var seriesId = 0;
        if (!int.TryParse(HttpUtility.HtmlEncode(SeriesID.Text.Trim()), out seriesId))
        {
            ErrorMessage.Text = "Invalid Series ID.";
            return;
        }

        if (!DatabaseHandler.SeriesExists(seriesId))
        {
            ErrorMessage.Text = "Series ID does not exist.";
            return;
        }

        Server.Transfer("~/Radiologist/UploadDICOM2.aspx?SeriesID=" + seriesId);
    }
}