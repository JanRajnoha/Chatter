using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.LogIn;
using Chatter.BL.Messages.LogMessages;
using Chatter.Commands;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class SignInLogDetailVM : VMBase
    {
        private readonly DBLogIn dbLogIn;

        private LogInDetailModelDTO loginDetail;

        public LogInDetailModelDTO LoginDetail
        {
            get
            {
                return loginDetail;
            }

            set
            {
                loginDetail = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; set; }

        public SignInLogDetailVM(DBLogIn dbLogin, IMessenger messenger)
        {
            this.dbLogIn = dbLogin;
            this.messenger = messenger;

            messenger.Register<LogInChangedMessage>(LogInChanged);
        }

        private void LogInChanged(LogInChangedMessage obj)
        {
            LoginDetail = dbLogIn.GetLogById(obj.UserId);
        }
    }
}
