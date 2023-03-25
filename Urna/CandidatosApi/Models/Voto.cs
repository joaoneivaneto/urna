using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace CandidatosApi.Models
{
    public class Voto
    {

        [Required]
        public Guid id { get; set; }

        
        public Guid? IdCandidato { get; set; }

        [Required]
        public DateTime DataVoto { get; set; }

        public string status { get; set; }

        [JsonIgnore]
        public RegistroCandidato RegistroCandidato { get; set; }
    }
}
