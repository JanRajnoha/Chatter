using Chatter.BL.DTO.Post;
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
    public partial class Post : UserControl
    {
        public Post()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public PostDetailModelDTO Data
        {
            get
            {
                return (PostDetailModelDTO)GetValue(DataProperty);
            }
            set
            {
                SetValue(DataProperty, value);
                NotifyPropertyChanged(nameof(Data));
            }
        }

        public static readonly DependencyProperty DataProperty =
           DependencyProperty.RegisterAttached("Data",
           typeof(PostDetailModelDTO), typeof(Post),
           new FrameworkPropertyMetadata(new PostDetailModelDTO(), OnDataPropertyChanged));

        public static PostDetailModelDTO GetData(DependencyObject dp)
        {
            return (PostDetailModelDTO)dp.GetValue(DataProperty);
        }

        public static void SetData(DependencyObject dp, PostDetailModelDTO value)
        {
            dp.SetValue(DataProperty, value);
        }

        private static void OnDataPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Post orgListItem = sender as Post;

            orgListItem.Data = (PostDetailModelDTO)e.NewValue;
        }
    }
}
