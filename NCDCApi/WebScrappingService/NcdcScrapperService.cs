using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NCDCApi.Helpers;
using NCDCWebScrapper;
using ScrapySharp.Network;

namespace NCDCApi.WebScrappingService
{
    public class NcdcScrapperService : INcdcScrapperService
    {
        public HtmlNode GetNcdcHtmlData()
        {
            var scrapingBrowser = new ScrapingBrowser();
            WebPage page = scrapingBrowser.NavigateToPage(new Uri(NodeUrlHelper.NcdcReportUrl));
            return page.Html;
        }

        public List<StateData> GetAllStateData()
        {
            var ncdcWebsiteNodeResult = GetNcdcHtmlData();

            var nodes = ncdcWebsiteNodeResult.SelectNodes(NodeUrlHelper.AllStateDataNode).First();

            string[] lines = nodes.InnerText.Split(
                new[] { "\n\n\n\n\n" },
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

        public StateData GetStateData(string state)
        {
            return GetAllStateData().FirstOrDefault(x => x.Name.ToLower().Contains(state));
        }

        public string GetCovidSingleSummary(string summaryName)
        {
            var xpath = summaryName switch
            {
                "SamplesTested" => NodeUrlHelper.SampleTestedNode,
                "ConfirmedCases" => NodeUrlHelper.ConfirmedCasesNode,
                "ActiveCases" => NodeUrlHelper.ActiveCasesNode,
                "DischargedCases" => NodeUrlHelper.DischargedCasesNode,
                "Deaths" => NodeUrlHelper.DeathsNode,
                _ => ""
            };
            var page = GetNcdcHtmlData();

            
            var summaryResult =
                page.OwnerDocument.DocumentNode.SelectSingleNode(xpath).InnerText;

            return NodeUrlHelper.GetJustNumbers(summaryResult);
        }

        public DataSummary GetCovidSummaryModel()
        {
            return GetCovidSummaryByNode();
        }

        public DataSummary GetCovidSummaryByNode()
        {
            var page = GetNcdcHtmlData();

            
            var samplesTested = NodeUrlHelper.GetJustNumbers(page.OwnerDocument.DocumentNode.SelectSingleNode(NodeUrlHelper.SampleTestedNode).InnerText);
            var confirmedCases = NodeUrlHelper.GetJustNumbers(page.OwnerDocument.DocumentNode.SelectSingleNode(NodeUrlHelper.ConfirmedCasesNode).InnerText);
            var activeCases = NodeUrlHelper.GetJustNumbers(page.OwnerDocument.DocumentNode.SelectSingleNode(NodeUrlHelper.ActiveCasesNode).InnerText);
            var dischargedCases = NodeUrlHelper.GetJustNumbers(page.OwnerDocument.DocumentNode.SelectSingleNode(NodeUrlHelper.DischargedCasesNode).InnerText);
            var deaths = NodeUrlHelper.GetJustNumbers(page.OwnerDocument.DocumentNode.SelectSingleNode(NodeUrlHelper.DeathsNode).InnerText);


            var model = new DataSummary
            {
                ActiveCases = activeCases,
                ConfirmedCases = confirmedCases,
                DischargedCases = dischargedCases,
                Deaths = deaths,
                SamplesTested = samplesTested
            };

            return model;
        }
    }
}
