using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


public partial class pat_update_record : System.Web.UI.Page
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //SqlConnection conPatientInfo = new SqlConnection(clinicConnStr);
        //conPatientInfo.Open();

        //SqlCommand cmdPatientInfo = new SqlCommand("SELECT p.* FROM Patientinfo p, userinfo u WHERE p.userID = u.userId AND u.userID = @UserID", conPatientInfo);
        //cmdPatientInfo.Parameters.Add("@UserID", Session["UserId"].ToString());

        //SqlDataReader dtrPatient = cmdPatientInfo.ExecuteReader();


        //conPatientInfo.Close();

    }
    protected void CompareGD_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckBox chk;

        foreach (GridViewRow rowItem in CompareGD.Rows)
        {
            // FindControl function gets the control placed inside the GridView control from the specified cell
            // FindControl fucntion accepts string id of the control that you want to access
            // type casting of control allows to access the properties of that particular control
            // here checkbox control type cast is used to access its properties
            chk = (CheckBox)(rowItem.Cells[0].FindControl("selectCB"));


            // chk.checked will access the checkbox state on button click event
            if (chk.Checked)
            {
                Response.Write(chk.ClientID + "<br />");
            }
        }
        
        //stringbuilder str = new stringbuilder();

        //for (int i = 0; i < CompareGD.Rows.Count; i++)
        //{
        //    GridViewRow row = CompareGD.Rows[i];
        //    bool isChecked = ((CheckBox)row.FindControl("selectCB")).Checked;

        //    if (isChecked)
        //    {
        //        str.Append(CompareGD.Rows[i].Cells[2].Text);
        //    }
        //}
        //Response.Write(str.ToString());        

    }
}