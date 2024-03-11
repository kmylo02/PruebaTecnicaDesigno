using BusinessLogic.Implement;
using BusinessLogic.Interface;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFactoryLogic _factoryLogic;

        public UserController(IFactoryLogic factoryLogic)
        {
            _factoryLogic = factoryLogic;
        }

        [HttpGet]
        [Route("ObtenerUsuarios")]
        public ActionResult<List<UsuarioDto>> GetAll()
        {
            return _factoryLogic.UsuarioLogic.GetAll().ToList();
        }

        [HttpGet]
        [Route("BuscarUsuario")]
        public dynamic GetById(int id)
        {
            return _factoryLogic.UsuarioLogic.GetById(id);
        }

        [HttpGet]
        [Route("BuscarUsuarioporNombreApellido")]
        public dynamic GetByAll(string? primerNombre = "", string? primerApellido = "", int pagina = 1, int registrosPorPagina = 5)
        {
            return _factoryLogic.UsuarioLogic.GetByAll(primerNombre, primerApellido, pagina, registrosPorPagina);
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public ActionResult<bool> Insert(UsuarioDto data)
        {
            return _factoryLogic.UsuarioLogic.Insert(data);
        }

        [HttpPut]
        [Route("ActualizarCliente")]
        public ActionResult<bool> Update(UsuarioDto input)
        {
            try
            {
                return _factoryLogic.UsuarioLogic.Update(input);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("EliminarUsuario")]
        public ActionResult<bool> Delete(int id)
        {
            return _factoryLogic.UsuarioLogic.Delete(id);
        }
    }
}
