﻿namespace BookShop.Services.Models.Books
{
    using System;
    using Data.Models;
    using Common.Mappings;

    public class AddBookModel : IMapTo<Book>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public string CategoriesString { get; set; }
    }
}
