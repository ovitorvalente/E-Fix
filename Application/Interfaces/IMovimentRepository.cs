using E_Fix.Domain.Entities;

namespace E_Fix.Application.Interfaces
{
    public interface IMovimentRepository
    {
        List<Moviment> GetMoviments();
        void UpdateMoviments(List<int> sequencias);
    }
}
