using SegundoParcialPrestamos.Entidades;

public abstract class Prestamo
{
    public Persona Persona { get; set; }
    public Plazo Plazo { get; set; }
    public decimal Monto { get; set; }
    public decimal TasaInteresAnual { get; protected set; }
    public TipoPrestamo Tipo { get; protected set; }
    public DateTime FechaInicio { get; protected set; }
    public Guid PrestamoNro { get; set; }

    protected Prestamo(Persona persona, Plazo plazo, decimal monto, DateTime fechaInicio)
    {
        Persona = persona;
        Plazo = plazo;
        Monto = monto;
        FechaInicio = fechaInicio;
    }

    public abstract void ConfigurarTasaIntereses();

    private decimal CalcularAmortizacion(decimal montoTotal)
    {
        decimal tasaMensual = (TasaInteresAnual / 100) / 12;
        return montoTotal * tasaMensual;
    }

    private decimal CalcularInteres(decimal saldoRestante)
    {
        decimal tasaMensual = (TasaInteresAnual / 100) / 12;
        return saldoRestante * tasaMensual;
    }

    public List<Cuota> ObtenerDetalleCuotas()
    {
        List<Cuota> cuotas = new List<Cuota>();
        decimal saldoRestante = Monto;
        DateTime fechaVencimiento = FechaInicio.AddMonths(1);

        for (int i = 1; i <= (int)Plazo; i++)
        {
            decimal amortizacion = CalcularAmortizacion(Monto);
            decimal interes = CalcularInteres(saldoRestante);
            decimal cuotaTotal = amortizacion + interes;

            decimal capital = amortizacion;
            decimal cuotaInteres = cuotaTotal - capital;

            cuotas.Add(new Cuota(i, fechaVencimiento, Math.Round(cuotaTotal, 2), Math.Round(capital, 2), Math.Round(cuotaInteres, 2)));

            saldoRestante -= amortizacion;

            fechaVencimiento = fechaVencimiento.AddMonths(1);
        }

        return cuotas;
    }
}
