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

        private void button1_Click(object sender, EventArgs e)
        {
            PH_Usuario new_usuario = new PH_Usuario();
            new_usuario.Show();

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
                dgvUsuario.Columns["idusuario"].DataPropertyName = "id_usuario";
                dgvUsuario.Columns["usuario"].DataPropertyName = "usuario";
                dgvUsuario.Columns["contraseña"].DataPropertyName = "contraseña";
                dgvUsuario.Columns["id_rol"].DataPropertyName = "id_rol";
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

            PH_Usuario new_usuario = new PH_Usuario();
            new_usuario.user = new ClaseUsuario();
            new_usuario.user.usuario = dgvUsuario.CurrentRow.Cells["usuario"].Value.ToString();
            new_usuario.user.nombre = dgvUsuario.CurrentRow.Cells["nombre"].Value.ToString();
        }
    }
}
