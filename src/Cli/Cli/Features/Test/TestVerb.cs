﻿namespace Cli.Features.Test
{
    using System.Text.Json;
    using CommandLine;
    using Core.Commands;
    using Core.Queries;
    using Domain.Centres.Read.CentreSummary;
    using Domain.Centres.Read.CentreSummary.Queries;
    using Domain.Centres.Write.Create;
    using Domain.Centres.Write.Update;
    using Microsoft.Extensions.Logging;

    public class TestOptions
    {
        [Option('n', nameof(Name), Required = true)]
        public string Name { get; init; } = null!;

        [Option('c', nameof(Code), Required = true)]
        public string Code { get; init; } = null!;
    }

    public class TestVerb(ICommandBus commandBus, IQueryBus queryBus, ILogger<TestVerb> logger)
    {
        public async Task Run(TestOptions options)
        {
            try
            {
                logger.LogInformation("Running TestVerb");

                var createCentre = new CreateCentre(options.Name, options.Code);

                await commandBus.Send(createCentre);

                var updateCentre = new UpdateCentre(createCentre.CentreId, options.Name + " Modified",
                    options.Code + " Modified");

                await commandBus.Send(updateCentre);

                var query = new GetCentreSummaryById(createCentre.CentreId);
                var result = await queryBus.Query<GetCentreSummaryById, CentreSummaryReadModel>(query);

                Console.WriteLine(JsonSerializer.Serialize(result));
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "{ExceptionMessage}", ex.Message);
            }
        }
    }
}
