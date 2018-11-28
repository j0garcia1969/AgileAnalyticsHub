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
            int duration = task_components.Duration;
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
        public ActionResult AssignConfiguration(Models.ConfigurationModel configuration)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            Session["config"] = configuration.Tasks;
            Session["edit_task_id"] = 0;
            Session["task_id"] = 0;

            string test = Session["task_id"].ToString();

            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            string task_type = configuration.Tasks[0].SelectedFeedbackTypeDesc;
            int duration = configuration.Tasks[0].Duration;
            string task_item = configuration.Tasks[0].TaskItem;

            if (configuration.Tasks[0].SelectedTaskTypeDesc == "Text")
            {
                //return RedirectToAction("Text.aspx", "Subject");
                return RedirectToAction("Text", "Subject");
                //return View("~/Views/Subject/Text", configuration);
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
            int duration = task_components.Duration;
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

        public void ExportToCSV()
        {
        //[DisplayName("Task Type")]
        //public int SelectedTaskTypeID { get; set; }
        //[DisplayName("Task Type")]
        //public string SelectedTaskTypeDesc { get; set; }
        //public IEnumerable<SelectListItem> TaskTypes { get; set; } = task_type;

        //[DisplayName("Task Item")]
        //public string TaskItem { get; set; }
        //[DisplayName("Duration")]
        //public int Duration { get; set; }

        //[DisplayName("Feedback Type")]
        //public int SelectedFeedbackTypeID { get; set; }
        //[DisplayName("Feedback Type")]
        //public string SelectedFeedbackTypeDesc { get; set; }
        //public IEnumerable<SelectListItem> FeedbackTypes { get; set; } = feedback_type;

        StringWriter sw = new StringWriter();
            sw.WriteLine("\"ID\",\"Task Type\",\"Task Item\",\"Duration\",\"Feedback Type\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=ExportedList.csv");
            Response.ContentType = "text/csv";

            var configuration = Session["config"] as List<Models.TaskModel>;

            for (int i=0; i < configuration.Count; i++)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                    i,
                    configuration[i].SelectedTaskTypeDesc,
                    configuration[i].TaskItem,
                    configuration[i].Duration,
                    configuration[i].SelectedFeedbackTypeDesc
                    ));
            }
            Response.Write(sw.ToString());
            Response.End();

        }
    }
}



// CSV Reader in Visual Studio (John)
// Write to CSV (Kaleigh).

// Login
// Create or Select Existing
// Display Task List
// Submit
// Go to Subject







//public class Location
//{
//    //properties here that describe a location
//}

//public class CsvHelperLocationRepository : ILocationRepository
//{
//    private readonly string _dataFileLocation;

//    public CsvHelperLocationRepository(string dataFileLocation)
//    {
//        _dataFileLocation = dataFileLocation;
//    }

//    public List<Location> GetLocations()
//    {
//        //use CsvHelper here to parse the CSV file and generate a list of Location objects to return
//        List<Location> something = new List<Location>();
//        return something;

//    }
//}

//public interface ILocationRepository
//{
//    List<Location> GetLocations();
//}