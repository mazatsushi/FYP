using System;
using System.Globalization;
using DB_Handlers;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/ManageStudy.aspx page
    /// </summary>
    public partial class ManageStudy : System.Web.UI.Page
    {
        private const string Beginning = "~/Common/SearchByNric.aspx";
        private const string Home = "~/Radiologist/ManagePatient.aspx";
        private const string HashFailure = "~/Error/HashFailure.aspx";
        private const string SuccessRedirect = "~/Radiologist/ManageSeries.aspx";

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize(int studyId)
        {
            NewButton.PostBackUrl = ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home));

            // Display all series in the study
            PatientName.Text = Session["PatientName"].ToString();
            StudyID.Text = studyId.ToString(CultureInfo.InvariantCulture);

            // Code block to initialize series
            var list = SeriesHandler.GetAllSeries(studyId);
            if (list.Count > 0)
            {
                Series.DataSource = SeriesHandler.GetAllSeries(studyId);
                Series.DataBind();
                None.Visible = false;
            }
            else
            {
                Series.Visible = false;
                None.Visible = true;
            }

            // Code block to initialize modality drop down list
            var list2 = ModalityHandler.GetAllModalities();
            list2.Insert(0, string.Empty);
            Modality.DataSource = list2;
            Modality.DataBind();
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

            // We need patient NRIC and Study ID before proceeding
            if (Session["Nric"] == null)
                Server.Transfer(ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home)));

            var temp = Request.QueryString["StudyId"];
            var checksum = Request.QueryString["Checksum"];
            if (String.IsNullOrEmpty(temp) || String.IsNullOrEmpty(checksum))
                Server.Transfer(ResolveUrl(Home + "?Nric=" + Session["Nric"] + "&Checksum=" +
                    CryptoHandler.GetHash(Session["Nric"].ToString())));

            // Make sure query string has not been ilegally modified and that study exists
            var studyId = int.Parse(temp);
            if (!CryptoHandler.IsHashValid(checksum, temp) || !StudyHandler.StudyExists(studyId))
                Server.Transfer(ResolveUrl(HashFailure));

            Session["StudyId"] = studyId;
            Initialize(studyId);
        }

        /// <summary>
        /// Event that triggers when the return button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ResetButtonClick(object sender, EventArgs e)
        {
            Session.Clear();
        }

        /// <summary>
        /// Event that triggers when the start button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void StartButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var seriesId = SeriesHandler.CreateSeries(Modality.SelectedValue.Trim(), int.Parse(Session["StudyId"].ToString()));
            if (seriesId < 0)
            {
                ErrorMessage.Text = "Failed to create a new series for the study. Please contact the administrator for assistance.";
                return;
            }

            Session["StudyId"] = null;
            Response.Redirect(ResolveUrl(SuccessRedirect + "?ReturnUrl=" + Request.Url + "&SeriesId=" + seriesId));
        }

        /// <summary>
        /// Event that triggers when the start over button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void StartOver(object sender, EventArgs e)
        {
            Session.Clear();
            Server.Transfer(ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home)));
        }
    }
}