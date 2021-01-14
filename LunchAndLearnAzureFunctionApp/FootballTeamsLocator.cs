using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using LunchAndLearnAzureFunctionApp.Services;
using System;

namespace LunchAndLearnAzureFunctionApp
{
    public class FootballTeamsLocator
    {
        private readonly IAustralianFootballTeamsService _australianFootballTeamsService;
        private readonly ICanadianFootballTeamsService _canadianFootballTeamsService;
        private readonly INorthAmericanFootballTeamsService _northAmericanFootballTeamsService;
        private readonly IXflFootballTeamsService _xflFootballTeamsService;

        public FootballTeamsLocator(IAustralianFootballTeamsService australianFootballTeamsService,
                                    ICanadianFootballTeamsService canadianFootballTeamsService,
                                    INorthAmericanFootballTeamsService northAmericanFootballTeamsService,
                                    IXflFootballTeamsService xflFootballTeamsService)
        {
            _australianFootballTeamsService = australianFootballTeamsService;
            _canadianFootballTeamsService = canadianFootballTeamsService;
            _northAmericanFootballTeamsService = northAmericanFootballTeamsService;
            _xflFootballTeamsService = xflFootballTeamsService;
        }

        //https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=csharp
        [FunctionName("GetNorthAmericanFootballNames")]
        public IActionResult GetNorthAmericanFootballNames([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string city = req.Query["city"];

            string responseMessage = string.IsNullOrEmpty(city)
                ? $"This HTTP triggered function executed successfully. Pass a city in the query string for a personalized response. Or claim the Jets..." +
                $"{Environment.NewLine}All NFL Teams:{Environment.NewLine}{string.Join(Environment.NewLine, _northAmericanFootballTeamsService.GetTeamNames())}"
                : $"{city} is home of the: {string.Join(", ", _northAmericanFootballTeamsService.GetTeamNames(city))}.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GetAustralianFootballNames")]
        public IActionResult GetAustralianFootballNames([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string city = req.Query["city"];

            string responseMessage = string.IsNullOrEmpty(city)
                ? $"This HTTP triggered function executed successfully. Pass a city in the query string for a personalized response." +
                $"{Environment.NewLine}All Australian Teams:{Environment.NewLine}{string.Join(Environment.NewLine, _australianFootballTeamsService.GetTeamNames())}"
                : $"{city} is home of the: {string.Join(", ", _australianFootballTeamsService.GetTeamNames(city))}.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GetCanadianFootballNames")]
        public IActionResult GetCanadianFootballNames([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string city = req.Query["city"];

            string responseMessage = string.IsNullOrEmpty(city)
                ? $"This HTTP triggered function executed successfully. Pass a city in the query string for a personalized response. Or claim the RedBlacks..." +
                $"{Environment.NewLine}All Canadian Teams:{Environment.NewLine}{string.Join(Environment.NewLine, _canadianFootballTeamsService.GetTeamNames())}"
                : $"{city} is home of the: {string.Join(", ", _canadianFootballTeamsService.GetTeamNames(city))}.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GetXflFootballNames")]
        public IActionResult GetXflFootballNames([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string city = req.Query["city"];

            string responseMessage = string.IsNullOrEmpty(city)
                ? $"This HTTP triggered function executed successfully. Pass a city in the query string for a personalized response." +
                $"{Environment.NewLine}All XFL Teams:{Environment.NewLine}{string.Join(Environment.NewLine, _xflFootballTeamsService.GetTeamNames())}"
                : $"{city} is home of the: {string.Join(", ", _xflFootballTeamsService.GetTeamNames(city))}.";

            return new OkObjectResult(responseMessage);
        }
    }
}
