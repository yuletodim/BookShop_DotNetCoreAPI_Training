namespace BookShop.Services.Models.Books
{
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Common.Mappings;

    public class BookDetailsWithAuthorModel : BookDetailsModel, IMapFrom<Book>, IHaveCustomMappings
    {
        public string Author { get; set; }

        public override void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Book, BookDetailsWithAuthorModel>()
                .ForMember(b => b.Author, cfg => cfg.MapFrom(b => $"{b.Author.FirstName} {b.Author.LastName}"))
                .ForMember(b => b.Categories, cfg => cfg.MapFrom(b => b.Categories.Select(c => c.Category.Name)));
        }
    }
}
