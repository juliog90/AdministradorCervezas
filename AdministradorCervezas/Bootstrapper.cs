using AdministradorCervezas.ViewModels;
using Caliburn.Micro;
using System.Windows;

namespace AdministradorCervezas
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<PrincipalViewModel>();
        }

    }


}
