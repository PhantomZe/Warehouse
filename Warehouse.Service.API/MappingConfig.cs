using AutoMapper;
using Warehouse.Service.API.Models;
using Warehouse.Service.API.Models.Dto;

namespace Warehouse.Service.API
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() 
        { 
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Item, ItemDto>();
                config.CreateMap<ItemDto, Item>();
                config.CreateMap<User, UserDto>();
                config.CreateMap<UserDto, User>();
                config.CreateMap<TransactionItem, TransactionItemDto>();
                config.CreateMap<TransactionItemDto, TransactionItem>();
            });
            return mappingConfig;
        }
    }
}
