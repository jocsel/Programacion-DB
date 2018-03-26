using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccesoDatos;
using ClaseSistema;


namespace GUI
{
    public partial class FP_Usuario : Form
    {
        private readonly UsuarioDAL _UsuarioDAL = new UsuarioDAL();
       
        public FP_Usuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) ///ACEPTAR   
        {
            FH_Usuario new_usuario = new FH_Usuario();
            new_usuario.Show();
            this.Hide();

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {

        }

        public void LlenarGrid()
        {
            List<ClaseUsuario> usuarios = new List<ClaseUsuario>();
            usuarios = _UsuarioDAL.selectRow();

            if(usuarios.Count>0)
            {
                dgvUsuario.AutoGenerateColumns = false;
                dgvUsuario.DataSource = usuarios;
               ///NOMBRE DE LA COLUMNA EN EL GRID                    NOMBRE DEL ATRIBUTO EN LA CLS C#
                dgvUsuario.Columns["Id_usuario"].DataPropertyName = "id_usuario";
                dgvUsuario.Columns["Usuario"].DataPropertyName = "usuario";
                dgvUsuario.Columns["Contraseña"].DataPropertyName = "contrasena";
                dgvUsuario.Columns["Id_rol"].DataPropertyName = "id_rol";
                dgvUsuario.Columns["nombre"].DataPropertyName = "nombre";
                dgvUsuario.Columns["apellido"].DataPropertyName = "apellido";
                dgvUsuario.Columns["rol"].DataPropertyName = "rol";

            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvUsuario_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            FH_Usuario new_usuario = new FH_Usuario();
            new_usuario.user = new ClaseUsuario();
            new_usuario.Text = "Modificar";
            new_usuario.user.usuario = dgvUsuario.CurrentRow.Cells["Usuario"].Value.ToString();
            new_usuario.user.contrasena = dgvUsuario.CurrentRow.Cells["Contraseña"].Value.ToString();
            new_usuario.user.nombre = dgvUsuario.CurrentRow.Cells["nombre"].Value.ToString();
            new_usuario.user.apellido = dgvUsuario.CurrentRow.Cells["apellido"].Value.ToString();
            new_usuario.user.rol = dgvUsuario.CurrentRow.Cells["rol"].Value.ToString();
            new_usuario.Show();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FP_Usuario_Load(object sender, EventArgs e)
        {
            dgvUsuario.MultiSelect = false;
            LlenarGrid();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbllogo_Click(object sender, EventArgs e)
        {

        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FH_Usuario nu = new FH_Usuario();
            nu.user = new ClaseUsuario();
            nu.Text = "Modificar";
            nu.user.usuario = dgvUsuario.CurrentRow.Cells["Usuario"].Value.ToString();
            nu.user.contrasena = dgvUsuario.CurrentRow.Cells["Contraseña"].Value.ToString();
            nu.user.nombre = dgvUsuario.CurrentRow.Cells["nombre"].Value.ToString();
            nu.user.apellido = dgvUsuario.CurrentRow.Cells["apellido"].Value.ToString();
            nu.user.rol = dgvUsuario.CurrentRow.Cells["rol"].Value.ToString();
            nu.user.id_usuario = Convert.ToInt32( dgvUsuario.CurrentRow.Cells["Id_usuario"].Value);
            nu.Show();
        }
    }
}
