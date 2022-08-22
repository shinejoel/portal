using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientPortal.Application.Wrappers
{
    public class Response
    {
        public Response()
        {
            Succeeded = true;
        }
        public Response(string message = null)
        {
            Succeeded = true;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
    public class Response<T>
    {
        public Response()
        {
            Succeeded = true;
        }
        public Response(string message)
        {
            Succeeded = true;
            Message = message;
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
