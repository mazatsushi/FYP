using System;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        switch (DatabaseHandler.FindMostPrivilegedRole(User.Identity.Name))
        {
            case 0:
                Response.Redirect("~/Admin/Default.aspx");
                break;
            case 1:
                Response.Redirect("~/Physician/Default.aspx");
                break;
            case 2:
                Response.Redirect("~/Radiologist/Default.aspx");
                break;
            case 3:
                Response.Redirect("~/Staff/Default.aspx");
                break;
            case 4:
                Response.Redirect("~/Patient/Default.aspx");
                break;
        }
    }
}
