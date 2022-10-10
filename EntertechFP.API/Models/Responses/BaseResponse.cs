using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.API.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public T Data { get; init; }

        public BaseResponse(bool isSuccess)
        {
            Data = default;
            Success = isSuccess;
            Message = isSuccess ? "Success" : "Fault";
        }
        public BaseResponse(T data)
        {
            Success = true;
            Message = "Success";
            Data = data;
        }
        public BaseResponse(string message)
        {
            Success = false;
            Data = default;
            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
        }
        public BaseResponse()
        {
        }

    }
}
