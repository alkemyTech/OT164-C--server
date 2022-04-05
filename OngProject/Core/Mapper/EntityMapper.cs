using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
        public Activities ActivityCreationDTOToActivity(ActivitiesCreationDTO activitiesCreationDTO)
        {
            var data = new Activities
            {

                DateModified = DateTime.Now,
                Content = activitiesCreationDTO.Content,

                Name = activitiesCreationDTO.Name,

                IsDeleted = false


            };

            return data;
        }

        public ActivitiesGetDto ActivityToActivitiesGetDTO(Activities activities)
        {
            var data = new ActivitiesGetDto
            {

               
                Content = activities.Content,

                Name = activities.Name
                
               


            };

            return data;
        }


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
        public Users UsersDTOToUserUpdate(int id, UserDTO UserDTO)
        {

            Users UpdatedUser = new Users();

            UpdatedUser.FirstName = UserDTO.FirstName;
            UpdatedUser.LastName = UserDTO.LastName;
            UpdatedUser.Email = UserDTO.Email;
            UpdatedUser.Password = UserDTO.Password;
            UpdatedUser.Photo = UserDTO.Photo;
            UpdatedUser.RolesId = 2;
            UpdatedUser.IsDeleted = false;
            UpdatedUser.Id = id;
            UpdatedUser.DateModified = DateTime.Now;

            return UpdatedUser;

        }
        public List<SlidesDTO> ToSlidesListDTO(IEnumerable<Slides> slides)
        {
            List<SlidesDTO> dtoList = new();
            foreach (Slides e in slides)
            {
                dtoList.Add(ToSlidesDTO(e));
            }
            return dtoList;
        }
        public SlidesDTO ToSlidesDTO(Slides data)
        {
            var dataDto = new SlidesDTO()
            {
                Image = data.image,
                Orden = Int16.Parse(data.orden),
                Text = data.text,
                OrganizationsId = data.OrganizationsId
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

        public Slides ToSlidesUpdateFromDTO(SlidesDTO slideDTO, int id)
        {
            var data = new Slides
            {
                Id = id,
                image = slideDTO.Image,
                text = slideDTO.Text,
                orden = slideDTO.Orden.ToString(),
                OrganizationsId = slideDTO.OrganizationsId

            };
            return data;
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

        public Members MemberDTOToMembers(MembersCreateDTO data, string imagePath)
        {
            return new Members
            {
                DateModified = DateTime.Now,
                description = data.Description,
                facebookUrl = data.FacebookUrl,
                instagramUrl = data.InstagramUrl,
                lindedinUrl = data.LinkedinUrl,
                name = data.Name,
                image = imagePath
            };
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

        public Comentaries ToComentariesFromDto(RequestComentariesDto comentariesDto)
        {
            var data = new Comentaries
            {
                UserId = comentariesDto.UserId,
                NewsId = comentariesDto.NewsId,
                Body = comentariesDto.Body
            };

            return data;
        }

        public List<ContactsGetDTO> ToContactsListDTO(IEnumerable<Contacts> data)
        {
            List<ContactsGetDTO> dtoList = new List<ContactsGetDTO>();
            foreach (Contacts c in data)
            {
                dtoList.Add(ToContactsDTO(c));
            }
            return dtoList;
        }
        public ContactsGetDTO ToContactsDTO(Contacts data)
        {
            var dataDto = new ContactsGetDTO()
            {
                Name = data.name,
                Phone = data.phone,
                Email = data.email,
                Message = data.message
            };

            return dataDto;
        }


        public Slides SlidesCreationDTOToSlides(SlidesDTO slidesDTO)
        {
            var data = new Slides
            {
                DateModified = DateTime.Now,
                orden = slidesDTO.Orden.ToString(),
                OrganizationsId = slidesDTO.OrganizationsId,
                text = slidesDTO.Text,
                IsDeleted = false
            };
            return data;
        }

    }
}