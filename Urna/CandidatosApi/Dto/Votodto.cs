using System.Text.Json.Serialization;

namespace CandidatosApi.Dto
{
    public class Votodto
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public Guid Id_Candidato { get; set; } 

        public DateTime Data_Voto { get; set; } = DateTime.Now;

        public string status { get; set; }
    }
}
