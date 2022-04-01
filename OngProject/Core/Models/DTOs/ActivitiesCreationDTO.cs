using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class ActivitiesCreationDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }

        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Image { get; set; }
    }
}
