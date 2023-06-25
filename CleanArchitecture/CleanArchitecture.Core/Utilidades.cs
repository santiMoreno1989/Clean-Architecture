using System.Text.Json;

namespace CleanArchitecture.Domain
{
    public class Utilidades
    {
        public static Dictionary<string, List<string>> ExtraerErroresWebApi(string json) 
        {
            var respuesta = new Dictionary<string, List<string>>();

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            var errorsJsonElements = jsonElement.GetProperty("errors");

            foreach (var errorElement in errorsJsonElements.EnumerateObject()) 
            {
                var campo = errorElement.Name;
                var errores = new List<string>();
                foreach (var errorKid in errorElement.Value.EnumerateArray())
                {
                    var error = errorKid.GetString();
                    errores.Add(error);
                }

                respuesta.Add(campo, errores);
            }

            return respuesta;
        }
    }
}
