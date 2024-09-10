namespace Domain.Students.Read.StudentSummary
{
    using Domain.Students.Write.Create;
    using Domain.Students.Write.Update;
    using Marten.Events.Aggregation;

    public class StudentSummaryProjection: SingleStreamProjection<StudentSummaryReadModel>
    {
        public StudentSummaryProjection()
        {
            ProjectEvent<StudentCreated>((model, @event) => model.Apply(@event));
            ProjectEvent<StudentUpdated>((model, @event) => model.Apply(@event));
        }
    }
}