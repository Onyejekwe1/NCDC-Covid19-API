using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace NCDCWebScrapper
{
    public class Program
    {
        static readonly ScrapingBrowser ScrapingBrowser = new ScrapingBrowser();    

        private const string NcdcReportUrl = "https://covid19.ncdc.gov.ng/report/#!";

        private const string SampleTestedNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]";

        private const string ConfirmedCasesNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]";

        private const string ActiveCasesNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]";

        private const string DischargedCasesNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[3]/div[1]/div[1]";

        private const string DeathsNode =
            "/html[1]/body[1]/div[2]/div[1]/div[1]/div[2]/div[4]/div[1]/div[1]";

        private const string AllStateDataNode = "/html[1]/body[1]/div[2]/div[1]/div[1]/div[3]/div[1]/div[1]/div[2]";

        static void Main(string[] args)
        {

            var summary = GetCovidSummaryModel();

            var allState = GetAllStateData();

        }

        static HtmlNode GetNcdcHtmlData()
        {
            WebPage page = ScrapingBrowser.NavigateToPage(new Uri(NcdcReportUrl));

            return page.Html;
        }


        public static List<StateData> GetAllStateData()
        {
            var ncdcWebsiteNodeResult = GetNcdcHtmlData();

            var nodes = ncdcWebsiteNodeResult.SelectNodes(AllStateDataNode).First();

            string[] lines = nodes.InnerText.Split(
                new[] {"\n\n\n\n\n" },
                StringSplitOptions.None
            );

            var totalStateModel = new List<StateData>();


            foreach (var line in lines)
            {
                var model = new StateData();
                string[] innerNode = line.Split(
                    new[] { "\n\n" },
                    StringSplitOptions.None
                );

                if (innerNode.Length == 5)
                {
                    model.Name = innerNode[0];
                    model.ConfirmedCases = innerNode[1];
                    model.ActiveCases = innerNode[2];
                    model.DischargedCases = innerNode[3];
                    model.Deaths = innerNode[4];

                    totalStateModel.Add(model);
                }

            }

            return totalStateModel;
        }

        private static StateData GetStateData(string state)
        {
            return GetAllStateData().FirstOrDefault(x => x.Name.ToLower().Contains(state));
        }


        private static string GetCovidSummary(string xpath)
        {
            var page = GetNcdcHtmlData();

            var summaryResult =
                page.OwnerDocument.DocumentNode.SelectSingleNode(xpath).InnerText;

            return GetJustNumbers(summaryResult);
        }

        static DataSummary GetCovidSummaryModel()
        {
            var summaries = new DataSummary();

            var testedSum = GetCovidSummary(SampleTestedNode);
            var confirmedSum = GetCovidSummary(ConfirmedCasesNode);
            var activeSum = GetCovidSummary(ActiveCasesNode);
            var dischargedSum = GetCovidSummary(DischargedCasesNode);
            var deathsSum = GetCovidSummary(DeathsNode);

            summaries.SamplesTested = testedSum;
            summaries.ConfirmedCases = confirmedSum;
            summaries.ActiveCases = activeSum;
            summaries.DischargedCases = dischargedSum;
            summaries.Deaths = deathsSum;


            return summaries;

        }

        private static string GetJustNumbers(string casesSummary)
        {
            return new string(casesSummary.Where(char.IsDigit).ToArray());
        }
    }
}
