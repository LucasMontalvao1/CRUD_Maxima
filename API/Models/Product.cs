using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Codigo { get; set; } 

        public string Descricao { get; set; } 

        public decimal Preco { get; set; } 

        public bool Status { get; set; } 

        [JsonIgnore]
        public string CodigoDepartamento { get; set; } 

        public Department Department { get; set; } 

        [JsonIgnore]
        public bool Deletado { get; set; } 
    }
}
