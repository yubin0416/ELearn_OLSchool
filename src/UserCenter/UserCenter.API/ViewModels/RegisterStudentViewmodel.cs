using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserCenter.API.ViewModels
{
    public class RegisterStudentViewmodel
    {
        [Required]
        [Phone]
        public string Mobile { get; set; }

        [Required]
        [StringLength(6,ErrorMessage ="验证码长度为6位")]
        public string Code { get; set; }

        [Required]
        [MinLength(8,ErrorMessage = "最短8位密码")]
        [MaxLength(12,ErrorMessage ="最长12位密码")]
        public string Password { get; set; }

        [Required]
        public string NickName { get; set; }

        public string Picture { get; set; }
    }
}
