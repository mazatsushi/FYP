using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class pat_view_record : System.Web.UI.Page
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    /*--
    protected void viewRecordGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection conRecord = new SqlConnection(clinicConnStr);
        conRecord.Open();

        SqlCommand cmdDoctorInfo = new SqlCommand("DELETE FROM MedicalRecord WHERE RecordID = @recordID", conRecord);
        cmdDoctorInfo.Parameters.Add("@recordID", SqlDbType.Int).Value = Convert.ToInt32(viewRecordGridView.DataKeys[e.RowIndex].Values[0].ToString());

        
        cmdDoctorInfo.ExecuteReader();
    }
    --*/
    /*--
    protected void AccessDateTime(dateandtime)
    {
        string myDay, myMonth, myYear;

        myDay = Day(dateandtime);

        if (Len(myDay)= 1)
        {
            myDay="0" + myDay;
        }

        myMonth = Month(dateandtime);

        if (Len(myMonth)=1) 
        {
            myMonth="0" + myMonth;
        }

        myYear = Year(dateandtime);

        AccessDateTime = myYear + "-" + myMonth + "-" + myDay + " " + Time();
    }
    --*/




    /*--
    protected void viewRecordGridView_RowDataBound(object sender,
                         GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton l = (LinkButton)e.Row.FindControl("deleteButton");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record " +
            DataBinder.Eval(e.Row.DataItem, "RecordID") + "')");
        }
    }
     * --*/
    protected void update(object sender, EventArgs e)
    {
        Response.Write("<script>alert('You have successfully update medical record!')</script>");
    }
   
}