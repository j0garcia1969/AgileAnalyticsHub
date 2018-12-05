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

        public ActionResult Text(Models.ConfigurationModel configuration, Models.TaskModel task)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            
            int edit_id = int.Parse(Session["edit_task_id"].ToString());
            task = configuration.Tasks[edit_id];

            return View("Text", task);
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

            // Write to the responses to a csv file.
            HandlerCSV handlerCSV = new HandlerCSV();
            string filepath = Server.MapPath("~/App_Data/exam_responses.csv");
            handlerCSV.ExportToCSV(filepath, configuration.Tasks);

            // Upload file to AWS.
            S3Uploader s3 = new S3Uploader();
            s3.UploadFile(filepath);

            if (edit_id < configuration.Tasks.Count - 1) {

                //string response = configuration.TaskResponse;
                //configuration.Tasks[edit_id].TaskResponse = response;

                edit_id = edit_id + 1;

                Session["config"] = configuration.Tasks;
                Session["edit_task_id"] = edit_id;

                // Prepare the next task.
                Models.TaskModel next_task = configuration.Tasks[edit_id];
                next_task.TaskResponse = null;
                
                return View("Text", next_task);
            }
            else
            {
                return View("Complete");
            }

            
        }
    }
}