using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DAL
{
    public abstract class DTO
    {
        public const string IDColumnName = "ID";
        protected DALMapper _mapper;
        public long Id { get; set; } = -1;
        protected DTO(DALMapper mapper)
        {
            _mapper = mapper;
        }
        public DALMapper GetMapper()
        {
            return this._mapper;
        }
    }
}
