using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatter.BL;
using Chatter.BL.DBAccess;
using Chatter.DAL.Factories;
using Chatter.ViewModels;

namespace Chatter.Classes
{
    public class VMLocator
    {
        private readonly Messenger messenger = new Messenger();
        private readonly DBUser dbUser = new DBUser(new DBContextFactory());
        private readonly DBLogIn dbLogin = new DBLogIn(new DBContextFactory());
        private readonly DBOrganization dbOrganization = new DBOrganization(new DBContextFactory());
        private readonly DBGroup dbGroup = new DBGroup(new DBContextFactory());
        private readonly DBPost dbPost = new DBPost(new DBContextFactory());
        private readonly DBComment dbComment = new DBComment(new DBContextFactory());

        private SignPageVM signPageVM = null;
        private SignInVM signInVM = null;
        private RegisterVM registerVM = null;
        private SignInLogListVM signInLogListVM = null;
        private ChatPageVM chatPageVM = null;
        private PostsListVM postsListVM = null;
        private UserDetailVM userDetailVM = null;
        private NewPostVM newPostVM = null;
        private OrganizationDetailVM organizationDetailVM = null;
        private GroupDetailVM groupDetailVM = null;
        private SideMenuVM sideMenuVM = null;

        public SignPageVM SignPageVM => CreateSignPageVM();
        public SignInVM SignInVM => CreateSignInVM();
        public RegisterVM RegisterVM => CreateRegisterVM();
        public SignInLogListVM SignInLogListVM => CreateSignInLogListVM();
        public ChatPageVM ChatPageVM => CreateChatPageVM();
        public PostsListVM PostsListVM => CreatePostsListVM();
        public UserDetailVM UserDetailVM => CreateUserDetailVM();
        public NewPostVM NewPostVM => CreateNewPostVM();
        public OrganizationDetailVM OrganizationDetailVM => CreateOrganizationDetailVM();
        public GroupDetailVM GroupDetailVM => CreateGroupDetailVM();
        public SideMenuVM SideMenuVM => CreateSideMenuVM();
        public PostVM PostVM => CreatePostVM();

        private PostVM CreatePostVM()
        {
            return new PostVM(messenger, dbPost, dbComment);
        }

        private SignPageVM CreateSignPageVM()
        {
            if (signPageVM == null)
            {
                signPageVM = new SignPageVM(messenger);
            }

            return signPageVM;
        }

        private SignInVM CreateSignInVM()
        {
            if (signInVM == null)
            {
                signInVM = new SignInVM(messenger, dbUser, dbLogin);
            }

            return signInVM;
        }

        private RegisterVM CreateRegisterVM()
        {
            if (registerVM == null)
            {
                registerVM = new RegisterVM(messenger, dbUser);
            }

            return registerVM;
        }

        private SignInLogListVM CreateSignInLogListVM()
        {
            if (signInLogListVM == null)
            {
                signInLogListVM = new SignInLogListVM(messenger, dbLogin);
            }

            return signInLogListVM;
        }

        private ChatPageVM CreateChatPageVM()
        {
            if (chatPageVM == null)
            {
                chatPageVM = new ChatPageVM(messenger, dbUser, dbOrganization, dbGroup, dbPost, dbLogin);
            }

            return chatPageVM;
        }

        private PostsListVM CreatePostsListVM()
        {
            if (postsListVM == null)
            {
                postsListVM = new PostsListVM(messenger, dbUser, dbOrganization, dbGroup, dbPost, dbComment);
            }

            return postsListVM;
        }

        private UserDetailVM CreateUserDetailVM()
        {
            if (userDetailVM == null)
            {
                userDetailVM = new UserDetailVM(messenger, dbUser, dbPost, dbComment);
            }

            return userDetailVM;
        }

        private NewPostVM CreateNewPostVM()
        {
            if (newPostVM == null)
            {
                newPostVM = new NewPostVM(messenger, dbPost, dbGroup, dbUser);
            }

            return newPostVM;
        }

        private OrganizationDetailVM CreateOrganizationDetailVM()
        {
            if (organizationDetailVM == null)
            {
                organizationDetailVM = new OrganizationDetailVM(messenger, dbOrganization, dbGroup, dbUser);
            }

            return organizationDetailVM;
        }

        private GroupDetailVM CreateGroupDetailVM()
        {
            if (groupDetailVM == null)
            {
                groupDetailVM = new GroupDetailVM(messenger, dbGroup, dbOrganization, dbUser);
            }

            return groupDetailVM;
        }

        private SideMenuVM CreateSideMenuVM()
        {
            if (sideMenuVM == null)
            {
                sideMenuVM = new SideMenuVM(messenger);
            }

            return sideMenuVM;
        }
    }
}
