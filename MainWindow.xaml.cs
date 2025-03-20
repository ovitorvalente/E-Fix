using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;
using E_Fix.Application.Interfaces;
using E_Fix.Infrastructure.Database;
using E_Fix.Infrastructure.Services;
using E_Fix.Presentation.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;

namespace E_Fix;

public partial class MainWindow : Window
{
    private MainViewModel _viewModel;
    private IFileService _fileService = new FileService();
    private string _pathArqId;
    private LogService _log;

    public MainWindow()
    {
        InitializeComponent();
        _log = new LogService(LogTextBox);
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }

    private void SelectFile_Click(object sender, RoutedEventArgs e)
    {
        var folderDialog = new OpenFolderDialog
        {
            Title = "Selecione sua pasta do E-Trade",
            Multiselect = false
        };

        if (folderDialog.ShowDialog() == true)
        {
            string selectedPath = folderDialog.FolderName;
            _pathArqId = Path.Combine(selectedPath, "ArqID.txt");

            if (!File.Exists(_pathArqId))
            {
                MessageBox.Show("Arquivo ArqId.txt não encontrado na pasta selecionada.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string connectionString = _fileService.GetConnectionFile(_pathArqId);

            _log.LogMesssage("Iniciando tentativa de conexão com o banco de dados...");

            try
            {
                _log.LogMesssage("Tentando se conectar ao banco de dados com a string de conexão fornecida...");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    _log.LogMesssage("Conexão estabelecida com sucesso.");

                    string databaseName = connection.Database;
                    _log.LogMesssage($"Conexão bem-sucedida com o banco de dados: {databaseName}");

                    var repository = new MovimentRepository(connectionString);
                    _viewModel = new MainViewModel(repository);
                    DataContext = _viewModel;


                    MainViewMode newWindow = new MainViewMode(connectionString, logMainViewMode);
                    newWindow.Show();
                }
            }
            catch (Exception ex)
            {
                _log.LogMesssage($"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }
    }

    private void logMainViewMode(string messege)
    {
        _log.LogMesssage(messege);
    }
}
