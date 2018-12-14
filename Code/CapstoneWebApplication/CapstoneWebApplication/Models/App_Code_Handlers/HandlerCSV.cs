using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplicationCapstone.Models;

namespace WebApplicationCapstone.Controllers
{
    public class HandlerCSV
    {
        private class enumList
        {
            public int value {get;set;}
            public string text {get;set;}
        }

        //public IEnumerable<SelectListItem> CSVtoList(string filePath, string search_item_name)
        //{

        //    int index = GetSearchItemIndex(search_item_name);

        //    int index_study = GetSearchItemIndex("study");
        //    int index_group = GetSearchItemIndex("group");
        //    int index_subject = GetSearchItemIndex("subject");
        //    int index_session = GetSearchItemIndex("session");

        //    ExamComponents examComponents = new ExamComponents();
        //    List<string> groups = new List<string>();
        //    List<string> subjects = new List<string>();
        //    List<string> sessions = new List<string>();

        //    //List<string[]> csvContents = new List<string[]>();
        //    List<enumList> csv_line_items = new List<enumList>();
        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        int i = 0;
        //        while (!reader.EndOfStream)
        //        {
        //            string[] line = reader.ReadLine().Split(new Char[] { ',' });
        //            string key_study = line[index_study];
        //            string key_study_group = line[index_study] + line[index_group];
        //            string key_study_group_subject = line[index_study] + line[index_group] + line[index_subject];

        //            groups.Add(line[index_group]);
        //            subjects.Add(line[index_subject]);
        //            sessions.Add(line[index_session]);
        //            if (examComponents.map_study_to_group.ContainsKey(key_study)) {
        //                examComponents.map_study_to_group[key_study] = groups.Distinct().ToList();
        //            }
        //            else
        //            {
        //                examComponents.map_study_to_group.Add(key_study, groups.Distinct().ToList());
        //            }

        //            if (examComponents.map_study_group_to_subject.ContainsKey(key_study_group))
        //            {
        //                examComponents.map_study_group_to_subject[key_study_group] = subjects.Distinct().ToList();
        //            }
        //            else
        //            {
        //                examComponents.map_study_group_to_subject.Add(key_study_group, subjects.Distinct().ToList());
        //            }

        //            if (examComponents.map_study_group_subject_to_session.ContainsKey(key_study_group_subject))
        //            {
        //                examComponents.map_study_group_subject_to_session[key_study_group_subject] = sessions.Distinct().ToList();
        //            }
        //            else
        //            {
        //                examComponents.map_study_group_subject_to_session.Add(key_study_group_subject, sessions.Distinct().ToList());
        //            }

                    


        //            var item = csv_line_items.FirstOrDefault(o => o.text == line[index]);

        //            // Store unique values.
        //            if (item == null) { 
        //                csv_line_items.Add(new enumList { value = i, text = line[index] });
        //                i++;
        //            }
        //        }
        //    }

        //    //csv_line_items.Distinct().ToList();

        //    IEnumerable<SelectListItem> csvContents = new SelectList(csv_line_items, "value", "text");

        //    return csvContents;
        //}

        public IEnumerable<SelectListItem> CSVtoList(string filePath, string search_item_name, string key)
        {
            int index = GetSearchItemIndex(search_item_name);
            int index_study = GetSearchItemIndex("study");
            int index_group = GetSearchItemIndex("group");
            int index_subject = GetSearchItemIndex("subject");
            int index_session = GetSearchItemIndex("session");

            //List<string[]> csvContents = new List<string[]>();
            List<enumList> csv_line_items = new List<enumList>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                int i = 1;
                while (!reader.EndOfStream)
                {

                    string[] line = reader.ReadLine().Split(new Char[] { ',' });

                    string temp_key = "";
                    if (search_item_name == "group")
                    {
                        temp_key = line[index_study];
                    }
                    else if (search_item_name == "subject")
                    {
                        temp_key = line[index_study] + "," + line[index_group];
                    }
                    else if (search_item_name == "session")
                    {
                        temp_key = line[index_study] + "," + line[index_group] + "," + line[index_subject];
                    }

                    if (key == temp_key) { 
                        var item = csv_line_items.FirstOrDefault(o => o.text == line[index]);

                        // Store unique values.
                        if (item == null)
                        {
                            csv_line_items.Add(new enumList { value = i, text = line[index] });
                            i++;
                        }
                    }
                }
            }

            IEnumerable<SelectListItem> csvContents = new SelectList(csv_line_items, "value", "text");
            return csvContents;
        }

        private int GetSearchItemIndex(string search_item_name)
        {
            int index = 0;

            switch (search_item_name)
            {
                case "study": index = 0; break;
                case "group": index = 1; break;
                case "subject": index = 2; break;
                case "session": index = 3; break;
            }

            return index;
        }

        public void ExportToCSV(string path, List<TaskModel> configuration)
        {
            var csv = new StringBuilder();

            var header = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",
                   "ID",
                   "Task Type",
                   "Task Content",
                   "Duration",
                   "Feedback Type",
                   "Task Response"
                   );

            csv.AppendLine(header);

            for (int i = 0; i < configuration.Count; i++)
            {
                var newLine = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",
                   i,
                   configuration[i].SelectedTaskTypeDesc,
                   configuration[i].TaskItem,
                   configuration[i].Duration,
                   configuration[i].SelectedFeedbackTypeDesc,
                   configuration[i].TaskResponse
                   );
                csv.AppendLine(newLine);
            }

            File.WriteAllText(path, csv.ToString());
        }
    }
}