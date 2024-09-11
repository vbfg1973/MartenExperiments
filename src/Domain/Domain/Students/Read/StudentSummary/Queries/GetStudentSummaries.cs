namespace Domain.Students.Read.StudentSummary.Queries
{
    public record GetStudentSummaries(int PageNumber, int PageSize)
    {
        public static GetStudentSummaries Create(int pageNumber = 1, int pageSize = 20)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);

            if (pageSize is <= 0 or > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            return new GetStudentSummaries(pageNumber, pageSize);
        }
    };
}
