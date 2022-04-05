namespace Template.Models
{

    public class ApiResult<T>
    {
        public ApiResult()
        {
            State = false;
            Message = "";
            StackTrace = "";
        }
        public bool State { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public T Result { get; set; }
    }
}
