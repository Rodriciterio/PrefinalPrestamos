namespace SegundoParcialPrestamos.Entidades
{
    
    public class Persona
    {
        private string? _nombre;
        private string? _apellido;
        private string? _dni;

        public string? Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede ser nulo o vacío.");
                _nombre = value;
            }
        }

        public string? Apellido
        {
            get => _apellido;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El apellido no puede ser nulo o vacío.");
                _apellido = value;
            }
        }

        public string? DNI
        {
            get => _dni;
            set
            {
                if (!EsDNIValido(value))
                    throw new ArgumentException("El DNI no es válido.");
                _dni = value;
            }
        }

        public static bool EsDNIValido(string? dni)
        {
            return !string.IsNullOrEmpty(dni) && dni.Length == 8 && dni.All(char.IsDigit);
        }

        public override bool Equals(object? obj)
        {
            return obj is Persona persona && DNI == persona.DNI;
        }

        public override int GetHashCode()
        {
            return DNI?.GetHashCode() ?? base.GetHashCode();
        }
    }

}
