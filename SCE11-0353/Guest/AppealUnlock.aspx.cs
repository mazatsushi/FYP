﻿using System;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Code behind for the ~/Guest/AppealUnlock.aspx page
/// TODO: This page currently does nothing.
/// </summary>
public partial class Guest_AppealUnlock : System.Web.UI.Page
{
    private const string RedirectUrl = "~/Guest/AppealUnlockSent.aspx";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Determines if the provided user name is currently in use
    /// </summary>
    /// <param name="source">The server control that invoked this method</param>
    /// <param name="args">The arguments</param>
    protected void UserNameExists(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.UserNameExists(HttpUtility.HtmlEncode(UserName.Text.Trim()));
    }

    /// <summary>
    /// Event handler for when the next button is clicked
    /// </summary>
    /// <param name="sender">The server control that raised the event</param>
    /// <param name="e">The event arguments</param>
    protected void NextButtonClick(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        Server.Transfer(RedirectUrl);
    }
}