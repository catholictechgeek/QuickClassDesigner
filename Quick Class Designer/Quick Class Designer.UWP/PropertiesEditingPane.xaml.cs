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
    public sealed partial class PropertiesEditingPane : Page, ISwitchable
    {
        private PropertyData data;

        public PropertiesEditingPane()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void serilizecheck_Unchecked(object sender, RoutedEventArgs e)
        {
            serializename.Text = string.Empty;
            serializename.ClearUndoRedoHistory();
        }

        private void serilizecheck_Unchecked_1(object sender, RoutedEventArgs e)
        {
            serilizelabel.IsChecked = false;
        }

        private void getinclude_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void setinclude_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        public void initModel()
        {
            data = new PropertyData();
        }

        public void initExistingModel(IGeneratable data)
        {
            if (!(data is PropertyData))
            {
                throw new ArgumentException("parameter must be PropertyData");
            }
            this.data = (PropertyData)data;
        }
    }
}
