
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneySave
{
    public partial class frmRegistrarCuenta : Form
    {
        CDatos datos = new CDatos();
        Cuenta cuenta = new Cuenta();
        private int Id;
        private void CargarGrid()
        {
            var Lst = datos.Read();
            dgvCuentas.DataSource = Lst;
        }
        public frmRegistrarCuenta()
        {
            InitializeComponent();
        }

        private void btnAñadirCuenta_Click(object sender, EventArgs e)
        {
            CargarDatos();
            datos.Create(cuenta);
            
            
        }

        private void LimpiarCampos()
        {
            Id = 0;
            txtTipoCuenta.Focus();
            txtTipoCuenta.Text = String.Empty;
            txtMonto.Text = String.Empty;
            txtComent.Text = String.Empty;
            CargarDatos();

        }

        private void frmRegistrarCuenta_Load(object sender, EventArgs e)
        {
            CargarGrid();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void CargarDatos()
        {
            
            cuenta.IdCuenta = Id;
            cuenta.Name = txtTipoCuenta.Text;
            cuenta.Monto = Convert.ToDecimal(txtMonto.Text);
            cuenta.CreatedDate = Convert.ToDateTime(txtFecha.Text);
            cuenta.Descripcion = txtComent.Text;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
