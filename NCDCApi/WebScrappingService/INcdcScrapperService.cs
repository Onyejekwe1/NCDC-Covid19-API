using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NCDCWebScrapper;

namespace NCDCApi.WebScrappingService
{
    public interface INcdcScrapperService
    {
        HtmlNode GetNcdcHtmlData();
        List<StateData> GetAllStateData();
        StateData GetStateData(string state);
        string GetCovidSingleSummary(string summaryName);
        DataSummary GetCovidSummaryModel();
        DataSummary GetCovidSummaryByNode();

    }
}
