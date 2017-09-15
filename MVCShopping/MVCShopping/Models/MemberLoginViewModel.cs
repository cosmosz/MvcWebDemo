using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCShopping.Models
{
    public class MemberLoginViewModel
    {
        [DisplayName("会员账号")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "请输入{0}")]
        public string email { get; set; }
        
        [DisplayName("会员密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="请输入{0}")]
        public string password { get; set; }
    }
}