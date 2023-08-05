using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Inbound
{
    public class FacebookLoginUser
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string PictureUrl { get; set; }
        public string Id { get; set; }
    }
}
