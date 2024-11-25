namespace SegundoParcialPrestamos.Datos
{
    public interface IArchivo<T>
    {
        void GuardarDatos(List<T> datos);
        List<T> LeerDatos();
    }
}
