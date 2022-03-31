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

namespace OngProject.Core.Business
{
    public class SlidesBusiness : ISlidesBusiness
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper mapper = new EntityMapper();

        public SlidesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return mapper.ToSlidesDTO(slide);
        }

        public Task Insert(Slides slides)
        {
            throw new NotImplementedException();
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
