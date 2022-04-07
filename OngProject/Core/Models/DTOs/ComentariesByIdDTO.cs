using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class ComentariesByIdDTO
    {

        public int UserId { get; set; }  
        public string Body { get; set; }
        public int NewsId { get; set; }
        

        public DateTime DateModified { get; set; }

        public bool IsDeleted { get; set; }



    }
}
