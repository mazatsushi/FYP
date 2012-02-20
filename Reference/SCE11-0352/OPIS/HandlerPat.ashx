<%@ WebHandler Language="C#" Class="handlerpat" %>

using System;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Data.SqlClient;

public class handlerpat : IHttpHandler 
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;
    
    public void ProcessRequest (HttpContext context) 
    {
        SqlConnection conDocPicture = new SqlConnection(clinicConnStr);

        SqlCommand cmdDocPic = new SqlCommand("SELECT p.Image FROM Patientinfo p WHERE p.userId = @UserID", conDocPicture);

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