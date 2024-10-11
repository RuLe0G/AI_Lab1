using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AI_Lab1.Core;

namespace AI_Lab1.ViewModel;

public class MyViewModel : BaseViewModel
{
    private readonly LoadDocument m_LoadDocument;
    private readonly TextProcessor m_TextProcessor;
    private readonly LinearRegressionSystem m_LinearRegression;
    
    private DrawPlotSystem m_DrawPlotSystem;

    private double m_A;
    private double m_B;

    public ICommand LoadFilesCommand { get; }
    public ObservableCollection<string> ListBoxItems { get; }
    
    private string m_InputValue;
    public string InputValue
    {
        get => m_InputValue;
        set
        {
            if (m_InputValue != value)
            {
                m_InputValue = value;
                OnPropertyChanged(nameof(InputValue));

                CalculateData();
            }
        }
    }

    private string m_ResultValue;
    public string ResultValue
    {
        get => m_ResultValue;
        private set
        {
            m_ResultValue = value;
            OnPropertyChanged(nameof(ResultValue));
        }
    }
    
    public MyViewModel(Canvas canvas)
    {
        m_LoadDocument = new LoadDocument();
        m_TextProcessor = new TextProcessor();
        m_LinearRegression = new LinearRegressionSystem();

        LoadFilesCommand = new RelayCommand(LoadFiles);

        ListBoxItems = new ObservableCollection<string>();
        m_DrawPlotSystem = new DrawPlotSystem(canvas);
    }

    private void LoadFiles()
    {
        var filesContent = m_LoadDocument.LoadFile();

        var listItems = m_TextProcessor.ProcessText(filesContent, out var dataSet);

        ListBoxItems.Clear();

        foreach (var line in listItems)
        {
            ListBoxItems.Add(line);
        }
        
        m_LinearRegression.GetData(dataSet, out m_A, out m_B);
        
        var data = dataSet.GetXYData();
        
        var maxX = data.Max(p => p.X);
        var maxY = data.Max(p => p.Y);
        m_DrawPlotSystem.SetData(data, maxX, maxY);
        m_DrawPlotSystem.SetLinaerRegressionLine(0,(m_B * 0) + m_A, maxX, (m_B * maxX) + m_A);
    }


    private void CalculateData()
    {
        if(double.TryParse(m_InputValue, CultureInfo.InvariantCulture.NumberFormat, out var inX))
        {
            var predictedValue = (m_B * inX) + m_A;

            ResultValue = predictedValue.ToString("C2");
        }
    }
}