using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.DAL.Model.Base
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
