using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace show
{
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        private string textshows = "null";

        [ObservableProperty]
        private bool isMouseHover;

        [ObservableProperty]
        private bool checkBoreder;

        [ObservableProperty]
        private bool isMouseHoverBorder;

        [ObservableProperty]
        private double borderY = 1;

        private bool CanMove() => IsMouseHover;


        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        //border悬浮判断,移入或点击
        [RelayCommand]
        private async Task BorderMove()
        {
            // 取消之前的延迟操作
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                if (IsMouseHover || IsMouseHoverBorder || CheckBoreder)
                {
                    BorderY = -120;
                }
                else 
                {
                    // 延迟隐藏悬浮窗
                    await Task.Delay(1000, cancellationTokenSource.Token);
                    // 再次检查状态（防止竞态条件）
                    if (!IsMouseHover && !IsMouseHoverBorder && !CheckBoreder)
                    {
                        BorderY = 1;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                
            }
        }


        //border文本
        [RelayCommand]
        private void Textshow()
        {
            if (IsMouseHover)
            {
                Textshows = "CheckBox Hover";
            }
            else if (IsMouseHoverBorder)
            {
                Textshows = "Border Hover";
            }
            else
            {
                Textshows = "No Hover";
            }

        }


        //鼠标悬浮判定,文本逻辑
        partial void OnIsMouseHoverChanged(bool value)
        {
            Textshow();
            BorderMove();
        }

        partial void OnIsMouseHoverBorderChanged(bool value) 
        {
            Textshow();
            BorderMove();
        }
    }
}