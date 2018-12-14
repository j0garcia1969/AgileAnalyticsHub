using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationCapstone.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Audio(Models.ConfigurationModel configuration, Models.TaskModel task)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;

            int edit_id = int.Parse(Session["edit_task_id"].ToString());
            task = configuration.Tasks[edit_id];

            return View("Audio", task);
        }

        public ActionResult Image(Models.ConfigurationModel configuration, Models.TaskModel task)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;

            int edit_id = int.Parse(Session["edit_task_id"].ToString());
            task = configuration.Tasks[edit_id];

            return View("Image", task);
        }

        public ActionResult Text(Models.ConfigurationModel configuration, Models.TaskModel task)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;

            int edit_id = int.Parse(Session["edit_task_id"].ToString());
            task = configuration.Tasks[edit_id];

            return View("Text", task);
        }

        public ActionResult Video(Models.ConfigurationModel configuration, Models.TaskModel task)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;

            int edit_id = int.Parse(Session["edit_task_id"].ToString());
            task = configuration.Tasks[edit_id];

            return View("Video", task);
        }

        public ActionResult Complete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NextTask(Models.TaskModel task)
        {

            ModelState.Clear();
            Models.ConfigurationModel configuration = new Models.ConfigurationModel();

            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            int edit_id = int.Parse(Session["edit_task_id"].ToString());

            // Add the user's task response to the list.
            Models.TaskModel previous_task = configuration.Tasks[edit_id];
            previous_task.TaskResponse = task.TaskResponse;
            configuration.Tasks[edit_id] = previous_task;

            //// Write to the responses to a csv file.
            HandlerCSV handlerCSV = new HandlerCSV();
            string filepath = Server.MapPath("~/App_Data/exam_responses.csv");
            handlerCSV.ExportToCSV(filepath, configuration.Tasks);

            // Upload file to AWS.
            S3Uploader s3 = new S3Uploader();
            s3.UploadFile(filepath);

            // Get the next task or end the exam.
            if (edit_id < configuration.Tasks.Count - 1) {

                // Increment the task id.
                edit_id = edit_id + 1;

                // Assign the session variables.
                Session["config"] = configuration.Tasks;
                Session["edit_task_id"] = edit_id;

                // Prepare the next task.
                Models.TaskModel next_task = configuration.Tasks[edit_id];
                next_task.TaskResponse = null;

                // Identify the task type of the next task.
                string task_type_description = next_task.SelectedTaskTypeDesc;
                
                // Navigate to the next view.
                if (task_type_description == "Audio") { return View("Audio", next_task); }
                else if (task_type_description == "Image") { return View("Image", next_task); }
                else if (task_type_description == "Text") { return View("Text", next_task); }
                else if (task_type_description == "Video") { return View("Video", next_task); }
                else { return View("Text", next_task); }

            }
            else
            {
                return View("Complete");
            }

            
        }
    }
}