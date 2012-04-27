using System;
using System.Web;
using System.Web.UI.WebControls;

public partial class Common_SearchByNric : System.Web.UI.Page
{
    /// <summary>
    /// Code behind for the ~/Common/SearchByNric.aspx page
    /// </summary>

    /// <summary>
    /// Checks whether NRIC already exists
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void NricExists(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.NricExists(HttpUtility.HtmlEncode(Nric.Text.Trim().ToUpperInvariant()));
    }

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Event that triggers when a new user account has been created.
    /// We will add them to their respective role(s) programatically here.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;
        
        Response.Redirect("~/Common/NricFound.aspx");
    }
}