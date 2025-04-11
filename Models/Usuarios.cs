using System.ComponentModel.DataAnnotations;

namespace CantinaBariri143.Models
{
    public class Usuarios
    {

        public Guid UsuariosId { get; set; }

        [Display(Name = "Nome do Usuário")]
        [Required(ErrorMessage = "O campo Nome do Usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome do Usuário deve ter no máximo 100 caracteres.")]
        [MinLength(3, ErrorMessage = "O Nome do Usuário deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O Nome do Usuário deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string CPF { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }


    }
}
