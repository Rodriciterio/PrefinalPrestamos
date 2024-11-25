using SegundoParcialPrestamos.Datos;
using SegundoParcialPrestamos.Entidades;
using SegundoParcialPrestamos.Windows.Helpers;

namespace SegundoParcialPrestamos.Windows
{
    public partial class frmPrestamos : Form
    {
        private EntidadFinanciera? entidad;

        public frmPrestamos()
        {
            InitializeComponent();
            entidad = new EntidadFinanciera();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            //entidad.GuardarDatos();
            Application.Exit();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPrestamoAE frm = new frmPrestamoAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            Prestamo? prestamo = frm.GetPrestamo();
            if (prestamo == null) return;
            TipoPrestamo tipoPrestamo = prestamo.Tipo; 
            var prestamos = entidad.GetPrestamos(tipoPrestamo);

            MessageBox.Show($"Prestamo otorgado", "Prestamo",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            GridHelper.MostrarDatosEnGrilla(prestamos, dgvDatos);

        }

        private void ActualizarCantidadPrestamos()
        {
            EntidadFinanciera entidadFinanciera = new EntidadFinanciera();
            txtCantidad.Text = $"Cantidad de Préstamos: {entidadFinanciera.GetCantidad(TipoPrestamo.Todos)}";
        }


        private void ActualizarGrilla()
        {
            var entidad = new EntidadFinanciera();
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = entidad.GetPrestamos(TipoPrestamo.Todos);
        }


    }
}
