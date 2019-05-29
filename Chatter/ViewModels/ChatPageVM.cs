using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Organization;
using Chatter.BL.DTO.User;
using Chatter.BL.Messages.GroupMessages;
using Chatter.BL.Messages.LogMessages;
using Chatter.BL.Messages.UserMessages;
using Chatter.BL.Messages.OrganizationMessages;
using Chatter.Commands;
using Chatter.Common.Enum;
using Chatter.ViewModels.Interface;
using Chatter.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Timers;
using Chatter.BL.Messages.CommonMessages;

namespace Chatter.ViewModels
{
    public class ChatPageVM : VMBase
    {
        private readonly DBUser dbUser;
        private readonly DBOrganization dbOrganization;
        private readonly DBGroup dbGroup;
        private readonly DBPost dbPost;
        private readonly DBLogIn dbLogin;

        private string header;

        public string Header
        {
            get => header;
            set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        private string description;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        private Visibility showChat = Visibility.Visible;

        public Visibility ShowChat
        {
            get => showChat;
            set
            {
                if (showChat != value)
                {
                    showChat = value;

                    if (showChat == Visibility.Visible)
                        ClearOther(WindowType.Chat);
                }

                OnPropertyChanged();
            }
        }

        private Visibility showProfile = Visibility.Collapsed;

        public Visibility ShowProfile
        {
            get => showProfile;
            set
            {
                if (showProfile != value)
                {
                    showProfile = value;

                    if (showProfile == Visibility.Visible)
                        ClearOther(WindowType.Profile);
                }

                OnPropertyChanged();
            }
        }

        private Visibility showOrganization = Visibility.Collapsed;

        public Visibility ShowOrganization
        {
            get => showOrganization;
            set
            {
                if (showOrganization != value)
                {
                    showOrganization = value;

                    if (showOrganization == Visibility.Visible)
                        ClearOther(WindowType.Organization);
                }

                OnPropertyChanged();
            }
        }

        private Visibility showTeam = Visibility.Collapsed;

        public Visibility ShowTeam
        {
            get => showTeam;
            set
            {
                if (showTeam != value)
                {
                    showTeam = value;

                    if (showTeam == Visibility.Visible)
                        ClearOther(WindowType.Team);
                }

                OnPropertyChanged();
            }
        }

        private Visibility showOrganizationList = Visibility.Visible;

        public Visibility ShowOrganizationList
        {
            get => showOrganizationList;
            set
            {
                if (showOrganizationList != value)
                {
                    showOrganizationList = value;

                    if (showOrganizationList == Visibility.Visible)
                        ClearOther(SideMenuType.OrganizationList);
                }

                OnPropertyChanged();
            }
        }

        private Visibility showUserList = Visibility.Collapsed;

        public Visibility ShowUserList
        {
            get => showUserList;
            set
            {
                if (showUserList != value)
                {
                    showUserList = value;

                    if (showUserList == Visibility.Visible)
                        ClearOther(SideMenuType.UserList);
                }

                OnPropertyChanged();
            }
        }

        private ChatPage chatPage;

        public ChatPage ChatPage
        {
            get
            {
                return chatPage;
            }
            set
            {
                chatPage = value;
                OnPropertyChanged();
            }
        }

        private UserDetailModelDTO user;

        public UserDetailModelDTO User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private UserListModelDTO selectedUser;

        public UserListModelDTO SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<OrganizationDetailModelDTO> organizations;

        public ObservableCollection<OrganizationDetailModelDTO> Organizations
        {
            get
            {
                return organizations;
            }
            set
            {
                organizations = value;
                OnPropertyChanged();
            }
        }

        private GroupDetailModelDTO selectedGroup;

        public GroupDetailModelDTO SelectedGroup
        {
            get => selectedGroup;
            set
            {
                if (value != null)
                    selectedGroup = value;
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

        private string currentGroup;

        public string CurrentGroup
        {
            get => currentGroup;
            set
            {
                currentGroup = " > " + value;
                OnPropertyChanged();
            }
        }

        private string currentOrganization;

        public string CurrentOrganization
        {
            get => currentOrganization;
            set
            {
                currentOrganization = value;
                OnPropertyChanged();
            }
        }

        Timer refresher = new Timer();

        public ICommand GroupChangedCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ContextChangedCommand { get; set; }
        public ICommand ShowProfileCommand { get; set; }
        public ICommand AddOrganizationCommand { get; set; }
        public ICommand EditOrganizationCommand { get; set; }
        public ICommand SelectedUserChangedCommand { get; set; }

        public ChatPageVM(Messenger messenger, DBUser dbUser, DBOrganization dbOrganization, DBGroup dbGroup, DBPost dbPost, DBLogIn dbLogin)
        {
            this.messenger = messenger;
            this.dbUser = dbUser;
            this.dbOrganization = dbOrganization;
            this.dbGroup = dbGroup;
            this.dbPost = dbPost;
            this.dbLogin = dbLogin;

            refresher.Elapsed += new ElapsedEventHandler(Refresh);
            refresher.Interval = 10000; // 1000 ms is one second
            refresher.Start();

            GroupChangedCommand = new RelayCommand(GroupChanged);
            LogOutCommand = new RelayCommand(LogOut);
            CloseCommand = new RelayCommand(Close);
            ContextChangedCommand = new RelayCommand(ContextChanged);
            ShowProfileCommand = new RelayCommand(ShowProfileCom);
            AddOrganizationCommand = new RelayCommand(AddOrg);
            EditOrganizationCommand = new RelayCommand(EditOrganization);
            SelectedUserChangedCommand = new RelayCommand(SelectedUserChanged);

            messenger.Register<UserChangedMessage>(UserChanged);
            messenger.Register<RequestUserDataMessage>(RequestUserData);
            messenger.Register<SignOutMessage>(SignOut);
            messenger.Register<AddedOrganizationMessage>(AddedOrganization);
            messenger.Register<DeletedOrganizationsMessage>(DeletedOrganization);
            messenger.Register<DeletedGroupMessage>(DeletedGroup);
            messenger.Register<RequestGroupDataMessage>(RequestGroupData);
            messenger.Register<AddGroupMessage>(AddGroup);
            messenger.Register<AddedGroupMessage>(AddedGroup);
            messenger.Register<SideMenuChangedMessage>(SideMenuChanged);
            messenger.Register<ShowGroupDetailMessage>(ShowGroupDetail);
            messenger.Register<ShowOrganizationDetailMessage>(ShowOrganizationDetail);
            messenger.Register<UpdateUserMessage>(UserUpdate);
            messenger.Register<ShowUpdateGroupMessage>(ShowEditGroup);
        }

        private void SelectedUserChanged(object obj)
        {
            if (obj is UserListModelDTO userList)
            {
                ShowProfile = Visibility.Visible;
                SelectedGroup = new GroupDetailModelDTO();
                Header = "Profile";
                Description = "";
                messenger.Send(new UserChangedMessage()
                {
                    Id = userList.Id,
                    UserChangedType = UserChangedType.Profile
                });
            }
        }

        private void DeletedGroup(DeletedGroupMessage obj)
        {
            ShowChat = Visibility.Visible;
            AddedOrganization(null);
            SelectFirstOrganization();
        }

        private void ShowEditGroup(ShowUpdateGroupMessage obj)
        {
            SelectedGroup = obj.SelectedGroup;
            EditGroup(null);
        }

        private void UserUpdate(UpdateUserMessage obj)
        {
            User = dbUser.GetUserById(User.Id);
        }

        private void ShowOrganizationDetail(ShowOrganizationDetailMessage obj)
        {
            if (SelectedGroup != null)
                if (SelectedGroup.Organization != null)
                    EditOrganization(SelectedGroup.Organization.Id);
        }

        private void ShowGroupDetail(ShowGroupDetailMessage obj)
        {
            if (SelectedGroup != null)
                if (SelectedGroup.Organization != null)
                    EditGroup(SelectedGroup.Id);
        }

        private void SideMenuChanged(SideMenuChangedMessage obj)
        {
            ClearOther(obj.SideMenuType);
        }

        private void Refresh(object sender, ElapsedEventArgs e)
        {
            AddedOrganization(null);

            if (SelectedGroup?.Organization != null)
                if (Organizations.Single(x => x.Id == SelectedGroup.Organization.Id) == null)
                    SelectFirstOrganization();
                else if (Organizations.Single(x => x.Id == SelectedGroup.Organization.Id).Groups.Single(x => x.Id == SelectedGroup.Id) == null)
                    SelectFirstOrganization();

            UpdateUserList();

            messenger.Send(new RefreshMessage());
        }

        private void UpdateUserList()
        {
            if (User != null)
                UserList = new ObservableCollection<UserListModelDTO>(dbUser.GetUserList()?.Where(x => x.Id != User.Id));
        }

        private void AddedGroup(AddedGroupMessage obj)
        {
            ShowChat = Visibility.Visible;
            AddedOrganization(null);
            SelectFirstOrganization();
        }

        private void AddGroup(AddGroupMessage obj)
        {
            ShowTeam = Visibility.Visible;
            messenger.Send(new NewGroupMessage() { OrganizationId = obj.OrganizationId });
            SelectedGroup = new GroupDetailModelDTO();
            Header = "New Group";
            Description = "";
        }

        private void EditGroup(object obj)
        {
            ShowTeam = Visibility.Visible;
            messenger.Send(new UpdateGroupMessage() { UpdatedGroup = SelectedGroup });
            Header = "Edit Group:";
            Description = " " + SelectedGroup.Name;
            SelectedGroup = new GroupDetailModelDTO();
        }

        private void EditOrganization(object obj)
        {
            ShowOrganization = Visibility.Visible;
            SelectedGroup = new GroupDetailModelDTO();
            var org = dbOrganization.GetOrganizationById((Guid)obj);
            messenger.Send(new UpdateOrganizationMessage() { UpdateOrganization = org });
            Header = "Edit Organization:";
            Description = " " + org.Name;
        }

        private void RequestGroupData(RequestGroupDataMessage obj)
        {
            switch (obj.RequestGroupDataType)
            {
                case RequestGroupDataType.CurrentGroup:
                    messenger.Send(new GroupDataMessage()
                    {
                        GroupData = SelectedGroup,
                        Receiver = obj.Sender
                    });
                    break;

                case RequestGroupDataType.Id:
                    messenger.Send(new GroupDataMessage()
                    {
                        GroupData = dbGroup.GetGroupById(obj.Id),
                        Receiver = obj.Sender
                    });
                    break;

                default:
                    break;
            }
        }

        private void DeletedOrganization(DeletedOrganizationsMessage obj)
        {
            ShowChat = Visibility.Visible;
            AddedOrganization(null);
            SelectFirstOrganization();
        }

        private void AddedOrganization(AddedOrganizationMessage obj)
        {
            if (User != null)
                Organizations = new ObservableCollection<OrganizationDetailModelDTO>(dbOrganization.GetOrganizationFullListByUser(User.Id));
        }

        private void ContextChanged(object obj) => ChatPage = obj as ChatPage;

        private void SignOut(SignOutMessage obj) => LogOut(null);

        private void RequestUserData(RequestUserDataMessage obj)
        {
            switch (obj.RequestUserDataType)
            {
                case RequestUserDataType.Id:
                    messenger.Send(new UserDataMessage()
                    {
                        Receiver = obj.Sender,
                        UserData = dbUser.GetUserById(obj.Id)
                    });
                    break;

                case RequestUserDataType.CurrentUser:
                    messenger.Send(new UserDataMessage()
                    {
                        Receiver = obj.Sender,
                        UserData = User
                    });
                    break;

                default:
                    break;
            }
        }

        private void ShowProfileCom(object obj)
        {
            ShowProfile = Visibility.Visible;
            SelectedGroup = new GroupDetailModelDTO();
            Header = "Profile";
            Description = "";
            messenger.Send(new UserChangedMessage()
            {
                Id = user.Id,
                UserChangedType = UserChangedType.Profile
            });
        }

        private void AddOrg(object obj)
        {
            ShowOrganization = Visibility.Visible;
            messenger.Send(new AddOrganizationMessage());
            SelectedGroup = new GroupDetailModelDTO();
            Header = "New Organization";
            Description = "";
        }

        internal void SetWindow(ChatPage chatPage) => ChatPage = chatPage;

        private void LogOut(object obj)
        {
            SignPage OP = new SignPage();
            OP.Show();
            ChatPage.Close();
        }

        public void Close(object obj)
        {
            messenger.Send(new LogInChangedMessage()
            {
                SignLogCode = SignLogCode.LogOut,
                UserId = User.Id
            });
        }

        private void GroupChanged(object obj)
        {
            if (obj is GroupDetailModelDTO groupModel && SelectedGroup.Organization != null)
            {
                ShowChat = Visibility.Visible;

                CurrentGroup = groupModel.Name;
                CurrentOrganization = groupModel.Organization.Name;

                Header = CurrentOrganization;
                Description = CurrentGroup;

                messenger.Send(new GroupChangedMessage()
                {
                    Id = groupModel.Id
                });
            }
        }

        private void UserChanged(UserChangedMessage obj)
        {
            if (obj.UserChangedType == UserChangedType.LogIn)
            {
                User = dbUser.GetUserById(obj.Id);
                ShowChat = Visibility.Visible;
            }
            else
                return;

            UpdateUserList();

            AddedOrganization(null);

            SelectFirstOrganization();
        }

        private void SelectFirstOrganization()
        {
            if (Organizations.Count == 0 && Organizations.FirstOrDefault(x => x.Groups.Count != 0) != null)
            {
                return;
            }

            GroupDetailModelDTO startGroup = Organizations.FirstOrDefault(x => x.Groups.Count != 0)?.Groups.FirstOrDefault();

            if (startGroup == null)
            {
                ShowProfileCom(null);
                return;
            }

            CurrentGroup = startGroup.Name;
            CurrentOrganization = startGroup.Organization.Name;
            SelectedGroup = startGroup;

            messenger.Send(new GroupChangedMessage()
            {
                Id = startGroup.Id
            });
        }

        private void ClearOther(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.Chat:
                    ShowChat = Visibility.Visible;
                    ShowOrganization = Visibility.Collapsed;
                    ShowProfile = Visibility.Collapsed;
                    ShowTeam = Visibility.Collapsed;
                    break;
                case WindowType.Profile:
                    ShowProfile = Visibility.Visible;
                    ShowChat = Visibility.Collapsed;
                    ShowOrganization = Visibility.Collapsed;
                    ShowTeam = Visibility.Collapsed;
                    break;
                case WindowType.Organization:
                    ShowOrganization = Visibility.Visible;
                    ShowChat = Visibility.Collapsed;
                    ShowProfile = Visibility.Collapsed;
                    ShowTeam = Visibility.Collapsed;
                    break;
                case WindowType.Team:
                    ShowTeam = Visibility.Visible;
                    ShowChat = Visibility.Collapsed;
                    ShowOrganization = Visibility.Collapsed;
                    ShowProfile = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }

        private void ClearOther(SideMenuType sideMenuType)
        {
            switch (sideMenuType)
            {
                case SideMenuType.OrganizationList:
                    ShowOrganizationList = Visibility.Visible;
                    ShowUserList = Visibility.Collapsed;
                    break;
                case SideMenuType.UserList:
                    ShowUserList = Visibility.Visible;
                    ShowOrganizationList = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }
    }
}
