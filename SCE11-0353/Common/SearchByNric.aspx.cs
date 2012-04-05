using System;
using System.Web;
using System.Web.UI.WebControls;

public partial class Common_SearchByNric : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void NricExists(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.NricExists(HttpUtility.HtmlEncode(Nric.Text.Trim().ToUpperInvariant()));
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;
        
        Response.Redirect("~/Common/NricFound.aspx");
    }
}