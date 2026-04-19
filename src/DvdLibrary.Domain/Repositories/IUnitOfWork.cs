namespace DvdLibrary.Domain.Repositories;

/// <summary>
/// Samlar SaveChanges i ett kontrakt så att Application inte behöver känna till EF Core.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
