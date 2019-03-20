using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class PrincipalViewModel : Conductor<object>
    {
        private string _titulo = "Brewery - Home";

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
                NotifyOfPropertyChange(() => Titulo);
            }
        }

        public void CargaCervezas()
        {
            ActivateItem(new DatosCervezasViewModel());
            Titulo = "Brewery - Administrando Cervezas";
        }

        public void CargaMarcas()
        {
            ActivateItem(new DatosMarcasViewModel());
            Titulo = "Brewery - Administrando Marcas De Cerveza";
        }

        public void CargaModelos()
        {
            ActivateItem(new DatosClasificacionesViewModel());
            Titulo = "Brewery - Administrando Clasificaciones";
        }

        public void CargaClientes()
        {
            ActivateItem(new DatosClientesViewModel());
            Titulo = "Brewery - Supervisando Clientes";
        }

        public void CargaOrdenes()
        {
            ActivateItem(new DatosOrdenesViewModel());
            Titulo = "Brewery - Supervisando Ordenes";
        }
        public void CargaTipos()
        {
            ActivateItem(new DatosTiposViewModel());
            Titulo = "Brewery - Administrando Tipos De Cerveza";
        }
    }
}
