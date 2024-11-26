using SegundoParcialPrestamos.Entidades;

namespace SegundoParcialPrestamos.Datos
{
    public class EntidadFinanciera
    {
        private List<Prestamo> _prestamos = new List<Prestamo>();
        public string Nombre { get; }
        private SerializadorJson SerializadorJson;
        private Dictionary<string, PrestamoPesos> _pesos;
        private Dictionary<string, PrestamoDolares> _dolares;


        public EntidadFinanciera(string nombre)
        {
            Nombre = nombre;
            SerializadorJson = new SerializadorJson();
            _prestamos = new List<Prestamo>();

        }

        public EntidadFinanciera()
        {
            
        }

        public (bool, string) AgregarPrestamo(Prestamo prestamo)
        {
            if (ExistePrestamo(prestamo))
                return (false, "Ya existe un prestamo con los mismos datos.");

            _prestamos.Add(prestamo);
            return (true, prestamo.ToString());
        }

        public bool ExistePrestamo(Prestamo prestamo)
        {
            return _prestamos.Any(p =>
                p.Persona.Equals(prestamo.Persona) &&
                p.fechaInicio == prestamo.fechaInicio &&
                p.Monto == prestamo.Monto &&
                p.Plazo == prestamo.Plazo &&
                p.Tipo == prestamo.Tipo);
        }

        public Prestamo? GetPrestamo(Guid id)
        {
            return _prestamos.FirstOrDefault(p => p.PrestamoNro == id);
        }

        public List<Prestamo> GetPrestamos(TipoPrestamo tipo)
        {
            if (_prestamos == null)
                throw new InvalidOperationException("La colección de préstamos no ha sido inicializada.");

            if (tipo == null)
                throw new ArgumentNullException(nameof(tipo));

            return tipo == TipoPrestamo.Todos
                ? _prestamos
                : _prestamos.Where(p => p.Tipo == tipo).ToList();
        }

        public int GetCantidad(TipoPrestamo tipo)
        {
            return GetPrestamos(tipo).Count;
        }

        public void GuardarDatos()
        {
            SerializadorJson!.GuardarDatos(_prestamos);
        }

        //private Dictionary<string, PrestamoPesos> LeerDatos()
        //{
        //    var lista = SerializadorJson!.LeerDatos();
        //    return lista.ToDictionary(l=>l.,l=>l);
        //}
    }

}
