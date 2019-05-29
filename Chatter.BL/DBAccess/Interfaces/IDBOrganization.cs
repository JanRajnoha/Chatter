using System;
using System.Collections.Generic;
using System.Text;
using Chatter.DAL.Model;
using Chatter.DAL.Data;
using Chatter.BL.DTO.Organization;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.User;

namespace Chatter.BL.DBAccess.Interfaces
{
    public interface IDBOrganization
    {
        //Add user
        //Add admin
        //Add orga.
        //Get user
        //Get admin
        //Get orga.
        //Edit orga.
        //Remove orga.
        //Remove user
        //Remove admin

        OrganizationDetailModelDTO AddOrganization(OrganizationDetailModelDTO organization);
        void EditOrganization(OrganizationDetailModelDTO organization);
        OrganizationDetailModelDTO GetOrganizationById(Guid Id);
        OrganizationDetailModelDTO GetOrganizationByGroupId(Guid groupId);
        List<OrganizationListModelDTO> GetOrganizationList();
        List<OrganizationDetailModelDTO> GetOrganizationFullList();
        List<OrganizationListModelDTO> GetOrganizationListByUser(Guid userId);
        List<OrganizationDetailModelDTO> GetOrganizationFullListByUser(Guid userId);
        List<GroupListModelDTO> GetGroupList(Guid orgId);
        void RemoveOrganization(Guid Id);
        void AddUserToOrganization(OrganizationDetailModelDTO organization, UserDetailModelDTO user);
        void RemoveOrganizationUser(OrganizationDetailModelDTO organization, Guid usrId);
    }
}
