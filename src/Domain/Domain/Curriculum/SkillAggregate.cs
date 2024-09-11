namespace Domain.Curriculum
{
    using Core.Aggregates;
    using Write.Create;

    public class SkillAggregate: Aggregate
    {
        public SkillAggregate()
        {
        }

        public SkillAggregate(CreateSkill command)
        {
            if (string.IsNullOrEmpty(command.SkillReference))
            {
                throw new ArgumentException(nameof(command.SkillReference));
            }

            if (string.IsNullOrEmpty(command.SkillName))
            {
                throw new ArgumentException(nameof(command.SkillName));
            }

            if (string.IsNullOrEmpty(command.TopicReference))
            {
                throw new ArgumentException(nameof(command.TopicReference));
            }

            if (string.IsNullOrEmpty(command.StrandReference))
            {
                throw new ArgumentException(nameof(command.StrandReference));
            }

            if (string.IsNullOrEmpty(command.SubjectReference))
            {
                throw new ArgumentException(nameof(command.SubjectReference));
            }

            var @event = new SkillCreated(
                command.SkillId,
                command.SkillName,
                command.SkillReference,
                command.TopicReference,
                command.StrandReference,
                command.SubjectReference,
                command.CurriculumReference
            );

            Enqueue(@event);
            Apply(@event);
        }

        public string SkillName { get; private set; }
        public string SkillReference { get; private set; }
        public string TopicReference { get; private set; }
        public string StrandReference { get; private set; }
        public string SubjectReference { get; private set; }
        public string CurriculumReference { get; private set; }

        private void Apply(SkillCreated @event)
        {
            Id = @event.SkillId;

            SkillName = @event.SkillName;
            SkillReference = @event.SkillReference;
            TopicReference = @event.TopicReference;
            StrandReference = @event.StrandReference;
            SubjectReference = @event.SubjectReference;
            CurriculumReference = @event.CurriculumReference;
            Version++;
        }
    }
}
