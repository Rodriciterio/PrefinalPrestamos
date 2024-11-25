public class Cuota
{
    public int Numero { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal Capital { get; set; } 
    public decimal Interes { get; set; } 

    public Cuota(int numero, DateTime fechaVencimiento, decimal montoTotal, decimal capital, decimal interes)
    {
        Numero = numero;
        FechaVencimiento = fechaVencimiento;
        MontoTotal = montoTotal;
        Capital = capital;
        Interes = interes;
    }
}
