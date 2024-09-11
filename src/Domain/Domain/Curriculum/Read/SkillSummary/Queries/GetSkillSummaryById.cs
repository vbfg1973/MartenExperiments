namespace Domain.Curriculum.Read.SkillSummary.Queries
{
    public record GetSkillSummaryById(Guid SkillId);
    public record GetSkillSummaryBySkillReference(string SkillReference);

}
