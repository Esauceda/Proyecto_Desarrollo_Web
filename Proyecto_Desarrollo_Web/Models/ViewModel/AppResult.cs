using System;

namespace Proyecto_Desarrollo_Web.Models.ViewModel
{
    public class AppResult
    {
        public Boolean IsValid { get; set; }
        public string Mensaje { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

        public static AppResult NoSucces(string mensaje)
        {
            return new AppResult { IsValid = false, Mensaje = mensaje };
        }

        public static AppResult Succes(string mensaje, object data)
        {
            return new AppResult { IsValid = true, Mensaje = mensaje, Data = data };
        }

    }
}
