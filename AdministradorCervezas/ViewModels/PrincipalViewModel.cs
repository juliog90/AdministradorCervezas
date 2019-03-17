using Caliburn.Micro;

namespace AdministradorCervezas.ViewModels
{
    class PrincipalViewModel : Conductor<object>
    {
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
        public void CargaTipos()
        {
            ActivateItem(new DatosTiposViewModel());
        }
    }
}
