namespace Cli.Features.LoadSkills
{
    using CommandLine;
    using Microsoft.Extensions.Logging;
    using Services;

    [Verb("LoadSkills", HelpText = "Load Skills from a csv file and send to API")]
    public class LoadSkillsOptions
    {
        [Option('s', nameof(SkillsFile), HelpText = "")]
        public string SkillsFile { get; init; } = null!;
    }

    public class LoadSkillsVerb(ApiClient apiClient, ILogger<LoadSkillsVerb> logger)
    {
        public async Task Run(LoadSkillsOptions options)
        {
            HashSet<Guid> skillsIds = new();
            var count = 0;
            await foreach (var curriculumSkill in CsvHelpers.ReadCsv<CurriculumSkill>(options.SkillsFile))
            {
                count++;

                try
                {
                    if (skillsIds.Contains(curriculumSkill.SkillId))
                    {
                        continue;
                    }

                    await apiClient.CreateSkill(curriculumSkill);

                    skillsIds.Add(curriculumSkill.SkillId);

                    logger.LogInformation("Skill {SkillId} {Count} loaded", curriculumSkill.SkillId, count);
                }

                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while loading skill {SkillId}", curriculumSkill.SkillId);
                }
            }
        }
    }
}
