using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/ManagePatient.aspx page
    /// </summary>
    public partial class ManagePatient : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Common/SearchByNric.aspx";
        private const string PhysicianHome = "~/Physician/Default.aspx";
        private const string UpdateSuccessRedirect = "~/Physician/UpdateBloodTypeSuccess.aspx";

        /// <summary>
        /// Event that triggers when the 'Update Blood Type' button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void BloodButtonClick(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            if (String.IsNullOrWhiteSpace(BloodType.Text))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("Please specify the patient's blood type.");
                return;
            }

            var bloodName = BloodType.Text.Trim().ToUpperInvariant();
            var nric = Session["Nric"].ToString().ToUpperInvariant();

            // Proceed to update patient's blood type in database
            if (!DatabaseHandler.UpdateBloodType(nric, bloodName))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("There was an error updating the patient's blood type." +
                                                           " Please contact the administrator for assistance.");
                return;
            }
            Response.Redirect(ResolveUrl(UpdateSuccessRedirect));
        }

        /// <summary>
        /// Checks whether blood type already exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void BloodTypeExists(object source, ServerValidateEventArgs args)
        {
            /*
             * Step 1: Desensitize the input
             * Step 2: Check for existing blood type
             */
            args.IsValid = DatabaseHandler.BloodTypeExists(HttpUtility.HtmlEncode(BloodType.Text.Trim().ToUpperInvariant()));
        }

        /// <summary>
        /// Hides the list of allergies that patient might have
        /// </summary>
        private void HideAllergyList()
        {
            AllergyList.Visible = false;
            None.Visible = true;
        }

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize()
        {
            // Set the postback URL of the "New Patient" button
            NewPatient.PostBackUrl = FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                CryptoHandler.GetHash(Request.Url.ToString());

            // Let physician know which patient is currently being managed
            var nric = Session["Nric"].ToString();
            var patientParticulars = DatabaseHandler.GetFullName(nric);
            PatientName.Text = patientParticulars.Prefix + " " + patientParticulars.FirstName + " " + patientParticulars.LastName;
            Session["PatientName"] = PatientName.Text;

            // Initialize the list of blood types
            var bloodTypes = DatabaseHandler.GetAllBloodTypes();
            bloodTypes.Insert(0, "");
            BloodType.DataSource = bloodTypes;
            BloodType.DataBind();

            HideAllergyList();

            // Check if patient has any prior medical records in system
            if (!DatabaseHandler.HasMedicalRecords(nric))
                return;

            // If exist, we must pre-select the patient's blood type
            BloodType.SelectedValue = DatabaseHandler.GetPatientBloodType(nric);

            // Show all of the patient's drug allergies if found
            ShowAllergyList(nric);
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

            // We need patient's NRIC to be able to display data and prompt for actions
            if (Session["Nric"] == null)
                Server.Transfer(ResolveUrl(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                    CryptoHandler.GetHash(Request.Url.ToString())));

            Initialize();
        }

        /// <summary>
        /// Event that triggers when the return button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ReturnButtonClick(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(ResolveUrl(PhysicianHome));
        }

        /// <summary>
        /// Hides the list of allergies that patient might have
        /// </summary>
        /// <param name="nric">The patient's NRIC</param>
        private void ShowAllergyList(string nric)
        {
            var list = DatabaseHandler.GetPatientAllergies(nric);
            if (list.Count == 0)
                return;

            // Set the list of patient allergies
            AllergyList.DataSource = list;
            AllergyList.DataBind();
            Session["Allergies"] = list;

            // Set the visibility of the appropriate components
            AllergyList.Visible = true;
            None.Visible = false;
        }
    }
}