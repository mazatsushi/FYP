<%@ WebHandler Language="C#" Class="handler" %>

using System;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Data.SqlClient;

public class handler : IHttpHandler 
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;
    
    public void ProcessRequest (HttpContext context) 
    {
        SqlConnection conDocPicture = new SqlConnection(clinicConnStr);

        SqlCommand cmdDocPic = new SqlCommand("SELECT p.Image FROM Doctorinfo p, Department d WHERE d.DepartmentID = p.DepartmentID AND p.userId = @UserID", conDocPicture);

        SqlParameter ImageID = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
        ImageID.Value = context.Request.QueryString["ID"];
        cmdDocPic.Parameters.Add(ImageID);

        conDocPicture.Open();

        SqlDataReader dtrPicture = cmdDocPic.ExecuteReader();
        dtrPicture.Read();
        context.Response.BinaryWrite((byte[])dtrPicture["Image"]);
        
        dtrPicture.Close();
        conDocPicture.Close();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}