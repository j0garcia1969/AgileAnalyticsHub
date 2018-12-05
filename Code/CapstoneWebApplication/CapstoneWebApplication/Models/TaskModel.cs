using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationCapstone.Models
{
    public class TaskModel
    {
        private static IEnumerable<SelectListItem> task_type = new SelectList(
            new List<Object> {
                new { value = 0, text = "Text"}, new { value = 1, text = "Image"},
                new { value = 2, text = "Audio"}, new { value = 3, text = "Video"}
            },
            "value",
            "text"
        );
        private static IEnumerable<SelectListItem> feedback_type = new SelectList(
            new List<Object> {
                new { value = 0, text = "Text"}, new { value = 1, text = "Image"},
            },
            "value",
            "text"
        );

        // Desc: Provides the name of the selected item (Text, Image, Audio, Video).
        // ID: Provides the numerical value associated with the name of the selected item (0,1,2,3).

        public int ID { get; set; } = -1;

        [DisplayName("Task Type")]
        public int SelectedTaskTypeID { get; set; }
        [DisplayName("Task Type")]
        public string SelectedTaskTypeDesc { get; set; }
        public IEnumerable<SelectListItem> TaskTypes { get; set; } = task_type;
        
        [DisplayName("Task Item")]
        public string TaskItem { get; set; }
        [DisplayName("Duration")]
        public int Duration { get; set; }

        [DisplayName("Feedback Type")]
        public int SelectedFeedbackTypeID { get; set; }
        [DisplayName("Feedback Type")]
        public string SelectedFeedbackTypeDesc { get; set; }
        public IEnumerable<SelectListItem> FeedbackTypes { get; set; } = feedback_type;

        [DisplayName("Response")]
        public string TaskResponse { get; set; } = "";

        // Ramanjit's Edits: 
        // Kaleigh's Comment: What information does this variable hold?
        //List<TaskModel> outputs = new List<TaskModel>();
        //[DisplayName("Is Enabled")]
        //public bool? IsEnabled { get; set; }
    }
}