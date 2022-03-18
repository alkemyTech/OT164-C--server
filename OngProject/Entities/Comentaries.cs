using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class Comentaries: EntityBase
    {
        public int UserId { get; set; }
        public string Body { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
