using Chatter.BL.DTO.User;
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
    public partial class UserList : UserControl, INotifyPropertyChanged
    {
        public UserList()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public UserListModelDTO Data
        {
            get
            {
                return (UserListModelDTO)GetValue(DataProperty);
            }
            set
            {
                SetValue(DataProperty, value);
                NotifyPropertyChanged(nameof(Data));
            }
        }

        public static readonly DependencyProperty DataProperty =
           DependencyProperty.RegisterAttached("Data",
           typeof(UserListModelDTO), typeof(UserList),
           new FrameworkPropertyMetadata(new UserListModelDTO(), OnDataPropertyChanged));

        public static UserListModelDTO GetData(DependencyObject dp)
        {
            return (UserListModelDTO)dp.GetValue(DataProperty);
        }

        public static void SetData(DependencyObject dp, UserListModelDTO value)
        {
            dp.SetValue(DataProperty, value);
        }

        private static void OnDataPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UserList userListItem = sender as UserList;

            userListItem.Data = (UserListModelDTO)e.NewValue;
        }
    }
}
