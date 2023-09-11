﻿using System.ComponentModel.DataAnnotations;

namespace Warehouse.Service.API.Models.Dto
{
    public class UserDto
    {

        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }
}
