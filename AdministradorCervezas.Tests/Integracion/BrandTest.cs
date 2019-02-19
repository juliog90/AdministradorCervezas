using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administradormarcas.Tests.Integracion
{
    [TestFixture]
    public class BrandTest
    {
        /// <summary>
        /// Verifica que se puede consultar una marca
        /// </summary>
        /// <param name="value">Id de la base de datos</param>
        [TestCase("BRA")]
        [TestCase("COR")]
        [TestCase("IND")]
        public void ObtenermarcaConId(string value)
        {
            Brand marca = new Brand(value);
            bool existeDatoPais = marca.Country != null;
            bool existeDatoNombre = marca.Name != "";
            bool existeDatoId = marca.Id != "";

            bool datosCorrectos = existeDatoPais && existeDatoNombre && existeDatoId;

            Assert.IsTrue(datosCorrectos, $"La id {value} no existe en la base de datos");
        }

        /// <summary>
        /// Verifica que existan marcas
        /// </summary>
        [Test]
        public void TodasLasmarcas()
        {
            List<Brand> marcas = Brand.GetAll();

            bool siExiste = marcas.Count > 0;

            Assert.IsTrue(siExiste, $"No existen marcas en la base de datos");
        }

        /// <summary>
        /// Verifica que se inserte una nueva marca en la base de datos
        /// </summary>
        [Test]
        public void CrearMarcaNueva()
        {
            Brand marca = new Brand();
            marca.Country = new Country("MEX");
            marca.Name = "LevantaMuertos";
            marca.Id = "LMS";

            bool marcaAgregada = marca.Add();
            string ultimoId = ObtenerUltimaID();

            Brand ultima = new Brand(ultimoId);
            marca.Id = ultima.Id;

            bool sonIguales = ComparaMarcas(marca, ultima);

            Assert.IsTrue(marcaAgregada && sonIguales, $"No se pueden crear marcas");
        }

        /// <summary>
        /// Verifica que se puedan actualizar marcas
        /// </summary>
        /// <param name="id">Id de la marca en la base de datos</param>
        [TestCase("TEC")]
        [TestCase("SKL")]
        [TestCase("MOD")]
        public void Actualizarmarca(string id)
        {
            Brand marca = new Brand(id);

            // guardamos informacion para restablecer
            Brand guardaInfo = new Brand(id);

            // cambiamos datos
            marca.Country = new Country("MEX");
            marca.Name = "Four Loko";

            // TODO: Verificar si se edito en cada campo
            // TODO: Terminar Querys

            bool siEdito = marca.Edit();

            Assert.IsTrue(siEdito, $"No se puede editar");

            // regresamos valores
            guardaInfo.Edit();

        }

        /// <summary>
        /// Verifica si se puede eliminar una marca
        /// </summary>
        [Test]
        public void EliminarMarca()
        {
            string ultimaId = ObtenerUltimaID();
            Brand marca = new Brand(ultimaId);

            bool seElimino = marca.Delete();
            // confirmamos
            Brand marcaEliminada = new Brand(ultimaId);

            bool noExiste = marcaEliminada.Id == "";

            Assert.IsTrue(seElimino && noExiste, $"No elimino de la base de datos");
        }

        #region MetodosPrivados

        /// <summary>
        /// Compara una marca con otra
        /// </summary>
        /// <param name="marca">marca a comparar</param>
        /// <param name="otraMarca">marca con que se compara</param>
        /// <returns>Si es la misma marca</returns>
        private bool ComparaMarcas(Brand marca, Brand otraMarca)
        {
            bool mismaMarca = marca.Brand.Id == otraMarca.Brand.Id;
            bool mismoPais = marca.Brand.Country.Id == otraMarca.Brand.Country.Id;
            bool mismoTipo = marca.Clasification.BrandType.Id == otraMarca.Clasification.BrandType.Id;
            bool mismaClasificacion = marca.Clasification.Code == otraMarca.Clasification.Code;
            bool mismoContenido = marca.Content == otraMarca.Content;
            bool mismaFermentacion = marca.Fermlevel == otraMarca.Fermlevel;
            bool mismoGrado = marca.GradoAlcohol == otraMarca.GradoAlcohol;
            bool mismoPrecio = marca.Price == otraMarca.Price;
            bool mismaImagen = marca.Image == otraMarca.Image;
            bool mismaPresentacion = marca.Presentation == otraMarca.Presentation;

            bool sonIguales = mismaMarca && mismoPais && mismoTipo && mismaClasificacion &&
                              mismoContenido && mismaFermentacion && mismoGrado && mismoPrecio &&
                              mismaImagen && mismaPresentacion;

            return sonIguales;
        }

        /// <summary>
        /// Obtiene la ultima id insertada en la base de datos
        /// </summary>
        /// <returns></returns>
        private string ObtenerUltimaID()
        {
            MySqlConnection conexion = new MySqlConnection();
            conexion.ConnectionSource = new AppSettings();
            conexion.Connect();
            MySqlCommand comando = new MySqlCommand("select be_id as ultima_id from Brand order by be_id desc limit 1;");
            DataTable tabla = conexion.ExecuteQuery(comando);
            DataRow row = tabla.Rows[0];
            string ultimoId = (string)row["ultima_id"];
            return ultimoId;
        }

        #endregion
    }
}
