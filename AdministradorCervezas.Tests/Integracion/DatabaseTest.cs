using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
    }
}
