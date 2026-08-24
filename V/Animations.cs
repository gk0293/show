using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace show
{
   public static class AnimationsHelper
    {
        public static readonly DependencyProperty AnimateYProperty =
            DependencyProperty.RegisterAttached(
                "AnimateY", 
                typeof(double),
                typeof(AnimationsHelper),
                new PropertyMetadata(0.0, OnAnimateYChanged));
        public static double GetAnimateY(DependencyObject obj) 
        {
            return (double)obj.GetValue(AnimateYProperty);
        }

        public static void SetAnimateY(DependencyObject obj, double value)
        {
            obj.SetValue(AnimateYProperty, value);
        }

        private static void OnAnimateYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                var transform = element.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    element.RenderTransform = transform;
                }
           

            var animation = new DoubleAnimation
            {
                To = (double)e.NewValue,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

                transform.BeginAnimation(TranslateTransform.YProperty, animation);
        }
        }
    }
}
