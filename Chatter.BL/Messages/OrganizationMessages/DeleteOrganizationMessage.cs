﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.OrganizationMessages
{
    public class DeleteOrganizationMessage
    {
        public Guid Id { get; set; }

        public DeleteOrganizationMessage(Guid ID)
        {
            this.Id = Id;
        }
    }
}
