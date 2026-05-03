namespace Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> CompleteAsync(CancellationToken ct = default);
}