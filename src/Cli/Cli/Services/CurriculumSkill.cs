namespace Cli.Services
{
    public record CurriculumSkill(
        Guid SkillId,
        string SkillName,
        string SkillReference,
        string TopicReference,
        string StrandReference,
        string SubjectReference,
        string CurriculumReference);
}
