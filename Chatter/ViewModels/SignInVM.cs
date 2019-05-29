using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.LogIn;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.LogMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class SignInVM : VMBase
    {
        private DBUser dbUser;
        private DBLogIn dbLogIn;

        private UserDetailModelDTO user = new UserDetailModelDTO();

        public UserDetailModelDTO User
        {
            get => user;
            set
            {
                user = value;
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

        private bool registerEnabled = true;

        public bool RegisterEnabled
        {
            get => registerEnabled;
            set
            {
                registerEnabled = value;
                OnPropertyChanged();
            }
        }

        public SignInVM()
        {

        }

        public ICommand SignCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public SignInVM(Messenger messenger, DBUser dbUser, DBLogIn dbLogIn) : this()
        {
            this.messenger = messenger;
            this.dbUser = dbUser;
            this.dbLogIn = dbLogIn;

            SignCommand = new SignUserCommand(this, messenger, dbUser, dbLogIn);
            RegisterCommand = new RelayCommand(() => messenger.Send(new ShowRegisterMessage()));

            messenger.Register<SignInApprovedMessage>(UserSigned);
            messenger.Register<LogInChangedMessage>(WriteLogMessage);
            messenger.Register<SignInDeniedMessage>(SignInDenied);
            messenger.Register<NotConnectionMessage>(NotConnection);

            if (!Classes.Functions.CheckConnection())
                NotConnection(null);
        }

        private void NotConnection(NotConnectionMessage obj)
        {
            ShowError = Visibility.Visible;
            ErrorMessage = "You’re not connected";
            RegisterEnabled = false;
        }

        private void SignInDenied(SignInDeniedMessage obj)
        {
            ShowError = Visibility.Visible;
            ErrorMessage = obj.Message;
            RegisterEnabled = true;
        }

        private void WriteLogMessage(LogInChangedMessage obj)
        {
            var user = dbUser.GetUserById(obj.UserId);

            if (user == null)
            {
                return;
            }

            var res = dbLogIn.AddLog(new LogInDetailModelDTO()
            {
                SignLogCode = obj.SignLogCode,
                User = user
            });
        }

        private void UserSigned(SignInApprovedMessage obj)
        {
            if (obj.SignInType == SignInType.SignIn)
            {
                user = new UserDetailModelDTO();
                ShowError = Visibility.Collapsed;
                RegisterEnabled = true;
            }
        }
    }
}
