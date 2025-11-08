using Microsoft.EntityFrameworkCore;

namespace SaldoFlex.API.Shared.Models
{

    public class PaginatedResult<T>
    {
        public IReadOnlyCollection<T> Data { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int TotalItems { get; }

        public PaginatedResult(IReadOnlyCollection<T> data, int totalItems, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            Data = data;
        }

        public PaginatedResult(IReadOnlyCollection<T> data, int totalItems, int currentPage, int pageSize, CancellationToken cancellationToken)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            Data = data;
        }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedResult<T> Create(IQueryable<T> source, int currentPage, int pageSize)
        {
            var totalItems = source.Count();
            var data = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedResult<T>(data, totalItems, currentPage, pageSize);
        }

        public static async Task<PaginatedResult<T>> CreateAsync(IQueryable<T> source, int currentPage, int pageSize, CancellationToken cancellationToken)
        {
            var totalItems = await source.CountAsync(cancellationToken);
            var data = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedResult<T>(data, totalItems, currentPage, pageSize, cancellationToken);
        }
    }
}
