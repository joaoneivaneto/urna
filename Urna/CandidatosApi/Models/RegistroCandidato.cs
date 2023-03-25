using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CandidatosApi.Models
{
    public class RegistroCandidato
    {
       
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public string NomeVice { get; set; }

        [Required]
        public DateTime DataRegistro { get; set; }

        [Required]
        public int Legenda { get; set; }

        [JsonIgnore]
        public ICollection<Voto> votos { get; set; }

    }
}
