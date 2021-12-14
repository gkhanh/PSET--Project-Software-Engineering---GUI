using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.ViewModels
{
    interface IGraphFormat<T>
    {
        void Graph(List<T> data);
        void ShowHighestData(List<T> data, Converter<T, int> projection);
    }
}
