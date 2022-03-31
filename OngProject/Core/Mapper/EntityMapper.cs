using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
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

        public Categories ToCategories(CategoriesUpdateDTO categoriesUpdateDTO, int id)
        {
            var data = new Categories
            {
                Id = id,
                DateModified = DateTime.Now,
                Description = categoriesUpdateDTO.Description,
                Image = categoriesUpdateDTO.Image,
                IsDeleted = false,
                Name = categoriesUpdateDTO.Name

            };

            return data;
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

        public List<UserDTO> ToUsersListDTO(IEnumerable<Users> users)
        {
            List<UserDTO> dtoList = new();
            foreach (Users e in users)
            {
                dtoList.Add(ToUsersDTO(e));
            }
            return dtoList;
        }
        public UserDTO ToUsersDTO(Users user)
        {
            UserDTO userDto = new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo
            };
            return userDto;
        }

        public Users UserDtoTOUsers(UserCreationDTO userDto)
        {
            Users user = new Users()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password
                
            };
            return user;
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

        public Members ToMembersFromDto(RequestUpdateMembersDto updateMembersDto, int id)
        {
            var data = new Members
            {
                Id = id,
                name = updateMembersDto.Name,
                facebookUrl = updateMembersDto.FacebookUrl,
                instagramUrl = updateMembersDto.InstagramUrl,
                lindedinUrl = updateMembersDto.LinkedinUrl,
                image = updateMembersDto.Image,
                description = updateMembersDto.Description
            };

            return data;
        }
    }
}
