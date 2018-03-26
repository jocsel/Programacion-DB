using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace ClaseSistema
{
     public class funciones
    {
        public string gen_cifrador(string contrasena) // ENTRA EL STRING SIN CIFRAR
        {
            //CLASE PARA CIFRAR ESTOY INSTANCIANDO LA VARIABLE
            SHA512Managed cifrar = new SHA512Managed();

            // CONVIERTO MI STRING A ARREGLO BYTE

            byte[] textascii = System.Text.Encoding.ASCII.GetBytes(contrasena);

            //LO PASO EL ARREGLO BYTE AL METODO QUE CIFRA Y ME DEVUELVE UN ARREGLO DE BYTE
            byte[] textcifrado = cifrar.ComputeHash(textascii);

            // CONVIERTO EL ARREGLO DE BYTE A STRING
            string clave = Convert.ToBase64String(textcifrado);

            // REGRESO EL STRIN CIFRADO
            return clave;
        }

    }
}
