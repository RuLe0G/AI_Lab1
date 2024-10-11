using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AI_Lab1.Core;

public class DrawPlotSystem
{
    private readonly Canvas m_Canvas;
    private (double X, double Y)[] m_Data;
    private double m_MaxX, m_MaxY;

    private readonly double m_PaddingLeft = 40;
    private readonly double m_PaddingBottom = 40;

    public DrawPlotSystem(Canvas canvas)
    {
        m_Canvas = canvas;
    }

    public void SetData((double X, double Y)[] data, double maxX, double maxY)
    {
        m_Data = data;
        m_MaxX = Math.Ceiling(maxX / 5) * 5;
        ;
        m_MaxY = Math.Ceiling(maxY / 5) * 5;
        ;

        DrawGraph(maxX, maxY, data);
    }

    public void SetLinaerRegressionLine(double a1, double b1, double a2, double b2)
    {
        
        var canvasWidth = m_Canvas.Width;
        var canvasHeight = m_Canvas.Height;
        
        var graphWidth = canvasWidth - m_PaddingLeft;
        var graphHeight = canvasHeight - m_PaddingBottom;
        
        
        var x1 = m_PaddingLeft + a1 / m_MaxX * graphWidth;
        var y1 = graphHeight - b1 / m_MaxY * graphHeight;
        
        var x2 = m_PaddingLeft + a2 / m_MaxX * graphWidth;
        var y2 = graphHeight - b2 / m_MaxY * graphHeight;


        DrawLine(x1, y1, x2, y2, Colors.Blue);
    }

    private void DrawGraph(double maxX, double maxY, (double X, double Y)[] data)
    {
        m_Canvas.Children.Clear();

        var canvasWidth = m_Canvas.Width;
        var canvasHeight = m_Canvas.Height;

        var graphWidth = canvasWidth - m_PaddingLeft;
        var graphHeight = canvasHeight - m_PaddingBottom;

        var stepX = graphWidth / 5;
        var stepY = graphHeight / 5;

        DrawLine(m_PaddingLeft, 0, m_PaddingLeft, graphHeight, Colors.Black);
        DrawLine(m_PaddingLeft, graphHeight, canvasWidth, graphHeight, Colors.Black);

        for (var i = 0; i <= 5; i++)
        {
            var xPos = m_PaddingLeft + i * stepX;
            DrawLine(xPos, graphHeight, xPos, graphHeight + 5, Colors.Black);
            AddText((i * maxX / 5).ToString(), xPos - 10, graphHeight + 10);

            var yPos = graphHeight - i * stepY;
            DrawLine(m_PaddingLeft - 5, yPos, m_PaddingLeft, yPos, Colors.Black);
            AddText((i * maxY / 5).ToString(), m_PaddingLeft - 35, yPos - 10);
        }

        foreach (var point in data)
        {
            var x = m_PaddingLeft + point.X / maxX * graphWidth;
            var y = graphHeight - point.Y / maxY * graphHeight;
            DrawEllipse(x, y, 5, Colors.Red);
        }
    }

    private void DrawLine(double x1, double y1, double x2, double y2, Color color)
    {
        var line = new Line
        {
            X1 = x1,
            Y1 = y1,
            X2 = x2,
            Y2 = y2,
            Stroke = new SolidColorBrush(color),
            StrokeThickness = 2
        };
        m_Canvas.Children.Add(line);
    }

    private void DrawEllipse(double x, double y, double radius, Color color)
    {
        var ellipse = new Ellipse
        {
            Width = radius * 2,
            Height = radius * 2,
            Fill = new SolidColorBrush(color)
        };

        Canvas.SetLeft(ellipse, x - radius);
        Canvas.SetTop(ellipse, y - radius);
        m_Canvas.Children.Add(ellipse);
    }

    private void AddText(string text, double x, double y)
    {
        var textBlock = new TextBlock
        {
            Text = text,
            Foreground = Brushes.Black
        };

        Canvas.SetLeft(textBlock, x);
        Canvas.SetTop(textBlock, y);
        m_Canvas.Children.Add(textBlock);
    }
}