using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaseSistema
{
    public class ClaseUsuario
    {
        public int id_usuario { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public int id_rol { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public string rol { get; set; }
    }
}
