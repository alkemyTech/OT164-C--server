using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class MembersCreateDTO
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string FacebookUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string LinkedinUrl { get; set; }

        [Required]
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Image { get; set; }

        public string Description { get; set; }

    }
}
