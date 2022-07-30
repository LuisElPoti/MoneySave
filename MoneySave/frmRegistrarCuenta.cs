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
    
    public partial class frmRegistrarCuenta : Form
    {
        public bool Adding { get; set; } = true;
        private int Id;      
        public frmRegistrarCuenta()
        {
            InitializeComponent();
            GetRecords();
            
        }
        private void frmRegistrarCuenta_Load(object sender, EventArgs e)
        {
            GetRecords();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            this.dgvCuentas.Columns["IdUsuario"].Visible = false;
            dgvCuentas.Columns[0].HeaderText = "ID";
            dgvCuentas.Columns[1].HeaderText = "Nombre";            
            dgvCuentas.Columns[4].HeaderText = "Fecha Creacion";

        }
        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\cuentas.json";
            var cuentas = new List<Cuentas>();
            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                cuentas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cuentas>>(json);
            }

            dgvCuentas.DataSource = cuentas;
        }
        

        private void SaveRecord()
        {
            var json = string.Empty;
            var cuentas = new List<Cuentas>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\cuentas.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                cuentas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cuentas>>(json);
            }
            var cuenta = new Cuentas();
            if (Adding == true)
            {
                cuenta = new Cuentas
                {
                    IdCuenta = (cuentas.Count + 1),
                    Name = txtTipoCuenta.Text,
                    Monto = decimal.Parse(txtMonto.Text),
                    CreatedDate = Convert.ToDateTime(txtFecha.Text),
                    Descripcion = txtComent.Text,
                    //id del usuario al cual pertenece esa cuenta

                };
                
            }
            else
            {
                
                cuenta = cuentas.FirstOrDefault(x => x.IdCuenta == Id);
                if (cuenta != null)
                {
                    cuentas.Remove(cuenta);
                    
                    cuenta.Name = txtTipoCuenta.Text;
                    cuenta.Descripcion = txtComent.Text;
                }
            }
            cuentas.Add(cuenta);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(cuentas);

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
            txtTipoCuenta.Text = String.Empty;
            txtMonto.Text = String.Empty;           
            txtComent.Text = String.Empty;
        }
        private void dgvCuentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Id = int.Parse(dgvCuentas.CurrentRow.Cells[0].Value.ToString());
                DataGridViewRow dgv = dgvCuentas.Rows[e.RowIndex];
                txtTipoCuenta.Text = dgv.Cells[1].Value.ToString();
                txtMonto.Text = dgv.Cells[2].Value.ToString();
                
                txtComent.Text = dgv.Cells[3].Value.ToString();

            }
        }

        private void btnAñadirCuenta_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.SelectedRows.Count == 1) //Si hay una fila seleccionada
            {


                if (MessageBox.Show("¿Realmente desea eliminar la cuenta?", "AVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var json = string.Empty;
                    var cuentas = new List<Cuentas>();
                    var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\cuentas.json";

                    if (File.Exists(pathFile))
                    {
                        json = File.ReadAllText(pathFile, Encoding.UTF8);
                        cuentas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cuentas>>(json);
                    }

                    int vId = int.Parse(dgvCuentas.SelectedRows[0].Cells["IdCuenta"].Value.ToString()); //Obtenemos el id de la provincia a modificar

                    var cuenta = new Cuentas();

                    cuenta = cuentas.FirstOrDefault(x => x.IdCuenta == vId);

                    if (cuenta != null)
                    {
                        cuentas.Remove(cuenta);

                    }


                    json = Newtonsoft.Json.JsonConvert.SerializeObject(cuentas);

                    var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
                    sw.WriteLine(json);
                    sw.Close();

                    MessageBox.Show("Eliminación exitosa", "Cuenta Manage", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearFields();
                    GetRecords();

                }
            }
            else MessageBox.Show("No se ha seleccionado ninguna fila", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.SelectedRows.Count == 1) //Si hay una fila seleccionada
            {


                if (MessageBox.Show("¿Desea actualizar el registro?", "AVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var json = string.Empty;
                    var cuentas = new List<Cuentas>();
                    var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\cuentas.json";

                    if (File.Exists(pathFile))
                    {
                        json = File.ReadAllText(pathFile, Encoding.UTF8);
                        cuentas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cuentas>>(json);
                    }

                    int vId = int.Parse(dgvCuentas.SelectedRows[0].Cells["IdCuenta"].Value.ToString()); //Obtenemos el id de la provincia a modificar

                    var cuenta = new Cuentas();

                    cuenta = cuentas.FirstOrDefault(x => x.IdCuenta == Id);
                    if (cuenta != null)
                    {
                        cuentas.Remove(cuenta);
                        cuenta.Name = txtTipoCuenta.Text;
                        cuenta.Monto = decimal.Parse(txtMonto.Text);               
                        cuenta.Descripcion = txtComent.Text;


                    }
                    cuentas.Add(cuenta);

                    json = Newtonsoft.Json.JsonConvert.SerializeObject(cuentas);

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
