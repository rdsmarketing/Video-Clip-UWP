﻿using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Input;

namespace 剪片.Control
{
    public sealed partial class ThemeButton : UserControl
    {
        //标题栏
        ApplicationViewTitleBar TitleBar = ApplicationView.GetForCurrentView().TitleBar;
       
        bool isLight//标题栏颜色
        {
            set
            {
                if (value==true)
                    TitleBar.ButtonInactiveBackgroundColor =
                        TitleBar.ButtonBackgroundColor =
                        TitleBar.InactiveBackgroundColor =
                        TitleBar.BackgroundColor = Color.FromArgb(255, 230, 230, 230);
                else
                    TitleBar.ButtonInactiveBackgroundColor =
                        TitleBar.ButtonBackgroundColor =
                        TitleBar.InactiveBackgroundColor =
                        TitleBar.BackgroundColor = Color.FromArgb(255, 31, 31, 31);
            }
        }


        //Delegate
        public delegate void ThemeChangeHandler(object sender, ElementTheme Theme);
        public event ThemeChangeHandler ThemeChange = null;


        public ThemeButton()
        {
            this.InitializeComponent();

            page.Loaded += new RoutedEventHandler(PageLoaded);
            button.Tapped += new TappedEventHandler(ButtonTapped);
        }



        #region Global：全局


        private void PageLoaded(object sender, RoutedEventArgs e)
            {
                if (App.Current.RequestedTheme == ApplicationTheme.Dark)
                {
                    ToNight.Begin();//Storyboard
                    isLight = false;//标题栏颜色
                }
                else
                {
                    ToLight.Begin();//Storyboard
                    isLight = true;//标题栏颜色
                }
            }



        private void ButtonTapped(object sender, TappedRoutedEventArgs e)
            {
                if (Window.Current.Content is FrameworkElement frameworkElement)
                {
                    if (frameworkElement.RequestedTheme == ElementTheme.Dark)
                    {
                        frameworkElement.RequestedTheme = ElementTheme.Light;
                        ToLight.Begin();//Storyboard
                        isLight = true;//标题栏颜色
                        ThemeChange?.Invoke(this, ElementTheme.Light);//Delegate
                    }
                    else
                    {
                        frameworkElement.RequestedTheme = ElementTheme.Dark;
                        ToNight.Begin();//Storyboard
                        isLight = false;//标题栏颜色
                        ThemeChange?.Invoke(this, ElementTheme.Dark);//Delegate
                    }
                }
            }



        #endregion
         
    }
}
