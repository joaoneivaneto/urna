﻿using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace CandidatosApi.Dto
{
    public class CreateRegistroCandidatosDto
    {
        [JsonIgnore]
        public Guid Id_Candidato { get; set; }

        public string Nome_Candidato { get; set;}

        public string Nome_Vice { get; set;}

        public DateTime Data_Registro { get; set; } = DateTime.Now;

        public int Legenda { get; set; }






    }
}
