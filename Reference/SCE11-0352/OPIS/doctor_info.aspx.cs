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

public partial class doctor_info : System.Web.UI.Page
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conDoctorInfo = new SqlConnection(clinicConnStr);
        conDoctorInfo.Open();

        SqlCommand cmdDoctorInfo = new SqlCommand("SELECT d.* FROM Doctorinfo p, Department d WHERE d.DepartmentID = p.DepartmentID AND p.userId = @UserID", conDoctorInfo);
        cmdDoctorInfo.Parameters.Add("@UserID", Session["UserId"].ToString());

        SqlDataReader dtrPatient = cmdDoctorInfo.ExecuteReader();

        while (dtrPatient.Read())
        {
            Session["DeptID"] = dtrPatient["DepartmentID"].ToString();
        }

        conDoctorInfo.Close();
    }
    protected void uploadButton_Click(object sender, EventArgs e)
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

                SqlConnection conUpload = new SqlConnection(clinicConnStr);

                SqlCommand cmdUpload = new SqlCommand("UPDATE Doctorinfo SET Image = @Image WHERE userId = @userID", conUpload);
                cmdUpload.Parameters.Add("@UserID", Session["UserId"].ToString());

                SqlParameter ImageUpload = new SqlParameter("@Image", SqlDbType.Image, imageSize.Length);
                ImageUpload.Value = imageSize;
                cmdUpload.Parameters.Add(ImageUpload);

                conUpload.Open();
                int uploadResult = cmdUpload.ExecuteNonQuery();

                conUpload.Close();
                if (uploadResult > 0)
                {
                    messageLabel.Text = "Image Uploaded";
                }
                docDetailsView.DataBind();

            }

           }
    }
}