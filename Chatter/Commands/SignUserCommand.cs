using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.LogMessages;
using Chatter.Common.Enum;
using Chatter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatter.Commands
{
    public class SignUserCommand : ICommand
    {
        private SignInVM signInVM;
        private Messenger messenger;
        private DBUser dbUser;
        private DBLogIn dbLogIn;

        public SignUserCommand(SignInVM signInVM, Messenger messenger, DBUser dbUser, DBLogIn dbLogIn)
        {
            this.signInVM = signInVM;
            this.messenger = messenger;
            this.dbUser = dbUser;
            this.dbLogIn = dbLogIn;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!Classes.Functions.CheckConnection())
            {
                messenger.Send(new NotConnectionMessage());
                return;
            }

            if (parameter is UserDetailModelDTO userData)
            {
                userData.Email = userData.Username;
                var user = dbUser.GetUserByModel(userData);

                if (user != null)
                {
                    if (dbLogIn.GetDeniedRow(user.Id) >= 3)
                    {
                        messenger.Send(new SignInDeniedMessage());
                        return;
                    }
                }
                else
                {
                    messenger.Send(new SignInDeniedMessage() { Message = "Wrong login or password" });
                    return;
                }

                if (dbUser.Authenticate(userData))
                {
                    messenger.Send(new SignInApprovedMessage()
                    {
                        SignInType = SignInType.SignIn,
                        UserId = user.Id
                    });

                    messenger.Send(new LogInChangedMessage()
                    {
                        UserId = user.Id,
                        SignLogCode = SignLogCode.Succesful
                    });
                }
                else
                {
                    messenger.Send(new SignInDeniedMessage() { Message = "Wrong login or password" });

                    if (user != null)
                        messenger.Send(new LogInChangedMessage()
                        {
                            UserId = user.Id,
                            SignLogCode = SignLogCode.AccessDenied
                        });
                }
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
