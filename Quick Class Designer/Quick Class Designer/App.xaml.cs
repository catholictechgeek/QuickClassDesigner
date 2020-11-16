using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Quick_Class_Designer.Services;
using Quick_Class_Designer.Views;

namespace Quick_Class_Designer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
