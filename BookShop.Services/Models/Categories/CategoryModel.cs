namespace BookShop.Services.Models.Categories
{
    using System.Collections.Generic;
    using AutoMapper;

    using Data.Models;
    using Common.Mappings;
    using System.Linq;

    public class CategoryModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> BooksTitles { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryModel>()
                .ForMember(
                    c => c.BooksTitles,
                    cfg => cfg.MapFrom(cb => cb.Books.Select(b => b.Book.Title)));
        }
    }
}
