using Chatter.Classes;
using Chatter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Chatter.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignPage : Window
    {
        public SignPage()
        {
            InitializeComponent();

            //this.DataContext = new MainPageVM(VMLocator.mess);
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) => ((SignPageVM)DataContext).SetWindow(this);

        //private void Chat_Win(object sender, RoutedEventArgs e)
        //{
        //    ChatWindow OP = new ChatWindow();
        //    OP.Show();
        //    this.Close();
        //}    
    }
}
