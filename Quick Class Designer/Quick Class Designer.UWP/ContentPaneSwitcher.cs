using QuickClassDesigner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Quick_Class_Designer.UWP
{
    public class ContentPaneSwitcher:Pivot
    {
        /*
        public static readonly DependencyProperty BaseDataSourceProperty =
    DependencyProperty.Register("BaseDataSource", typeof(IGeneratable), typeof(ContentPaneSwitcher), null);

        public static readonly DependencyProperty CurrentPaneProperty =
    DependencyProperty.Register("CurrentPane", typeof(ISwitchable), typeof(ContentPaneSwitcher), null);
        */

        public ContentPaneSwitcher()
        {

        }

        /*
        public IGeneratable BaseDataSource
        {
            get { return (IGeneratable)GetValue(BaseDataSourceProperty); }
            set { SetValue(BaseDataSourceProperty, value); }
        }

        public ISwitchable CurrentPane
        {
            get { return (ISwitchable)GetValue(CurrentPaneProperty); }
            set { SetValue(CurrentPaneProperty, value); }
        }
        */

        public ISwitchable CurrentPane
        {
            get
            {
                return (this.Items[this.SelectedIndex] as PivotItem).Content as ISwitchable;
            }
        }

        public bool switchContentPane(int index)
        {
            if (index > this.Items.Count || index < 0)
            {
                return false;
            }
            this.SelectedIndex = index;
            return true;
        }
    }
}
