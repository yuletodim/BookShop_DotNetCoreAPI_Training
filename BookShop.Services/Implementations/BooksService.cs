namespace BookShop.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Data.Models;
    using Contracts;
    using Models.Books;
    using Common.Extensions;

    public class BooksService : IBooksService
    {
        private readonly BookShopDbContext dbContext;
        private readonly IMapper mapper;

        public BooksService(BookShopDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<BookDetailsWithAuthorModel> GetByIdAsync(int id)
            => await this.dbContext
                .Books
                .Where(b => b.Id == id)
                .ProjectTo<BookDetailsWithAuthorModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<BookShortModel>> GetFirstTenBySearch(string search)
            => await this.dbContext
                .Books
                .Where(b => b.Title.ToLower().Contains(search.ToLower()))
                .OrderBy(b => b.Title)
                .Take(10)
                .ProjectTo<BookShortModel>()
                .ToListAsync();

        public async Task<int> AddAsync(AddBookModel bookModel)
        {
            var categoriesNames = bookModel.CategoriesString
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var existingCategories = await this.dbContext
                .Categories
                .Where(c => categoriesNames.Contains(c.Name))
                .ToListAsync();

            foreach (var name in categoriesNames)
            {
                if (existingCategories.All(c => c.Name != name))
                {
                    var category = new Category { Name = name };
                    dbContext.Categories.Add(category);
                    existingCategories.Add(category);
                }
            }

            await this.dbContext.SaveChangesAsync();

            var book = this.mapper.Map<Book>(bookModel);
            existingCategories.ForEach(c => book.Categories.Add(new CategoryBook { CategoryId = c.Id }));

            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();

            return book.Id;
        }

        public async Task<bool> ExistAsync(int id)
            => await this.dbContext
                .Books
                .AnyAsync(b => b.Id == id);

        public async Task DeleteAsync(int id)
        {
            var book = this.dbContext.Books.Find(id);
            this.dbContext.Books.Remove(book);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(int id, EditBookModel bookModel)
        {
            var book = this.dbContext.Books.Find(id);

            book.Title = bookModel.Title;
            book.Description = bookModel.Description;
            book.Price = bookModel.Price;
            book.Copies = bookModel.Copies;
            book.Edition = bookModel.Edition;
            book.AgeRestriction = bookModel.AgeRestriction;
            book.ReleaseDate = bookModel.ReleaseDate;
            book.AuthorId = bookModel.AuthorId;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
