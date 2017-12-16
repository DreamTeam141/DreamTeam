using AutoMapper;
using Faculty.BLL.Mapper;

namespace Faculty.App_Start
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
                cfg.AddProfile<WebMapperProfile>();
            });
            return config;
        }
    }
}   