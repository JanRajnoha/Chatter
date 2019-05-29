using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.User;

namespace Chatter.BL.DBAccess.Interfaces
{
    public interface IDBUser
    {
        UserDetailModelDTO AddUser(UserDetailModelDTO user);
        UserDetailModelDTO GetUserById(Guid Id);
        UserDetailModelDTO GetUserByEmail(string mail);
        UserDetailModelDTO GetUserByUsername(string username);
        UserDetailModelDTO GetUserByModel(UserDetailModelDTO model);
        void EditUser(UserDetailModelDTO user);
        void EditPassword(UserDetailModelDTO user);
        void RemoveUser(Guid Id);
        List<UserListModelDTO> GetUserList();
    }
}
