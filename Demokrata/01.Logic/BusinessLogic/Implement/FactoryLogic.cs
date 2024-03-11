using AutoMapper;
using BusinessLogic.Implement.User;
using BusinessLogic.Interface;
using BusinessLogic.Interface.User;
using Domain.Model;
using WorkUnit.Interface;

namespace BusinessLogic.Implement
{
    public class FactoryLogic : IFactoryLogic
    {

        public IUsuarioLogic UsuarioLogic { get; private set; }
        public FactoryLogic(IUnitOfWork unitOfWork, IMapper mapper, IFactoryAbstractRepository factoryAbstractRepository, DataContext context)
        {
            UsuarioLogic = new UsuarioLogic(unitOfWork, mapper, context);
        }
    }
}
