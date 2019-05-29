using Chatter.BL;
using Chatter.BL.Messages.LogMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using Chatter.Views;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class SignPageVM : VMBase
    {
        private bool signInVisible;

        public bool SignInVisible
        {
            get => signInVisible;
            set
            {
                signInVisible = value;
                OnPropertyChanged();
            }
        }

        private SignPage mainPage;

        public SignPage MainPage
        {
            get
            {
                return mainPage;
            }
            set
            {
                mainPage = value;
                OnPropertyChanged();
            }
        }

        public ICommand ContextChangedCommand { get; set; }

        public SignPageVM(Messenger messenger)
        {
            this.messenger = messenger;

            SignInVisible = true;

            ContextChangedCommand = new RelayCommand(ContextChanged);

            messenger.Register<ShowRegisterMessage>(ShowRegister);
            messenger.Register<ShowSignInMessage>(ShowSignIn);
            messenger.Register<SignInApprovedMessage>(UserSigned);
        }

        private void ContextChanged(object obj)
        {
            MainPage = obj as SignPage;
        }

        private void UserSigned(SignInApprovedMessage obj)
        {
            ChatPage OP = new ChatPage();
            OP.Show();
            MainPage.Close();

            messenger.Send(new UserChangedMessage()
            {
                Id = obj.UserId,
                UserChangedType = UserChangedType.LogIn
            });
        }

        internal void SetWindow(SignPage mainPage)
        {
            MainPage = mainPage;
        }

        private void ShowRegister(ShowRegisterMessage obj)
        {
            SignInVisible = false;
        }

        private void ShowSignIn(ShowSignInMessage obj)
        {
            SignInVisible = true;
        }
    }
}
