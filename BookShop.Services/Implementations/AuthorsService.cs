namespace BookShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using Contracts;
    using Models.Authors;
    using Models.Books;
    using AutoMapper.QueryableExtensions;
    using BookShop.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class AuthorsService : IAuthorsService
    {
        private readonly BookShopDbContext dbContext;

        public AuthorsService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AuthorDetailsModel> GetByIdAsync(int id)
            => await this.dbContext
                .Authors
                .Where(a => a.Id == id)
                .ProjectTo<AuthorDetailsModel>()
                .FirstOrDefaultAsync();


        public async Task<int> AddAsync(Author author)
        {
            await this.dbContext.Authors.AddAsync(author);
            await this.dbContext.SaveChangesAsync();

            return author.Id;
        }

        public async Task<IEnumerable<BookDetailsModel>> GetAuthorBooksByIdAsync(int id)
            => await this.dbContext
                .Books
                .Where(b => b.AuthorId == id)
                .ProjectTo<BookDetailsModel>()
                .ToListAsync();

        public async Task<bool> AuthorExistAsync(int id)
            => await this.dbContext
                .Authors
                .AnyAsync(a => a.Id == id);

        public async Task<IEnumerable<AuthorDetailsModel>> GetAllAsync()
            => await this.dbContext
                .Authors
                .ProjectTo<AuthorDetailsModel>()
                .ToListAsync();
    }
}
