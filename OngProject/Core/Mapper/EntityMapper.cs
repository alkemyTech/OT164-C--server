using OngProject.Core.Helper;
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
        public Users UsersDTOToUserUpdate(int id, UserUpdateDTO UserDTO)
        {

            Users UpdatedUser = new Users();

            UpdatedUser.FirstName = UserDTO.FirstName;
            UpdatedUser.LastName = UserDTO.LastName;
            UpdatedUser.Email = "";
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
                Orden = Int32.Parse(data.orden),
                Text = data.text,
                OrganizationsId = data.OrganizationsId
            };
            return dataDto;
        }

        public List<SlidesGetAllDTO> GetAllSlides(IEnumerable<Slides> slides)
        {
            List<SlidesGetAllDTO> slidesDTO = new();
            foreach(Slides e in slides)
            {
                SlidesGetAllDTO sli = new();
                sli.Image = e.image;
                sli.Orden = e.orden;
                slidesDTO.Add(sli);
            }
            return slidesDTO;
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

        public PagedResponse<List<MembersGetDTO>> PagedResponseMembersDTO(PagedResponse<List<Members>> prmembers)
        {
            PagedResponse<List<MembersGetDTO>> result = new PagedResponse<List<MembersGetDTO>>();
            result.FirstPage = prmembers.FirstPage;
            result.LastPage = prmembers.LastPage;

            result.NextPage = prmembers.NextPage;
            result.PreviousPage = prmembers.PreviousPage;
            result.PageNumber = prmembers.PageNumber;
            result.PageSize = prmembers.PageSize;
            result.TotalPages = prmembers.TotalPages;
            result.TotalRecords = prmembers.TotalRecords;
            result.Succeeded = true;

            List<MembersGetDTO> lstDto = new List<MembersGetDTO>();
            lstDto = ListMembersTOListMembersGetDTO(prmembers.Data);
            result.Data = lstDto;
            return result;
        }

        public List<MembersGetDTO> ListMembersTOListMembersGetDTO(List<Members> data)
        {
            List<MembersGetDTO> dtoList = new List<MembersGetDTO>();
            foreach (Members e in data)
            {
                MembersGetDTO dto = new MembersGetDTO();
                dto = ToMembersDTO(e);
                dtoList.Add(dto);
            }
            return dtoList;
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
                Body = comentariesDto.Body,
                DateModified = DateTime.Now
            };

            return data;
        }

        public Comentaries ToComentariesUpdateFromDto(RequestUpdateComentariesDto comentariesDto, int id)
        {
            var data = new Comentaries
            {
                Id = id,
                UserId = comentariesDto.UserId,
                Body = comentariesDto.Body,
                DateModified = DateTime.Now
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

        public Contacts ToContactsFromDTO(ContactsGetDTO contactsDTO)
        {
            var data = new Contacts
            {
                name = contactsDTO.Name,
                email = contactsDTO.Email,
                phone = contactsDTO.Phone,
                message = contactsDTO.Message
            };

            return data;
        }

        public List<NewsDTO> ToNewsListDTO(IEnumerable<News> data)
        {
            List<NewsDTO> dtoList = new List<NewsDTO>();
            foreach (News c in data)
            {
                dtoList.Add(ToNewsDTO(c));
            }
            return dtoList;
        }

        public NewsDTO ToNewsDTO(News data)
        {
            var dataDto = new NewsDTO()
            {
                Name = data.Name,
                Content = data.Content,
                Image = data.Image,
                CategoriesId = data.CategoriesId
            };

            return dataDto;
        }

        public News ToNews(NewsDTO newsDTO, int id)
        {
            var data = new News
            {
                Id = id,
                DateModified = DateTime.Now,
                Image = newsDTO.Image,
                IsDeleted = false,
                Name = newsDTO.Name,
                Content = newsDTO.Content,
                CategoriesId = newsDTO.CategoriesId

            };

            return data;
        }

        public NewsGetByIdDTO ToNewsByIdDTO(Task<News> query)
        {
            NewsGetByIdDTO data = new NewsGetByIdDTO();

            data.Name = query.Result.Name;
            data.Content = query.Result.Content;
            data.Image = query.Result.Image;
            data.CategoriesId = query.Result.CategoriesId;
            data.Categories = query.Result.Categories;
            data.DateModified = query.Result.DateModified;
            data.IsDeleted = query.Result.IsDeleted;

            return data;

        }

        public PagedResponse<List<NewsDTO>> PagedResponseNewsDTO(PagedResponse<List<News>> prmembers)
        {
            PagedResponse<List<NewsDTO>> result = new PagedResponse<List<NewsDTO>>();
            result.FirstPage = prmembers.FirstPage;
            result.LastPage = prmembers.LastPage;

            result.NextPage = prmembers.NextPage;
            result.PreviousPage = prmembers.PreviousPage;
            result.PageNumber = prmembers.PageNumber;
            result.PageSize = prmembers.PageSize;
            result.TotalPages = prmembers.TotalPages;
            result.TotalRecords = prmembers.TotalRecords;
            result.Succeeded = true;

            List<NewsDTO> lstDto = new List<NewsDTO>();
            lstDto = ToNewsListDTO(prmembers.Data);
            result.Data = lstDto;
            return result;
        }

        public testimonials TestimonialCreateDTOToTestimonial(TestimonialsCreateDTO data, string imagePath)
        {
            return new testimonials
            {
                content = data.Content,
                image = imagePath,
                name = data.Name
            };
        }


        public testimonials TestimonialPutDTOToTestimonial(int id, TestimonialsPutDto data, string imagePath)
        {
            return new testimonials
            {
                Id = id,
                content = data.Content,
                image = imagePath,
                name = data.Name
            };
        }

        public TestimonialsDTO TestimonialToTestimonialDTO(testimonials data)
        {
            return new TestimonialsDTO
            {
                Content = data.content,
                Id = data.Id,
                Image = data.image,
                Name = data.name
            };

        }

        public List<TestimonialsDTO> ListTestimonialsTOListTestimonialsDTO(List<testimonials> data)
        {
            List<TestimonialsDTO> dtoList = new List<TestimonialsDTO>();
            foreach (testimonials e in data)
            {
                TestimonialsDTO dto = new TestimonialsDTO();
                dto = TestimonialToTestimonialDTO(e);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public PagedResponse<List<TestimonialsDTO>> PagedResponseTestimonialsDTO(PagedResponse<List<testimonials>> prtestimonials)
        {
            PagedResponse<List<TestimonialsDTO>> result = new PagedResponse<List<TestimonialsDTO>>();
            result.FirstPage = prtestimonials.FirstPage;
            result.LastPage = prtestimonials.LastPage;

            result.NextPage = prtestimonials.NextPage;
            result.PreviousPage = prtestimonials.PreviousPage;
            result.PageNumber = prtestimonials.PageNumber;
            result.PageSize = prtestimonials.PageSize;
            result.TotalPages = prtestimonials.TotalPages;
            result.TotalRecords = prtestimonials.TotalRecords;


            List<TestimonialsDTO> lstDto = new List<TestimonialsDTO>();
            lstDto = ListTestimonialsTOListTestimonialsDTO(prtestimonials.Data);
            result.Data = lstDto;
            return result;
        }

      


        public OrganizationsUpdateDTO OrganizationsToOrganizationUpdateDTO(Organizations organization)
        {
            var dataDTO = new OrganizationsUpdateDTO()
            {
                Name = organization.Name,
                Image = organization.Image,
                Address = organization.Address,
                Phone = organization.Phone,
                Email = organization.Email,
                WelcomeText = organization.WelcomeText,
                AboutUSText = organization.AboutUsText,
                Facebook = organization.facebookUrl,
                Instagram = organization.instagramUrl,
                Linkedin = organization.linkedinUrl
            };
            return dataDTO;

        }

            

            public Organizations OrganizationUpdateDTOToOrganizations(OrganizationsUpdateDTO organizationsUpdateDTO, int id)
            {
                var data = new Organizations
                {
                    Id = id,
                    DateModified = DateTime.Now,
                    IsDeleted = false,
                    Name = organizationsUpdateDTO.Name,
                    Image = organizationsUpdateDTO.Image,
                    Address = organizationsUpdateDTO.Address,
                    Phone = organizationsUpdateDTO.Phone,
                    Email = organizationsUpdateDTO.Email,
                    WelcomeText = organizationsUpdateDTO.WelcomeText,
                    AboutUsText = organizationsUpdateDTO.AboutUSText,
                    facebookUrl = organizationsUpdateDTO.Facebook,
                    instagramUrl = organizationsUpdateDTO.Instagram,
                    linkedinUrl = organizationsUpdateDTO.Linkedin
                };
                return data;
            }

            public Categories CategoriesCreationDTOToCategories(CategorieCreationDTO creationCategorieDTO)
            {
                var data = new Categories
                {
                    DateModified = DateTime.Now,
                    Name = creationCategorieDTO.Name,
                    Description = creationCategorieDTO.Description,
                    Image = creationCategorieDTO.Image
                };
                return data;

            }


            public Activities ToActivities(ActivitiesDTO activitiesCreationDTO, int id)
            {
                var data = new Activities
                {
                    Id = id,
                    DateModified = DateTime.Now,
                    //   Image = activitiesCreationDTO.Image,
                    IsDeleted = false,
                    Name = activitiesCreationDTO.Name,
                    Content = activitiesCreationDTO.Content,


                };

                return data;

            }

            public ActivitiesDTO ToActivitiesDTO(Activities data)
            {
                var dataDto = new ActivitiesDTO()
                {
                    Name = data.Name,
                    Content = data.Content,
                    Image = data.Image,

                };

                return dataDto;
            }


            public News NewsDTOToNewsForInsert(NewsDTO news)
            {
                News ToInsert = new News();

                ToInsert.Name = news.Name;
                ToInsert.Content = news.Content;
                ToInsert.Image = news.Image;
                ToInsert.CategoriesId = news.CategoriesId;
                ToInsert.IsDeleted = false;
                ToInsert.DateModified = DateTime.Now;

                return ToInsert;
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



        public ComentariesByIdDTO ComentariesByIdToDTO (Comentaries comentary)
        {

            ComentariesByIdDTO data = new ComentariesByIdDTO();

            data.UserId = comentary.UserId;
            data.Body = comentary.Body;
            data.NewsId = comentary.NewsId;
            data.DateModified = comentary.DateModified;
            data.IsDeleted = comentary.IsDeleted;

            return data;

        }

        public PagedResponse<List<CategoriesGetDTO>> PagedResponseCategoriesDTO(PagedResponse<List<Categories>> prcategories)
        {
            PagedResponse<List<CategoriesGetDTO>> result = new PagedResponse<List<CategoriesGetDTO>>();
            result.FirstPage = prcategories.FirstPage;
            result.LastPage = prcategories.LastPage;

            result.NextPage = prcategories.NextPage;
            result.PreviousPage = prcategories.PreviousPage;
            result.PageNumber = prcategories.PageNumber;
            result.PageSize = prcategories.PageSize;
            result.TotalPages = prcategories.TotalPages;
            result.TotalRecords = prcategories.TotalRecords;


            List<CategoriesGetDTO> lstDto = new List<CategoriesGetDTO>();
            lstDto = ListCategoriesTOListCateggoriesDTO(prcategories.Data);
            result.Data = lstDto;
            return result;
        }

        public List<CategoriesGetDTO> ListCategoriesTOListCateggoriesDTO(List<Categories> data)
        {
            List<CategoriesGetDTO> dtoList = new List<CategoriesGetDTO>();
            foreach (Categories e in data)
            {
                CategoriesGetDTO dto = new CategoriesGetDTO();
                dto = CategoriesToCategoriesDTO(e);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public CategoriesGetDTO CategoriesToCategoriesDTO(Categories entity)
        {
            var dto = new CategoriesGetDTO
            {
                Name = entity.Name,
            };
            return dto;
        }
    }
    }