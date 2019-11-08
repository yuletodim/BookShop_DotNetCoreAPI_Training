namespace BookShop.Api.Models.Books
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    using Services.Models.Books;
    using Common.Mappings;

    public class AddBookBindingModel : IMapTo<AddBookModel>
    {
        [Required]
        [MinLength(BookTileMinLength)]
        [MaxLength(BookTileMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public string CategoriesString { get; set; }
    }
}
