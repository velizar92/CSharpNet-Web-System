namespace CSharpNet_Web_System.Services
{
    public class ErrorServiceModel
    {
        public bool Result { get; set; }
        public string ErrorMessage { get; set; }

        public ErrorServiceModel(bool result, string errorMessage)
        {
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
