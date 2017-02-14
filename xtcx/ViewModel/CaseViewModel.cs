using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtcx.ViewModel
{
    public class CaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }            
        public int OrganizationId { get; set; }
    }

    public class PolicyViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public int OrganizationId { get; set; }
    }

    public class PerformanceViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public int OrganizationId { get; set; }
    }
}