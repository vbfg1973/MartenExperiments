namespace Cli.Features.Test
{
    using CommandLine;
    using Core.Commands;
    using Domain.Centres.Write.Create;
    using Domain.Centres.Write.Update;
    using Microsoft.Extensions.Logging;

    public class TestOptions
    {
        [Option('n', nameof(Name), Required = true)]
        public string Name { get; set; } = null!;

        [Option('c', nameof(Code), Required = true)]
        public string Code { get; set; } = null!;
    }

    public class TestVerb(ICommandBus commandBus, ILogger<TestVerb> logger)
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
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "{ExceptionMessage}", ex.Message);
            }
        }
    }
}
