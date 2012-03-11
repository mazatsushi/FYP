using System;
using System.Web;
using System.Web.UI.WebControls;

public partial class Radiologist_StartNewSeries : System.Web.UI.Page
{
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

    protected void CreateButtonClick(object sender, EventArgs e)
    {
        Validate();
        if (!IsValid)
            return;

        var nric = HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant());

        if (!DatabaseHandler.IsInRole(nric, "Patient"))
        {
            ErrorMessage.Text = "NRIC provided is not a patient.";
        }

        if (!DatabaseHandler.HasOpenStudies(nric))
        {
            ErrorMessage.Text = "Patient has no imaging orders.";
            return;
        }

        Server.Transfer("~/Radiologist/StartNewSeries2.aspx?NRIC=" + HttpUtility.HtmlEncode(NRIC.Text.Trim()));
    }
}