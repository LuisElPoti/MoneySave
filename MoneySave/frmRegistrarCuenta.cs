
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
        
        private void CargarGrid()
        {
            var Lst = datos.Read().Select(x=> new {ID = x.IdCuenta, Cuenta = x.Name, Monto = x.Monto, Fecha = x.CreatedDate, Nota = x.Descripcion  }).ToList();
            
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
            LimpiarCampos();
            CargarGrid();
            
        }

        private void LimpiarCampos()
        {
            
            txtTipoCuenta.Focus();
            txtTipoCuenta.Text = String.Empty;
            txtMonto.Text = String.Empty;
            txtComent.Text = String.Empty;
            //CargarDatos();

        }

        private void frmRegistrarCuenta_Load(object sender, EventArgs e)
        {
            CargarGrid();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //dgvCuentas.Columns[0].HeaderText = "ID";
            //dgvCuentas.Columns[1].HeaderText = "Cuenta";
            //dgvCuentas.Columns[2].HeaderText = "Monto";
            //dgvCuentas.Columns[3].HeaderText = "Fecha";
            //dgvCuentas.Columns[4].HeaderText = "Nota";
        }
        private void CargarDatos()
        {
            cuenta.Name = txtTipoCuenta.Text;
            cuenta.Monto = decimal.Parse(txtMonto.Text);
            cuenta.CreatedDate = Convert.ToDateTime(txtFecha.Text);
            cuenta.Descripcion = txtComent.Text;
            cuenta.IdUsuario = null;//id del usuario al cual pertenece esa cuenta
            cuenta.IdCuenta = null;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        
    }
}
