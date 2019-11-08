namespace BookShop.Services.Models.Books
{
    using Data.Models;
    using Common.Mappings;

    public class BookShortModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
