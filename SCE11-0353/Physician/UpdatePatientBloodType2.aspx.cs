using System;

/// <summary>
/// Code behind for the ~/Physician/UpdatePatientBlood2.aspx page
/// </summary>

public partial class Physician_UpdatePatientBloodType2 : System.Web.UI.Page
{
    private const string RedirectBack = "~/Physician/UpdatePatientBloodType.aspx";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        var name = Request.QueryString["PatientName"];
        if (string.IsNullOrEmpty(name))
            Server.Transfer(RedirectBack);

        PatientNameLabel.Text += name;
        Session["Nric"] = Request.QueryString["Nric"];

        BloodType.DataSource = DatabaseHandler.GetAllBloodTypes();
        BloodType.DataBind();
    }

    /// <summary>
    /// Event that triggers when clicking on the update blood type button.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void UpdateButtonClick(object sender, EventArgs e)
    {
        // The patient might or might not have prior medical records in the database
        var username = Session["Nric"].ToString();
        var bloodType = BloodType.Text.Trim();

        if (DatabaseHandler.HasMedicalRecords(username))
        {
            if (!DatabaseHandler.UpdateMedicalRecord(username, bloodType))
            {
                ErrorMessage.Text = "There was an error updating the medical records.";
                return;
            }
        }
        else
        {
            if (!DatabaseHandler.CreateMedicalRecord(username, bloodType))
            {
                ErrorMessage.Text = "There was an error creating the medical records.";
                return;
            }
        }

        Server.Transfer("~/Physician/UpdatePatientBloodTypeSuccess.aspx");
    }
}