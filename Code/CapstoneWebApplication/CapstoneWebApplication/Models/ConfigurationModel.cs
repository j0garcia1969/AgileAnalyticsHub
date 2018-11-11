using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplicationCapstone.Models
{
    public class ConfigurationModel
    {
        public string Name { get; set; } = "CONFIG_NAME_UNKNOWN";
        public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}