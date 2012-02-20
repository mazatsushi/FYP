using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class doctor : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void logoutButton_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Server.Transfer("default.aspx");
    }
}