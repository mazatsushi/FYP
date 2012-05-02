using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/ManageImaging.aspx page
    /// It is assumed that every patient can have at most 1 unresolved study at any time
    /// </summary>
    public partial class ManageImaging : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Physician/ManagePatient.aspx";
        private const string SuccessRedirect = "~/Physician/AppointmentAdded.aspx";

        /// <summary>
        /// Event that triggers when the 'Add Allergy' button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void AddButtonClick(object sender, EventArgs e)
        {
            Validate("AddValidationGroup");
            if (!IsValid)
                return;

            // Check that the user is not creating an appointment for a completed study

            // Case 1) No existing study
            if (Session["list"] == null)
            {
                ;
            }
            // Case 2) With existing study
            
            /*
             * Steps to create a new entry in database
             * 1) Create new study in database (remember to get current staff ID)
             * 2) Create new appointment
             * 3) Link appointment via foreign key to study
             */
            
            Response.Redirect(ResolveUrl(SuccessRedirect));
        }

        /// <summary>
        /// Checks whether the proposed appointment time is later than the current time
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void DateTimeCheck(object sender, ServerValidateEventArgs args)
        {
            var proposed = DateTime.Parse(HttpUtility.HtmlEncode(DatePicker.Text.Trim()));
            args.IsValid = proposed > DateTime.Now;
        }

        /// <summary>
        /// Displays & hides appropriate elements if patient does not have any medical imaging history
        /// </summary>
        private void HideApplicable()
        {
            AllStudies.Visible = false;
            Some.Visible = false;
            newStudyDiv.Visible = true;
            None.Visible = true;
            existingStudyDiv.Visible = false;
        }

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize()
        {
            // Let physician know which patient is currently being managed
            var nric = Session["Nric"].ToString();
            PatientName.Text = Session["PatientName"].ToString();

            // Determine if patient has existing studies first
            var list = DatabaseHandler.GetStudies(nric);
            if (list.Count == 0)
            {
                // No existing studies
                HideApplicable();
                return;
            }
            // Has existing studies
            ShowApplicable(nric);
            Session["Studies"] = list;
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

            // We must have the patient's NRIC and full name before proceeding.
            if (Session["Nric"] == null || Session["PatientName"] == null)
                Server.Transfer(ResolveUrl(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                    CryptoHandler.GetHash(Request.Url.ToString())));

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
        /// Displays & hides appropriate elements if patient has any medical imaging history
        /// </summary>
        private void ShowApplicable(string nric)
        {
            AllStudies.Visible = true;
            Some.Visible = true;
            newStudyDiv.Visible = false;
            None.Visible = false;
            existingStudyDiv.Visible = true;

            // No point displaying what study to associate with if no studies open
            var openStudies = DatabaseHandler.GetOpenStudies(nric);
            Session["Studies"] = openStudies;
            var val = openStudies.Count > 0;
            Associate.Visible = val;
            if (!val)
                return;

            Associate.DataSource = openStudies;
            Associate.DataBind();
        }
    }
}