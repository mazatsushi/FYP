using System;

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

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize()
        {
            Study.Text = Request.QueryString["StudyId"];
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

            // Make sure study ID exists in the system
            ;

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
    }
}