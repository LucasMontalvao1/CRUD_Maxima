namespace API.Models.DTOs
{
    public class ProductAddDTO
    {
        public string Codigo { get; set; } 

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public bool Status { get; set; }

        public string CodigoDepartamento { get; set; } 
    }
}
