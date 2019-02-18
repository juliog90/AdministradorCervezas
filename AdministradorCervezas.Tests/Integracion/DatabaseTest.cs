using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data;
using MySql.Data.MySqlClient;

namespace AdministradorCervezas.Tests.Integracion
{
    [TestFixture]
    class DatabaseTest
    {
        [Test]
        public void BaseDeDatosDisponible()
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionSource = new AppSettings();
            connection.Connect();

            Assert.IsTrue(connection.Open(), $"Base de datos no disponible");
        }

        [TestCase("brand")]
        [TestCase("beer")]
        [TestCase("country")]
        public void EjecutarQuery(string tablaBaseDatos)
        {
            MySqlConnection conexion = new MySqlConnection();
            conexion.ConnectionSource = new AppSettings();
            conexion.Connect();
            MySqlCommand comando = new MySqlCommand("select * from " + tablaBaseDatos);

            DataTable tabla = conexion.ExecuteQuery(comando);

            Assert.IsTrue(tabla.Rows.Count > 0);
        }

        [Test]
        public void EjecutarNoQuery()
        {
            MySqlConnection conexion = new MySqlConnection();
            conexion.ConnectionSource = new AppSettings();
            conexion.Connect();
            MySqlCommand comando = new MySqlCommand("update beer set be_content = 50 where be_id = 1");

            Assert.IsTrue(conexion.ExecuteNonQuery(comando));
        }
    }
}
