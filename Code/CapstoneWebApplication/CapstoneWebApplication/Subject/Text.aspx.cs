using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net.Http;
using System.Web.Mvc;
using WebApplicationCapstone.Models;
using System.Threading.Tasks;
using System.Drawing;

namespace CapstoneWebApplication.Subject
{
    public partial class Text1 : System.Web.UI.Page
    {
        public object ForeColor { get; set; }

        //public ConfigurationModel id { get; private set; }
        //public TaskModel configuration { get; private set; }
        //public object Models { get; private set; }
        //public object task_components { get; private set; }
        public string b { get; private set; }
        public string a { get; private set; }
        public object BackColor { get; private set; }

        // public string inputResponse { get; set; }
        //public string LitMsg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Added Code.
            List<TaskModel> configuration = Session["config"] as List<TaskModel>;
            int task_id = int.Parse(Session["task_id"].ToString());

            string task_type = configuration[task_id].SelectedTaskTypeDesc; // Text, Image, Audio, or Video (Type of task).
            string task_content = configuration[task_id].TaskItem; // Content of (question)
            int duration = configuration[task_id].Duration; // Duration of task.
            string feedback_type = configuration[task_id].SelectedFeedbackTypeDesc; // Text, or Image (User response).

            Label_Value.Text = task_content;

            if (!IsPostBack)
            {
                // Display_Output(task_components,configuration);
                //new Timer_Method().Start();

                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => { Timer_Method(); }));
                // tasks.Add(Task.Run(() => { Display_Output(); }));
                Task.WaitAll(tasks.ToArray());
            }
        }
        public void Timer_Method()
        {
            if (!IsPostBack)
            {
                Session["Timer"] = DateTime.Now.AddMinutes(5).ToString();
                // ex.AssignConfiguration(id,configuration,task_components);
                /*  List<TaskModel> configuration = Session["config"] as List<WebApplicationCapstone.Models.TaskModel>;
                      Session["config"] = configuration[0].Duration;
                      double Time;
                      double.TryParse(Session["config"].ToString(), out Time);
                      // Label_Value.Text = Session["config"].ToString();
                      Session["Timer"] = DateTime.Now.AddMinutes(Time).ToString();          */
            }
        }

        [HttpPost]
        public void Display_Output()
        {
            // ExperimenterController ex = new ExperimenterController();
            // ex.AssignConfiguration(id,configuration,task_components);
            /*List<TaskModel> configuration = Session["config"] as List<WebApplicationCapstone.Models.TaskModel>;
            Session["config"] = configuration[0].TaskItem;
            double output;
            //double.TryParse(Session["config"].ToString(), out output);
            Label_Value.Text = Session["config"].ToString();
            */
            //List<TaskModel> configuration = Session["config"] as List<WebApplicationCapstone.Models.TaskModel>;
            //Session["config"] = configuration[0].TaskItem;
            //Label_Value.Text = Session["config"].ToString();
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
        /*
         Exporting to csv
          public void SaveText1()
          {
              StringWriter sw = new StringWriter();
              sw.WriteLine("\"Text\",\"End Time\"");
              Response.ClearContent();
              Response.AddHeader("content-disposition", "attachment;filename= Problem.csv");
              Response.ContentType = "text/csv";
             var text1 = rough();
             foreach (var t in text1)
             {
                 sw.WriteLine(String.Format("\"{0}\",\"{1}\"", litMsg.Text, inputResponse.InnerText));
             }
              Response.Write(sw.ToString());
              Response.End();
          }
          */

        public void SaveText()
        {
            String path = @"C:\S3Temp\Problem1.csv";
            //This path is added only once to the file 
            if (!File.Exists(path))
            {
                //file is creted for writing
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("\"Text\",\"End Time\"");
                    Response.ClearContent();
                }
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(String.Format("\"{0}\",\"{1}\"", litMsg.Text, inputResponse.InnerText));
                sw.Close();
            }
        }
        /*
         * Sowing the value into gridView, it works
        public void SaveText()
    {
        var grid = new GridView();
        grid.DataSource = from data in rough()
                          select new {
                              time = data.a, response = data.b
                          };
        grid.DataBind();
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename= Problem.xls");
        Response.ContentType = "application/excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);
        grid.RenderControl(htmlTextWriter);
        Response.Write(sw.ToString());
        Response.End();
    }
    public List<Text1> rough()
    {
        List<Text1> text = new List<Text1>
        {
        new Text1{a = litMsg.Text},
        new Text1{b = inputResponse.InnerText}
    };
        return text;
    }
    */


        //when user press the save button, it will automatically call the  S3Uploader class.
        //In that class, we are storing the file into the aws bucket
        protected void Save_Button_Click(object sender, EventArgs e)
        {
            SaveText();
            S3Uploader s3 = new S3Uploader();
            s3.UploadFile();
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
                Response.Redirect("Audio.aspx");
            }
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            // Added Code.
            List<TaskModel> configuration = Session["config"] as List<TaskModel>;
            int task_id = int.Parse(Session["task_id"].ToString());
            task_id = task_id + 1;

            string task_type = configuration[task_id].SelectedTaskTypeDesc; // Text, Image, Audio, or Video (Type of task).
            string task_content = configuration[task_id].TaskItem; // Content of (question)
            int duration = configuration[task_id].Duration; // Duration of task.
            string feedback_type = configuration[task_id].SelectedFeedbackTypeDesc; // Text, or Image (User response).

            if (task_type == "Text") {
                Label_Value.Text = task_content;
            }

            Session["task_id"] = task_id;

            //Response.Redirect("Audio.aspx");
        }
    }
}