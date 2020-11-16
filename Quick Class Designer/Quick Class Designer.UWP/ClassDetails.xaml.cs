using QuickClassDesigner;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quick_Class_Designer.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClassDetails : Page
    {
        event PanelClosedHandler PanelClosed;
        private ClassData data;

        public ClassDetails()
        {
            this.InitializeComponent();
            data = new ClassData();
        }

        public void switchContentPane(int index, IGeneratable existing = null)
        {
            if (paneswitcher.switchContentPane(index)) {
                if (existing != null)
                {
                    paneswitcher.CurrentPane.initExistingModel(existing);
                }
                else
                {
                    paneswitcher.CurrentPane.initModel();
                }
            }
        }

        private void Fields_Click(object sender, RoutedEventArgs e)
        {
            switchContentPane(0);
        }

        private void Properties_Click(object sender, RoutedEventArgs e)
        {
            switchContentPane(1);
        }

        private void Funtions_Click(object sender, RoutedEventArgs e)
        {
            switchContentPane(2);
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            switchContentPane(3);
        }
    }
}
