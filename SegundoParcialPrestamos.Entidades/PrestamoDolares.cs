using SegundoParcialPrestamos.Entidades;

public class PrestamoDolares : Prestamo
{
    public static readonly Dictionary<Plazo, decimal> TasasPorPlazo = new()
    {
        { Plazo.Doce, 5m },
        { Plazo.Veinticuatro, 6m },
        { Plazo.TreintaYSeis, 7m },
        { Plazo.CuarentaYOcho, 8m }
    };

    public PrestamoDolares(Persona persona, Plazo plazo, decimal monto) : base(persona, plazo, monto)
    {
        Tipo = TipoPrestamo.Dolares;
        ConfigurarTasaIntereses();
    }

    public static decimal ObtenerTasa(Plazo plazo)
    {
        if (TasasPorPlazo.TryGetValue(plazo, out var tasa))
        {
            return tasa;
        }
        else
        {
            throw new ArgumentException($"El plazo {plazo} no tiene una tasa definida.");
        }
    }

    public override void ConfigurarTasaIntereses()
    {
        TasaInteresAnual = TasasPorPlazo[Plazo];
    }
}