using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using QuickClassDesigner;
using System.Threading.Tasks;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quick_Class_Designer.UWP
{
    public sealed partial class ParameterAdd : ContentDialog
    {
        private TaskCompletionSource<ParameterData> source;
        private ParameterData data;
        private ParameterAdd()
        {
            this.InitializeComponent();
            data = new ParameterData();
            source = new TaskCompletionSource<ParameterData>();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            source.SetResult(data);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            source.SetResult(null);
        }

        public static Task<ParameterData> showDialog()
        {
            ParameterAdd wizard = new ParameterAdd();
            wizard.ShowAsync();
            return wizard.source.Task;

        }
    }
}
