namespace API.Models.DTOs
{
    public class ProductDTO
    {
        public int? Id { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public bool Status { get; set; }

        public string CodigoDepartamento { get; set; }
    }
}
