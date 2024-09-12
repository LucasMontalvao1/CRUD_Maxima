using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Product
    {
        public int Id { get; set; } // UUID para identificador do produto

        public string Codigo { get; set; } // Código apresentável

        public string Descricao { get; set; } // Descrição do produto

        public decimal Preco { get; set; } // Preço do produto

        public bool Status { get; set; } // Status ativo/inativo

        [JsonIgnore]
        public string CodigoDepartamento { get; set; } // Código do departamento (para ligação com Department)

        public Department Department { get; set; } // Relacionamento com a tabela Department

        [JsonIgnore]
        public bool Deletado { get; set; } // Exclusão lógica
    }
}
