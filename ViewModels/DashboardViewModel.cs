using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTime.ViewModels
{
    public class DashboardViewModel
    {
        public BarChartViewModel BarChart { get; set; } = new BarChartViewModel();

    }

    public class BarChartViewModel
    {
        public string[] Labels { get; set; }
        public List<Bar> DataSets { get; set; } = new List<Bar>();

        public class Bar
        {
            public string Label { get; set; }
            public string BackgroundColor { get; set; }
            public double[] Data { get; set; }
        }
    }
    
}
