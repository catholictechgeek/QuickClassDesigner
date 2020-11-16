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
using QuickClassDesigner;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quick_Class_Designer.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventsEditingPage : Page, ISwitchable
    {
        private EventData data;

        public EventsEditingPage()
        {
            this.InitializeComponent();
        }

        public void initExistingModel(IGeneratable data)
        {
            if (!(data is EventData))
            {
                throw new ArgumentException("parameter must be EventData");
            }
            this.data = (EventData)data;
        }

        public void initModel()
        {
            throw new NotImplementedException();
        }
    }
}
