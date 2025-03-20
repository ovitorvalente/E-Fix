using System.Windows;
using E_Fix.Domain.Entities;
using System.Windows.Controls;
using E_Fix.Infrastructure.Database;
using Microsoft.Identity.Client;

namespace E_Fix.Presentation.ViewModels
{
    public partial class MainViewMode : Window
    {
        private MainViewModel _viewModel;
        private readonly Action<string> _logCallBack;

        public MainViewMode(string connection, Action<string> logCallBack)
        {
            InitializeComponent();
            _logCallBack = logCallBack;
            var repository = new MovimentRepository(connection);
            _viewModel = new MainViewModel(repository);
            DataContext = _viewModel;  
        }

        private void CorrectSelected_Click(object sender, RoutedEventArgs e)
        {
            var correctedMovements = _viewModel.CorrectSelectedMovements();
                        
            if (correctedMovements.Count > 0)
            {
                string logMessage = $"Correção realizada: {correctedMovements.Count} movimentos corrigidos.";
                MessageBox.Show(logMessage, "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                _logCallback?.Invoke(logMessage); // Envia a mensagem para MainWindow
            }
            else
            {
                MessageBox.Show("Nenhum movimento selecionado para correção.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                foreach (var item in dataGrid.SelectedItems)
                {
                    if (item is Moviment movimento)
                        movimento.Selected = !movimento.Selected;
                }
                dataGrid.Items.Refresh();
            }
        }

    }
}
