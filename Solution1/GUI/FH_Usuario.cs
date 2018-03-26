using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClaseSistema;
using AccesoDatos;

namespace GUI
{
    public partial class FH_Usuario : Form
    {
        private readonly RolesDAL _RolesDAL = new RolesDAL();
        public ClaseUsuario user; 
        private readonly funciones _funciones = new funciones();
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        public FH_Usuario()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void LlenarCombo()
        {
            List<ClaseRoles> roles = new List<ClaseRoles>();
            roles = _RolesDAL.selectrow();
            cmbRol.DataSource = roles;
            cmbRol.DisplayMember = "rol";
            cmbRol.ValueMember = "id_rol";

        }

        private void PH_Usuario_Load(object sender, EventArgs e)
        {
            LlenarCombo();
            if (user != null)
            {
                btnAceptar.Text = "Modificar";
                lbllogo.Text = this.Text;
                txtusuario.Text = user.usuario;
                txtnombre.Text = user.nombre;
              ///  txtcontraseña.Text = user.contrasena;
                txtcontraseña.Text = "*******";
                txtapellido.Text = user.apellido;
                cmbRol.SelectedValue = user.id_rol;
                
            }
            else
            {
                btnAceptar.Text = "Agregar";
                lbllogo.Text = btnAceptar.Text;
               
            }
                
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            FP_Usuario fpu = new FP_Usuario();
            fpu.Show();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            if(user != null)
            {
                user = new ClaseUsuario();
            }
            user.usuario = txtusuario.Text; ///
            user.apellido = txtapellido.Text;
            user.contrasena = _funciones.gen_cifrador(txtcontraseña.Text);
            txtcontraseña.Text = "*******";
            user.nombre = txtnombre.Text;
            user.id_rol = Convert.ToInt32(cmbRol.SelectedValue);
            if (btnAceptar.Text == "Modificar")
            {
                mensaje =_usuarioDAL.UpdateRow(user);
                MessageBox.Show(mensaje);
            }
            else
            {
                mensaje = _usuarioDAL.insertarRow(user);
            }
        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
