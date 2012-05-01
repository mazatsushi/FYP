using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/UpdateAllergy.aspx page
    /// </summary>
    public partial class UpdateAllergy : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Physician/NoBloodType.aspx";
        private const string Previous = "~/Physician/ManagePatient.aspx";
        private const string SuccessRedirect = "~/Physician/UpdateAllergySuccess.aspx";

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

            // Update patient records with selected drug allergy
            if (!DatabaseHandler.UpdateAllergy(Session["Nric"].ToString(),
                new CultureInfo("en-SG").TextInfo.ToTitleCase(Addable.SelectedValue.Trim()), false))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("There was an error updating the patient's drug allergies." +
                                                           " Please contact the administrator for assistance.");
                return;
            }
            Session["Allergies"] = null;
            Response.Redirect(ResolveUrl(SuccessRedirect));
        }

        /// <summary>
        /// Checks whether drug name already exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void DrugExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = DatabaseHandler.DrugExists(new CultureInfo("en-SG").TextInfo.ToTitleCase(args.Value.Trim()));
        }

        /// <summary>
        /// Hides the list of allergies that patient might have
        /// </summary>
        private void HideAllergyList()
        {
            AllergyList.Visible = false;
            SomeLabel.Visible = false;
            NoneLabel.Visible = true;
            Removal.Visible = false;
        }

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize()
        {
            // Let physician know which patient is currently being managed
            var nric = Session["Nric"].ToString();
            var patientParticulars = DatabaseHandler.GetFullName(nric);
            PatientName.Text = patientParticulars.Prefix + " " + patientParticulars.FirstName + " " + patientParticulars.LastName;

            HideAllergyList();

            // Check if patient has any prior medical records in system
            if (!DatabaseHandler.HasMedicalRecords(nric))
                Server.Transfer(ResolveUrl(FailureRedirect));

            ShowAddableDrugs();
            var list = DatabaseHandler.GetPatientAllergies(nric);
            if (list.Count == 0)
                return;

            // Show all of the patient's drug allergies if found
            ShowAllergyList(list);
            Session["Allergies"] = list;

            // Show all drugs that can be removed from patient's drug allergies, provided that s/he has existing allergies
            ShowRemovableDrugs(list.ToList());
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
                Server.Transfer(ResolveUrl(Previous));

            Initialize();
        }

        /// <summary>
        /// Checks that patient already has an existing allergy that user is removing
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void HasAllergy(object source, ServerValidateEventArgs args)
        {
            var list = (IList<string>)Session["Allergies"];
            if (list == null)
            {
                args.IsValid = false;
                return;
            }
            args.IsValid = (from drug in list
                             where drug.Equals(new CultureInfo("en-SG").TextInfo.ToTitleCase(Addable.SelectedValue.Trim()))
                             select drug).Any();
        }

        /// <summary>
        /// Checks that patient has no existing allergy that user is adding
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void HasNoAllergy(object source, ServerValidateEventArgs args)
        {
            var list = (IList<string>)Session["Allergies"];
            if (list == null)
            {
                args.IsValid = true;
                return;
            }
            args.IsValid = !(from drug in list
                            where drug.Equals(new CultureInfo("en-SG").TextInfo.ToTitleCase(Addable.SelectedValue.Trim()))
                            select drug).Any();
        }

        /// <summary>
        /// Event that triggers when the return button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ResetButtonClick(object sender, EventArgs e)
        {
            Session["Allergies"] = null;
            Session["Nric"] = null;
        }

        /// <summary>
        /// Shows the list of medical drugs that can be added to patient's allergies.
        /// </summary>
        private void ShowAddableDrugs()
        {
            // Display list of all medical drugs that can be added
            var drugList = DatabaseHandler.GetAllDrugs();
            drugList.Insert(0, "");
            Addable.DataSource = drugList;
            Addable.DataBind();
        }

        /// <summary>
        /// Shows the list of current allergies that patient might have.
        /// </summary>
        /// <param name="list">A list of the patient's current allergies</param>
        private void ShowAllergyList(IList<string> list)
        {
            // Set the list of patient allergies
            AllergyList.DataSource = list;
            AllergyList.DataBind();

            // Set the visibility of the appropriate components
            AllergyList.Visible = true;
            SomeLabel.Visible = true;
            NoneLabel.Visible = false;
        }

        /// <summary>
        /// Displays HTML elements that allow physician to remove any existing drug allergies
        /// </summary>
        /// <param name="list">The list of patient's current drug allergies</param>
        private void ShowRemovableDrugs(IList<string> list)
        {
            Removal.Visible = true;
            list.Insert(0, "");
            Removable.DataSource = list;
            Removable.DataBind();
        }

        /// <summary>
        /// Event that triggers when the 'Remove Allergy' button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void RemoveButtonClick(object sender, EventArgs e)
        {
            Validate("RemoveValidationGroup");
            if (!IsValid)
                return;

            // Remove the select drug allergy from patient records
            if (!DatabaseHandler.RemoveAllergy(Session["Nric"].ToString(),
                new CultureInfo("en-SG").TextInfo.ToTitleCase(Removable.SelectedValue.Trim())))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("There was an error updating the patient's drug allergies." +
                                                           " Please contact the administrator for assistance.");
                return;
            }
            Session["Allergies"] = null;
            Response.Redirect(ResolveUrl(SuccessRedirect));
        }
    }
}