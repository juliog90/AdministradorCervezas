using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class PrincipalViewModel : Conductor<object>
    {
        private Beer _cervezaActual;

        public Beer CervezaActual
        {
            get { return _cervezaActual; }
            set
            {
                _cervezaActual = value;
                NotifyOfPropertyChange(() => CervezaActual);
            }
        }

        public void CargaCervezas()
        {
            ActivateItem(new DatosCervezasViewModel());
        }

        public void CargaMarcas()
        {
            ActivateItem(new DatosMarcasViewModel());
        }

        public void CargaModelos()
        {
            ActivateItem(new DatosModelosViewModel());
        }

        public void CargaClientes()
        {
            ActivateItem(new DatosClientesViewModel());
        }

        public void CargaOrdenes()
        {
            ActivateItem(new DatosOrdenesViewModel());
        }
    }
}
