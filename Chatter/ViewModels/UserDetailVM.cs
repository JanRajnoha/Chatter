using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DBAccess.Interfaces;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.LogMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.Classes;
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
    public class UserDetailVM : VMBase
    {
        private DBUser dbUser;
        private readonly DBPost dbPost;
        private readonly DBComment dbCOmment;
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

        private IEnumerable<Gender> genderSource = System.Enum.GetValues(typeof(Gender)).Cast<Gender>();

        public IEnumerable<Gender> GenderSource
        {
            get => genderSource;
            set
            {
                genderSource = value;
                OnPropertyChanged();
            }
        }

        private EditUserHelpClass pass = new EditUserHelpClass();

        public EditUserHelpClass Pass
        {
            get => pass;
            set
            {
                pass = value;
                OnPropertyChanged();
            }
        }

        private bool showDelete = false;

        public bool ShowDelete
        {
            get => showDelete;
            set
            {
                showDelete = value;
                DeleteAcc = showDelete ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged();
            }
        }

        private Visibility deleteAcc = Visibility.Collapsed;

        public Visibility DeleteAcc
        {
            get => deleteAcc;
            set
            {
                deleteAcc = value;
                OnPropertyChanged();
            }
        }

        private UserDetailModelDTO userDetail;

        public UserDetailModelDTO UserDetail
        {
            get
            {
                return userDetail;
            }

            set
            {
                userDetail = value;
                OnPropertyChanged();
            }
        }

        private bool editEnabled;

        public bool EditEnabled
        {
            get => editEnabled;
            set
            {
                editEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public UserDetailVM()
        {

        }

        public UserDetailVM(Messenger messenger, DBUser dbUser, DBPost dbPost, DBComment dbComment) : this()
        {
            this.messenger = messenger;
            this.dbUser = dbUser;
            this.dbPost = dbPost;
            this.dbCOmment = dbComment;

            DeleteCommand = new RelayCommand(DeleteUser);
            LogOutCommand = new RelayCommand(() => messenger.Send(new SignOutMessage()));
            EditCommand = new RelayCommand(Edit);

            messenger.Register<UserChangedMessage>(UserChanged);
            messenger.Register<UserDataMessage>(CurrentUser);
            messenger.Register<UserEditFailMessage>(EditUserBadPass);
        }

        private void EditUserBadPass(UserEditFailMessage obj)
        {
            ShowError = Visibility.Visible;
            ErrorMessage = obj.Message;
        }

        private void Edit(object obj)
        {
            var existUser = dbUser.GetUserByEmail(UserDetail.Email);

            if (existUser != null)
                if (existUser.Id != UserDetail.Id)
                    messenger.Send(new UserEditFailMessage() { Message = "User with this email is existing" });
                else
                {
                    dbUser.EditUser(UserDetail);
                    ShowError = Visibility.Collapsed;
                }
            else
            {
                dbUser.EditUser(UserDetail);
                ShowError = Visibility.Collapsed;
            }

            if (Pass.OldPass != string.Empty)
            {
                if (Pass.NewPass != Pass.NewPassAgain || Pass.NewPass.Length < 6 || Pass.NewPass == Pass.OldPass)
                {
                    if (Pass.NewPass != Pass.NewPassAgain)
                        messenger.Send(new UserEditFailMessage() { Message = "Passwords are not same" });
                    else if (Pass.NewPass.Length < 6)
                        messenger.Send(new UserEditFailMessage() { Message = "New password is too short" });
                    else if (Pass.NewPass == Pass.OldPass)
                        messenger.Send(new UserEditFailMessage() { Message = "Password is same as old" });
                }
                else
                {
                    ShowError = Visibility.Collapsed;

                    UserDetail.Password = Pass.OldPass;

                    if (dbUser.Authenticate(UserDetail))
                    {
                        UserDetail.Password = Pass.NewPass;
                        dbUser.EditPassword(UserDetail);
                        Pass = new EditUserHelpClass();
                    }
                    else
                        messenger.Send(new UserEditFailMessage() { Message = "Old password is wrong" });
                }
            }

            messenger.Send(new UpdateUserMessage());
        }

        private void CurrentUser(UserDataMessage obj)
        {
            if (obj.Receiver == ToString())
            {
                ShowError = Visibility.Collapsed;
                Pass = new EditUserHelpClass();
                ShowDelete = false;
                EditEnabled = obj.UserData?.Id == UserDetail?.Id;
            }
        }

        private void DeleteUser(object obj)
        {
            if (dbUser.Authenticate(UserDetail))
            {
                foreach (var comment in dbCOmment.GetCommentsByUser(UserDetail).ToList())
                {
                    dbCOmment.RemoveComment(comment.Id);
                }

                foreach (var post in dbPost.GetPostList().Where(x => x.User.Id == UserDetail.Id).ToList())
                {
                    dbPost.RemovePost(post.Id);
                }

                dbUser.RemoveUser(userDetail.Id);
                ShowDelete = false;
                messenger.Send(new SignOutMessage());
                ShowError = Visibility.Collapsed;
            }
            else
                messenger.Send(new UserEditFailMessage() { Message = "User password is wrong" });
        }

        private void UserChanged(UserChangedMessage obj)
        {
            if (obj.UserChangedType == UserChangedType.Profile)
                UserDetail = dbUser.GetUserById(obj.Id);

            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = RequestUserDataType.CurrentUser,
                Sender = ToString()
            });
        }
    }
}