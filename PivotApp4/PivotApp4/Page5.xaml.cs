using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PivotApp4
{
    public partial class Page5 : PhoneApplicationPage
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void PlayMedia(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void PauseMedia(object sender, RoutedEventArgs e)
        {
            media.Pause();
        }
    }
}