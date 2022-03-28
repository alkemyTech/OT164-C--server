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

        public List<ComentariesFromNewsDTO> ToComentariesListDTO(IEnumerable<Comentaries> comentaries)
        {
            List<ComentariesFromNewsDTO> dtoList = new();
            foreach (Comentaries e in comentaries)
            {
                dtoList.Add(ToComentariesDTO(e));
            }
            return dtoList;
        }
        public ComentariesFromNewsDTO ToComentariesDTO(Comentaries data)
        {
            var dataDto = new ComentariesFromNewsDTO()
            {
                Body = data.Body
            };

            return dataDto;
        }

    }
}
