using System.IO;
using System.Windows.Controls;

namespace E_Fix.Infrastructure.Services
{
    public class LogService
    {
        private readonly TextBox _logTextBox;
        private readonly string _logFilePath;

        public LogService(TextBox logTextBox)
        {
            _logTextBox = logTextBox;
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "E_Fix_Log.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath));
        }

        public void LogMesssage(string messege)
        {
            string timeStamp = DateTime.Now.ToString("dd/MM/yy - HH:mm:ss");
            string formatMessege = $"> {timeStamp} - {messege}";

            _logTextBox.Dispatcher.Invoke(() =>
            {
                _logTextBox.AppendText(formatMessege + Environment.NewLine);
                _logTextBox.ScrollToEnd();
            });

            File.AppendAllText(_logFilePath, formatMessege + Environment.NewLine);
        }
    }
}
