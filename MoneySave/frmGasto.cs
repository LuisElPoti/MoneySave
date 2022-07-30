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
    public partial class frmGasto : Form
    {
        public bool Adding { get; set; } = true;
        private int Id;
        public frmGasto()
        {
            InitializeComponent();

        }

        private void frmGasto_Load(object sender, EventArgs e)
        {
            GetRecords();
            this.dgvGasto.Columns["IdUsuario"].Visible = false;
            dgvGasto.Columns[0].HeaderText = "ID";           
            dgvGasto.Columns[2].HeaderText = "Categoria";

        }
        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\gastos.json";
            var gastos = new List<Gasto>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                gastos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Gasto>>(json);
            }

            dgvGasto.DataSource = gastos;
        }
        private void SaveRecord()
        {
            var json = string.Empty;
            var gastos = new List<Gasto>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\gastos.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                gastos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Gasto>>(json);
            }
            var gasto = new Gasto();
            if (Adding == true)
            {
                gasto = new Gasto
                {
                    IdGasto = (gastos.Count + 1),
                    Cuenta = cmbCuentas.Text,
                    TipoGasto = cmbTipoGasto.Text,
                    Monto = decimal.Parse(txtMonto.Text),
                    Descripcion = txtComent.Text,
                    Fecha = dtpFecha.Value

                    //id del usuario al cual pertenece esa gasto

                };

            }
            else
            {

                gasto = gastos.FirstOrDefault(x => x.IdGasto == Id);
                if (gasto != null)
                {
                    gastos.Remove(gasto);
                    gasto.Descripcion = txtComent.Text;
                }
            }
            gastos.Add(gasto);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(gastos);

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
            cmbCuentas.Text = String.Empty;
            cmbTipoGasto.Text = String.Empty;
            txtMonto.Text = String.Empty;
            txtComent.Text = String.Empty;
        }
        private void dgvGasto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Id = int.Parse(dgvGasto.CurrentRow.Cells[0].Value.ToString());
                DataGridViewRow dgv = dgvGasto.Rows[e.RowIndex];
                cmbCuentas.Text = dgv.Cells[1].Value.ToString();
                cmbTipoGasto.Text = dgv.Cells[2].Value.ToString();
                txtMonto.Text = dgv.Cells[3].Value.ToString();
                txtComent.Text = dgv.Cells[4].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(dgv.Cells[5].Value.ToString());
            }
        }

        private void btnAñadirGasto_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGasto.SelectedRows.Count == 1) //Si hay una fila seleccionada
            {


                if (MessageBox.Show("¿Realmente desea eliminar la gasto?", "AVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var json = string.Empty;
                    var gastos = new List<Gasto>();
                    var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\gastos.json";

                    if (File.Exists(pathFile))
                    {
                        json = File.ReadAllText(pathFile, Encoding.UTF8);
                        gastos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Gasto>>(json);
                    }

                    int vId = int.Parse(dgvGasto.SelectedRows[0].Cells["IdGasto"].Value.ToString()); //Obtenemos el id de la provincia a modificar

                    var gasto = new Gasto();

                    gasto = gastos.FirstOrDefault(x => x.IdGasto == vId);

                    if (gasto != null)
                    {
                        gastos.Remove(gasto);

                    }


                    json = Newtonsoft.Json.JsonConvert.SerializeObject(gastos);

                    var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
                    sw.WriteLine(json);
                    sw.Close();

                    MessageBox.Show("Eliminación exitosa", "Gasto Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearFields();
                    GetRecords();

                }
            }
            else MessageBox.Show("No se ha seleccionado ninguna fila", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvGasto.SelectedRows.Count == 1) //Si hay una fila seleccionada
            {


                if (MessageBox.Show("¿Desea actualizar el registro?", "AVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var json = string.Empty;
                    var gastos = new List<Gasto>();
                    var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\gastos.json";

                    if (File.Exists(pathFile))
                    {
                        json = File.ReadAllText(pathFile, Encoding.UTF8);
                        gastos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Gasto>>(json);
                    }

                    int vId = int.Parse(dgvGasto.SelectedRows[0].Cells["IdGasto"].Value.ToString()); //Obtenemos el id de la provincia a modificar

                    var gasto = new Gasto();

                    gasto = gastos.FirstOrDefault(x => x.IdGasto == Id);
                    if (gasto != null)
                    {
                        gastos.Remove(gasto);
                        gasto.Cuenta = cmbCuentas.Text;
                        gasto.TipoGasto = cmbTipoGasto.Text;
                        gasto.Monto = decimal.Parse(txtMonto.Text);
                        gasto.Descripcion = txtComent.Text;
                        gasto.Fecha = dtpFecha.Value;


                    }
                    gastos.Add(gasto);

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(gastos);

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
