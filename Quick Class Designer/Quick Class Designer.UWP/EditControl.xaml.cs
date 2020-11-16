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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Quick_Class_Designer.UWP
{
    public sealed partial class EditControl : UserControl
    {
        private Visibility editing;
        private string oldvalue;
        public EditControl()
        {
            this.InitializeComponent();
            oldvalue = "";
        }

        public Visibility isEditing
        {
            get
            {
                return editing;
            }
            set
            {
                editing = value;
            }
        }

        public void switchModes()
        {
            if (isEditing == Visibility.Visible)
            {
                isEditing = Visibility.Collapsed;
            }
            else
            {
                isEditing = Visibility.Visible;
            }
        }

        private void validateType()
        {
            try
            {
                Type t = Type.GetType(null, true);
            }
            catch
            {
                typebox.Text = oldvalue;
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            //sender.Text = args.SelectedItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            validateType();
            switchModes();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void AutoSuggestBox_FocusDisengaged(Control sender, FocusDisengagedEventArgs args)
        {
            validateType();
        }
    }
}
