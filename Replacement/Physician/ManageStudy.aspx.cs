using System;
using System.Globalization;
using System.Web;
using DB_Handlers;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/ManageStudy.aspx page
    /// It is assumed that every patient can have at most 1 unresolved study at any time
    /// </summary>
    public partial class ManageStudy : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Common/SearchStudies.aspx";
        private const string HashFailure = "~/Error/HashFailure.aspx";
        private const string SuccessRedirect = "~/Physician/StudyClosed.aspx";

        /// <summary>
        /// Event that triggers when the close study button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void CloseButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var studyId = int.Parse(Session["StudyId"].ToString());
            var diag = HttpUtility.HtmlEncode(Diagnosis.Text.Trim());
            var date = DateTime.Now;

            // Check that the user is not closing a completed study
            if (studyId == -1 || !StudyHandler.IsStudyOpen(studyId) || !StudyHandler.CloseStudy(studyId, date, diag))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("Unable to create a new appointment. Please contact the administrator for assistance.");
                return;
            }

            Session["StudyId"] = null;
            Response.Redirect(ResolveUrl(SuccessRedirect));
        }

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize()
        {
            var studyId = int.Parse(Request.QueryString["StudyId"]);

            // Code block for enabling/disabling the page as a whole
            switch (StudyHandler.StudyExists(studyId))
            {
                case true:
                    Session["StudyId"] = studyId;
                    StudyIdLabel.Text = "Study ID: " + studyId.ToString(CultureInfo.InvariantCulture);
                    toggle.Visible = true;
                    StudyDetails.DataSource = StudyHandler.GetStudy(studyId);
                    StudyDetails.DataBind();
                    ToggleDiagOption(studyId);
                    ToggleImages(studyId);
                    break;
                case false:
                    Session["StudyId"] = null;
                    StudyIdLabel.Text = "Study ID cannot be found in the system";
                    toggle.Visible = false;
                    break;
            }
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

            // We need a Study ID before proceeding
            var studyId = Request.QueryString["StudyId"];
            var checksum = Request.QueryString["Checksum"];
            if (String.IsNullOrEmpty(studyId) || String.IsNullOrEmpty(checksum))
                Server.Transfer(ResolveUrl(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                    CryptoHandler.GetHash(Request.Url.ToString())));

            // Make sure query string has not been ilegally modified
            if (!CryptoHandler.IsHashValid(checksum, studyId))
                Server.Transfer(HashFailure);

            Initialize();
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
        /// Method for enabling/disabling the close study option
        /// </summary>
        private void ToggleDiagOption(int studyId)
        {
            switch (StudyHandler.IsStudyOpen(studyId))
            {
                case true:
                    closeStudy.Visible = true;
                    DiagValidate.Enabled = true;
                    break;
                case false:
                    closeStudy.Visible = false;
                    DiagValidate.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Method for showing/hiding all images associated with this study
        /// </summary>
        private void ToggleImages(int studyId)
        {
            switch (StudyHandler.HasImages(studyId))
            {
                case true:
                    break;
                case false:
                    break;
            }
            // TODO: Return to this after implementing the functionalities for Radiologist role
            throw new NotImplementedException();
        }
    }
}