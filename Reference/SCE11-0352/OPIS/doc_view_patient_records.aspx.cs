using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class doc_view_patient_records : System.Web.UI.Page
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void update(object sender, EventArgs e)
    {
        Response.Write("<script>alert('You have successfully update medical record!')</script>");
    }

}