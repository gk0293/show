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
        /// <summary>
        /// Y轴动画附加属性。
        /// 使用方式： local:AnimationsHelper.AnimateY="{Binding BorderY}"
        /// 当 BorderY 发生变化时，会自动触发 Y 轴动画。
        /// </summary>
        public static readonly DependencyProperty AnimateYProperty =
            DependencyProperty.RegisterAttached(
                "AnimateY", 
                typeof(double),
                typeof(AnimationsHelper),
                new PropertyMetadata(0.0, OnAnimateYChanged));
        /// <summary>
        /// X轴动画附加属性。
        /// 使用方式： local:AnimationsHelper.AnimateY="{Binding BorderX}"
        /// 当 BorderX 发生变化时，会自动触发 Y 轴动画。
        /// </summary>
        public static readonly DependencyProperty AnimateXProperty =
            DependencyProperty.RegisterAttached(
                "AnimateX",
                typeof(double),
                typeof(AnimationsHelper),
                new PropertyMetadata(0.0, OnAnimateXChanged));

        /// <summary>
        /// 透明度动画属性
        /// 使用方法 local:AnimationsHelper.AnimateOpacity="{Binding ButtonCheckShow}"
        /// 当AnimateOpacity发生变化，触发透明度动画
        /// </summary>
        public static readonly DependencyProperty AnimateOpacityProperty =
         DependencyProperty.RegisterAttached(
             "AnimateOpacity",
             typeof(double),
             typeof(AnimationsHelper),
             new PropertyMetadata(1.0, OnAnimateOpacityChanged));



        /// <summary>
        /// 获取  属性。
        /// WPF 附加属性必须提供 Get 方法。
        /// </summary>
        public static double GetAnimateY(DependencyObject obj) 
        {
            return (double)obj.GetValue(AnimateYProperty);
        }

        public static double GetAnimateX(DependencyObject obj)
        {
            return (double)obj.GetValue(AnimateXProperty);
        }

        public static double GetAnimateOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(AnimateOpacityProperty);
        }


        /// <summary>
        /// 设置  属性。
        /// WPF 附加属性必须提供 Set 方法。
        /// </summary>
        public static void SetAnimateY(DependencyObject obj, double value)
        {
            obj.SetValue(AnimateYProperty, value);
        }

        public static void SetAnimateX(DependencyObject obj, double value)
        {
            obj.SetValue(AnimateXProperty, value);
        }

        public static void SetAnimateOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(AnimateOpacityProperty, value);
        }

        /// <summary>
        /// AnimateY 属性发生变化时触发。
        /// </summary>
        private static void OnAnimateYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 判断当前附加属性是不是应用在 FrameworkElement 上
            if (d is FrameworkElement element)
            {
                // 获取当前控件的 RenderTransform
                var transform = element.RenderTransform as TranslateTransform;
                // 如果控件没有创建一个
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    element.RenderTransform = transform;
                }

                // 创建 Y 轴动画
                var animation = new DoubleAnimation
                {
                    // 动画目标值
                    To = (double)e.NewValue,
                    //持续时间
                    Duration = TimeSpan.FromSeconds(0.3),
                    // 动画缓动函数，贝塞尔曲线
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                //将动画应用到 TranslateTransform 的 Y 属性
                transform.BeginAnimation(TranslateTransform.YProperty, animation);
            }
        }
        /// <summary>
        /// AnimateX 属性发生变化时触发。
        /// </summary>
        private static void OnAnimateXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 判断当前附加属性是不是应用在 FrameworkElement 上
            if (d is FrameworkElement element)
            {
                // 获取当前控件的 RenderTransform
                var transform = element.RenderTransform as TranslateTransform;
                // 如果控件没有创建一个
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    element.RenderTransform = transform;
                }

                // 创建 Y 轴动画
                var animation = new DoubleAnimation
                {
                    // 动画目标值
                    To = (double)e.NewValue,
                    //持续时间
                    Duration = TimeSpan.FromSeconds(0.3),
                    // 动画缓动函数，贝塞尔曲线
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                //将动画应用到 TranslateTransform 的 Y 属性
                transform.BeginAnimation(TranslateTransform.XProperty, animation);
            }
        }
        /// <summary>
        /// Opacity属性发生变化时触发。
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>

        private static void OnAnimateOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 判断当前附加属性是不是应用在 FrameworkElement 上
            if (d is UIElement element)
            {
                // 创建 透明度动画
                var animation = new DoubleAnimation
                {
                    // 动画目标值
                    To = (double)e.NewValue,
                    //持续时间
                    Duration = TimeSpan.FromMilliseconds(300),
                    // 动画缓动函数，贝塞尔曲线
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                //将动画应用到 TranslateTransform 的 Y 属性
                element.BeginAnimation(UIElement.OpacityProperty, animation);
            }
        }
    }
}
