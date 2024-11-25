namespace SegundoParcialPrestamos.Entidades
{
    public class PrestamoPesos : Prestamo
    {
        public static readonly Dictionary<Plazo, decimal> TasasPorPlazo = new()
    {
        { Plazo.Doce, 25m },
        { Plazo.Veinticuatro, 30m },
        { Plazo.TreintaYSeis, 35m },
        { Plazo.CuarentaYOcho, 40m }
    };

        public PrestamoPesos(Persona persona, Plazo plazo, decimal monto) : base(persona, plazo, monto)
        {
            Tipo = TipoPrestamo.Pesos;
            ConfigurarTasaIntereses();
        }

        public static decimal ObtenerTasa(Plazo plazo)
        {
            if (TasasPorPlazo.TryGetValue(plazo, out var tasa))
            {
                return tasa;
            }
            throw new ArgumentException($"El plazo {plazo} no tiene una tasa definida.");
        }

        public override void ConfigurarTasaIntereses()
        {
            TasaInteresAnual = TasasPorPlazo[Plazo];
        }
    }

}