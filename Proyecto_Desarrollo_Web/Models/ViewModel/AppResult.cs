namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class AppResult
    {
        public bool IsValid { get; set; }
        public string Mensaje { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static AppResult NoSucces(string message)
        {
            return new AppResult { IsValid = false, Message = message };
        }

        public static AppResult Succes(string message, object data)
        {
            return new AppResult { IsValid = true, Message = message, Data = data };
        }


    }
}
