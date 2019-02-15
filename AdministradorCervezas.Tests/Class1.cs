using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.Tests
{
    [TestFixture]
    public class BeerModel
    {
        [Test]
        public void CervezaIDBaseDatos()
        {
            Beer cervezaPrueba = new Beer(1);
            Assert.IsNotNull(cervezaPrueba);
        }
    }
}
