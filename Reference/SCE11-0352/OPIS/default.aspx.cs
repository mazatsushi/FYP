using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _default : System.Web.UI.Page 
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void loginButton_Click(object sender, EventArgs e)
    {
        SqlConnection objConn = new SqlConnection(clinicConnStr);
        objConn.Open();

        if (objConn.State == ConnectionState.Open)
        {
            SqlCommand objCmd = new SqlCommand("SELECT * FROM userinfo WHERE userName = @UserName AND userPwd = @Password AND userRole = @Role", objConn);
            objCmd.Parameters.Add("@UserName", userTextBox.Text);
            objCmd.Parameters.Add("@Password", pwdTextBox.Text);
            objCmd.Parameters.Add("@Role", roleDDL.SelectedValue);

            SqlDataReader objReader = objCmd.ExecuteReader();

            if (objReader.HasRows)
            {
                while (objReader.Read())
                {
                    string role = roleDDL.SelectedItem.ToString();
                    if (role == "Doctor")
                    {
                        Session["UserId"] = objReader["userId"].ToString();
                        Server.Transfer("doctor_info.aspx");
                    }
                    else if (role == "Patient")
                    {
                        Session["UserId"] = objReader["userId"].ToString();
                        Server.Transfer("patient_info.aspx");
                    }
                }

            }
            else
            {
                FailureText.Text = "Sorry Login Fail, Please Try Again.";
                userTextBox.Text = "";
                pwdTextBox.Text = "";
                userTextBox.Focus();
            }

            objConn.Close();
        }
    }
}