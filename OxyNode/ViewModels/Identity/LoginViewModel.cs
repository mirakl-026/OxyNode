using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OxyNode.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        // обратная ссылка для возврата на страницу, с которой был запрщён доступ
        // и был перевод на вход

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}
