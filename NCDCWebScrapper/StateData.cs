using System;
using System.Collections.Generic;
using System.Text;

namespace NCDCWebScrapper
{
    public class StateData
    {
        public string Name { get; set; }
        public string ConfirmedCases { get; set; }
        public string ActiveCases { get; set; }
        public string DischargedCases { get; set; }
        public string Deaths { get; set; }
    }


    public class DataSummary
    {
        public string SamplesTested { get; set; }
        public string ConfirmedCases { get; set; }
        public string ActiveCases { get; set; }
        public string DischargedCases { get; set; }
        public string Deaths { get; set; }
    }
}
