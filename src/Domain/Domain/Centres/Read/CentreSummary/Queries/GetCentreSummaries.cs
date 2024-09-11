namespace Domain.Centres.Read.CentreSummary.Queries
{
    public record GetCentreSummaries(int PageNumber, int PageSize)
    {
        public static GetCentreSummaries Create(int pageNumber = 1, int pageSize = 20)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);

            if (pageSize is <= 0 or > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            return new GetCentreSummaries(pageNumber, pageSize);
        }
    };
}