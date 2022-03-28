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
        public List<MembersGetDTO> ToMembersListDTO(IEnumerable<Members> data)
        {
            List<MembersGetDTO> dtoList = new List<MembersGetDTO>();
            foreach (Members e in data)
            {
                dtoList.Add(ToMembersDTO(e));
            }
            return dtoList;
        }
        public MembersGetDTO ToMembersDTO(Members data)
        {
            var dataDto = new MembersGetDTO()
            {
                Name = data.name,
                Facebook = data.facebookUrl,
                Instagram = data.instagramUrl,
                Linkedin = data.lindedinUrl
            };

            return dataDto;
        }
    }
}
