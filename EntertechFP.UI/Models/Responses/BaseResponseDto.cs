namespace EntertechFP.UI.Models.Responses
{
    public class BaseResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
