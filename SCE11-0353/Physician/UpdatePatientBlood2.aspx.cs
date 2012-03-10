using System;

public partial class Physician_UpdatePatientBlood2 : System.Web.UI.Page
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Event that triggers when clicking on the update blood type button.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void UpdateButtonClick(object sender, EventArgs e)
    {
        Server.Transfer("~/Physician/UpdateBloodSuccess.aspx");
    }
}