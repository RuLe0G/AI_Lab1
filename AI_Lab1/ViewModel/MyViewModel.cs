using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using AI_Lab1.Core;

namespace AI_Lab1.ViewModel;

public class MyViewModel : BaseViewModel
{
    private readonly LoadDocument m_LoadDocument;
    private readonly TextProcessor m_TextProcessor;

    public ObservableCollection<string> LogMessages { get; set; }
    public ICommand LoadFilesCommand { get; }
    public string LogText => string.Join(Environment.NewLine, LogMessages);


    public MyViewModel()
    {
        m_LoadDocument = new LoadDocument();
        m_TextProcessor = new TextProcessor();
        LogMessages = new ObservableCollection<string>();

        LoadFilesCommand = new RelayCommand(LoadFiles);

        LogConsole.LogAction = AddLogMessage;
    }

    private void LoadFiles()
    {
        var filesContent = m_LoadDocument.LoadFiles();

        foreach (var content in filesContent)
        {
            var processedText = m_TextProcessor.ProcessText(content);
            LogConsole.Log($"Текст обработан: {processedText.Substring(0, Math.Min(50, processedText.Length))}...");
        }
    }

    private void AddLogMessage(string message, SolidColorBrush color)
    {
        LogMessages.Add(message);
        OnPropertyChanged(nameof(LogText));
    }
}