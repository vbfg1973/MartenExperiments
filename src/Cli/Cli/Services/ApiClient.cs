namespace Cli.Services
{
    using System.Net;
    using System.Net.Http.Json;
    using Microsoft.Extensions.Logging;

    public class ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
    {
        public async Task CreateSkill(CurriculumSkill curriculumSkill)
        {
            var response = await httpClient.PostAsJsonAsync($"api/Curriculum/Skills/", curriculumSkill);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Failed to create skill: {StatusCode}", response.StatusCode.ToString());
            }
        }

        public async Task<bool> SkillExists(CurriculumSkill curriculumSkill)
        {
            var response = await httpClient.GetAsync($"api/Curriculum/Skills/{curriculumSkill.SkillId}");

            return response.StatusCode switch
            {
                HttpStatusCode.OK => true,
                HttpStatusCode.NotFound => false,
                _ => throw new HttpRequestException($"Problem testing for existence of {curriculumSkill.SkillId}")
            };
        }
    }
}
