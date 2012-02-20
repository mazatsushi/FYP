using System;
using System.Globalization;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    // Page load event
    protected void Page_Load(object sender, EventArgs e)
    {
        GreetUser();
        SetYearLabel();
    }

    // Method to greet the user
    private void GreetUser()
    {
        //greetLabel.Text = Request.IsAuthenticated ? user.Name : "Guest";
    }

    private void SetYearLabel()
    {
        yearLabel.Text = DateTime.Now.Year.ToString(CultureInfo.CurrentCulture);
    }
}
