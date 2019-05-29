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
    public partial class Post : UserControl, INotifyPropertyChanged
    {
        public Post()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public PostDetailModelDTO PostData
        {
            get
            {
                return (PostDetailModelDTO)GetValue(PostDataProperty);
            }
            set
            {
                SetValue(PostDataProperty, value);
                NotifyPropertyChanged(nameof(PostData));
            }
        }

        public static readonly DependencyProperty PostDataProperty =
           DependencyProperty.RegisterAttached("PostData",
           typeof(PostDetailModelDTO), typeof(Post),
           new FrameworkPropertyMetadata(new PostDetailModelDTO(), OnPostDataPropertyChanged));

        public static PostDetailModelDTO GetPostData(DependencyObject dp)
        {
            return (PostDetailModelDTO)dp.GetValue(PostDataProperty);
        }

        public static void SetPostData(DependencyObject dp, PostDetailModelDTO value)
        {
            dp.SetValue(PostDataProperty, value);
        }

        private static void OnPostDataPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Post orgListItem = sender as Post;

            orgListItem.PostData = (PostDetailModelDTO)e.NewValue;
        }
    }
}
