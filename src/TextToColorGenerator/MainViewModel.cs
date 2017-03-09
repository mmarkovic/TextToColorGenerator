namespace TextToColorGenerator
{
    using System;
    using System.ComponentModel;
    using System.Windows.Media;

    using TextToColorGenerator.Annotations;

    public class MainViewModel : INotifyPropertyChanged
    {
        private string textToConvert;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            this.textToConvert = "sample text";
        }

        public string TextToConvert
        {
            get
            {
                return this.textToConvert;
            }

            set
            {
                if (value != this.textToConvert)
                {
                    this.textToConvert = value;
                    OnPropertyChanged("TextToConvert");
                    OnPropertyChanged("ForegroundColor");
                    OnPropertyChanged("BackgroundColor");
                }
            }
        }

        public SolidColorBrush ForegroundColor
        {
            get
            {
                const int Threshold = 105;
                Color bg = this.BackgroundColor.Color;
                int delta = Convert.ToInt32(bg.R * 0.299 + bg.G * 0.587 + bg.B * 0.114);

                Color foreColor = 255 - delta < Threshold ? Colors.Black : Colors.White;
                return new SolidColorBrush(foreColor);
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get
            {
                int hash = TextToConvert.GetHashCode();
                int r = (hash & 0xFF0000) >> 16;
                int g = (hash & 0x00FF00) >> 8;
                int b = hash & 0x0000FF;

                return new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));
            }
        }
    }
}