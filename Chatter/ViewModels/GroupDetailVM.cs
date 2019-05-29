using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.CommonMessages;
using Chatter.BL.Messages.GroupMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.Commands;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class GroupDetailVM : VMBase
    {
        private readonly DBGroup dbGroup;
        private readonly DBOrganization dbOrganization;
        private readonly DBUser dbUser;

        private GroupDetailModelDTO groupDetail;

        public GroupDetailModelDTO GroupDetail
        {
            get
            {
                return groupDetail;
            }

            set
            {
                groupDetail = value;
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

        private string addEditText = "Add Group";

        public string AddEditText
        {
            get => addEditText;
            set
            {
                addEditText = value;
                OnPropertyChanged();
            }
        }

        private Visibility deleteVisible = Visibility.Collapsed;

        public Visibility DeleteVisible
        {
            get => deleteVisible;
            set
            {
                deleteVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<UserListModelDTO> userList;

        public ObservableCollection<UserListModelDTO> UserList
        {
            get => userList;
            set
            {
                userList = value;
                OnPropertyChanged();
            }
        }

        private UserDetailModelDTO currentUser;

        public UserDetailModelDTO CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged();
            }
        }

        private bool editEnabled = true;

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
        public ICommand AddCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand AddAdminCommand { get; set; }
        public ICommand RemoveUserCommand { get; set; }
        public ICommand RemoveAdminCommand { get; set; }

        public GroupDetailVM(IMessenger messenger, DBGroup dbGroup, DBOrganization dbOrganization, DBUser dbUser)
        {
            this.messenger = messenger;
            this.dbGroup = dbGroup;
            this.dbOrganization = dbOrganization;
            this.dbUser = dbUser;

            DeleteCommand = new RelayCommand(DeleteGroup);
            AddCommand = new RelayCommand(AddGroup);
            AddUserCommand = new RelayCommand(AddUser);
            AddAdminCommand = new RelayCommand(AddAdmin);
            RemoveUserCommand = new RelayCommand(RemoveUser);
            RemoveAdminCommand = new RelayCommand(RemoveAdmin);

            messenger.Register<GroupChangedMessage>(GroupChanged);
            messenger.Register<AddGroupFailMessage>(AddFail);
            messenger.Register<UserDataMessage>(FinishAdding);
            messenger.Register<UpdateGroupMessage>(LoadGroup);
            messenger.Register<NewGroupMessage>(AddGroup);
            messenger.Register<RefreshMessage>(Refresh);
            messenger.Register<DeleteGroupMessage>(DeleteGroup);
            messenger.Register<RemoveGroupMessage>(RemoveGroup);
        }

        private void RemoveGroup(RemoveGroupMessage obj)
        {
            GroupDetail = obj.SelectedGroup;
            DeleteGroup(null); 
        }

        private void Refresh(RefreshMessage obj)
        {
            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = Common.Enum.RequestUserDataType.CurrentUser,
                Sender = ToString()
            });
        }

        private void AddGroup(NewGroupMessage obj)
        {
            GroupDetail = new GroupDetailModelDTO()
            {
                Organization = dbOrganization.GetOrganizationById(obj.OrganizationId)
            };

            AddEditText = "Add Group";
            DeleteVisible = Visibility.Collapsed;

            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = Common.Enum.RequestUserDataType.CurrentUser,
                Sender = ToString()
            });
        }

        private void RemoveAdmin(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                if (user.Id == CurrentUser.Id)
                    messenger.Send(new AddGroupFailMessage() { Message = "Creator couldn't be removed" });
                else
                {
                    GroupDetail.Users.Add(user);
                    GroupDetail.Admins.Remove(user);
                }
            }
        }

        private void RemoveUser(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                UserList.Add(user);
                GroupDetail.Users.Remove(user);
            }
        }

        private void AddAdmin(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                GroupDetail.Admins.Add(user);
                GroupDetail.Users.Remove(user);
            }
        }

        private void AddUser(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                GroupDetail.Users.Add(user);
                UserList.Remove(user);
            }
        }

        private void LoadGroup(UpdateGroupMessage obj)
        {
            GroupDetail = dbGroup.GetGroupById(obj.UpdatedGroup.Id);

            if (GroupDetail.Organization == null)
                GroupDetail.Organization = dbOrganization.GetOrganizationByGroupId(GroupDetail.Id);

            AddEditText = "Edit Group";
            DeleteVisible = Visibility.Visible;

            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = Common.Enum.RequestUserDataType.CurrentUser,
                Sender = ToString()
            });

            GetUserList();
        }

        private void GetUserList()
        {
            if (GroupDetail.Organization == null)
                GroupDetail.Organization = dbOrganization.GetOrganizationByGroupId(GroupDetail.Id);

            UserList = new ObservableCollection<UserListModelDTO>(dbOrganization.GetOrganizationById(GroupDetail.Organization.Id).Users.Where(x => !GroupDetail.Users.Select(u => u.Id).Contains(x.Id) && !GroupDetail.Admins.Select(u => u.Id).Contains(x.Id)));

            if (GroupDetail.Organization == null)
                GroupDetail.Organization = dbOrganization.GetOrganizationByGroupId(GroupDetail.Id);

            foreach (var user in dbOrganization.GetOrganizationById(GroupDetail.Organization.Id).Admins.Where(x => !GroupDetail.Users.Select(u => u.Id).Contains(x.Id) && !GroupDetail.Admins.Select(u => u.Id).Contains(x.Id)))
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    UserList.Add(user);
                });
            }
        }

        private void FinishAdding(UserDataMessage obj)
        {
            if (obj.Receiver == ToString())
            {
                CurrentUser = obj.UserData;

                if (GroupDetail == null)
                    return;

                if (!GroupDetail.Admins.Select(a => a.Id).Contains(CurrentUser.Id) && AddEditText == "Add Group")
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        GroupDetail.Admins.Add(CurrentUser);
                    });
                }

                EditEnabled = GroupDetail.Admins.Select(a => a.Id).Contains(CurrentUser.Id);
                GetUserList();
            }
        }

        private void AddFail(AddGroupFailMessage obj)
        {
            ShowError = Visibility.Visible;
            ErrorMessage = obj.Message;
        }

        private void AddGroup(object obj)
        {
            if (GroupDetail.Name == "")
                messenger.Send(new AddGroupFailMessage() { Message = "Group name couldn't be empty" });
            else
            {
                ShowError = Visibility.Collapsed;

                if (AddEditText == "Edit Group")
                {
                    dbGroup.EditGroup(GroupDetail);
                    messenger.Send(new AddedGroupMessage());
                }
                else
                {
                    var org = dbOrganization.GetOrganizationById(GroupDetail.Organization.Id);
                    org.Groups.Add(GroupDetail);

                    dbOrganization.EditOrganization(org);

                    dbGroup.EditGroup(GroupDetail);

                    GroupDetail = new GroupDetailModelDTO();
                    messenger.Send(new AddedGroupMessage());
                }

                messenger.Send(new AddedGroupMessage());
            }
        }

        private void DeleteGroup(object obj)
        {
            dbGroup.RemoveGroup(groupDetail.Id);
            messenger.Send(new DeletedGroupMessage());
            GroupDetail = new GroupDetailModelDTO();

            messenger.Send(new DeletedGroupMessage());
        }

        private void GroupChanged(GroupChangedMessage obj)
        {
            GroupDetail = dbGroup.GetGroupById(obj.Id);

            if (GroupDetail.Organization == null)
                GroupDetail.Organization = dbOrganization.GetOrganizationByGroupId(GroupDetail.Id);

            AddEditText = "Edit Group";
            DeleteVisible = Visibility.Visible;
        }
    }
}
