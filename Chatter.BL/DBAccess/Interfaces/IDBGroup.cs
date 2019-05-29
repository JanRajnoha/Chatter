using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Post;

namespace Chatter.BL.DBAccess.Interfaces
{
    public interface IDBGroup
    {
        GroupDetailModelDTO AddGroup(GroupDetailModelDTO group);
        void EditGroup(GroupDetailModelDTO group);
        GroupDetailModelDTO GetGroupById(Guid Id);
        List<GroupDetailModelDTO> GetGroupDetailList();
        List<GroupListModelDTO> GetGroupList();
        List<PostDetailModelDTO> GetPosts(Guid groupId);
        void RemoveGroup(Guid Id);
        GroupDetailModelDTO FindGroupById(Guid Id);
        void UpdateGroup(GroupDetailModelDTO model);
    }
}