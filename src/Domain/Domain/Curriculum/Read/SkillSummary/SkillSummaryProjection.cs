namespace Domain.Curriculum.Read.SkillSummary
{
    using Domain.Curriculum.Write.Create;
    using Marten.Events.Aggregation;

    public class SkillSummaryProjection: SingleStreamProjection<SkillSummaryReadModel>
    {
        public SkillSummaryProjection()
        {
            ProjectEvent<SkillCreated>((model, @event) => model.Apply(@event));
        }
    }
}