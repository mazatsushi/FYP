using System;
using System.Web;

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
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            NewPatient.PostBackUrl = FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                CryptoHandler.GetHash(Request.Url.ToString());

            // We need patient's NRIC to be able to display data and prompt for actions
            if (Session["Nric"] == null)
                Server.Transfer(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" + CryptoHandler.GetHash(Request.Url.ToString()));

            // Let physician know which patient is currently being referenced
            var nric = Session["Nric"].ToString();
            var patientParticulars = DatabaseHandler.GetParticularsFromNric(nric);
            PatientName.Text = patientParticulars.Prefix + " " + patientParticulars.FirstName + " " + patientParticulars.LastName;

            // Initialize blood types
            var bloodTypes = DatabaseHandler.GetAllBloodTypes();
            bloodTypes.Insert(0, "");
            BloodType.DataSource = bloodTypes;
            BloodType.DataBind();

            if (DatabaseHandler.HasMedicalRecords(nric))
            {
                // Preselect patient's blood type
                BloodType.SelectedValue = DatabaseHandler.GetBloodType(nric);
            }
            else
            {
                AllergyList.Visible = false;
                None.Visible = true;
            }
        }

        /// <summary>
        /// Event that triggers when the 'Update Allergies' button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void AllergyButtonClick(object sender, EventArgs e)
        {
            ;
        }

        /// <summary>
        /// Event that triggers when the 'Update Blood Type' button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void BloodButtonClick(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            var bloodName = BloodType.Text.Trim();
            var nric = Session["Nric"].ToString();
            if (String.IsNullOrWhiteSpace(bloodName))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("Please specify the patient's blood type.");
                return;
            }

            // Proceed to update patient's blood type in database
            if (!DatabaseHandler.UpdateBloodType(nric, bloodName))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("There was an error updating the patient's blood type." +
                                                           " Please contact the administrator for assistance.");
                return;
            }
            Response.Redirect(UpdateSuccessRedirect);
        }

        /// <summary>
        /// Event that triggers when the 'View Imaging History' button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ImagingButtonClick(object sender, EventArgs e)
        {
            ;
        }

        /// <summary>
        /// Event that triggers when the return button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ReturnButtonClick(object sender, EventArgs e)
        {
            Session["Nric"] = null;
            Response.Redirect(PhysicianHome);
        }
    }
}