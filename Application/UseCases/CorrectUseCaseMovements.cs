using E_Fix.Application.Interfaces;

namespace E_Fix.Application.UseCases
{
    public class CorrectUseCaseMovements
    {
        private readonly IMovimentRepository _repository;

        public CorrectUseCaseMovements(IMovimentRepository repository)
        {
            _repository = repository;
        }

        public void Execute(List<int> sequencias)
        {
            if (sequencias == null || sequencias.Count == 0) return;
            _repository.UpdateMoviments(sequencias);
        }
    }
}
