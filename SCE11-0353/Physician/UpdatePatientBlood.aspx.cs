using System;
using System.Web;
using System.Web.UI.WebControls;

public partial class Physician_UpdatePatientBlood : System.Web.UI.Page
{
    private const string NameLabel = "Patient Name: ";
    private const string role = "Patient";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Event that triggers when clicking on the retrieve user button.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void RetrieveButtonClick(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        var nric = HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant());

        // Make sure that the NRIC provided is in the patient role
        if (!DatabaseHandler.IsInRole(nric, role))
        {
            ErrorMessage.Text = HttpUtility.HtmlDecode("This user is not a patient.");
            return;
        }

        Server.Transfer("~/Physician/UpdatePatientBlood2.aspx");
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
}