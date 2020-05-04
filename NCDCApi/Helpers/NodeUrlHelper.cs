using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCDCApi.Helpers
{
    public static class NodeUrlHelper
    {
        public  const string NcdcReportUrl = "https://covid19.ncdc.gov.ng/report/#!";

        public const string SampleTestedNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]";

        public const string ConfirmedCasesNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]";

        public const string ActiveCasesNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]";

        public const string DischargedCasesNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[3]/div[1]/div[1]";

        public const string DeathsNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[4]/div[1]/div[1]";

        public const string AllStateDataNode = "/html[1]/body[1]/div[2]/div[1]/div[1]/div[3]/div[1]/div[1]/div[2]";

        public static string GetJustNumbers(string casesSummary)
        {
            return new string(casesSummary.Where(char.IsDigit).ToArray());
        }
    }
}
