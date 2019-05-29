using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.UserMessages;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class PostVM : VMBase
    {
        private DBPost dbPost;
        private DBComment dbComment;

        private Visibility showReply = Visibility.Collapsed;

        public Visibility ShowReply
        {
            get => showReply;
            set
            {
                showReply = value;
                OnPropertyChanged();
            }
        }

        private PostDetailModelDTO post;

        public PostDetailModelDTO Post
        {
            get => post;
            set
            {
                post = value;
                OnPropertyChanged();
            }
        }

        private UserDetailModelDTO user;

        public UserDetailModelDTO User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private CommentDetailModelDTO newComment = new CommentDetailModelDTO();

        public CommentDetailModelDTO NewComment
        {
            get => newComment;
            set
            {
                newComment = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CommentDetailModelDTO> commentsList = new ObservableCollection<CommentDetailModelDTO>();

        public ObservableCollection<CommentDetailModelDTO> CommentsList
        {
            get => commentsList;
            set
            {
                commentsList = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddReplyCommand { get; set; }
        public ICommand AddCommentCommand { get; set; }
        public ICommand ListViewInitCommand { get; set; }

        public PostVM(Messenger messenger, DBPost dbPost, DBComment dbComment)
        {
            this.messenger = messenger;
            this.dbPost = dbPost;
            this.dbComment = dbComment;

            AddReplyCommand = new RelayCommand(() => ShowReply = Visibility.Visible);
            AddCommentCommand = new RelayCommand(AddComment);
            ListViewInitCommand = new RelayCommand(GetPost);

            messenger.Register<UserDataMessage>(UserData);
            messenger.Register<UserChangedMessage>((x) => messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = RequestUserDataType.CurrentUser,
                Sender = ToString()
            }));

            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = RequestUserDataType.CurrentUser,
                Sender = ToString()
            });
        }

        private void GetPost(object obj)
        {
            if (obj is PostListModelDTO post)
            {
                Post = dbPost.GetPostById(post.Id);
                NewComment.Post = Post;

                GetComments();
            }
        }

        private void GetComments()
        {
            var comments = ((ObservableCollection<CommentDetailModelDTO>)dbComment.GetCommentsByPostID(Post.Id)).OrderBy(x => x.DateTime);

            foreach (var comment in comments)
                if (!CommentsList.Select(x => x.Id).Contains(comment.Id))
                    CommentsList.Add(comment);
        }

        private void AddComment(object obj)
        {
            dbComment.AddComment(NewComment);

            GetComments();

            NewComment = new CommentDetailModelDTO
            {
                Post = Post,
                User = User
            };

            ShowReply = Visibility.Collapsed;
        }

        private void UserData(UserDataMessage obj)
        {
            if (obj.Receiver == ToString())
            {
                User = obj.UserData;
                NewComment.User = User;
            }
        }
    }
}