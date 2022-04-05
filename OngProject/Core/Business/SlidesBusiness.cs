using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.DataAccess;

using OngProject.Repositories.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Business
{
    public class SlidesBusiness : ISlidesBusiness
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();
        private readonly IFileManager _fileManager;
        public SlidesBusiness(IUnitOfWork unitOfWork,IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }
        public Task Delete(Slides slides)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SlidesDTO>> GetAll()
        {
            var data = await _unitOfWork.SlidesRepository.GetAll();
            if (data != null)
            {
                return mapper.ToSlidesListDTO(data);

            }

            return null;
        }

        public async Task<SlidesDTO> GetById(int id)
        {
            var slide = await _unitOfWork.SlidesRepository.GetById(id);
            if(slide != null)
            {
                return mapper.ToSlidesDTO(slide);
            }
            else
            {
                return null;
            }
            
        }

        public async Task<Response<SlidesDTO>> Insert(SlidesDTO slidesDTO)
        {
            Slides slides = mapper.SlidesCreationDTOToSlides(slidesDTO);
            Response<SlidesDTO> response = new Response<SlidesDTO>();

            if (!string.IsNullOrEmpty(slidesDTO.Image))
            {
                try
                {
                    if (slidesDTO.Orden == 0)
                    {
                        await SetOrderAsTheLastExistentAsync(slidesDTO);
                    }
                    string base64Data = "image/" + slidesDTO.Image.Substring("data:image/".Length, slidesDTO.Image.IndexOf(";base64") - "data:image/".Length);
                    string imagenString = slidesDTO.Image.Replace("data:image/jpeg;base64,", "");
                    byte[] bytes = Convert.FromBase64String(imagenString);
                    var stream = new MemoryStream(bytes);

                    IFormFile file = new FormFile(stream, 0, bytes.Length, "slides", Guid.NewGuid().ToString());

                    var nameFile = $"{Guid.NewGuid()}{base64Data}";

                    slides.image = await _fileManager.UploadFileAsync(file, base64Data, "slides",
                    base64Data);

                    await _unitOfWork.SlidesRepository.Insert(slides);
                    await _unitOfWork.SaveChangesAsync();
                }

                catch (Exception e)
                {
                    response.Message = e.Message;
                    response.Errors = new[] { e.InnerException.Message };
                    response.Succeeded = false;
                }
            }

            return response;
        }

        private async Task SetOrderAsTheLastExistentAsync(SlidesDTO model)
        {
            IEnumerable<Slides> slides = await _unitOfWork.SlidesRepository.GetAll();

            if (slides.Count() > 0)
            {

                var maxOrder = slides.Select(s => Int32.Parse(s.orden)).Max();
                model.Orden = maxOrder + 1;
            }
            else
            {

                model.Orden = 1;
            }
        }

        public async Task<bool> Update(SlidesDTO slides, int id)
        {;
            Slides slideData = mapper.ToSlidesUpdateFromDTO(slides,id);
            await _unitOfWork.SlidesRepository.Update(slideData);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
