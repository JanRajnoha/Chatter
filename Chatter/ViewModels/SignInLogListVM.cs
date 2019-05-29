using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.LogIn;
using Chatter.BL.Messages.LogMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.ViewModels
{
    public class SignInLogListVM : VMBase
    {
        private DBLogIn dbLogin;
        private LogInChangedMessage logMessage = null;

        public SignInLogListVM()
        {

        }

        public SignInLogListVM(Messenger messenger, DBLogIn dbLogin) : this()
        {
            this.messenger = messenger;
            this.dbLogin = dbLogin;

            messenger.Register<UserDataMessage>(FinishWriting);
        }

        private void FinishWriting(UserDataMessage obj)
        {
            if (obj.Receiver == ToString() && logMessage != null)
            {
                dbLogin.AddLog(new LogInDetailModelDTO()
                {
                    SignLogCode = logMessage.SignLogCode,
                    User = obj.UserData
                });

                logMessage = null;
            }
        }

        private void WriteLogMessage(LogInChangedMessage obj)
        {
            messenger.Send(new RequestUserDataMessage()
            {
                Id = obj.UserId,
                Sender = ToString()
            });
        }
    }
}
