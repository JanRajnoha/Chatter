using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Organization;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.CommonMessages;
using Chatter.BL.Messages.GroupMessages;
using Chatter.BL.Messages.OrganizationMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.Commands;
using Chatter.ViewModels.Interface;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Chatter.ViewModels
{
    public class OrganizationDetailVM : VMBase
    {
        private readonly DBOrganization dbOrganization;
        private readonly DBGroup dbGroup;
        private readonly DBUser dbUser;

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

        private OrganizationDetailModelDTO organizationDetail = new OrganizationDetailModelDTO();

        public OrganizationDetailModelDTO OrganizationDetail
        {
            get => organizationDetail;
            set
            {
                organizationDetail = value;
                OnPropertyChanged();
            }
        }

        private string addEditText = "Add Organization";

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

        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddGroupCommand { get; set; }
        public ICommand EditGroupCommand { get; set; }
        public ICommand RemoveGroupCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand AddAdminCommand { get; set; }
        public ICommand RemoveUserCommand { get; set; }
        public ICommand RemoveAdminCommand { get; set; }

        public OrganizationDetailVM(IMessenger messenger, DBOrganization dbOrganization, DBGroup dbGroup, DBUser dbUser)
        {
            this.messenger = messenger;
            this.dbOrganization = dbOrganization;
            this.dbGroup = dbGroup;
            this.dbUser = dbUser;

            DeleteCommand = new RelayCommand(DeleteOrganization);
            AddCommand = new RelayCommand(AddOrganization);
            AddGroupCommand = new RelayCommand(AddGroup);
            RemoveGroupCommand = new RelayCommand(RemoveGroup);
            EditGroupCommand = new RelayCommand(EditGroup);
            AddUserCommand = new RelayCommand(AddUser);
            AddAdminCommand = new RelayCommand(AddAdmin);
            RemoveUserCommand = new RelayCommand(RemoveUser);
            RemoveAdminCommand = new RelayCommand(RemoveAdmin);

            messenger.Register<OrganizationChangedMessage>(OrganizationChanged);
            messenger.Register<AddOrganizationFailMessage>(AddFail);
            messenger.Register<UserDataMessage>(FinishAdding);
            messenger.Register<UpdateOrganizationMessage>(LoadOrganization);
            messenger.Register<AddOrganizationMessage>(AddOrganization);
            messenger.Register<RefreshMessage>(Refresh);
        }

        private void EditGroup(object obj)
        {
            if (obj is GroupListModelDTO group)
            {
                messenger.Send(new ShowUpdateGroupMessage()
                {
                    SelectedGroup = dbGroup.GetGroupById(group.Id)
                });

                OrganizationDetail = dbOrganization.GetOrganizationById(OrganizationDetail.Id);
            }
        }

        private void RemoveGroup(object obj)
        {
            if (obj is GroupListModelDTO group)
            {
                messenger.Send(new RemoveGroupMessage()
                {
                    SelectedGroup = dbGroup.GetGroupById(group.Id)
                });

                OrganizationDetail = dbOrganization.GetOrganizationById(OrganizationDetail.Id);

                messenger.Send(new DeletedGroupMessage());
            }
        }

        private void Refresh(RefreshMessage obj)
        {
            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = Common.Enum.RequestUserDataType.CurrentUser,
                Sender = ToString()
            });

            //OrganizationDetail = 
        }

        private void RemoveAdmin(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                if (user.Id == CurrentUser.Id)
                    messenger.Send(new AddOrganizationFailMessage() { Message = "Creator couldn't be removed" });
                else
                {
                    OrganizationDetail.Users.Add(user);
                    OrganizationDetail.Admins.Remove(user);
                }
            }
        }

        private void RemoveUser(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                UserList.Add(user);
                OrganizationDetail.Users.Remove(user);
            }
        }

        private void AddAdmin(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                OrganizationDetail.Admins.Add(user);
                OrganizationDetail.Users.Remove(user);
            }
        }

        private void AddUser(object obj)
        {
            if (obj is UserListModelDTO user)
            {
                OrganizationDetail.Users.Add(user);
                UserList.Remove(user);
            }
        }

        private void AddGroup(object obj)
        {
            messenger.Send(new AddGroupMessage() { OrganizationId = OrganizationDetail.Id });
        }

        private void AddOrganization(AddOrganizationMessage obj)
        {
            OrganizationDetail = new OrganizationDetailModelDTO();
            AddEditText = "Add Organization";
            DeleteVisible = Visibility.Collapsed;

            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = Common.Enum.RequestUserDataType.CurrentUser,
                Sender = ToString()
            });
        }

        private void LoadOrganization(UpdateOrganizationMessage obj)
        {
            OrganizationDetail = obj.UpdateOrganization;
            AddEditText = "Edit Organization";

            messenger.Send(new RequestUserDataMessage()
            {
                RequestUserDataType = Common.Enum.RequestUserDataType.CurrentUser,
                Sender = ToString()
            });

            DeleteVisible = Visibility.Visible;
            UserList = new ObservableCollection<UserListModelDTO>(dbUser.GetUserList().Where(x => !OrganizationDetail.Users.Select(u => u.Id).Contains(x.Id) && !OrganizationDetail.Admins.Select(u => u.Id).Contains(x.Id)));
        }

        private void FinishAdding(UserDataMessage obj)
        {
            if (obj.Receiver == ToString())
            {
                CurrentUser = obj.UserData;

                if (!OrganizationDetail.Admins.Select(a => a.Id).Contains(CurrentUser.Id) && AddEditText == "Add Organization")
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        OrganizationDetail.Admins.Add(CurrentUser);
                    });
                }

                EditEnabled = OrganizationDetail.Admins.Select(a => a.Id).Contains(CurrentUser.Id);
                UserList = new ObservableCollection<UserListModelDTO>(dbUser.GetUserList().Where(x => !OrganizationDetail.Users.Select(u => u.Id).Contains(x.Id) && !OrganizationDetail.Admins.Select(u => u.Id).Contains(x.Id)));
            }
        }

        private void AddFail(AddOrganizationFailMessage obj)
        {
            ShowError = Visibility.Visible;
            ErrorMessage = obj.Message;
        }

        private void AddOrganization(object obj)
        {
            if (OrganizationDetail.Name == "")
                messenger.Send(new AddOrganizationFailMessage() { Message = "Organizaiton name couldn't be empty" });
            else
            {
                ShowError = Visibility.Collapsed;

                if (AddEditText == "Edit Organization")
                {
                    dbOrganization.EditOrganization(OrganizationDetail);
                    messenger.Send(new AddedOrganizationMessage());
                }
                else
                {
                    dbOrganization.AddOrganization(OrganizationDetail);

                    OrganizationDetail = new OrganizationDetailModelDTO();
                    messenger.Send(new AddedOrganizationMessage());
                }
            }
        }

        private void DeleteOrganization(object obj)
        {
            foreach (GroupDetailModelDTO group in OrganizationDetail.Groups)
            {
                dbGroup.RemoveGroup(group.Id);
            }

            dbOrganization.RemoveOrganization(OrganizationDetail.Id);
            messenger.Send(new DeletedOrganizationsMessage());
            OrganizationDetail = new OrganizationDetailModelDTO();
        }

        private void OrganizationChanged(OrganizationChangedMessage obj)
        {
            OrganizationDetail = dbOrganization.GetOrganizationById(obj.Id);
            AddEditText = "Edit Organization";
            DeleteVisible = Visibility.Visible;
        }
    }
}
