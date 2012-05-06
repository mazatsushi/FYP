using System;
using DB_Handlers;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/ManagePatient.aspx page
    /// </summary>
    public partial class ManagePatient : System.Web.UI.Page
    {
        private const string Beginning = "~/Common/SearchByNric.aspx";
        private const string Home = "~/Radiologist/ManagePatient.aspx";
        
        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize(string nric)
        {
            NewButton.PostBackUrl = ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home));

            // Display all studies of the patient
            var patientName = UserParticularsHandler.GetFullName(nric);
            PatientName.Text = patientName;
            Session["PatientName"] = patientName;
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
                Server.Transfer(ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home)));

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