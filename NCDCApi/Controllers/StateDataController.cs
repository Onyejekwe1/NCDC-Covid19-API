using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NCDCApi.Models;
using NCDCApi.WebScrappingService;

namespace NCDCApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StateDataController : ControllerBase
    {
        private readonly INcdcScrapperService _scrapperService;
        public StateDataController(INcdcScrapperService scrapperService)
        {
            _scrapperService = scrapperService;
        }


        private static readonly StateData StateData = new StateData();

        [HttpGet("GetAllStatesData")]
        public IActionResult GetAllStateData()  
        {
            return Ok(_scrapperService.GetAllStateData());
        }

        [HttpGet("GetStateData")]
        public IActionResult GetStateData(string state)
        {
            return Ok(_scrapperService.GetStateData(state));
        }

        [HttpGet("GetSingleSummary")]
        public IActionResult GetSingleSummary(string summaryName)
        {
            return Ok(_scrapperService.GetCovidSingleSummary(summaryName));
        }

        [HttpGet("GetAllSummary")]
        public IActionResult GetAllSummary()
        {
            return Ok(_scrapperService.GetCovidSummaryModel());
        }
    }
}