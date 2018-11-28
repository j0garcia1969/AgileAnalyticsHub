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

        public ActionResult Text(Models.ConfigurationModel configuration)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;

            return View("Text", configuration);
        }

        [HttpPost]
        public ActionResult NextTask(Models.ConfigurationModel configuration)
        {
            configuration.Tasks = Session["config"] as List<Models.TaskModel>;
            int edit_id = int.Parse(Session["edit_task_id"].ToString());
            
            edit_id = edit_id + 1;
            Session["edit_task_id"] = edit_id;
            return View("Text", configuration);
        }
    }
}