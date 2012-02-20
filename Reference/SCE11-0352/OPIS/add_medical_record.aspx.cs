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

public partial class add_medical_record : System.Web.UI.Page
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;
    //string patNRIC;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (picFileUpload.PostedFile != null && picFileUpload.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(picFileUpload.FileName);
            if ((strExtension.ToUpper() == ".JPG") | (strExtension.ToUpper() == ".GIF"))
            {
                System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(picFileUpload.PostedFile.InputStream);
                int imageHeight = imageToBeResized.Height;
                int imageWidth = imageToBeResized.Width;
                int maxHeight = 120;
                int maxWidth = 160;
                imageHeight = (imageHeight * maxWidth) / imageWidth;
                imageWidth = maxWidth;

                if (imageHeight > maxHeight)
                {
                    imageWidth = (imageWidth * maxHeight) / imageHeight;
                    imageHeight = maxHeight;
                }
                Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                System.IO.MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;

                byte[] imageSize = new byte[stream.Length + 1];
                stream.Read(imageSize, 0, imageSize.Length);

                SqlConnection conMedicalRecord = new SqlConnection(clinicConnStr);
                string dt = DateTime.Now.ToLocalTime().ToString();
                SqlCommand cmdMedicalRecord = new SqlCommand("INSERT INTO [MedicalRecord](DepartmentID, SickDescription, UserId ,UpdatedTime ,Image) VALUES(@DeptID, @SickDesc, @UserId,@UpdatedTime ,@Image)", conMedicalRecord);
                cmdMedicalRecord.Parameters.Add("@UserId", Session["UserId"].ToString());
                cmdMedicalRecord.Parameters.Add("@DeptID", deptDDL.SelectedValue);
                cmdMedicalRecord.Parameters.Add("@SickDesc", sickDescTextBox.Text);
                cmdMedicalRecord.Parameters.Add("@UpdatedTime", dt);
                SqlParameter ImageUpload = new SqlParameter("@Image", SqlDbType.Image, imageSize.Length);
                ImageUpload.Value = imageSize;
                cmdMedicalRecord.Parameters.Add(ImageUpload);
                conMedicalRecord.Open();
                cmdMedicalRecord.ExecuteReader();
                Response.Write("<script>alert('You have successfully adding a new medical record!')</script>");
                conMedicalRecord.Close();
            }
        }

     /* SqlConnection conMedicalRecord = new SqlConnection(clinicConnStr);
        conMedicalRecord.Open();

        SqlCommand cmdSelectPatInfo = new SqlCommand("SELECT p.UserId FROM Patientinfo p, userinfo u WHERE p.userID = u.userId AND u.UserId = @UserId", conMedicalRecord);
        cmdSelectPatInfo.Parameters.Add("@UserId", Session["UserId"].ToString());
        SqlDataReader dtrPatient = cmdSelectPatInfo.ExecuteReader();
        conMedicalRecord.Close();
         string dt = DateTime.Now.ToLocalTime().ToString();
            SqlCommand cmdMedicalRecord = new SqlCommand("INSERT INTO [MedicalRecord](DepartmentID, SickDescription, UserId,UpdatedTime) VALUES(@DeptID, @SickDesc, @UserId,@UpdatedTime)", conMedicalRecord);
            cmdMedicalRecord.Parameters.Add("@UserId", Session["UserId"].ToString());
            cmdMedicalRecord.Parameters.Add("@DeptID", deptDDL.SelectedValue);
            cmdMedicalRecord.Parameters.Add("@SickDesc", sickDescTextBox.Text);
            cmdMedicalRecord.Parameters.Add("@UpdatedTime", dt);
         // cmdMedicalRecord.Parameters.Add("@Image", ImageUpload);
            conMedicalRecord.Open();
            cmdMedicalRecord.ExecuteReader();
            Response.Write("<script>alert('You have successfully adding a new medical record!')</script>");
            conMedicalRecord.Close();
    */
    }
}