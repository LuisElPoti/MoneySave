using MoneySave.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneySave
{
    public partial class frmIngreso : Form
    {
        public bool Adding { get; set; } = true;
        private int Id;
        public frmIngreso()
        {
            InitializeComponent();
        }
        private void frmIngreso_Load(object sender, EventArgs e)
        {
            GetRecords();
            this.dgvIngreso.Columns["IdUsuario"].Visible = false;
            dgvIngreso.Columns[0].HeaderText = "ID";
            dgvIngreso.Columns[2].HeaderText = "Categoria";
        }
        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\ingresos.json";
            var ingresos = new List<Ingreso>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                ingresos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ingreso>>(json);
            }

            dgvIngreso.DataSource = ingresos;
        }
        private void SaveRecord()
        {
            var json = string.Empty;
            var ingresos = new List<Ingreso>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\ingresos.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                ingresos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ingreso>>(json);
            }
            var ingreso = new Ingreso();
            if (Adding == true)
            {
                ingreso = new Ingreso
                {
                    IdIngreso = (ingresos.Count + 1),
                    Cuenta = cmbCuenta.Text,
                    TipoIngreso = cmbTipoIngreso.Text,
                    Monto = decimal.Parse(txtMonto.Text),
                    Descripcion = txtComent.Text,
                    Fecha = dtpFecha.Value

                    //id del usuario al cual pertenece esa ingreso

                };

            }
            else
            {

                ingreso = ingresos.FirstOrDefault(x => x.IdIngreso == Id);
                if (ingreso != null)
                {
                    ingresos.Remove(ingreso);
                    ingreso.Descripcion = txtComent.Text;
                }
            }
            ingresos.Add(ingreso);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(ingresos);

            var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
            sw.WriteLine(json);
            sw.Close();

            MessageBox.Show("Registro exitoso", "Patient Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);

            GetRecords();
            ClearFields();

        }

        private void ClearFields()
        {
            Id = 0;
            cmbCuenta.Text = String.Empty;
            cmbTipoIngreso.Text = String.Empty;
            txtMonto.Text = String.Empty;
            txtComent.Text = String.Empty;
        }
        private void dgvIngreso_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Id = int.Parse(dgvIngreso.CurrentRow.Cells[0].Value.ToString());
                DataGridViewRow dgv = dgvIngreso.Rows[e.RowIndex];
                cmbCuenta.Text = dgv.Cells[1].Value.ToString();
                cmbTipoIngreso.Text = dgv.Cells[2].Value.ToString();
                txtMonto.Text = dgv.Cells[3].Value.ToString();
                txtComent.Text = dgv.Cells[4].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(dgv.Cells[5].Value.ToString());
            }
        }

        private void btnAñadirIngreso_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvIngreso.SelectedRows.Count == 1) //Si hay una fila seleccionada
            {


                if (MessageBox.Show("¿Realmente desea eliminar la ingreso?", "AVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var json = string.Empty;
                    var ingresos = new List<Ingreso>();
                    var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\ingresos.json";

                    if (File.Exists(pathFile))
                    {
                        json = File.ReadAllText(pathFile, Encoding.UTF8);
                        ingresos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ingreso>>(json);
                    }

                    int vId = int.Parse(dgvIngreso.SelectedRows[0].Cells["IdIngreso"].Value.ToString()); //Obtenemos el id de la provincia a modificar

                    var ingreso = new Ingreso();

                    ingreso = ingresos.FirstOrDefault(x => x.IdIngreso == vId);

                    if (ingreso != null)
                    {
                        ingresos.Remove(ingreso);

                    }


                    json = Newtonsoft.Json.JsonConvert.SerializeObject(ingresos);

                    var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
                    sw.WriteLine(json);
                    sw.Close();

                    MessageBox.Show("Eliminación exitosa", "Ingreso Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearFields();
                    GetRecords();

                }
            }
            else MessageBox.Show("No se ha seleccionado ninguna fila", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvIngreso.SelectedRows.Count == 1) //Si hay una fila seleccionada
            {


                if (MessageBox.Show("¿Desea actualizar el registro?", "AVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var json = string.Empty;
                    var ingresos = new List<Ingreso>();
                    var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\ingresos.json";

                    if (File.Exists(pathFile))
                    {
                        json = File.ReadAllText(pathFile, Encoding.UTF8);
                        ingresos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ingreso>>(json);
                    }

                    int vId = int.Parse(dgvIngreso.SelectedRows[0].Cells["IdIngreso"].Value.ToString()); //Obtenemos el id de la provincia a modificar

                    var ingreso = new Ingreso();

                    ingreso = ingresos.FirstOrDefault(x => x.IdIngreso == Id);
                    if (ingreso != null)
                    {
                        ingresos.Remove(ingreso);
                        ingreso.Cuenta = cmbCuenta.Text;
                        ingreso.TipoIngreso = cmbTipoIngreso.Text;
                        ingreso.Monto = decimal.Parse(txtMonto.Text);
                        ingreso.Descripcion = txtComent.Text;
                        ingreso.Fecha = dtpFecha.Value;


                    }
                    ingresos.Add(ingreso);

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(ingresos);

                    var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
                    sw.WriteLine(json);
                    sw.Close();

                    MessageBox.Show("Registro exitoso", "Patient Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearFields();
                    GetRecords();

                }
            }
            else MessageBox.Show("No se ha seleccionado ninguna fila", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        
    }
}

    
