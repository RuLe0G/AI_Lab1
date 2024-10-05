using System.Globalization;

namespace AI_Lab1.Core;

public class TextProcessor
{
    public List<string> ProcessText(string data)
    {
        var DSlb = new List<string>();
        var dataSet = new DataSet();

        var lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var headers = lines[0].Split(',').ToList();

        var tmp = string.Empty;
        var headsLength = 0;

        for (var i = 0; i < headers.Count; i++)
        {
            tmp += headers[i].Substring(0, Math.Min(10, headers[i].Length)) + "\t";
        }

        headsLength = tmp.Length;
        DSlb.Add(tmp);

        for (var lineIndex = 1; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            var values = line.Split(',').ToList();

            dataSet.AddEntry(values);
            tmp = string.Empty;

            for (var i = 0; i < values.Count; i++)
            {
                if (double.TryParse(values[i], NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    var stringValue = value.ToString("0.0");

                    for (var j = stringValue.Length; j < headsLength / headers.Count; j++)
                    {
                        stringValue += " ";
                    }

                    tmp += stringValue + "\t";
                }
            }

            DSlb.Add(tmp);
        }
        return DSlb;
    }
}

internal class DataSetEntry
{
    public readonly List<double> Data = new();

    public DataSetEntry(List<string> values)
    {
        foreach (var value in values)
        {
            Data.Add(double.Parse(value, CultureInfo.InvariantCulture.NumberFormat));
        }
    }
}

internal class DataSet
{
    public readonly List<DataSetEntry> DataSetTable = new();

    public void AddEntry(List<string> values)
    {
        DataSetTable.Add(new DataSetEntry(values));
    }

    public double[] GetColumn(int num)
    {
        var column = new double[DataSetTable.Count];

        for (var i = 0; i < column.Length; i++)
        {
            column[i] = DataSetTable[i].Data[num];
        }

        return column;
    }
}