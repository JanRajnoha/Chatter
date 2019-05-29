using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.CommentMessages;
using Chatter.BL.Messages.GroupMessages;
using Chatter.BL.Messages.PostMessages;
using Chatter.Commands;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class PostsVM : VMBase
    {
        private readonly DBPost postDBAccess;
        //private readonly DBGroup groupDBAccess;

        private ObservableCollection<PostDetailModelDTO> postsList;

        public ObservableCollection<PostDetailModelDTO> PostsList
        {
            get
            {
                return postsList;
            }

            set
            {
                postsList = value;
                OnPropertyChanged();
            }
        }

        private PostDetailModelDTO PostDTO;

        public PostDetailModelDTO NewPost
        {
            get
            {
                return PostDTO;
            }

            set
            {
                PostDTO = value;
                OnPropertyChanged();
            }
        }


        private GroupListModelDTO groupInfo;

        public GroupListModelDTO GroupInfo
        {
            get
            {
                return groupInfo;
            }

            set
            {
                groupInfo = value;
                OnPropertyChanged();
            }
        }


        private UserListModelDTO userInfo;

        public UserListModelDTO UserInfo
        {
            get
            {
                return userInfo;
            }

            set
            {
                userInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public PostsVM(DBPost dbPost, IMessenger messenger)
        {
            postDBAccess = dbPost;
            this.messenger = messenger;
            
            DeleteCommand = new RelayCommand(DeletePost);
            AddCommand = new RelayCommand(RemovePost);

            GetAllPosts();

            messenger.Register<AddedPostMessage>(AddedPost);
            messenger.Register<AddedCommentMessage>(AddedComment);
            messenger.Register<GroupChangedMessage>(GroupChanged);
        }

        private void GroupChanged(GroupChangedMessage obj)
        {
            //GroupInfo = groupDBAccess.GetGroupById(obj.Id);
            GetAllPosts();
        }

        private void RemovePost(object obj)
        {
            if (obj is PostDetailModelDTO postDetail)
            {
                postDBAccess.AddPost(postDetail);
                GetAllPosts();
            }
        }

        private void AddedComment(AddedCommentMessage obj)
        {
            GetAllPosts();
        }

        private void AddedPost(AddedPostMessage obj)
        {
            GetAllPosts();
        }

        private void GetAllPosts()
        {
            PostsList = new ObservableCollection<PostDetailModelDTO>(postDBAccess.GetPostDetailList(groupInfo.Id));
        }

        private void DeletePost(object obj)
        {
            if (obj is PostDetailModelDTO postDetail)
            {
                postDBAccess.RemovePost(postDetail.Id);
                GetAllPosts();
            }
        }
    }
}
