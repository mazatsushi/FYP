using System;
using System.Web;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/CreateSeries2.aspx page
    /// </summary>

    public partial class CreateSeries2 : System.Web.UI.Page
    {
        // TODO: Refactor this class to fit in with its predecessor (most probably removing)
        /// <summary>
        /// Event that triggers when the create button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
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

            Response.Redirect("~/Radiologist/CreateSeriesSuccess.aspx?SeriesID=" + seriesId);
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var nric = Request.QueryString["Nric"];
            if (string.IsNullOrEmpty(nric))
                Server.Transfer("~/Radiologist/StartNewSeries.aspx");

            PatientNameLabel.Text += DatabaseHandler.GetUserNameFromNric(nric);
            Modality.DataSource = DatabaseHandler.GetAllModalities();
            Modality.DataBind();
            StudyGrid.DataSource = DatabaseHandler.GetStudies(nric);
            StudyGrid.DataBind();
        }
    }
}