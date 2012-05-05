using System;
using System.Globalization;
using System.Web;
using DB_Handlers;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/ManagePatient.aspx page
    /// </summary>
    public partial class ManagePatient : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Common/SearchByNric.aspx";
        
        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize(string nric)
        {
            NewButton.PostBackUrl = FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                CryptoHandler.GetHash(Request.Url.ToString());

            // Display all studies of the patient
            PatientName.Text = UserParticularsHandler.GetFullName(nric);
            Session["PatientName"] = PatientName;
            var openStudyId = StudyHandler.GetOpenStudy(nric);
            
            if (openStudyId> 0)
            {
                Study.Visible = true;
                None.Visible = false;
                Study.DataSource = StudyHandler.GetStudy(openStudyId);
                Study.DataBind();
            }
            else
            {
                Study.Visible = false;
                None.Visible = true;
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

            // We need a Patient ID before proceeding
            if (Session["Nric"] == null)
                Server.Transfer(ResolveUrl(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                    CryptoHandler.GetHash(Request.Url.ToString())));

            var nric = Session["Nric"].ToString();

            Initialize(nric);
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