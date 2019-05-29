using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.LogMessages;
using Chatter.Classes;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class RegisterVM : VMBase
    {
        private DBUser dbUser;

        private string passAgain = "";

        public string PassAgain
        {
            get => passAgain;
            set
            {
                passAgain = value;
                Data.PassAgain = passAgain;
                OnPropertyChanged();
            }
        }

        private UserDetailModelDTO user = new UserDetailModelDTO();

        public UserDetailModelDTO User
        {
            get => user;
            set
            {
                user = value;
                Data.User = user;
                OnPropertyChanged();
            }
        }

        private RegisterHelpClass data = new RegisterHelpClass();

        public RegisterHelpClass Data
        {
            get => data; set
            {
                data = value;
                OnPropertyChanged();
            }
        }

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

        public RegisterVM()
        {

        }

        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public RegisterVM(Messenger messenger, DBUser dbUser) : this()
        {
            this.messenger = messenger;
            this.dbUser = dbUser;

            RegisterCommand = new AddUserCommand(this, messenger, dbUser);
            LoginCommand = new RelayCommand(() => messenger.Send(new ShowSignInMessage()));
            //LoginCommand = new RelayCommand(() => messenger.Send(new ShowSignInMessage()));

            messenger.Register<UserExistMessage>(UserExist);
            messenger.Register<SignInApprovedMessage>(UserSigned);
            messenger.Register<NotConnectionMessage>(NotConnection);
        }

        private void NotConnection(NotConnectionMessage obj)
        {
            messenger.Send(new ShowSignInMessage());
        }

        private void UserExist(UserExistMessage obj)
        {
            if (obj.Message != string.Empty)
            {
                ShowError = Visibility.Visible;
                ErrorMessage = obj.Message;
            }
            else
                ShowError = Visibility.Collapsed;
        }

        private void UserSigned(SignInApprovedMessage obj)
        {
            if (obj.SignInType == SignInType.Register)
            {
                Data = new RegisterHelpClass();
                ShowError = Visibility.Collapsed;
                messenger.Send(new ShowSignInMessage());
            }
        }
    }
}
