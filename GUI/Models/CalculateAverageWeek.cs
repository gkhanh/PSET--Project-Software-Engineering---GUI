using System;
using System.Collections.Generic;

namespace GUI.Models
{
    class CalculateAverageWeek
    {
        List<GraphData> WeekAverageWierdenPy = new List<GraphData>();
        List<GraphData> WeekAverageSaxionPy = new List<GraphData>();
        List<GraphData> WeekAverageWierdenLHT = new List<GraphData>();
        List<GraphData> WeekAverageGronauLHT = new List<GraphData>();

        public List<GraphData> getWeekAverageWierdenPy() { return WeekAverageWierdenPy; }
        public List<GraphData> getWeekAverageSaxionPy() { return WeekAverageSaxionPy; }
        public List<GraphData> getWeekAverageWierdenLHT() { return WeekAverageWierdenLHT; }
        public List<GraphData> getWeekAverageGronauLHT() { return WeekAverageGronauLHT; }

        //-----------------------------------------------------------------------------------------------------------------------------
        // Calculate for Py
        //-----------------------------------------------------------------------------------------------------------------------------
        public void CalculateAveragePy(List<PySensor> Wierden_py, List<PySensor> Saxion_py)
        {
            int count_TemperatureWierden = 0,
                    count_TemperatureSaxion = 0,
                    count_PressureWierden = 0,
                    count_PressureSaxion = 0,
                    count_LightWierden = 0,
                    count_LightSaxion = 0;

            float sum_TemperatureWierden = 0,
                    sum_TemperatureSaxion = 0,
                    sum_PressureWierden = 0,
                    sum_PressureSaxion = 0,
                    sum_LightWierden = 0,
                    sum_LightSaxion = 0;

            //-----------------------------------------------------------------------------------------------------------------------------
            // For loop that makes sure every past 6 Weeks gets calculated
            //-----------------------------------------------------------------------------------------------------------------------------
            for (int i = 1; i < 8; i++)
            {
                //-----------------------------------------------------------------------------------------------------------------------------
                // Calculating average for Wierden PY
                //-----------------------------------------------------------------------------------------------------------------------------
                foreach (var item in Wierden_py)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_TemperatureWierden++;
                        sum_TemperatureWierden += item.Temperature;
                    }
                }
                foreach (var item in Wierden_py)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_PressureWierden++;
                        sum_PressureWierden += item.Pressure;
                    }
                }
                foreach (var item in Wierden_py)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_LightWierden++;
                        sum_LightWierden += item.Light;
                    }
                }
                //-----------------------------------------------------------------------------------------------------------------------------
                // Calculating average for Saxion PY
                //-----------------------------------------------------------------------------------------------------------------------------
                foreach (var item in Saxion_py)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_TemperatureSaxion++;
                        sum_TemperatureSaxion += item.Temperature;
                    }
                }
                foreach (var item in Saxion_py)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_PressureSaxion++;
                        sum_PressureSaxion += item.Pressure;
                    }
                }
                foreach (var item in Saxion_py)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_LightSaxion++;
                        sum_LightSaxion += item.Light;
                    }
                }

                //-----------------------------------------------------------------------------------------------------------------------------
                // Adding the average data to a list for Wierden & Saxion
                //-----------------------------------------------------------------------------------------------------------------------------
                WeekAverageWierdenPy.Add(new GraphData(sum_TemperatureWierden / count_TemperatureWierden,
                    sum_PressureWierden / count_PressureWierden,
                    0.0f,
                    sum_LightWierden / count_LightWierden,
                    "wierden-Py",
                    DateTime.Now.AddHours(-i).ToString("MM/dd/yyyy H")));

                WeekAverageSaxionPy.Add(new GraphData(sum_TemperatureSaxion / count_TemperatureSaxion,
                    sum_PressureSaxion / count_PressureSaxion,
                    0.0f,
                    sum_LightSaxion / count_LightSaxion,
                    "saxion-py",
                    DateTime.Now.AddHours(-i).ToString("MM/dd/yyyy H")));
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        // Calculate for LHT
        //-----------------------------------------------------------------------------------------------------------------------------
        public void CalculateAverageLHT(List<LhtSensor> Wierden_LHT, List<LhtSensor> Gronau_LHT)
        {
            int count_TemperatureWierden = 0,
                    count_TemperatureGronau = 0,
                    count_HumidityWierden = 0,
                    count_PressureGronau = 0,
                    count_LightWierden = 0,
                    count_LightGronau = 0;

            float sum_TemperatureWierden = 0,
                    sum_TemperatureGronau = 0,
                    sum_HumidityWierden = 0,
                    sum_PressureGronau = 0,
                    sum_LightWierden = 0,
                    sum_LightGronau = 0;

            //-----------------------------------------------------------------------------------------------------------------------------
            // For loop that makes sure every past 6 Weeks gets calculated
            //-----------------------------------------------------------------------------------------------------------------------------
            for (int i = 1; i < 8; i++)
            {
                //-----------------------------------------------------------------------------------------------------------------------------
                // Calculating average for Wierden LHT
                //-----------------------------------------------------------------------------------------------------------------------------
                foreach (var item in Wierden_LHT)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_TemperatureWierden++;
                        sum_TemperatureWierden += item.Temperature;
                    }
                }
                foreach (var item in Wierden_LHT)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_HumidityWierden++;
                        sum_HumidityWierden += item.Humidity;
                    }
                }
                foreach (var item in Wierden_LHT)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_LightWierden++;
                        sum_LightWierden += item.Light;
                    }
                }
                //-----------------------------------------------------------------------------------------------------------------------------
                // Calculating average for Saxion PY
                //-----------------------------------------------------------------------------------------------------------------------------
                foreach (var item in Gronau_LHT)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_TemperatureGronau++;
                        sum_TemperatureGronau += item.Temperature;
                    }
                }
                foreach (var item in Gronau_LHT)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_PressureGronau++;
                        sum_PressureGronau += item.Humidity;
                    }
                }
                foreach (var item in Gronau_LHT)
                {
                    if (DateTime.Now.AddHours(-i).Day == item.Time.Day)
                    {
                        count_LightGronau++;
                        sum_LightGronau += item.Light;
                    }
                }

                //-----------------------------------------------------------------------------------------------------------------------------
                // Adding the average data to a list for Wierden & Saxion
                //-----------------------------------------------------------------------------------------------------------------------------
                WeekAverageWierdenLHT.Add(new GraphData(sum_TemperatureWierden / count_TemperatureWierden,
                    0.0f,
                    sum_HumidityWierden / count_HumidityWierden,
                    sum_LightWierden / count_LightWierden,
                    "wierden-lht",
                    DateTime.Now.AddHours(-i).ToString("MM/dd/yyyy")));

                WeekAverageGronauLHT.Add(new GraphData(sum_TemperatureGronau / count_TemperatureGronau,
                    0.0f,
                    sum_PressureGronau / count_PressureGronau,
                    sum_LightGronau / count_LightGronau,
                    "gronau-lht",
                    DateTime.Now.AddHours(-i).ToString("MM/dd/yyyy")));
            }
        }
    }
}
