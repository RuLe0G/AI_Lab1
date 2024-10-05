using System.IO;
using Microsoft.Win32;

namespace AI_Lab1.Core;

public class LoadDocument
{
    public string LoadFile()
    {
        var openFileDialog = new OpenFileDialog
        {
            Multiselect = false,
            Filter = "Text files (*.csv)|*.csv",
            InitialDirectory = Path.Combine(Environment.CurrentDirectory, "Resources")
        };

        var fileContent = string.Empty;

        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                fileContent = File.ReadAllText(openFileDialog.FileName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        return fileContent;
    }

}