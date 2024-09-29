namespace AI_Lab1.Core;

public class TextProcessor
{
    public string ProcessText(string input)
    {
        LogConsole.Log("Начата обработка текста...");
        var processedText = input.ToLower();
        LogConsole.LogSuccess("Текст приведен к нижнему регистру");
        return processedText;
    }
}