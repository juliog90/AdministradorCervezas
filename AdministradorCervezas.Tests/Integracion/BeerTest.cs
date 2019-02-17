using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.Tests.Integracion
{
    [TestFixture]
    public class BeerTest
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ObtenerCervezaConId(int value)
        {
            Beer cervezaPrueba = new Beer(value);
            Assert.IsTrue(cervezaPrueba.Id != 0, $"La id {value} no existe en la base de datos");
        }

        [Test]
        public void TodasLasCervezas()
        {
            List<Beer> cervezas = Beer.GetAll();
            Assert.IsTrue(cervezas.Count > 0, $"No existen cervezas en la base de datos");
        }

        [Test]
        public void CrearCervezaNueva()
        {
            Beer cerveza = new Beer();
            cerveza.Brand = new Brand("HEN");
            cerveza.Clasification = new Clasification("RED");
            cerveza.Content = 40;
            cerveza.Fermlevel = Fermentation.Alto;
            cerveza.GradoAlcohol = 40;
            cerveza.Price = 40;
            cerveza.Image = "nobeer1.png";
            cerveza.Presentation = PresentationType.Bottle;

            Assert.IsTrue(cerveza.Add(), $"No se pueden crear cervezas");
        }
    }
}
