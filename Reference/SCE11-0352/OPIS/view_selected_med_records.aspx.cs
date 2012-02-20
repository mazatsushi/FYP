using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
public partial class view_selected_med_records : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;
        string strWhere="";
        if (!IsPostBack)
        {

           for(int i =0;i<Request.QueryString.Count-1;i++)
               strWhere += Request.QueryString["ID"]; 
         
        }
              
        SqlConnection conPatientInfo = new SqlConnection(clinicConnStr);
        conPatientInfo.Open();
        SqlCommand cmdPatientInfo = new SqlCommand("SELECT d.DepartmentName, m.RecordID, m.SickDescription, m.DoctorComment, m.UpdatedTime, m.Image FROM MedicalRecord AS m INNER JOIN Department AS d ON m.DepartmentID = d.DepartmentID INNER JOIN Patientinfo AS p ON m.userID = p.userID WHERE m.RecordID in (" + strWhere + ") ", conPatientInfo);
           //     SqlCommand cmdPatientInfo = new SqlCommand("SELECT d.DepartmentName, m.RecordID, m.SickDescription, m.DoctorComment, m.UpdatedTime, m.Image FROM MedicalRecord AS m INNER JOIN Department AS d ON m.DepartmentID = d.DepartmentID INNER JOIN Patientinfo AS p ON m.userID = p.userID WHERE m.RecordID in (" + strWhere + ") ", conPatientInfo);
         GridView1.DataSource = cmdPatientInfo.ExecuteReader();
         GridView1.DataBind();
         conPatientInfo.Close();
            }
        
    }
