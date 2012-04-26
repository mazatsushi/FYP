using System;
using System.Globalization;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    // Page load event
    protected void Page_Load(object sender, EventArgs e)
    {
        yearLabel.Text = DateTime.Now.Year.ToString(CultureInfo.CurrentCulture);
    }
}
