namespace BookShop.Common.Mappings
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void ConfigureMapping(Profile profile);
    }
}
