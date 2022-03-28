using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
<<<<<<< Updated upstream
=======

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
        public List<SlidesGetDTO> ToSlideListDTO(IEnumerable<Slides> data)
        {
            List<SlidesGetDTO> dtoListSlide = new List<SlidesGetDTO>();
            foreach (Slides s in data)
            {
                dtoListSlide.Add(ToSlidesDTO(s));
            }
            return dtoListSlide;
        }
        public SlidesGetDTO ToSlidesDTO(Slides data)
        {
            var dataDtoSlides = new SlidesGetDTO()
            {
                image = data.image,
                orden=data.orden
            };

            return dataDtoSlides;
        }


>>>>>>> Stashed changes
    }
}
