using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catálogo_Jogos.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
        public string Nome {get; set;}
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string produtora {get; set;}
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço do jogo deve ser entre R$ 1,00 e R$ 1.000,00")]
        public double Preco {get; set;}
        
    }
}