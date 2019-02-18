using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
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
            Beer cerveza = new Beer(value);
            bool existeDatoMarca = cerveza.Brand != null;
            bool existeDatoClas = cerveza.Clasification != null;
            bool existeDatoCont = cerveza.Content < 0;
            bool existeDatoFerme = cerveza.Fermlevel < 0;
            bool existeDatoGrad = cerveza.GradoAlcohol < 0;
            bool existeDatoPre = cerveza.Price < 0;
            bool existeDatoIma = cerveza.Image != null;
            bool existeDatoPres = cerveza.Presentation < 0;

            bool datosCorrectos = existeDatoMarca && existeDatoClas && existeDatoCont && existeDatoFerme
                                  && existeDatoGrad && existeDatoPre && existeDatoIma && existeDatoPres;

            Assert.IsTrue(cerveza.Id != 0, $"La id {value} no existe en la base de datos");
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
            // TODO: Verfificar si esta cerveza tiene todos los campos
            Beer cerveza = new Beer();
            cerveza.Brand = new Brand("HEN");
            cerveza.Clasification = new Clasification("RED");
            cerveza.Content = 40;
            cerveza.Fermlevel = Fermentation.Alto;
            cerveza.GradoAlcohol = 40;
            cerveza.Price = 40;
            cerveza.Image = "nobeer1.png";
            cerveza.Presentation = PresentationType.Bottle;

            bool cervezaAgregada = cerveza.Add();

            MySqlConnection conexion = new MySqlConnection();
            conexion.ConnectionSource = new AppSettings();
            conexion.Connect();
            MySqlCommand comando = new MySqlCommand("select be_id as ultima_id from beer order by be_id desc limit 1;");
            DataTable tabla = conexion.ExecuteQuery(comando);
            DataRow row = tabla.Rows[0];
            int ultimoId = (int)row["ultima_id"];

            Beer ultima = new Beer(ultimoId);
            cerveza.Id = ultima.Id;

            bool mismaMarca = cerveza.Brand.Id == ultima.Brand.Id;
            bool mismoPais = cerveza.Brand.Country.Id == ultima.Brand.Country.Id;
            bool mismoTipo = cerveza.Clasification.BeerType.Id == ultima.Clasification.BeerType.Id;
            bool mismaClasificacion = cerveza.Clasification.Code == ultima.Clasification.Code;
            bool mismoContenido = cerveza.Content == ultima.Content;
            bool mismaFermentacion = cerveza.Fermlevel == ultima.Fermlevel;
            bool mismoGrado = cerveza.GradoAlcohol == ultima.GradoAlcohol;
            bool mismoPrecio = cerveza.Price == ultima.Price;
            bool mismaImagen = cerveza.Image == ultima.Image;
            bool mismaPresentacion = cerveza.Presentation == ultima.Presentation;


            Assert.IsTrue(cervezaAgregada && mismaMarca && mismaClasificacion && mismoContenido && mismaFermentacion &&
                          mismoGrado && mismoPrecio && mismaImagen && mismaPresentacion && mismoPais && mismoTipo,
                          $"No se pueden crear cervezas");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        public void ActualizarCerveza(int id)
        {
            Beer cerveza = new Beer(id);
            cerveza.Price = 500;

            // TODO: Verificar si se edito en cada campo
            // TODO: Terminar Querys

            Assert.IsTrue(cerveza.Edit(), $"No se puede editar");

        }
    }
}
