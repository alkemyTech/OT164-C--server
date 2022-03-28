using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {

        public List<CategoriesGetDTO> ToCagegoriesListDTO(IEnumerable<Categories> data)
        {
            List<CategoriesGetDTO> dtoList = new List<CategoriesGetDTO>();
            foreach (Categories e in data)
            {
                dtoList.Add(ToCategoriesDTO(e));
            }
            return dtoList;
        }
        public CategoriesGetDTO ToCategoriesDTO(Categories data)
        {
            var dataDto = new CategoriesGetDTO()
            {
                Name = data.Name
            };

            return dataDto;
        }

        public List<ComentariesGetDTO> ToComentariesListDTO(IEnumerable<Comentaries> data)
        {
            List<ComentariesGetDTO> dtoList = new List<ComentariesGetDTO>();
            foreach (Comentaries e in data)
            {
                dtoList.Add(ToComentariesDTO(e));
            }
            return dtoList;
        }
        public ComentariesGetDTO ToComentariesDTO(Comentaries data)
        {
            var dataDto = new ComentariesGetDTO()
            {
                Body = data.Body
            };

            return dataDto;
        }


    }
}
