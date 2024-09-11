namespace Domain.Curriculum.Read.SkillSummary
{
    using Domain.Curriculum.Write.Create;

    public class SkillSummaryReadModel
    {
        public Guid Id { get; private set; }
        public string SkillName { get; private set; } = null!;
        public string SkillReference { get; private set; } = null!;
        public string TopicReference { get; private set; } = null!;
        public string StrandReference { get; private set; } = null!;
        public string SubjectReference { get; private set; } = null!;
        public string CurriculumReference { get; private set; } = null!;

        public void Apply(SkillCreated @event)
        {
            Id = @event.SkillId;
            SkillName = @event.SkillName;
            SkillReference = @event.SkillReference;
            TopicReference = @event.TopicReference;
            StrandReference = @event.StrandReference;
            SubjectReference = @event.SubjectReference;
            CurriculumReference = @event.CurriculumReference;
        }
    }
}
