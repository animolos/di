using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();

                //container.Bind<IUiAction>().To<SaveImageAction>();
                //container.Bind<IUiAction>().To<ImageSettingsAction>();
                //container.Bind<IUiAction>().To<PaletteSettingsAction>();
                //container.Bind<IUiAction>().To<DragonFractalAction>();
                //container.Bind<IUiAction>().To<KochFractalAction>();

                container.Bind(c => c.FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IUiAction>()
                    .BindAllInterfaces());

                container.Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>()
                    .InSingletonScope();

                container.Bind<Palette>().To<Palette>().InSingletonScope();

                container.Bind<IObjectSerializer>()
                    .To<XmlObjectSerializer>()
                    .WhenInjectedInto<SettingsManager>();

                container.Bind<IBlobStorage>()
                    .To<FileBlobStorage>()
                    .WhenInjectedInto<SettingsManager>();

                container.Bind<AppSettings>()
                    .ToMethod(context => context.Kernel.Get<SettingsManager>().Load())
                    .InSingletonScope();

                container.Bind<IImageDirectoryProvider>()
                    .ToMethod(context => context.Kernel.Get<AppSettings>());

                container.Bind<ImageSettings>()
                    .ToMethod(context => context.Kernel.Get<AppSettings>().ImageSettings)
                    .InSingletonScope();

                //container.Bind<IDragonPainterFactory>().ToFactory();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}