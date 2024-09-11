namespace Domain.Curriculum.SkillSummary.Queries
{
    public record GetSkillSummaries(int PageNumber, int PageSize)
    {
        public static GetSkillSummaries Create(int pageNumber = 1, int pageSize = 20)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);

            if (pageSize is <= 0 or > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            return new GetSkillSummaries(pageNumber, pageSize);
        }
    };
}
