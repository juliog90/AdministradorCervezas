using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdministradorCervezas.Models;
using Moq;
using NUnit.Framework;

namespace AdministradorCervezas.Tests.Unitarias
{
    // Decimos que esto es un grupo de pruebas.
    [TestFixture]
    public class ManejarCervezas
    {

        // Prueba 1
        [Test]
        public void Cuando_Probamos_Sin_Especificar_Tipo_No_Agrega()
        {
            //Arrange
            //Creamos objeto mock
            var mock = new Mock<IDatabaseCrud>();

            //Act
            //Debe regresar falso
            mock.Setup(db => db.Add()).Returns(false);

            //Assert
            Assert.AreEqual(false, mock.Object.Add());
        }

        [Test]
        public void Suma_Incorrecta_Regresa_Falso()
        {
            //Arrange
            var ciudad = new City("323", "Tijuana");
            var direccion = new Address(2, "Chula Vista", "Brooklyn", "4", "2222", ciudad);
            var cliente = new Customer(2, "Julio", "Martineza", "hola@mundo.com", "664323222", direccion, "22222");
            var order = new Order(1, DateTime.Parse("20181102"), DateTime.Parse("20181202"), cliente);
            var marca = new Brand("2", "Corona");
            var clasification = new Clasification("3", "Premium");

            var corona = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 499, "image");
            var corona2 = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 599, "image");
            var corona3 = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 399, "image");
            var corona4 = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 299, "image");
            var detail = new OrderDetail(corona, 30, 499);
            var detail2 = new OrderDetail(corona2, 30, 599);
            var detail3 = new OrderDetail(corona3, 30, 399);
            var detail4 = new OrderDetail(corona4, 30, 299);

            List<OrderDetail> details = new List<OrderDetail>
            {
                detail,
                detail2,
                detail3,
                detail4
            };

            //Act
            order.AllDetails = details;

            //Assert
            Assert.IsFalse(order.Total == 23);
        }

        [Test]
        public void Suma_Correcta_Regresa_Verdadero()
        {
            //Arrange
            var ciudad = new City("323", "Tijuana");
            var direccion = new Address(2, "Chula Vista", "Brooklyn", "4", "2222", ciudad);
            var cliente = new Customer(2, "Julio", "Martineza", "hola@mundo.com", "664323222", direccion, "22222");
            var order = new Order(1, DateTime.Now, DateTime.Now, cliente);
            var marca = new Brand("2", "Corona");
            var clasification = new Clasification("3", "Premium");

            var corona = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 499, "image");
            var corona2 = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 599, "image");
            var corona3 = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 399, "image");
            var corona4 = new Beer(2, 30, PresentationType.Barrel, Fermentation.Alto, MeasurementUnit.L, 500, marca, clasification, 299, "image");
            var detail = new OrderDetail(corona, 30, 499);
            var detail2 = new OrderDetail(corona2, 30, 599);
            var detail3 = new OrderDetail(corona3, 30, 399);
            var detail4 = new OrderDetail(corona4, 30, 299);

            List<OrderDetail> details = new List<OrderDetail>
            {
                detail,
                detail2,
                detail3,
                detail4
            };

            //Act
            order.AllDetails = details;
            Console.WriteLine(order.Total);

            //Assert
            Assert.IsTrue(order.Total == 53880);
        }

    }
}
