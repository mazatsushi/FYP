﻿using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using DB_Handlers;

namespace Admin
{
    /// <summary>
    /// Code behind for the ~/Physician/AddDrug.aspx page
    /// </summary>
    public partial class AddDrug : System.Web.UI.Page
    {
        private const string SuccessRedirect = "~/Physician/AddDrugSuccess.aspx";

        /// <summary>
        /// Event that triggers when the add button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void AddButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;
            /*
             * At this point, all user entered information has been verified.
             * We shall now programmatically insert the new drug allergy to database
             */
            if (!DrugHandler.AddNewDrug(new CultureInfo("en-SG").TextInfo.ToTitleCase(HttpUtility.HtmlEncode(DrugName.Text.Trim()))))
                return;

            Server.Transfer(ResolveUrl(SuccessRedirect));
        }

        /// <summary>
        /// Server side validation to check whether drug name already exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void DrugNotExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !DrugHandler.DrugExists(HttpUtility.HtmlEncode(new CultureInfo("en-SG").TextInfo.ToTitleCase(DrugName.Text.Trim())));
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}