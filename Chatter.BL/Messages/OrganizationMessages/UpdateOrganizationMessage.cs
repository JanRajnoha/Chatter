using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.Organization;

namespace Chatter.BL.Messages.OrganizationMessages
{
     public class UpdateOrganizationMessage
     {
        public OrganizationDetailModelDTO UpdateOrganization { get; set; }
    }
}
