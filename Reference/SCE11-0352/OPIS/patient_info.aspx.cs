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

public partial class patient_info : System.Web.UI.Page
{
    string clinicConnStr = WebConfigurationManager.ConnectionStrings["OPISWebConnectionString"].ConnectionString;
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

                SqlCommand cmdUpload = new SqlCommand("UPDATE Patientinfo SET Image = @Image WHERE userID = @userID", conUpload);
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
                patDetailsView.DataBind();

            }

            /*--
            byte[] imageSize = new byte[picFileUpload.PostedFile.ContentLength];
            HttpPostedFile uploadedImage = picFileUpload.PostedFile;
            uploadedImage.InputStream.Read(imageSize, 0, (int)picFileUpload.PostedFile.ContentLength);
            --*/
        }
    }
  /* protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conPatientInfo = new SqlConnection(clinicConnStr);
        conPatientInfo.Open();

        SqlCommand cmdPatientInfo = new SqlCommand("SELECT p.* FROM Patientinfo p, userinfo u WHERE p.userID = u.userId AND u.userID = @UserID", conPatientInfo);
        cmdPatientInfo.Parameters.Add("@UserID", Session["UserId"].ToString());
       
        SqlDataReader dtrPatient = cmdPatientInfo.ExecuteReader();

        while (dtrPatient.Read())
        {
            patNameLabel.Text = dtrPatient["Name"].ToString();
            patGenderLabel.Text = dtrPatient["Gender"].ToString();
            patAgeLabel.Text = dtrPatient["Age"].ToString();
            patNRICLabel.Text = dtrPatient["NRIC"].ToString();
            patNationalityLabel.Text = dtrPatient["Nationality"].ToString();
            patPhoneNoLabel.Text = dtrPatient["Phone_number"].ToString();
            patEmailLabel.Text = dtrPatient["Email"].ToString();
            patAddressLabel.Text = dtrPatient["Address"].ToString();
        }

        conPatientInfo.Close();
    }*/
}