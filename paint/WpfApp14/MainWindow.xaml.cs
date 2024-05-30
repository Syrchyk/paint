using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Polyline line;
        private string image;
        private void paint_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(image == "penG")
            {
                line = new Polyline();
                line.StrokeThickness = sliderSize.Value;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Color color = (colorB1.Background as SolidColorBrush).Color;
                    line.Stroke = new SolidColorBrush(Color.FromArgb((byte)alpha.Value, color.R, color.G, color.B));
                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    Color color = (colorB2.Background as SolidColorBrush).Color;
                    line.Stroke = new SolidColorBrush(Color.FromArgb((byte)alpha.Value, color.R, color.G, color.B));
                }
                paint.Children.Add(line);
            }
            if (image == "gumkaG")
            {
                line = new Polyline();
                line.StrokeThickness = sliderSize.Value;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    line.Stroke = colorB2.Background;
                }
                paint.Children.Add(line);
            }
            if (image == "pipG")
            {
                if (e.Source is Polyline)
                {
                    colorB1.Background = (e.Source as Polyline).Stroke;
                }
            }
            if (image == "bucketG")
            {
                if (e.Source is Polyline)
                {
                    (e.Source as Polyline).Fill =  colorB1.Background;
                }
            }
        }
        private void paint_MouseMove(object sender, MouseEventArgs e)
        {
            if (image == "penG" || image == "gumkaG")
            {
                if (line != null)
                {
                    if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                    {
                        Point point = e.GetPosition(paint);
                        line.Points.Add(point);
                    }
                }
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            image = ((Grid)((Image)sender).Parent).Name;
            switch (((Grid)((Image)sender).Parent).Name)
            {
                case "penG": penG.Background = Brushes.Blue; gumkaG.Background = Brushes.Transparent; bucketG.Background = Brushes.Transparent; pipG.Background = Brushes.Transparent; break;
                case "gumkaG": penG.Background = Brushes.Transparent; gumkaG.Background = Brushes.Blue; bucketG.Background = Brushes.Transparent; pipG.Background = Brushes.Transparent; break;
                case "bucketG": penG.Background = Brushes.Transparent; gumkaG.Background = Brushes.Transparent; bucketG.Background = Brushes.Blue; pipG.Background = Brushes.Transparent; break;
                case "pipG": penG.Background = Brushes.Transparent; gumkaG.Background = Brushes.Transparent; bucketG.Background = Brushes.Transparent; pipG.Background = Brushes.Blue; break;
            }
        }

        private void butSize_Click(object sender, RoutedEventArgs e)
        {
            if(popUpSize.IsOpen)
            {
                popUpSize.IsOpen = false;
            }
            else
            {
                popUpSize.IsOpen = true;
            }
        }

        private void ButtonClr_Click(object sender, RoutedEventArgs e)
        {
            if (nameOfB == colorB1.Name)
            {
                colorB1.Background = (sender as Button).Background;
            }

            else if (nameOfB == colorB2.Name)
            {
                colorB2.Background = (sender as Button).Background;
            }
            gridColors.IsEnabled = false;
        }

        private void RGBB_Click(object sender, RoutedEventArgs e)
        {
            if (popUpColor.IsOpen)
            {
                gridColors.IsEnabled = false;
                if (nameOfB == colorB1.Name)
                {
                    colorB1.Background = new SolidColorBrush(Color.FromArgb((byte)alpha.Value, (byte)sliderCR.Value, (byte)sliderCG.Value, (byte)sliderCB.Value));
                }
                else if (nameOfB == colorB2.Name)
                {
                    colorB2.Background = new SolidColorBrush(Color.FromArgb((byte)alpha.Value, (byte)sliderCR.Value, (byte)sliderCG.Value, (byte)sliderCB.Value));
                }
                popUpColor.IsOpen = false;
            }
            else
            {
                popUpColor.IsOpen = true;
            }
        }

        private string nameOfB;

        private void colorB_Click(object sender, RoutedEventArgs e)
        {
            nameOfB = ((Button)sender).Name;
            gridColors.IsEnabled = true;
        }
    }
}
