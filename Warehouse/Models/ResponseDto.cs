﻿namespace Warehouse.Models
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = "";
    }
}
