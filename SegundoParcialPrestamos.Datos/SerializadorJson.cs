using SegundoParcialPrestamos.Entidades;
using System.Text.Json;

namespace SegundoParcialPrestamos.Datos
{
    public class SerializadorJson : IArchivo<Prestamo>
    {
        private const string NombreArchivo = "Prestamos.json";
        private readonly string RutaCompletaArchivo;

        public SerializadorJson()
        {
            var rutaPrograma = AppDomain.CurrentDomain.BaseDirectory;
            RutaCompletaArchivo = Path.Combine(rutaPrograma, NombreArchivo);
        }

        public void GuardarDatos(List<Prestamo> datos)
        {
            var json = JsonSerializer.Serialize(datos);
            File.WriteAllText(RutaCompletaArchivo, json);
        }

        public List<Prestamo> LeerDatos()
        {
            if (!File.Exists(RutaCompletaArchivo))
                return new List<Prestamo>();

            var json = File.ReadAllText(RutaCompletaArchivo);
            return JsonSerializer.Deserialize<List<Prestamo>>(json) ?? new List<Prestamo>();
        }
    }

}
