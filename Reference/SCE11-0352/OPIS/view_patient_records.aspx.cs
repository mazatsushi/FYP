using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_patient_records : System.Web.UI.Page
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(clinicConnStr);
        SqlCommand objCmd = new SqlCommand("SELECT DISTINCT p.userID, p.Name, p.Gender, p.Age, p.NRIC, p.Nationality, p.Phone_number, p.Email, p.Address FROM Patientinfo AS p INNER JOIN MedicalRecord AS m ON p.userID = m.userID WHERE (m.DepartmentID = @deptIDParam)", objConn);
        objCmd.Parameters.Add("@deptIDParam", Session["DeptID"].ToString()); 
        
        try
        {
            objConn.Open();

            viewPatGridView.DataSource = objCmd.ExecuteReader();
            viewPatGridView.DataBind();

            objConn.Close();
            objCmd.Dispose();
        }
        catch (Exception ex)
           {
               Console.WriteLine("Exception occured");
           }
        
    }
    protected void update(object sender, EventArgs e)
    {
        Response.Write("<script>alert('You have successfully update medical record!')</script>");
    }

}