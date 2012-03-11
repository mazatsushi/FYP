using System;

/// <summary>
/// Code behind for the ~/Physician/CreateImagingOrder2.aspx page
/// </summary>

public partial class Physician_ManageImagingOrder2 : System.Web.UI.Page
{
    private const string RedirectBack = "~/Physician/ManageImagingOrder.aspx";
    private const string Redirect = "~/Physician/ViewStudy.aspx";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            return;
        }

        var nric = Request.QueryString["Nric"];
        if (string.IsNullOrEmpty(nric))
            Response.Redirect(RedirectBack);

        PatientNameLabel.Text += DatabaseHandler.GetUserName(nric);
        StudyGrid.DataSource = DatabaseHandler.GetStudies(nric);
        StudyGrid.DataBind();
    }

    protected void RetrieveButtonClick(object sender, EventArgs e)
    {
        Validate();
        if (!IsValid)
            return;

        var id = 0;
        if (!int.TryParse(Study.Text.Trim(), out id))
        {
            ErrorMessage.Text = "Please enter a valid Study ID";
            return;
        }

        Server.Transfer(Redirect + "?StudyID=" + id);
    }
}