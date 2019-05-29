using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.DTO.Comment
{
    public class CommentListModelDTO : ListModelBase
    {
        public string Content { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
