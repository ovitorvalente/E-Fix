using System.IO;
using E_Fix.Application.Interfaces;
using Newtonsoft.Json;

namespace E_Fix.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public string GetConnectionFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("O ArqId não encontrado!");

            var jsonContent = File.ReadAllText(path);
            dynamic config = JsonConvert.DeserializeObject(jsonContent);

            if (config == null)
                throw new Exception("Erro ao ler o ArqId");

            return $"Server={config.DataSource}; Database=ETrade; User Id={config.User};Password={config.Password};TrustServerCertificate=True;Connection Timeout={config.Timeout};";
        }
    }
}
