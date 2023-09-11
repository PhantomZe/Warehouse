using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Service.API.Data;
using Warehouse.Service.API.Models;
using Warehouse.Service.API.Models.Dto;

namespace Warehouse.Service.API.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext db;
        private ResponseDto responseDto;
        private IMapper mapper;

        public ItemController(AppDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;   
            this.responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto GetAllItem()
        {
            try
            {
                IEnumerable<Item> items = db.items.ToList();
                responseDto.Result = mapper.Map<IEnumerable<ItemDto>>(items);
                responseDto.IsSuccess = true;

                return responseDto;
            }catch(Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Item item = db.items.First(x => x.ItemID == id);
                responseDto.Result = mapper.Map<ItemDto>(item); 
                responseDto.IsSuccess = true;
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto GetByName(string name)
        {
            try
            {
                IEnumerable<Item> items = db.items.Where(x => x.ItemName.Contains(name)).ToList();
                responseDto.Result = mapper.Map<IEnumerable<ItemDto>>(items);
                responseDto.IsSuccess = true;
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        [HttpPost]
        [Route("InsertUpdateItem")]
        public ResponseDto InsertUpdateItem([FromBody] ItemDto itemDto)
        {
            try
            {
                itemDto.LastUpdated = DateTime.Now;
                Item item = mapper.Map<Item>(itemDto);
                if(item.ItemID != 0)
                {
                    db.Update(item);
                    db.SaveChanges();
                }
                else
                {
                    int existsItem = db.items.Where(x => x.ItemName.Equals(item.ItemName)).Count();
                    if(existsItem<=0)
                    {
                        db.Add(item);
                        db.SaveChanges();
                    }
                }
                responseDto.Result = mapper.Map<ItemDto>(item);
                responseDto.IsSuccess = true;
                if (item.ItemID == 0)
                {
                    responseDto.IsSuccess = false;
                    responseDto.ErrorMessage = "There is an existing item with the same name.";
                }
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }
        [HttpDelete]
        [Route("DeleteItem/{id:int}")]
        public ResponseDto DeleteItem(int id)
        {
            try
            {
                Item item = db.items.First(x => x.ItemID == id);
                if (item.ItemID != 0)
                {
                    db.Remove(item);
                    db.SaveChanges();
                }
                responseDto.Result = mapper.Map<ItemDto>(item);
                responseDto.IsSuccess = true;
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }
    }
}
