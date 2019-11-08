namespace BookShop.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Data.Models;
    using Models.Authors;
    using Models.Books;

    public interface IAuthorsService
    {
        Task<AuthorDetailsModel> GetByIdAsync(int id);

        Task<int> AddAsync(Author author);

        Task<IEnumerable<BookDetailsModel>> GetAuthorBooksByIdAsync(int id);

        Task<bool> AuthorExistAsync(int id);

        Task<IEnumerable<AuthorDetailsModel>> GetAllAsync();
    }
}
