namespace CleanArchitectureExample.Api.Contracts;

public sealed record PagedQueryRequest
{
    public PagedQueryRequest(int? take, int? skip)
    {
        Take = take ?? 100;
        Skip = skip ?? 0;
    }

    public int Take { get; set; }
    public int Skip { get; set; }
}
