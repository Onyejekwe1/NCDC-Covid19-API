using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCDCApi.Models
{
    public class StateData
    {
        public string Name { get; set; }
        public string ConfirmedCases { get; set; }
        public string ActiveCases { get; set; }
        public string DischargedCases { get; set; }
        public string Deaths { get; set; }
    }
}
