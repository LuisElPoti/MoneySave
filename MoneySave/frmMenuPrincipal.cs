using System;
using System.Windows.Forms;

namespace MoneySave
{
    
    public partial class frmMenuPrincipal : Form
    {
        public int xClick = 0, yClick = 0;
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void ptbCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void ptbMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ptbMaximizar.Visible = false;
            ptbRestaurar.Visible = true;
        }

        private void ptbRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            ptbMaximizar.Visible = true;
            ptbRestaurar.Visible = false;
        }

        private void ptbMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRegistrarCuenta_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new frmRegistrarCuenta());
        }

        

        private void pnlBarra_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void btnIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new frmIngreso());
        }

        private void btnGasto_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new frmGasto());
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new frmInicio());
        }

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            btnInicio_Click(null, e);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new frmReporte());
        }

        private void AbrirFormHija(object formhija)
        {
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);

            Form fh = formhija as Form;

            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(fh);
            this.pnlContenedor.Tag = fh;
            fh.Show();
        }
    }
}
