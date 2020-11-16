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
    public sealed partial class FunctionsEditingPane : Page, ISwitchable
    {
        private FunctionData data;

        public FunctionsEditingPane()
        {
            this.InitializeComponent();
        }

        public void initExistingModel(IGeneratable data)
        {
            if (!(data is FunctionData))
            {
                throw new ArgumentException("parameter must be FunctionData");
            }
            this.data = (FunctionData)data;
        }

        public void initModel()
        {
            data = new FunctionData();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ParameterData inputs = await ParameterAdd.showDialog();
            data.parameters.Add(inputs);
        }
    }
}
