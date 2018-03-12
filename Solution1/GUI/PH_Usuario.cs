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
    public partial class PH_Usuario : Form
    {
        private readonly RolesDAL _RolesDAL = new RolesDAL();
        public ClaseUsuario user;
        public PH_Usuario()
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

        }
    }
}
