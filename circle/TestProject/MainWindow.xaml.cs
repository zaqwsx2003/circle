using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TestProject
{
    public partial class MainWindow : Window
    {
   
        private RotateTransform rotateTransform;
        private double angle;
        private double rotateAngle;
        private Vector position;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.rotateTransform = new RotateTransform();

            this.image.RenderTransformOrigin = new Point(0.5, 0.5);
            this.image.RenderTransform       = this.rotateTransform;

            this.position = VisualTreeHelper.GetOffset(this.image);

            this.position.X += this.image.Width  / 2;
            this.position.Y += this.image.Height / 2;    

            this.image.MouseLeftButtonDown += image_MouseLeftButtonDown;
            this.image.MouseMove           += image_MouseMove;
            this.image.MouseLeftButtonUp   += image_MouseLeftButtonUp;
        }
      
        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this.image, CaptureMode.Element);

            Point mousePoint = e.GetPosition(this);
            
            this.angle = Math.Atan2(position.Y - mousePoint.Y, position.X - mousePoint.X) * (180 / Math.PI);

            this.rotateAngle = this.rotateTransform.Angle;
        }
  
        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePoint = e.GetPosition(this);

                double mouseAngle = Math.Atan2(position.Y - mousePoint.Y, position.X - mousePoint.X) * (180 / Math.PI);

                this.rotateTransform.Angle = this.rotateAngle + mouseAngle - this.angle;
            }
        }

        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this.image, CaptureMode.None);
        }

    }
}