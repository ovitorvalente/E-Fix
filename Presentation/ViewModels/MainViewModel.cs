using System.Collections.ObjectModel;
using E_Fix.Application.Interfaces;
using E_Fix.Application.UseCases;
using E_Fix.Domain.Entities;

namespace E_Fix.Presentation.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Moviment> Moviments { get; set; } = new();
        private readonly CorrectUseCaseMovements _correctUseCaseMovements;
        private readonly IMovimentRepository _repository;

        public MainViewModel(IMovimentRepository repository)
        {
            _repository = repository;
            _correctUseCaseMovements = new CorrectUseCaseMovements(repository);
            LoadMovements();
        }

        private void LoadMovements()
        {
            var movimentos = _repository.GetMoviments();
            foreach (var movimento in movimentos)
            {
                Moviments.Add(movimento);
            }


        }

        public List<Moviment> CorrectSelectedMovements()
        {
            var sequencias = Moviments.Where(m => m.Selected).Select(m => m.Sequencia).ToList();
            var correctedMovements = _correctUseCaseMovements.Execute(sequencias);

            return correctedMovements;
        }
    }
}
