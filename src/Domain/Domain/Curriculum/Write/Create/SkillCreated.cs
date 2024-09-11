namespace Domain.Curriculum.Write.Create
{
    using Core.Events;

    public class SkillCreated(
        Guid skillId,
        string skillName,
        string skillReference,
        string topicReference,
        string strandReference,
        string subjectReference,
        string curriculumReference)
        : DomainEventBase
    {
        public Guid SkillId { get; } = skillId;
        public string SkillName { get; init; } = skillName;
        public string SkillReference { get; init; } = skillReference;
        public string TopicReference { get; init; } = topicReference;
        public string StrandReference { get; init; } = strandReference;
        public string SubjectReference { get; init; } = subjectReference;
        public string CurriculumReference { get; init; } = curriculumReference;
    }
}
