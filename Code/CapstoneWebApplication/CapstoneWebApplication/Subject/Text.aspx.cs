using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.S3;
using Amazon.S3.Model;


namespace CapstoneWebApplication.Subject
{
    public partial class Text1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label_Value.Text = Session["newSession"].ToString();
            if (!IsPostBack)
            {
                Session["Timer"] = DateTime.Now.AddMinutes(2).ToString();
            }
        }


        //AWS get an object
        public void GetObject(AmazonS3Client client, string bucketName, String keyName)
        {
            try
            {
                using (client)
                {
                    Amazon.S3.Model.GetObjectRequest request = new Amazon.S3.Model.GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = keyName
                    };
                    using (GetObjectResponse response = client.GetObject(request))
                    {
                        String dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), keyName);
                        if (!File.Exists(dest))
                        {
                            response.WriteResponseStreamToFile(dest);
                        }

                    }
                }
            }
            catch
            {

            }
        }





        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Image.aspx");
        }


        //This method save the user text box input into the file and 
        public void SaveText()
        {

            String path = @"C:\S3Temp\Problem1.csv";
            //This path is added only once to the file 
            if (!File.Exists(path))
            {
                //file is creted for writing
                using (StreamWriter sw = File.CreateText(path))
                {
                    //sw.WriteLine("Text" + "\t"  + "End Time");
                }
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(TextBox1.Text);
            }
        }

        //when user press the save button, it will automatically call the  S3Uploader class.
        //In that class, we are storing the file into the aws bucket
        protected void save_Click(object sender, EventArgs e)
        {
            SaveText();
            S3Uploader s3 = new S3Uploader();
            s3.UploadFile();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, DateTime.Parse(Session["Timer"].ToString())) < 0)
            {
                litMsg.Text = "Time Left: " + ((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalMinutes)
                    .ToString() + " Minute " + (((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalSeconds) % 60)
                .ToString() + "seconds";
            }
            else
            {
                Response.Redirect("tablet.aspx");
            }
        }
    }
}