namespace Api.Controllers.Curriculum.Requests
{
    public record CreateSkillRequest(
        Guid SkillId,
        string SkillName,
        string SkillReference,
        string TopicReference,
        string StrandReference,
        string SubjectReference,
        string CurriculumReference);
}
