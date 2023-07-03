using Core.Utilities.Results;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ErrorDetails 
    {
        [JsonProperty(PropertyName ="statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        public ErrorDetails(string message)
        {
            this.Success = false;
            this.Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
       
        }

    }
}
