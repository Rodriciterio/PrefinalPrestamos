using SegundoParcialPrestamos.Entidades;
using SegundoParcialPrestamos.Windows.Helpers;

namespace SegundoParcialPrestamos.Windows
{
    public partial class frmPrestamoAE : Form
    {
        public frmPrestamoAE()
        {
            InitializeComponent();
        }
        private Dictionary<Plazo, decimal> plazosTasas;
        private Prestamo prestamo;


        public Prestamo GetPrestamo()
        {
            return prestamo;
        }

        private void rbtDolares_CheckedChanged(object sender, EventArgs e)
        {
            plazosTasas = PrestamoDolares.TasasPorPlazo;
            cboPlazos.SelectedIndex = 0;
            MostrarTasaInteres();
        }

        private void MostrarTasaInteres()
        {
            if (cboPlazos.SelectedItem is Plazo plazoSeleccionado && plazosTasas != null)
            {
                txtTasa.Text = $"Tasa de interés: {plazosTasas[plazoSeleccionado]}%";
            }
        }

        private void rbtPesos_CheckedChanged(object sender, EventArgs e)
        {
            plazosTasas = PrestamoPesos.TasasPorPlazo;
            cboPlazos.SelectedIndex = 0;
            MostrarTasaInteres();
        }

        private void frmPrestamoAE_Load(object sender, EventArgs e)
        {
            cboPlazos.DataSource = Enum.GetValues(typeof(Plazo));
        }

        private void cboPlazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtPesos.Checked)
            {
                txtTasa.Text = $"Tasa Anual: {PrestamoPesos.ObtenerTasa((Plazo)cboPlazos.SelectedItem)}%";
            }
            else if (rbtDolares.Checked)
            {
                txtTasa.Text = $"Tasa Anual: {PrestamoDolares.ObtenerTasa((Plazo)cboPlazos.SelectedItem)}%";
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (!Persona.EsDNIValido(txtDni.Text))
            {
                MessageBox.Show("El DNI ingresado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("DNI válido. Complete los datos restantes.", "Validación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HabilitarCampos();
            }
        }

        private void HabilitarCampos()
        {
            txtNombres.Enabled = true;
            txtApellido.Enabled = true;
            cboPlazos.Enabled = true;
            txtMonto.Enabled = true;
            rbtPesos.Enabled = true;
            rbtDolares.Enabled = true;
            btnOK.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Persona persona = new Persona
                {
                    Nombre = txtNombres.Text,
                    Apellido = txtApellido.Text,
                    DNI = txtDni.Text
                };

                Prestamo prestamo;

                if (rbtPesos.Checked)
                {
                    prestamo = new PrestamoPesos(persona, (Plazo)cboPlazos.SelectedItem, decimal.Parse(txtMonto.Text));
                }
                else
                {
                    prestamo = new PrestamoDolares(persona, (Plazo)cboPlazos.SelectedItem, decimal.Parse(txtMonto.Text));
                }

                this.prestamo = prestamo;
                DialogResult = DialogResult.OK;
            }
        }


        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtDni.Text))
            {
                MessageBox.Show("Complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("El monto ingresado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}