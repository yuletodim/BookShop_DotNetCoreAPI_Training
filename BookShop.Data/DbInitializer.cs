namespace BookShop.Data
{
    using System.Threading.Tasks;
    using System.Linq;
    using Models;
    using System;
    using System.Collections.Generic;

    public class DbInitializer : IDbInitializer
    {
        private readonly BookShopDbContext dbContext;

        public DbInitializer(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Initialize()
        {
            this.dbContext.Database.EnsureCreated();

            if (!this.dbContext.Authors.Any())
            {
                await this.SeedAuthorsAsync();
            }

            if (!this.dbContext.Categories.Any())
            {
                await this.SeedCategoriesAsync();
            }

            if (!this.dbContext.Books.Any())
            {
                await this.SeedBooksAsync();
            }
        }

        private async Task SeedCategoriesAsync()
        {
            await this.dbContext.AddAsync(new Category { Name = "Horror" });
            await this.dbContext.AddAsync(new Category { Name = "Fiction" });
            await this.dbContext.AddAsync(new Category { Name = "Romantic" });
            await this.dbContext.AddAsync(new Category { Name = "Textbooks" });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task SeedAuthorsAsync()
        {
            await this.dbContext.AddAsync(new Author { FirstName = "Ivan", LastName = "Ivanov" });
            await this.dbContext.AddAsync(new Author { FirstName = "Pesho", LastName = "Dimitrov" });
            await this.dbContext.AddAsync(new Author { FirstName = "Tosho", LastName = "Petrov" });
            await this.dbContext.AddAsync(new Author { FirstName = "Gosho", LastName = "Todorov" });
            await this.dbContext.AddAsync(new Author { FirstName = "Sasho", LastName = "Ivanov" });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task SeedBooksAsync()
        {
            var authors = this.dbContext.Authors.ToArray();
            var categories = this.dbContext.Categories.Take(3).ToList();
            var books = new List<Book>();

            books.Add(new Book
            {
                Title = "C#",
                Description = "Alabala C#",
                Price = 10,
                Copies = 200,
                ReleaseDate = DateTime.UtcNow,
                AuthorId = authors[0].Id,
                Edition = 1
            });

            books.Add(new Book
            {
                Title = "Java",
                Description = "Alabala Java",
                Price = 10,
                Copies = 200,
                ReleaseDate = DateTime.UtcNow,
                AuthorId = authors[1].Id,
                Edition = 3
            });

            books.Add(new Book
            {
                Title = "JavaScript",
                Description = "Alabala JavaScript",
                Price = 10,
                Copies = 200,
                ReleaseDate = DateTime.UtcNow,
                AuthorId = authors[2].Id,
                Edition = 10
            });

            books.Add(new Book
            {
                Title = "PHP",
                Description = "Alabala PHP",
                Price = 10,
                Copies = 200,
                ReleaseDate = DateTime.UtcNow,
                AuthorId = authors[0].Id,
                Edition = 1
            });

            books.Add(new Book
            {
                Title = "Angular",
                Description = "Alabala Angular",
                Price = 10,
                Copies = 200,
                ReleaseDate = DateTime.UtcNow,
                AuthorId = authors[1].Id,
                Edition = 5
            });

            foreach (var book in books)
            {
                categories.ForEach(c => book.Categories.Add(new CategoryBook { CategoryId = c.Id }));
                await this.dbContext.AddAsync(book);
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
