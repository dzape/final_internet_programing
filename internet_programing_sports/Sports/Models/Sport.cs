using System;
using System.Collections.Generic;
using Sports.Data;

namespace Sports.Models
{
    public partial class Sport
    {
        public int SportId { get; set; }
        public string SportName { get; set; }
        public string Description { get; set; }
    }
}
