using MvvmCross.Core.ViewModels;
using Zakupy.Core.ViewModel;
using MvvmCross.Platform;
using Zakupy.Core.DataBase;

namespace Zakupy.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.ConstructAndRegisterSingleton<IZakupyDb, SqliteDb>();

            RegisterAppStart<ListaZakupowViewModel>();
        }
    }
}
