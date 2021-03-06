﻿
using IndoorMap.ViewModels;
using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IndoorMap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AtlasPage : MVVMPage
    {
        public AtlasPage()
        {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as AtlasPage_Model;
            });
            StrongTypeViewModel = this.ViewModel as AtlasPage_Model;
            this.SizeChanged += AtlasPage_SizeChanged;
        }

        private void AtlasPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ApplicationView.GetForCurrentView().VisibleBounds.Width;
       
            if (this.ActualWidth <= 500)
            {
                this.gridAtlas.Width = width;
                this.gridAtlas.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                this.gridAtlas.HorizontalAlignment = HorizontalAlignment.Stretch;
            }
        }

        public AtlasPage_Model StrongTypeViewModel
        {
            get { return (AtlasPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(AtlasPage_Model), typeof(AtlasPage), new PropertyMetadata(null));
        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        } 
    }
}
