using System;
using System.Web;

public partial class Radiologist_StartNewSeries2 : System.Web.UI.Page
{
    private const string RedirectBack = "~/Radiologist/StartNewSeries.aspx";
    private const string Redirect = "~/Radiologist/StartNewSeriesSuccess.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        var nric = Request.QueryString["Nric"];
        if (string.IsNullOrEmpty(nric))
            Response.Redirect(RedirectBack);

        PatientNameLabel.Text += DatabaseHandler.GetUserName(nric);
        Modality.DataSource = DatabaseHandler.GetAllModalities();
        Modality.DataBind();
        StudyGrid.DataSource = DatabaseHandler.GetStudies(nric);
        StudyGrid.DataBind();
    }

    protected void CreateButtonClick(object sender, EventArgs e)
    {
        Validate();
        if (!IsValid)
            return;

        var studyId = 0;
        if (!int.TryParse(HttpUtility.HtmlEncode(Study.Text.Trim()), out studyId))
        {
            ErrorMessage.Text = "Invalid Study ID.";
            return;
        }

        if (!DatabaseHandler.StudyExists(studyId))
        {
            ErrorMessage.Text = "The study ID does not exist.";
            return;
        }

        var seriesId = DatabaseHandler.CreateNewSeries(DatabaseHandler.GetModalityId(HttpUtility.HtmlEncode(Modality.Text.Trim())), studyId);
        if (seriesId == 0)
        {
            ErrorMessage.Text = "An error occured while trying to create a new series.";
            return;
        }

        Server.Transfer(Redirect + "?SeriesID=" + seriesId);
    }
}