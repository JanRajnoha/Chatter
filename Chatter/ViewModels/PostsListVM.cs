using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.GroupMessages;
using Chatter.BL.Messages.PostMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class PostsListVM : VMBase
    {
        private readonly DBUser dbUser;
        private readonly DBOrganization dbOrganization;
        private readonly DBGroup dbGroup;
        private readonly DBPost dbPost;
        private readonly DBComment dbComment;

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


        private GroupDetailModelDTO group;

        public GroupDetailModelDTO Group
        {
            get { return group; }
            set
            {
                group = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PostDetailModelDTO> posts = new ObservableCollection<PostDetailModelDTO>();

        public ObservableCollection<PostDetailModelDTO> Posts
        {
            get { return posts; }
            set
            {
                posts = value;
                OnPropertyChanged();
            }
        }

        private ListView postList;

        public ListView PostList
        {
            get => postList;
            set
            {
                postList = value;
                OnPropertyChanged();
            }
        }

        Timer refresher = new Timer();

        public ICommand ListViewInitCommand { get; set; }

        public PostsListVM(Messenger messenger, DBUser dbUser, DBOrganization dbOrganization, DBGroup dbGroup, DBPost dbPost, DBComment dbComment)
        {
            this.messenger = messenger;
            this.dbUser = dbUser;
            this.dbOrganization = dbOrganization;
            this.dbGroup = dbGroup;
            this.dbPost = dbPost;
            this.dbComment = dbComment;

            refresher.Elapsed += new ElapsedEventHandler(Refresh);
            refresher.Interval = 10000; // 1000 ms is one second
            refresher.Start();

            ListViewInitCommand = new RelayCommand(ListViewInit);

            messenger.Register<GroupChangedMessage>(GroupChanged);
            messenger.Register<UserChangedMessage>(UserChanged);
            messenger.Register<AddedPostMessage>(AddedPost);
        }

        private void ListViewInit(object obj)
        {
            if (obj is ListView pstList)
                PostList = pstList;
        }

        private void Refresh(object sender, ElapsedEventArgs e)
        {
            AddedPost(null);
        }

        private void AddedPost(AddedPostMessage obj)
        {
            if (Group != null)
            {
                var newPosts = new ObservableCollection<PostDetailModelDTO>(dbGroup.GetPosts(Group.Id)?.OrderBy(x => x.DateTime));

                foreach (var post in newPosts)
                    if (!Posts.Select(x => x.Id).Contains(post.Id))
                        Posts.Add(post);

                try
                {
                    if (Posts.Count != 0)
                        PostList.ScrollIntoView(Posts[Posts.Count - 1]);
                }
                catch { }
            }
        }

        private void UserChanged(UserChangedMessage obj)
        {
            if (obj.UserChangedType == UserChangedType.LogIn)
                User = dbUser.GetUserById(obj.Id);
        }

        private void GroupChanged(GroupChangedMessage obj)
        {
            Group = dbGroup.GetGroupById(obj.Id);

            Posts.Clear();

            AddedPost(null);
        }
    }
}
