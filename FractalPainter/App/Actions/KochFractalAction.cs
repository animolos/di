using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly Lazy<KochPainter> painter;

        public KochFractalAction(Lazy<KochPainter> painter)
        {
            this.painter = painter;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
            => painter.Value.Paint();
    }
}