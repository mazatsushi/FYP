﻿using System;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Code behind for the ~/Physician/UpdatePatientBlood.aspx page
/// </summary>

public partial class Physician_UpdatePatientBloodType : System.Web.UI.Page
{
    private const string ExpectedUserRole = "Patient";
    private const string RedirectUrl = "~/Physician/UpdatePatientBloodType2.aspx";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Event that triggers when clicking on the retrieve user button.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void RetrieveButtonClick(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        var nric = NRIC.Text.Trim().ToUpperInvariant();
        Server.Transfer(RedirectUrl + "?Nric=" + nric + "&PatientName=" + DatabaseHandler.GetPatientName(nric));
    }

    /// <summary>
    /// Server side validation to check whether NRIC already exists
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void NricExists(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for existing NRIC
         */
        args.IsValid = DatabaseHandler.NricExists(HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant()));
    }

    /// <summary>
    /// Server side validation to check whether NRIC provided is a patient
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void IsPatient(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.IsInRole(NRIC.Text.Trim().ToUpperInvariant(), ExpectedUserRole);
    }
}