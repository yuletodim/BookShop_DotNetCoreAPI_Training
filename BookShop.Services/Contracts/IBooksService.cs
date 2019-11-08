namespace BookShop.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Books;

    public interface IBooksService
    {
        Task<BookDetailsWithAuthorModel> GetByIdAsync(int id);

        Task<IEnumerable<BookShortModel>> GetFirstTenBySearch(string search);

        Task<int> AddAsync(AddBookModel bookModel);

        Task<bool> ExistAsync(int id);

        Task DeleteAsync(int id);

        Task EditAsync(int id, EditBookModel bookModel);
    }
}
