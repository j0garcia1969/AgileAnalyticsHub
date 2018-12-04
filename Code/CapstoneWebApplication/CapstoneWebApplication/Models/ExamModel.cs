using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationCapstone.Models
{
    public class ExamModel
    {
        //private static IEnumerable<SelectListItem> study_type = new SelectList(
        //    new List<Object> {
        //        new { value = 0, text = ""},
        //        new { value = 1, text = "Study 1"}, new { value = 2, text = "Study 2"},
        //        new { value = 3, text = "Study 3"}, new { value = 4, text = "Study 4"},
        //        new { value = 5, text = "Study 5"}, new { value = 6, text = "Study 6"},
        //        new { value = 7, text = "Study 7"}, new { value = 8, text = "Study 8"}
        //    },
        //    "value",
        //    "text"
        //);
        private static IEnumerable<SelectListItem> group_type = new SelectList(
            new List<Object> {
                new { value = 0, text = ""}
            },
            "value",
            "text"
        );
        private static IEnumerable<SelectListItem> subject_type = new SelectList(
            new List<Object> {
                new { value = 0, text = ""}
            },
            "value",
            "text"
         );

        private static IEnumerable<SelectListItem> configuration_type = new SelectList(
            new List<Object> {
                new { value = 0, text = ""}
            },
            "value",
            "text"
         );

        [DisplayName("Study")]
        public int SelectedStudyID { get; set; }
        [DisplayName("Study")]
        public string SelectedStudyDesc { get; set; }
        public IEnumerable<SelectListItem> StudyType { get; set; }

        [DisplayName("Group")]
        public int SelectedGroupID { get; set; }
        [DisplayName("Group")]
        public string SelectedGroupDesc { get; set; }
        public IEnumerable<SelectListItem> GroupType { get; set; } = group_type;

        [DisplayName("Subject")]
        public int SelectedSubjectID { get; set; }
        [DisplayName("Subject")]
        public string SelectedSubjectDesc { get; set; }
        public IEnumerable<SelectListItem> SubjectType { get; set; } = subject_type;

        [DisplayName("Session")]
        public int SelectedSessionID { get; set; }
        [DisplayName("Session")]
        public string SelectedSessionDesc { get; set; }
        public IEnumerable<SelectListItem> SessionType { get; set; } = configuration_type;

        [DisplayName("Configuration")]
        public int SelectedConfigurationID { get; set; }
        [DisplayName("Configuration")]
        public string SelectedConfigurationDesc { get; set; }
        public IEnumerable<SelectListItem> ConfigurationType { get; set; } = configuration_type;

        public List<string> study { get; set; } = new List<string>();
        public Dictionary<string, List<string>> map_study_to_group { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> map_study_group_to_subject { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> map_study_group_subject_to_session { get; set; } = new Dictionary<string, List<string>>();
    }
}