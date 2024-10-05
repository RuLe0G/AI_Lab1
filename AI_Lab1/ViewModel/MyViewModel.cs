using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using AI_Lab1.Core;

namespace AI_Lab1.ViewModel;

public class MyViewModel : BaseViewModel
{
    private readonly LoadDocument m_LoadDocument;
    private readonly TextProcessor m_TextProcessor;
    
    public ICommand LoadFilesCommand { get; }
    public ObservableCollection<string> ListBoxItems { get; }
    private string m_SelectedItem;
    public string SelectedItem
    {
        get => m_SelectedItem;
        set
        {
            m_SelectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));

            ShowData(m_SelectedItem);
        }
    }

    public MyViewModel()
    {
        m_LoadDocument = new LoadDocument();
        m_TextProcessor = new TextProcessor();

        LoadFilesCommand = new RelayCommand(LoadFiles);
        
        ListBoxItems = new ObservableCollection<string>();
    }

    private void LoadFiles()
    {
        var filesContent = m_LoadDocument.LoadFile();

        var data = m_TextProcessor.ProcessText(filesContent);
        
        ListBoxItems.Clear();
        
        foreach (var line in data)
        {
            ListBoxItems.Add(line);
        }
    }
    public void ShowData(string selectedItem)
    {
        if (!string.IsNullOrEmpty(selectedItem))
        {
            MessageBox.Show($"Данные выбранного элемента: {selectedItem}", "Информация");
        }
    }
}