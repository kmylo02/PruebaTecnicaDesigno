using AutoMapper;
using BusinessLogic.Interface.User;
using Domain.Dto;
using Domain.Entity;
using Domain.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkUnit.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogic.Implement.User
{
    public class UsuarioLogic : IUsuarioLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        internal DataContext _context;

        public UsuarioLogic(IUnitOfWork unitOfWork, IMapper mapper, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<UsuarioDto> GetAll()
        {
            var result = this._unitOfWork.Repository<Usuario>().Get().AsEnumerable();
            return _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioDto>>(result);
        }


        public dynamic GetById(int id)
        {
            var result = this._unitOfWork.Repository<Usuario>().Get(x => x.Id == id).ToList();
            var totalRegistros = result.Count();
            var response = result != null ? _mapper.Map<List<Usuario>, List<UsuarioDto>>(result) : new List<UsuarioDto>();

            dynamic dataResult = new ExpandoObject();
            dataResult.registros = totalRegistros;
            dataResult.resultado = response;

            return dataResult;
        }

        public dynamic GetByAll(string? primerNombre, string? primerApellido, int pagina, int registrosPorPagina)
        {
            var result = new List<Usuario>();

            if (!string.IsNullOrEmpty(primerNombre))
            {
                result = this._unitOfWork.Repository<Usuario>().Get(x => x.PrimerNombre.Contains(primerNombre)).ToList();
            }

            if (!string.IsNullOrEmpty(primerApellido))
            {
                result = this._unitOfWork.Repository<Usuario>().Get(x => x.PrimerApellido.Contains(primerApellido)).ToList();
            }

            if (!string.IsNullOrEmpty(primerNombre) && !string.IsNullOrEmpty(primerApellido))
            {
                result = this._unitOfWork.Repository<Usuario>().Get(x => x.PrimerNombre.Contains(primerNombre) || x.PrimerApellido.Contains(primerApellido)).ToList();
            }

            var totalRegistros = result.Count();
            var usuariosPaginados = result.Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();

            var response = result != null ? _mapper.Map<List<Usuario>, List<UsuarioDto>>(usuariosPaginados) : new List<UsuarioDto>();

            dynamic dataResult = new ExpandoObject();
            dataResult.registros = totalRegistros;
            dataResult.resultado = response;

            return dataResult;
        }

        public bool Insert(UsuarioDto data)
        {
            try
            {
                Usuario _input = _mapper.Map<UsuarioDto, Usuario>(data);
                _unitOfWork.Repository<Usuario>().Insert(_input);
                return _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(UsuarioDto input)
        {
            try
            {
                var entity = _context.Usuarios.Where(x => x.Id == input.Id).FirstOrDefault();
                if (entity != null)
                {
                    entity.PrimerNombre = input.PrimerNombre;
                    entity.SegundoNombre = input.SegundoNombre;
                    entity.PrimerApellido = input.PrimerApellido;
                    entity.SegundoApellido = input.SegundoApellido;
                    entity.FechaNacimiento = input.FechaNacimiento;
                    entity.Sueldo = input.Sueldo;
                    _context.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.Repository<Usuario>().Delete(id);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
