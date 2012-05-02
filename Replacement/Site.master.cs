using System;
using System.Globalization;


/// <summary>
/// Master page layout file for entire site
/// </summary>

public partial class SiteMaster : System.Web.UI.MasterPage
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        yearLabel.Text = DateTime.Now.Year.ToString(CultureInfo.CurrentCulture);
    }
}
