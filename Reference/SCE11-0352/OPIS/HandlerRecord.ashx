<%@ WebHandler Language="C#" Class="handlerRecord" %>

using System;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Data.SqlClient;

public class handlerRecord : IHttpHandler 
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;
    
    public void ProcessRequest (HttpContext context) 
    {
        SqlConnection conDocPicture = new SqlConnection(clinicConnStr);

        SqlCommand cmdDocPic = new SqlCommand("SELECT m.Image FROM MedicalRecord m WHERE m.RecordID = @RecordID", conDocPicture);

        SqlParameter ImageID = new SqlParameter("@RecordID", System.Data.SqlDbType.Int);
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