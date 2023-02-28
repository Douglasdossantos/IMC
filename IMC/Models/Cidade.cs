using System.ComponentModel.DataAnnotations;

namespace IMC.Models
{
    public class Cidade 
    {
        [Key]
        public int CID_CODIGO { get; set; }
        public string CID_NOME { get; set; }
        public string EST_UF { get; set; }
    }
}
