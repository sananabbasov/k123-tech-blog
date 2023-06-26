using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.DTOs
{
	public class UserRegisterDTO
	{
		[Required(ErrorMessage = "Bütün xanaları doldurun.")]
		[StringLength(15,MinimumLength =  3,ErrorMessage = "Ad minimum 3 maksimum 15 simvol ola bilər.")]
		public string FirstName { get; set; }
        [Required(ErrorMessage = "Bütün xanaları doldurun.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Soyad minimum 3 maksimum 15 simvol ola bilər.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Bütün xanaları doldurun.")]
        [EmailAddress]
		public string Email { get; set; }
        [Required(ErrorMessage = "Bütün xanaları doldurun.")]
        [DataType(DataType.Password)]
		[StringLength(30, MinimumLength = 6, ErrorMessage = "Şifrə minimum 6 maksimum 30 simvol ola bilər.")]
		public string Password { get; set; }
        [Required(ErrorMessage = "Bütün xanaları doldurun.")]
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Şifrələr eyni deyil.")]
		public string PasswordRepeat { get; set; }
	}
}



