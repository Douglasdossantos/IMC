using System.ComponentModel.DataAnnotations;

namespace IMC.Models
{
    public class Funcionario 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [StringLength(200,ErrorMessage ="o campo {0} precisa ter entre {2} e {1} carateres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Sexo  Sexo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public float Peso { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public float Altura { get; set; }

        public float IMC { get; set; }

    }

    
}
