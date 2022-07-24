using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlData.DTO
{
    public class BaseResponse<T>
    {
        public BaseResponse(bool success, T data, string message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
