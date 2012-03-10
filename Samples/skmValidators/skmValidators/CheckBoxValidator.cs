using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace skmValidators
{
    public class CheckBoxValidator : BaseValidator
    {
        [Description("Whether the CheckBox must be checked or unchecked to be considered valid."),
         DefaultValue(true)]
        public bool MustBeChecked
        {
            get
            {
                object o = ViewState["MustBeChecked"];
                if (o == null)
                    return true;
                else
                    return (bool)o;
            }
            set
            {
                ViewState["MustBeChecked"] = value;
            }
        }

        private CheckBox _ctrlToValidate = null;
        protected CheckBox CheckBoxToValidate
        {
            get
            {
                if (_ctrlToValidate == null)
                    _ctrlToValidate = FindControl(this.ControlToValidate) as CheckBox;
                
                return _ctrlToValidate;
            }
        }

        public string AssociatedButtonControlId
        {
            get
            {
                object o = ViewState["AssociatedButtonControlId"];
                if (o == null)
                    return string.Empty;
                else
                    return (string)o;
            }
            set
            {
                ViewState["AssociatedButtonControlId"] = value;
            }
        }

        private WebControl _associatedButton = null;
        protected WebControl AssociatedButton
        {
            get
            {
                if (_associatedButton == null && !string.IsNullOrEmpty(this.AssociatedButtonControlId))
                    _associatedButton = FindControl(this.AssociatedButtonControlId) as WebControl;

                return _associatedButton;
            }
        }

        protected override bool ControlPropertiesValid()
        {
            // Make sure ControlToValidate is set
            if (this.ControlToValidate.Length == 0)
                throw new HttpException(string.Format("The ControlToValidate property of '{0}' cannot be blank.", this.ID));

            // Ensure that the control being validated is a CheckBox
            if (this.CheckBoxToValidate == null)
                throw new HttpException(string.Format("The CheckBoxValidator can only validate controls of type CheckBox."));

            // Make sure AssociatedButton, if set, referenced a Button, LinkButton, or ImageButton            
            bool btnCtrlIdSetButNoRef = (!string.IsNullOrEmpty(this.AssociatedButtonControlId) && this.AssociatedButton == null);
            
            bool btnCtrlIdNotRefButton = false;
            if (AssociatedButton != null)
            {
                if (AssociatedButton is Button || AssociatedButton is LinkButton || AssociatedButton is ImageButton)
                    // No problem here...
                    btnCtrlIdNotRefButton = false;
                else
                    // AssociatedButton is an invalid type
                    btnCtrlIdNotRefButton = true;
            }

            if (btnCtrlIdSetButNoRef || btnCtrlIdNotRefButton)
                throw new HttpException(string.Format("The AssociatedButtonControlId property of '{0}', if set, must reference a Button, LinkButton, or ImageButton control.", this.ID));


            return true;    // if we reach here, everything checks out
        }

        protected override bool EvaluateIsValid()
        {
            // Make sure that the CheckBox is set as directed by MustBeChecked
            return CheckBoxToValidate.Checked == MustBeChecked;
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            // Add the client-side code (if needed)
            if (this.RenderUplevel)
            {
                // Indicate the mustBeChecked value and the client-side function to used for evaluation
                // Use AddAttribute if Helpers.EnableLegacyRendering is true; otherwise, use expando attributes                
                if (Helpers.EnableLegacyRendering())
                {
                    writer.AddAttribute("evaluationfunction", "CheckBoxValidatorEvaluateIsValid", false);
                    writer.AddAttribute("mustBeChecked", MustBeChecked ? "true" : "false", false);
                }
                else
                {
                    this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "CheckBoxValidatorEvaluateIsValid", false);
                    this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "mustBeChecked", MustBeChecked ? "true" : "false", false);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.RenderUplevel && this.Page != null)
            {
                // Register the client-side function using WebResource.axd (if needed)
                // see: http://aspnet.4guysfromrolla.com/articles/080906-1.aspx
                if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "skmValidators"))
                    this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "skmValidators", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "skmValidators.skmValidators.js"));

                // If there's an associated Button for this validator, add the script to enable/disable
                // it when the checkbox is clicked AND when the page is first loaded
                if (this.AssociatedButton != null)
                {
                    string callCheckBoxValidatorDisableButton = string.Format("CheckBoxValidatorDisableButton('{0}', {1}, '{2}');", this.CheckBoxToValidate.ClientID, MustBeChecked ? "true" : "false", this.AssociatedButton.ClientID);

                    this.CheckBoxToValidate.Attributes.Add("onclick", callCheckBoxValidatorDisableButton);

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), callCheckBoxValidatorDisableButton, true);
                }
            }
        }
    }
}
