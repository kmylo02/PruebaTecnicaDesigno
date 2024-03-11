using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface.User
{
    public interface IUsuarioLogic
    {
        public bool Delete(int id);
        public IEnumerable<UsuarioDto> GetAll();
        public dynamic GetById(int id);
        public dynamic GetByAll(string? primerNombre, string? primerApellido, int pagina, int registrosPorPagina);
        public bool Insert(UsuarioDto data);
        public bool Update(UsuarioDto data);
    }
}
