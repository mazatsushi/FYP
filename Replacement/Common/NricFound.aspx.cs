using System;

namespace Common
{
    /// <summary>
    /// Code behind for the ~/Common/NricFound.aspx page
    /// </summary>
    public partial class NricFound : System.Web.UI.Page
    {
        /// <summary>
        /// Event that triggers when the search again button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void CancelButtonClick(object sender, EventArgs e)
        {
            Server.Transfer("~/Common/SearchByNric.aspx?ReturnUrl=" + Request.QueryString["ReturnUrl"]);
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var nric = Request.QueryString["Nric"];
            if (String.IsNullOrEmpty(nric))
                Server.Transfer("~/Common/SearchByNric.aspx?ReturnUrl=" + Request.QueryString["ReturnUrl"]);

            NricDetails.DataSource = DatabaseHandler.GetParticularsFromNric(nric);
            NricDetails.DataBind();
        }

        /// <summary>
        /// Event that triggers when the search button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ProceedButtonClick(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            var nric = Request.QueryString["Nric"];
            Response.Redirect(Request.QueryString["ReturnUrl"] + "?Nric=" + nric + "&Checksum=" + CryptoHandler.GetHash(nric));
        }
    }
}