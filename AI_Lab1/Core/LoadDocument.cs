using System.IO;
using System.Text;
using Microsoft.Win32;

namespace AI_Lab1.Core;

public class LoadDocument
{
    public List<string> LoadFiles()
    {
        var openFileDialog = new OpenFileDialog
        {
            Multiselect = true,
            Filter = "Text files (*.txt)|*.txt",
            InitialDirectory = Path.Combine(Environment.CurrentDirectory, "Resources")
        };

        var fileContent = new List<string>();

        if (openFileDialog.ShowDialog() == true)
        {
            foreach (var filename in openFileDialog.FileNames)
            {
                try
                {
                    var content = File.ReadAllText(filename, Encoding.UTF8);
                    fileContent.Add(content);
                    LogConsole.LogSuccess($"{filename} - загружен");
                }
                catch (Exception ex)
                {
                    LogConsole.LogError($"{filename} - ошибка - {ex.Message}");
                }
            }
        }

        return fileContent;
    }
}