using System.Windows.Media;
using ReactiveUI;

namespace DC.RxExamples.ColorSlider
{
    public class ViewModel : ReactiveObject
    {
        #region Red

        private byte red;

        public byte Red
        {
            get { return red; }
            set { this.RaiseAndSetIfChanged(ref red, value); }
        }

        readonly ObservableAsPropertyHelper<Color> redColor;
        public Color RedColor => redColor.Value;

        #endregion

        #region Green

        private byte green;

        public byte Green
        {
            get { return green; }
            set { this.RaiseAndSetIfChanged(ref green, value); }
        }

        readonly ObservableAsPropertyHelper<Color> greenColor;
        public Color GreenColor => greenColor.Value;
        #endregion

        #region Blue

        private byte blue;

        public byte Blue
        {
            get { return blue; }
            set { this.RaiseAndSetIfChanged(ref blue, value); }
        }

        readonly ObservableAsPropertyHelper<Color> blueColor;
        public Color BlueColor => blueColor.Value;
        #endregion

        #region FinalColor

        readonly ObservableAsPropertyHelper<Color> finalColor;
        public Color FinalColor => finalColor.Value;

        #endregion

        /// <summary>
        /// Any changes in the values of Red, Green or Blue will cause a change in the FinalColor,
        /// RedColor, GreenColor and BlueColor.
        /// 
        /// The Color properties use the ObservableAsPropertyHelper<> to watch for changes in the color values
        /// then update there Color, which the SolidColorBrush.Color properties are bound to.
        /// 
        /// The individual color values (Red, Green, Blue) bind to the View's sliders via the
        /// RaiseAndSetIfChanged() method.
        /// </summary>
        public ViewModel()
        {
            finalColor = this
                .WhenAny(x => x.Red, x => x.Green, x => x.Blue,
                (r, g, b) => Color.FromRgb(r.Value, g.Value, b.Value)).ToProperty(this, x => x.FinalColor);

            redColor = this.WhenAny(x => x.Red, (r) => Color.FromRgb(r.Value, 0, 0))
                .ToProperty(this, x => x.RedColor);

            greenColor = this.WhenAny(x => x.Green, (g) => Color.FromRgb(0, g.Value, 0))
                .ToProperty(this, x => x.GreenColor);

            blueColor = this.WhenAny(x => x.Blue, (b) => Color.FromRgb(0, 0, b.Value))
                .ToProperty(this, x => x.BlueColor);

            Red = 100;
            Green = 150;
            Blue = 200;
        }
    }
}