using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Post;
using Chatter.BL.Messages.GroupMessages;
using Chatter.BL.Messages.PostMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class NewPostVM : VMBase
    {
        private readonly DBPost dbPost;
        private readonly DBGroup dbGroup;
        private readonly DBUser dbUser;
        private PostDetailModelDTO post = new PostDetailModelDTO();
        private readonly string NewPostTemplate = @"{\rtf1\ansi\ansicpg1252\uc1\htmautsp\deff2{\fonttbl{\f0\fcharset0 Times New Roman;}{\f2\fcharset0 Segoe UI;}}{\colortbl\red0\green0\blue0;\red255\green255\blue255;}\loch\hich\dbch\pard\plain\ltrpar\itap0{\lang1033\fs18\f2\cf0 \cf0\ql{\f2 \li0\ri0\sa0\sb0\fi0\ql\par}
}
}";

        private Visibility showError = Visibility.Collapsed;

        public Visibility ShowError
        {
            get => showError;
            set
            {
                showError = value;
                OnPropertyChanged();
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        public PostDetailModelDTO Post
        {
            get => post;
            set
            {
                post = value;
                OnPropertyChanged();
            }
        }

        private GroupDetailModelDTO currentGroup;

        public GroupDetailModelDTO CurrentGroup
        {
            get => currentGroup;
            set
            {
                currentGroup = value;
                OnPropertyChanged();
            }
        }


        public ICommand NewPostCommand { get; set; }

        public NewPostVM(Messenger messenger, DBPost dbPost, DBGroup dbGroup, DBUser dbUser)
        {
            this.messenger = messenger;
            this.dbPost = dbPost;
            this.dbGroup = dbGroup;
            this.dbUser = dbUser;

            NewPostCommand = new RelayCommand(AddNewPost);

            messenger.Register<UserDataMessage>(RequestedUserData);
            messenger.Register<NewPostFailMessage>(NewPostFail);
            messenger.Register<GroupDataMessage>(RequestedGroupData);
        }

        private void RequestedGroupData(GroupDataMessage obj)
        {
            if (obj.Receiver == ToString())
            {
                CurrentGroup = obj.GroupData;

                CurrentGroup.Posts.Add(Post);

                //dbPost.AddPost(Post);
                ShowError = Visibility.Collapsed;
                dbGroup.EditGroup(CurrentGroup);
                Post = new PostDetailModelDTO();

                messenger.Send(new AddedPostMessage());
            }
        }

        private void NewPostFail(NewPostFailMessage obj)
        {
            ShowError = Visibility.Visible;
            ErrorMessage = obj.Message;
        }

        private void AddNewPost(object obj)
        {
            if (Post.Content == NewPostTemplate || Post.Content == "")
                messenger.Send(new NewPostFailMessage() { Message = "Post content couldn't be empty." });
            else
                messenger.Send(new RequestUserDataMessage()
                {
                    RequestUserDataType = RequestUserDataType.CurrentUser,
                    Sender = ToString()
                });
        }

        private void RequestedUserData(UserDataMessage obj)
        {
            if (obj.Receiver == ToString())
            {
                Post.User = dbUser.GetUserById(obj.UserData.Id);

                messenger.Send(new RequestGroupDataMessage()
                {
                    Sender = ToString(),
                    RequestGroupDataType = RequestGroupDataType.CurrentGroup
                });
            }
        }
    }
}
