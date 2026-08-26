using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
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


    public class WindowShadowTrackingBehavior : Behavior<FrameworkElement>
    {
        private Window _parentWindow;
        // 设定一个最大影响半径，超过这个距离阴影偏移量不再增加
        private const double MaxInfluenceRadius = 300;
        private const double MaxShadowDepth = 10;

        protected override void OnAttached()
        {
            base.OnAttached();

            // 1. 获取该 Border 所在的 Window
            _parentWindow = Window.GetWindow(AssociatedObject);

            if (_parentWindow != null)
            {
                // 2. 监听 Window 的 MouseMove 事件
                // 使用 AddHandler 并设置 handledEventsToo = true，防止事件被窗口内的其他控件(如TextBox)拦截
                _parentWindow.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(OnWindowMouseMove), true);
                _parentWindow.MouseLeave += OnWindowMouseLeave;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (_parentWindow != null)
            {
                _parentWindow.RemoveHandler(UIElement.MouseMoveEvent, new MouseEventHandler(OnWindowMouseMove));
                _parentWindow.MouseLeave -= OnWindowMouseLeave;
            }
        }

        private void OnWindowMouseMove(object sender, MouseEventArgs e)
        {
            var shadow = AssociatedObject.Effect as DropShadowEffect;
            if (shadow == null) return;

            // 3. 核心魔法：获取鼠标相对于 Border 的坐标
            // 即使事件是在 Window 触发的，WPF 的 GetPosition 也能自动帮你算出相对于 Border 的坐标！
            Point mousePos = e.GetPosition(AssociatedObject);

            double centerX = AssociatedObject.ActualWidth / 2;
            double centerY = AssociatedObject.ActualHeight / 2;

            // 4. 计算相对偏移
            double dx = mousePos.X - centerX;
            double dy = mousePos.Y - centerY;

            // 5. 计算角度 (Direction)
            double angleRad = Math.Atan2(dy, -dx);
            shadow.Direction = (angleRad * 180 / Math.PI + 360) % 360;

            // 6. 计算深度 (ShadowDepth)
            double distance = Math.Sqrt(dx * dx + dy * dy);

            // 注意：因为鼠标在整个窗口移动，距离可能会非常大。
            // 我们需要将距离映射到 0 ~ MaxShadowDepth 之间
            double depth = (distance / MaxInfluenceRadius) * MaxShadowDepth;
            shadow.ShadowDepth = Math.Min(depth, MaxShadowDepth); // 限制最大偏移量
        }

        private void OnWindowMouseLeave(object sender, MouseEventArgs e)
        {
            // 鼠标离开窗口时，阴影恢复居中
            if (AssociatedObject.Effect is DropShadowEffect shadow)
            {
                shadow.ShadowDepth = 0;
            }
        }
    }
}
