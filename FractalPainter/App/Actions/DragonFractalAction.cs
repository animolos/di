using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    //public interface IDragonPainterFactory
    //{
    //    DragonPainter CreateDragonPainter(DragonSettings settings);
    //}

    public class DragonFractalAction : IUiAction
    {
        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        private readonly /*IDragonPainterFactory*/ Func<DragonSettings, DragonPainter> dragonPainterFactory;

        public DragonFractalAction(/*IDragonPainterFactory*/ Func<DragonSettings, DragonPainter> dragonPainterFactory)
            => this.dragonPainterFactory = dragonPainterFactory;

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            SettingsForm.For(dragonSettings).ShowDialog();
            var painter = dragonPainterFactory(dragonSettings);
            painter.Paint();
        }

        private static DragonSettings CreateRandomSettings() 
            => new DragonSettingsGenerator(new Random()).Generate();
    }
}