using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.DTO.Post
{
    public class PostListModelDTO : ListModelBase
    {
        public string PostHeader { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
