using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CapstoneWebApplication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationCapstone.Controllers
{
    public class ExperimenterController : Controller
    {
        // GET: Experimenter
        public ActionResult Assign(Models.ExamModel exam)
        {
            HandlerCSV handlerCSV = new HandlerCSV();

            string filePath = Server.MapPath("~/App_Data/exam_load.csv");
            IEnumerable<SelectListItem> exam_category_studies = handlerCSV.CSVtoList(filePath, "study","");
            //////List<string> exam_category_studies = handlerCSV.CSVtoList(filePath, "study").Distinct().ToList();
            //////ViewBag.exam_category_studies_unique = new SelectList(exam_category_studies, "value", "text");
            //Session["exam"] = exam_category_studies;
            exam.StudyType = exam_category_studies;
            Session["study_list"] = exam_category_studies;

            return View("Assign", exam);
        }

        public ActionResult Configuration(WebApplicationCapstone.Models.ConfigurationModel configuration, WebApplicationCapstone.Models.TaskModel task_components)
        {
            string today_year = DateTime.Today.Year.ToString();
            string today_month = DateTime.Today.Month.ToString();
            string today_day = DateTime.Today.Day.ToString();
            string today_hour = DateTime.Today.Hour.ToString();
            string today_minute = DateTime.Today.Minute.ToString();
            string today_second = DateTime.Today.Second.ToString();
            string timestamp_id = today_year + today_month + today_day + today_hour + today_minute + today_second;

            string config_name_default = configuration.Name;
            configuration.Name = Session["config_name"] as string;
            if ((configuration.Name == null) || (configuration.Name == config_name_default))
            {
                configuration.Name = "SESSION_CONFIG_" + timestamp_id;
                Session["config_name"] = configuration.Name;
            }

            configuration.Tasks = Session["config"] as List<Models.TaskModel>;

            return View("Configuration", configuration);
        }

        public ActionResult Create(WebApplicationCapstone.Models.TaskModel task_components)
        {
            return View("Create", task_components);
        }

        public ActionResult Edit(int id, Models.ConfigurationModel configuration, Models.TaskModel task_components)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            Session["edit_task_id"] = id;
            task_components = configuration.Tasks[id];
            //configuration.Tasks.RemoveAt(id);
            int type_id = task_components.SelectedTaskTypeID;
            string type_desc = task_components.TaskTypes.ToArray()[type_id].Text;
            string task = task_components.TaskItem;
            TimeSpan duration = task_components.Duration;
            int feedback_id = task_components.SelectedFeedbackTypeID;
            string feedback_type_desc = task_components.TaskTypes.ToArray()[feedback_id].Text;

            return View("Edit", task_components);
        }

        public ActionResult Details(int id, Models.ConfigurationModel configuration, Models.TaskModel task_components)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            Session["edit_task_id"] = id;
            task_components = configuration.Tasks[id];
            task_components.ID = id;

            return View("Details", task_components);
        }

        public ActionResult Delete(int id, Models.ConfigurationModel configuration, Models.TaskModel task_components)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            Session["edit_task_id"] = id;
            task_components = configuration.Tasks[id];
            //configuration.Tasks.RemoveAt(id);
            int type_id = task_components.SelectedTaskTypeID;
            string type_desc = task_components.TaskTypes.ToArray()[type_id].Text;
            int feedback_id = task_components.SelectedFeedbackTypeID;
            string feedback_type_desc = task_components.TaskTypes.ToArray()[feedback_id].Text;

            task_components.SelectedTaskTypeDesc = type_desc;
            task_components.SelectedFeedbackTypeDesc = feedback_type_desc;

            return View("Delete", task_components);
        }

        [HttpPost]
        public ActionResult AssignConfiguration(Models.ConfigurationModel configuration, Models.ExamModel exam)
        {

            int study_id = exam.SelectedStudyID;


            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            Session["config"] = configuration.Tasks;
            Session["edit_task_id"] = 0;
            Session["task_id"] = 0;

            string test = Session["task_id"].ToString();

            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            string task_type = configuration.Tasks[0].SelectedFeedbackTypeDesc;
            TimeSpan duration = configuration.Tasks[0].Duration;
            string task_item = configuration.Tasks[0].TaskItem;

            if (configuration.Tasks[0].SelectedTaskTypeDesc == "Text")
            {
                //return RedirectToAction("Text.aspx", "Subject");
                return RedirectToAction("Text", "Subject");
                //return View("~/Views/Subject/Text", configuration);
            }
            if (configuration.Tasks[0].SelectedTaskTypeDesc == "Video")
            {
                return RedirectToAction("Video", "Subject");
            }

            return View("Configuration", configuration);
        }

        [HttpPost]
        public ActionResult CreateTask(Models.TaskModel task_components, Models.ConfigurationModel configuration)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;

            //int id = task_components.ID + 1;
            int type_id = task_components.SelectedTaskTypeID;
            string type_desc = task_components.TaskTypes.ToArray()[type_id].Text;
            string task = task_components.TaskItem;
            TimeSpan duration = task_components.Duration;
            int feedback_id = task_components.SelectedFeedbackTypeID;
            string feedback_type_desc = task_components.TaskTypes.ToArray()[feedback_id].Text;

            task_components.SelectedTaskTypeDesc = type_desc;
            task_components.SelectedFeedbackTypeDesc = feedback_type_desc;

            if (configuration.Tasks != null)
            {
                //int id = configuration.Tasks[configuration.Tasks.Count-1].ID + 1;
                //task_components.ID = id;
                configuration.Tasks.Add(task_components);
            }
            else
            {
                //int id = task_components.ID + 1;
                //task_components.ID = id;
                configuration.Tasks = new List<Models.TaskModel>();
                configuration.Tasks.Add(task_components);
            }

            Session["config"] = configuration.Tasks;
            return View("Configuration", configuration);
        }

        [HttpPost]
        public ActionResult EditTask(Models.TaskModel task_components, Models.ConfigurationModel configuration)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            int edit_id = int.Parse(Session["edit_task_id"].ToString());

            int type_id = task_components.SelectedTaskTypeID;
            string type_desc = task_components.TaskTypes.ToArray()[type_id].Text;
            int feedback_id = task_components.SelectedFeedbackTypeID;
            string feedback_type_desc = task_components.TaskTypes.ToArray()[feedback_id].Text;

            task_components.SelectedTaskTypeDesc = type_desc;
            task_components.SelectedFeedbackTypeDesc = feedback_type_desc;

            if (configuration.Tasks != null)
            {
                configuration.Tasks[edit_id] = task_components;
            }

            Session["config"] = configuration.Tasks;
            return View("Configuration", configuration);
        }

        [HttpPost]
        public ActionResult DeleteTask(Models.TaskModel task_components, Models.ConfigurationModel configuration)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            int edit_id = int.Parse(Session["edit_task_id"].ToString());

            configuration.Tasks.RemoveAt(edit_id);

            Session["config"] = configuration.Tasks;
            return View("Configuration", configuration);
        }

        [HttpPost]
        public ActionResult PopulateListGroup(Models.ExamModel exam)
        {

            int study_id = exam.SelectedStudyID;
            exam.StudyType = Session["study_list"] as IEnumerable<SelectListItem>;

            string study_desc = exam.StudyType.ToArray()[study_id - 1].Text;
            string key_desc = study_desc;

            string filePath = Server.MapPath("~/App_Data/exam_load.csv");
            HandlerCSV handlerCSV = new HandlerCSV();
            IEnumerable<SelectListItem> exam_category_group = handlerCSV.CSVtoList(filePath, "group", key_desc);

            exam.GroupType = exam_category_group;

            Session["study_id"] = study_id;
            Session["group_list"] = exam_category_group;

            return View("Assign", exam);
        }
        
        [HttpPost]
        public ActionResult PopulateListSubject(Models.ExamModel exam)
        {
            
            int study_id = int.Parse(Session["study_id"].ToString());
            int group_id = exam.SelectedGroupID;

            exam.StudyType = Session["study_list"] as IEnumerable<SelectListItem>;
            exam.GroupType = Session["group_list"] as IEnumerable<SelectListItem>;

            string study_desc = exam.StudyType.ToArray()[study_id - 1].Text;
            string group_desc = exam.GroupType.ToArray()[group_id - 1].Text;
            string key_desc = study_desc + "," + group_desc;

            HandlerCSV handlerCSV = new HandlerCSV();
            string filePath = Server.MapPath("~/App_Data/exam_load.csv");
            IEnumerable<SelectListItem> exam_category_subjects = handlerCSV.CSVtoList(filePath, "subject", key_desc);

            exam.SelectedStudyID = study_id;
            exam.SubjectType = exam_category_subjects;

            Session["group_id"] = group_id;
            Session["subject_list"] = exam_category_subjects;

            return View("Assign", exam);
        }

        [HttpPost]
        public ActionResult PopulateListSession(Models.ExamModel exam)
        {

            int study_id = int.Parse(Session["study_id"].ToString());
            int group_id = int.Parse(Session["group_id"].ToString());
            int subject_id = exam.SelectedSubjectID;

            exam.StudyType = Session["study_list"] as IEnumerable<SelectListItem>;
            exam.GroupType = Session["group_list"] as IEnumerable<SelectListItem>;
            exam.SubjectType = Session["subject_list"] as IEnumerable<SelectListItem>;

            string study_desc = exam.StudyType.ToArray()[study_id - 1].Text;
            string group_desc = exam.GroupType.ToArray()[group_id - 1].Text;
            string subject_desc = exam.SubjectType.ToArray()[subject_id - 1].Text;

            string key_desc = study_desc + "," + group_desc + "," + subject_desc;

            HandlerCSV handlerCSV = new HandlerCSV();
            string filePath = Server.MapPath("~/App_Data/exam_load.csv");
            IEnumerable<SelectListItem> exam_category_sessions = handlerCSV.CSVtoList(filePath, "session", key_desc);

            exam.SelectedStudyID = study_id;
            exam.SelectedGroupID = group_id;
            exam.SessionType = exam_category_sessions;
            Session["session_list"] = exam_category_sessions;

            return View("Assign", exam);
        }

        [HttpPost]
        public ActionResult PopulateListConfiguration(Models.ExamModel exam)
        {

            int study_id = int.Parse(Session["study_id"].ToString());
            int group_id = int.Parse(Session["group_id"].ToString());
            int subject_id = exam.SelectedSubjectID;

            exam.StudyType = Session["study_list"] as IEnumerable<SelectListItem>;
            exam.GroupType = Session["group_list"] as IEnumerable<SelectListItem>;
            exam.SubjectType = Session["subject_list"] as IEnumerable<SelectListItem>;

            string study_desc = exam.StudyType.ToArray()[study_id - 1].Text;
            string group_desc = exam.GroupType.ToArray()[group_id - 1].Text;
            string subject_desc = exam.SubjectType.ToArray()[subject_id - 1].Text;

            string key_desc = study_desc + "," + group_desc + "," + subject_desc;

            HandlerCSV handlerCSV = new HandlerCSV();
            string filePath = Server.MapPath("~/App_Data/exam_load.csv");
            IEnumerable<SelectListItem> exam_category_sessions = handlerCSV.CSVtoList(filePath, "session", key_desc);

            exam.SelectedStudyID = study_id;
            exam.SelectedGroupID = group_id;
            exam.SessionType = exam_category_sessions;
            Session["session_list"] = exam_category_sessions;

            return View("Assign", exam);
        }

        //public ActionResult ConfigurationExport(Models.ExamModel exam)
        //{

        //    //StringWriter sw = new StringWriter();
        //    //sw.WriteLine("\"ID\",\"Task Type\",\"Task Item\",\"Duration\",\"Feedback Type\"");

        //    //Response.ClearContent();
        //    //Response.AddHeader("content-disposition", "attachment; filename=ExportedList.csv");
        //    //Response.ContentType = "text/csv";

        //    var configuration = Session["config"] as List<Models.TaskModel>;
        //    HandlerCSV handlerCSV = new HandlerCSV();
        //    string path = Server.MapPath("~/App_Data/exam_configuration.csv");
        //    string sw = handlerCSV.ExportToCSV(path, configuration);

        //    //for (int i=0; i < configuration.Count; i++)
        //    //{
        //    //    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
        //    //        i,
        //    //        configuration[i].SelectedTaskTypeDesc,
        //    //        configuration[i].TaskItem,
        //    //        configuration[i].Duration,
        //    //        configuration[i].SelectedFeedbackTypeDesc
        //    //        ));
        //    //}
        //    Response.Write(sw);
        //    //Response.End();

        //    View("Assign", exam);

        //}

        public ActionResult ConfigurationExport(Models.ConfigurationModel configuration)
        {

            //StringWriter sw = new StringWriter();
            //sw.WriteLine("\"ID\",\"Task Type\",\"Task Item\",\"Duration\",\"Feedback Type\"");

            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment; filename=ExportedList.csv");
            //Response.ContentType = "text/csv";

            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            HandlerCSV handlerCSV = new HandlerCSV();
            string filepath = Server.MapPath("~/App_Data/exam_configuration.csv");
            handlerCSV.ExportToCSV(filepath, configuration.Tasks);

            //for (int i=0; i < configuration.Count; i++)
            //{
            //    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
            //        i,
            //        configuration[i].SelectedTaskTypeDesc,
            //        configuration[i].TaskItem,
            //        configuration[i].Duration,
            //        configuration[i].SelectedFeedbackTypeDesc
            //        ));
            //}
            //Response.Write(sw);
            //Response.End();

            return View("Configuration", configuration);
        }


        [HttpPost]
        public ActionResult AssignConfiguration2(Models.ConfigurationModel configuration, Models.ExamModel exam)
        {

            string filepath = Server.MapPath("~/App_Data/");
            string bucket_name = "capstudy1";
            TransferUtility fileTransferUtility = new TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.USEast1));

            //string accessKey = "";
            //string secretKey = "";

            //string filepath_dest = Path.Combine(filepath, "test_file.csv");
            //using (StreamWriter _testData = new StreamWriter(filepath_dest, true))
            //{
            //    for (int i = 0; i < 4; i++)
            //    {
            //        var newLine = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",
            //           3,
            //           "hello",
            //           "world",
            //           "of",
            //           "mystery",
            //           "!"
            //           );
            //        //csv.AppendLine(newLine);
            //        _testData.WriteLine(newLine); // Write the file.
            //    }

            //}

            //// Get file from AWS S3 bucket.
            //string filepath_aws = "exam_configuration.csv";
            //string filepath_dest = Path.Combine(filepath, "temp_exam_configuration.csv");
            //using (AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            //{
            //    Amazon.S3.Model.GetObjectRequest request = new Amazon.S3.Model.GetObjectRequest
            //    {
            //        BucketName = bucket_name,
            //        Key = filepath_aws
            //    };
            //    try
            //    {
            //        using (GetObjectResponse response = client.GetObject(request))
            //        {
            //            if (!System.IO.File.Exists(filepath_dest))
            //            {
            //                response.WriteResponseStreamToFile(filepath_dest);
            //            }
            //        }
            //    }
            //    catch (Amazon.S3.AmazonS3Exception ex)
            //    {
            //        if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            //            //return false;

            //            //status wasn't not found, so throw the exception
            //            throw;
            //    }
            //}


            //// Upload a file to AWS S3 bucket.
            //filepath = Server.MapPath("~/App_Data/exam_configuration.csv");
            ///* Upload the file(file name is taken as the object key name). */
            //try
            //{
            //    fileTransferUtility.Upload(filepath, bucket_name);
            //}
            //catch (Exception ex)
            //{
            //    //System.Windows.Forms.MessageBox.Show(ex.Message);
            //}

            ///* Set the file storage class and the sharing type*/
            //TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest
            //{
            //    BucketName = bucket_name,
            //    FilePath = filepath,
            //    StorageClass = S3StorageClass.Standard,
            //    CannedACL = S3CannedACL.PublicReadWrite,
            //    ContentType = "application/vnd.ms-excel"

            //};
            //try
            //{
            //    //fileTransferUtility.Upload(fileTransferUtilityRequest);
            //    fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
            //}
            //catch
            //{
            //    //return false;
            //}

            return View("Configuration", configuration);
        }
    }
}