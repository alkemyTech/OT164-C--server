﻿using OngProject.Core.Models.DTOs;
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


        public List<OrganizationsPublicDTO> ToOrgPublicDTO(Task<IEnumerable<Organizations>> OrganizationsData)
        {
            List<OrganizationsPublicDTO> result = new List<OrganizationsPublicDTO>();
            foreach (Organizations org in OrganizationsData.Result)
            {

                OrganizationsPublicDTO organizationdto = new OrganizationsPublicDTO();
                organizationdto.Name = org.Name;
                organizationdto.Image = org.Image;
                organizationdto.Address = org.Address;
                organizationdto.Phone = org.Phone;
                organizationdto.facebookUrl = org.facebookUrl;
                organizationdto.instagramUrl = org.instagramUrl;
                organizationdto.linkedinUrl = org.linkedinUrl;
                organizationdto.Slides = org.Slides;
                result.Add(organizationdto);


            }

            return result;

        }
    }
}
