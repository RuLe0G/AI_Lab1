namespace AI_Lab1.Core;

public class LinearRegressionSystem
{
    private void LinearRegression(double[] xValues, double[] yValues, /*out double rSquared,*/ out double a,
        out double b)
    {
        var lenght = Math.Min(xValues.Length, yValues.Length);

        double sumOfX = 0;
        double sumOfY = 0;
        double sumOfXSq = 0;
        double sumOfXY = 0;

        for (var i = 0; i < lenght; i++)
        {
            var x = xValues[i];
            var y = yValues[i];

            sumOfXY += x * y;
            sumOfX += x;
            sumOfY += y;
            sumOfXSq += x * x;
        }

        var count = lenght;
        var bDenum = sumOfXSq * count - sumOfX * sumOfX;
        var bNum = sumOfXY * count - sumOfX * sumOfY;

        var meanX = sumOfX / count;
        var meanY = sumOfY / count;

        b = bNum / bDenum;
        a = meanY - b * meanX;
    }

    public void GetData(DataSet dataSet,out double a,out double b)
    {
        double[] xValues = dataSet.GetColumn(1);
        double[] yValues = dataSet.GetColumn(2);
        
        LinearRegression(xValues, yValues, out a,out b);
    }
}