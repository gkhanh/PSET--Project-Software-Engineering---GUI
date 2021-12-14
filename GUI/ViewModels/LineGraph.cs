using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.ViewModels
{
    class LineGraph<T>: IGraphFormat<T>
    {
        public void Graph(List<T> data)
        {
            Console.WriteLine("Draw line graph here!");
        }
        public void ShowHighestData(List<T> data, Converter<T, int> projection)
        {
            if (data.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int maxValue = int.MinValue;
            foreach (T item in data)
            {
                int value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            //return maxValue;
        }

        public void ShowHighestData(List<T> data, Converter<T, T> projection)
        {
            throw new NotImplementedException();
        }
    }
}
