namespace CSharpNet_Web_System.Services
{
    public class ResultServiceModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }

        public ResultServiceModel(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
