using Chatter.BL;
using Chatter.BL.Messages.CommonMessages;
using Chatter.BL.Messages.GroupMessages;
using Chatter.BL.Messages.OrganizationMessages;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class SideMenuVM : VMBase
    {
        public ICommand ShowUserListCommand { get; set; }
        public ICommand ShowOrganizationListCommand { get; set; }
        public ICommand ShowGroupDetailCommand { get; set; }
        public ICommand ShowOrganizationDetailCommand { get; set; }

        public SideMenuVM(Messenger messenger)
        {
            this.messenger = messenger;

            ShowUserListCommand = new RelayCommand(() => messenger.Send(new SideMenuChangedMessage() { SideMenuType = SideMenuType.UserList }));
            ShowOrganizationListCommand = new RelayCommand(() => messenger.Send(new SideMenuChangedMessage() { SideMenuType = SideMenuType.OrganizationList }));
            ShowGroupDetailCommand = new RelayCommand(() => messenger.Send(new ShowGroupDetailMessage()));
            ShowOrganizationDetailCommand = new RelayCommand(() => messenger.Send(new ShowOrganizationDetailMessage()));
        }
    }
}
