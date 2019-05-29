using System;
using Chatter.Common.Enum;
using Chatter.DAL.Model.Base;

namespace Chatter.DAL.Model
{
    public class SignInLog : EntityBase
    {
        public SignLogCode SignLogCode { get; set; }

        public DateTime SignInLogTime { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }
    }
}
