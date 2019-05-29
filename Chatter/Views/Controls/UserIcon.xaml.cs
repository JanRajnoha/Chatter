using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chatter.Views.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserIcon : UserControl, INotifyPropertyChanged
    {
        public UserIcon()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                NotifyPropertyChanged(nameof(Text));
            }
        }

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.RegisterAttached("Text",
           typeof(string), typeof(UserIcon),
           new FrameworkPropertyMetadata(string.Empty, OnTextPropertyChanged));

        public static string GetText(DependencyObject dp)
        {
            return (string)dp.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject dp, string value)
        {
            dp.SetValue(TextProperty, value);
        }

        private static void OnTextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UserIcon orgListItem = sender as UserIcon;

            orgListItem.Text = (string)e.NewValue;
        }
    }
}
