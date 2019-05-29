using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.LogMessages;
using Chatter.Classes;
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
    public class AddUserCommand : ICommand
    {
        private RegisterVM registerVM;
        private Messenger messenger;
        private DBUser dbUser;

        public AddUserCommand(RegisterVM registerVM, Messenger messenger, DBUser dbUser)
        {
            this.registerVM = registerVM;
            this.messenger = messenger;
            this.dbUser = dbUser;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is RegisterHelpClass regHelp)
            {
                bool validRes = false;

                try
                {
                    var addr = new System.Net.Mail.MailAddress(regHelp.User.Email);
                    validRes = addr.Address == regHelp.User.Email;
                }
                catch { }

                if (!validRes)
                {
                    if (regHelp.User.Email != "")
                        messenger.Send(new UserExistMessage() { Message = "Not valid email" });
                    else
                        messenger.Send(new UserExistMessage() { Message = string.Empty });
                    return false;
                }

                if (regHelp.PassAgain.Length < 6)
                {
                    messenger.Send(new UserExistMessage() { Message = "Password is too short" });
                    return false;
                }

                if (regHelp.PassAgain != regHelp.User.Password)
                {
                    messenger.Send(new UserExistMessage() { Message = "Passwords are not same" });
                    return false;
                }

                messenger.Send(new UserExistMessage() { Message = string.Empty });
                return true;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (!Classes.Functions.CheckConnection())
            {
                messenger.Send(new NotConnectionMessage());
                return;
            }

            if (parameter is RegisterHelpClass userData)
            {
                var storedUser = dbUser.GetUserByModel(userData.User);

                if (storedUser != null)
                {
                    if (storedUser.Username == userData.User.Username)
                        messenger.Send(new UserExistMessage() { Message = "User with this username already exist" });
                    else
                        messenger.Send(new UserExistMessage() { Message = "User with this email already exist" });
                }
                else
                {
                    var userAdded = dbUser.AddUser(userData.User);

                    messenger.Send(new SignInApprovedMessage()
                    {
                        SignInType = SignInType.Register,
                        UserId = userAdded.Id
                    });

                    messenger.Send(new LogInChangedMessage()
                    {
                        UserId = userAdded.Id,
                        SignLogCode = SignLogCode.Registered
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

