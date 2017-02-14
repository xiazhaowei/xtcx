using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtcx.ViewModel
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }        
        public string OrgName { get; set; }
    }
}