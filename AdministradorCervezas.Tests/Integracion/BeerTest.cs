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
        /// <summary>
        /// Verifica que se puede consultar una cerveza
        /// </summary>
        /// <param name="value">Id de la base de datos</param>
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ObtenerCervezaConId(int value)
        {
            Beer cerveza = new Beer(value);
            bool existeDatoMarca = cerveza.Brand != null;
            bool existeDatoClas = cerveza.Clasification != null;
            bool existeDatoCont = cerveza.Content > 0;
            bool existeDatoFerme = cerveza.Fermlevel >= 0;
            bool existeDatoGrad = cerveza.GradoAlcohol >= 0;
            bool existeDatoPre = cerveza.Price > 0;
            bool existeDatoIma = cerveza.Image != null;
            bool existeDatoPres = cerveza.Presentation >= 0;
            bool existeDatoId = cerveza.Id > 0;

            bool datosCorrectos = existeDatoMarca && existeDatoClas && existeDatoCont && existeDatoFerme
                                  && existeDatoGrad && existeDatoPre && existeDatoIma && existeDatoPres
                                  && existeDatoId;

            Assert.IsTrue(datosCorrectos, $"La id {value} no existe en la base de datos");
        }

        /// <summary>
        /// Verifica que existan cervezas
        /// </summary>
        [Test]
        public void TodasLasCervezas()
        { 
            List<Beer> cervezas = Beer.GetAll();

            bool existenCervezas = cervezas.Count > 0;

            Assert.IsTrue(existenCervezas, $"No existen cervezas en la base de datos");
        }

        /// <summary>
        /// Verifica que se inserte una nueva cerveza en la base de datos
        /// </summary>
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

            bool cervezaAgregada = cerveza.Add();
            int ultimoId = ObtenerUltimaID();

            Beer ultima = new Beer(ultimoId);
            cerveza.Id = ultima.Id;

            bool sonIguales = ComparaCervezas(cerveza, ultima);

            Assert.IsTrue(cervezaAgregada && sonIguales, $"No se pueden crear cervezas");
        }

        /// <summary>
        /// Verifica que se puedan actualizar cervezas
        /// </summary>
        /// <param name="id">Id de la cerveza en la base de datos</param>
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        public void ActualizarCerveza(int id)
        {
            Beer cerveza = new Beer(id);

            // guardamos informacion para restablecer
            Beer guardaInfo = new Beer(id);

            // cambiamos datos
            cerveza.Price = 500;
            cerveza.Brand = new Brand("IND");
            cerveza.Clasification = new Clasification("AMB");
            cerveza.Content = 99;
            cerveza.Fermlevel = Fermentation.Espontaneo;
            cerveza.MeasurementUnit = MeasurementUnit.ML;

            // TODO: Verificar si se edito en cada campo
            // TODO: Terminar Querys

            bool siEdito = cerveza.Edit();

            Assert.IsTrue(siEdito, $"No se puede editar");

            // regresamos valores
            guardaInfo.Edit();

        }

        /// <summary>
        /// Verifica si se puede eliminar una cerveza
        /// </summary>
        [Test]
        public void EliminarCerveza()
        {
            int ultimaId = ObtenerUltimaID();
            Beer cerveza = new Beer(ultimaId);

            bool seElimino = cerveza.Delete();
            // confirmamos
            Beer cervezaEliminada = new Beer(ultimaId);

            bool noExiste = cervezaEliminada.Id < 1;

            Assert.IsTrue(seElimino && noExiste, $"No elimino de la base de datos");
        }

        #region MetodosPrivados

        /// <summary>
        /// Compara una cerveza con otra
        /// </summary>
        /// <param name="cerveza">Cerveza a comparar</param>
        /// <param name="otraCerveza">Cerveza con que se compara</param>
        /// <returns>Si es la misma cerveza</returns>
        private bool ComparaCervezas(Beer cerveza, Beer otraCerveza)
        {
            bool mismaMarca = cerveza.Brand.Id == otraCerveza.Brand.Id;
            bool mismoPais = cerveza.Brand.Country.Id == otraCerveza.Brand.Country.Id;
            bool mismoTipo = cerveza.Clasification.BeerType.Id == otraCerveza.Clasification.BeerType.Id;
            bool mismaClasificacion = cerveza.Clasification.Code == otraCerveza.Clasification.Code;
            bool mismoContenido = cerveza.Content == otraCerveza.Content;
            bool mismaFermentacion = cerveza.Fermlevel == otraCerveza.Fermlevel;
            bool mismoGrado = cerveza.GradoAlcohol == otraCerveza.GradoAlcohol;
            bool mismoPrecio = cerveza.Price == otraCerveza.Price;
            bool mismaImagen = cerveza.Image == otraCerveza.Image;
            bool mismaPresentacion = cerveza.Presentation == otraCerveza.Presentation;

            bool sonIguales = mismaMarca && mismoPais && mismoTipo && mismaClasificacion &&
                              mismoContenido && mismaFermentacion && mismoGrado && mismoPrecio &&
                              mismaImagen && mismaPresentacion;

            return sonIguales;
        }

        /// <summary>
        /// Obtiene la ultima id insertada en la base de datos
        /// </summary>
        /// <returns></returns>
        private int ObtenerUltimaID()
        {
            MySqlConnection conexion = new MySqlConnection();
            conexion.ConnectionSource = new AppSettings();
            conexion.Connect();
            MySqlCommand comando = new MySqlCommand("select be_id as ultima_id from beer order by be_id desc limit 1;");
            DataTable tabla = conexion.ExecuteQuery(comando);
            DataRow row = tabla.Rows[0];
            int ultimoId = (int)row["ultima_id"];
            return ultimoId;
        }

        #endregion
    }
}
