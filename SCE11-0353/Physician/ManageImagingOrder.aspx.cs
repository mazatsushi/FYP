using System;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Code behind for the ~/Physician/CreateImagingOrder.aspx page
/// </summary>

public partial class Physician_ManageImagingOrder : System.Web.UI.Page
{
    private const string ExpectedUserRole = "Patient";
    private const string RedirectUrl = "~/Physician/ManageImagingOrder2.aspx";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Server side validation to check whether NRIC already exists
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void NricExists(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for existing NRIC
         */
        args.IsValid = DatabaseHandler.NricExists(HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant()));
    }

    /// <summary>
    /// Server side validation to check whether patient has uncompleted studies
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void HasOpenStudies(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.HasOpenStudies(HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant()));
    }

    /// <summary>
    /// Server side validation to check whether NRIC provided is a patient
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void IsPatient(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.IsInRole(NRIC.Text.Trim().ToUpperInvariant(), ExpectedUserRole);
    }

    /// <summary>
    /// Server side validation to check whether user has medical records in system
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void HasMedicalRecords(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.HasMedicalRecords(NRIC.Text.Trim().ToUpperInvariant());
    }

    /// <summary>
    /// Event that triggers when the retrieve imaging order button is clicked.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void RetrieveButtonClick(object sender, EventArgs e)
    {
        Validate();
        if (!IsValid)
            return;

        var nric = HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant());
        if (!DatabaseHandler.IsInRole(nric, ExpectedUserRole))
        {
            ErrorMessage.Text = "NRIC provided does not belong to any patient.";
            return;
        }

        if (!DatabaseHandler.HasMedicalRecords(nric))
        {
            ErrorMessage.Text = "Patient has no medical records in system. Please update blood type to create it.";
            return;
        }

        if (!DatabaseHandler.HasOpenStudies(nric))
        {
            ErrorMessage.Text = "Patient still has no ongoing studies.";
            return;
        }

        Server.Transfer(RedirectUrl + "?Nric=" + nric);
    }
}