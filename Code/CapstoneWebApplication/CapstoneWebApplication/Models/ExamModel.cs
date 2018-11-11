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
        private static IEnumerable<SelectListItem> study_type = new SelectList(
            new List<Object> {
                    new { value = 0, text = "Study 1"}, new { value = 1, text = "Study 2"},
                    new { value = 2, text = "Study 3"}, new { value = 3, text = "Study 4"},
                    new { value = 4, text = "Study 5"}, new { value = 5, text = "Study 6"},
                    new { value = 6, text = "Study 7"}, new { value = 7, text = "Study 8"}
            },
            "value",
            "text"
        );
        private static IEnumerable<SelectListItem> group_type = new SelectList(
            new List<Object> {
                new { value = 0, text = "Group 1"}, new { value = 1, text = "Group 2"},
                new { value = 2, text = "Group 3"}, new { value = 3, text = "Group 4"},
                new { value = 4, text = "Group 5"}, new { value = 5, text = "Group 6"},
                new { value = 6, text = "Group 7"}, new { value = 7, text = "Group 8"}
            },
            "value",
            "text"
        );
        private static IEnumerable<SelectListItem> subject_type = new SelectList(
            new List<Object> {
                new { value = 0, text = ""},
                new { value = 1, text = "Subject 1"}, new { value = 2, text = "Subject 2"},
                new { value = 3, text = "Subject 3"}, new { value = 4, text = "Subject 4"},
                new { value = 5, text = "Subject 5"}, new { value = 6, text = "Subject 6"},
                new { value = 7, text = "Subject 7"}, new { value = 8, text = "Subject 8"}
            },
            "value",
            "text"
         );

        private static IEnumerable<SelectListItem> configuration_type = new SelectList(
            new List<Object> {
                new { value = 0, text = ""},
                new { value = 1, text = "Configuration 1"}, new { value = 2, text = "Configuration 2"},
                new { value = 3, text = "Configuration 3"}, new { value = 4, text = "Configuration 4"},
                new { value = 5, text = "Configuration 5"}, new { value = 6, text = "Configuration 6"},
                new { value = 7, text = "Configuration 7"}, new { value = 8, text = "Configuration 8"}
            },
            "value",
            "text"
         );

        [DisplayName("Study")]
        public int SelectedStudyID { get; set; }
        [DisplayName("Study")]
        public string SelectedStudyDesc { get; set; }
        public IEnumerable<SelectListItem> StudyType { get; set; } = study_type;

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

        [DisplayName("Configuration")]
        public int SelectedConfigurationID { get; set; }
        [DisplayName("Configuration")]
        public string SelectedConfigurationDesc { get; set; }
        public IEnumerable<SelectListItem> ConfigurationType { get; set; } = configuration_type;
    }
}