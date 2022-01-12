using System;
using System.Collections.Generic;

namespace GUI.Models
{
    class CalculateAverageWeek
    {
        private List<GraphData> WeekAverageWierdenPy = new List<GraphData>();
        private List<GraphData> WeekAverageSaxionPy = new List<GraphData>();
        private List<GraphData> WeekAverageWierdenLHT = new List<GraphData>();
        private List<GraphData> WeekAverageGronauLHT = new List<GraphData>();

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

            var avg_TempWierden = 0.0f;
            var avg_PresWierden = 0.0f;
            var avg_LightWierden = 0.0f;

            var avg_TempSaxion = 0.0f;
            var avg_PresSaxion = 0.0f;
            var avg_LightSaxion = 0.0f;

            //-----------------------------------------------------------------------------------------------------------------------------
            // For loop that makes sure every past 6 Days gets calculated
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

                if (sum_LightWierden == 0 || count_LightWierden == 0)
                {
                    avg_LightWierden = 0;
                }
                else
                {
                    avg_LightWierden = sum_LightWierden / count_LightWierden;
                }

                if (sum_TemperatureWierden == 0 || count_TemperatureWierden == 0)
                {
                    avg_TempWierden = 0;
                }
                else
                {
                    avg_TempWierden = sum_TemperatureWierden / count_TemperatureWierden;
                }

                if (sum_PressureWierden == 0 || count_PressureWierden == 0)
                {
                    avg_PresWierden = 0;
                }
                else
                {
                    avg_PresWierden = sum_PressureWierden / count_PressureWierden;
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (sum_LightSaxion == 0 || count_LightSaxion == 0)
                {
                    avg_LightSaxion = 0;
                }
                else
                {
                    avg_LightSaxion = sum_LightSaxion / count_LightSaxion;
                }

                if (sum_TemperatureSaxion == 0 || count_TemperatureSaxion == 0)
                {
                    avg_TempSaxion = 0;
                }
                else
                {
                    avg_TempSaxion = sum_TemperatureSaxion / count_TemperatureSaxion;
                }

                if (sum_PressureSaxion == 0 || count_PressureSaxion == 0)
                {
                    avg_PresSaxion = 0;
                }
                else
                {
                    avg_PresSaxion = sum_PressureSaxion / count_PressureSaxion;
                }

                //-----------------------------------------------------------------------------------------------------------------------------
                // Adding the average data to a list for Wierden & Saxion
                //-----------------------------------------------------------------------------------------------------------------------------
                WeekAverageWierdenPy.Add(new GraphData(avg_TempWierden,
                    avg_PresWierden,
                    0.0f,
                    avg_LightWierden,
                    "wierden-Py",
                    DateTime.Now.AddHours(-i).ToString("MM/dd/yyyy H")));

                WeekAverageSaxionPy.Add(new GraphData(avg_TempSaxion,
                    avg_PresSaxion,
                    0.0f,
                    avg_LightWierden,
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
                    count_HumidityGronau = 0,
                    count_LightWierden = 0,
                    count_LightGronau = 0;

            float sum_TemperatureWierden = 0,
                    sum_TemperatureGronau = 0,
                    sum_HumidityWierden = 0,
                    sum_HumidityGronau = 0,
                    sum_LightWierden = 0,
                    sum_LightGronau = 0;

            var avg_TempWierden = 0.0f;
            var avg_HumWierden = 0.0f;
            var avg_LightWierden = 0.0f;

            var avg_TempGronau = 0.0f;
            var avg_HumGronau = 0.0f;
            var avg_LightGronau = 0.0f;

            //-----------------------------------------------------------------------------------------------------------------------------
            // For loop that makes sure every past 6 Days gets calculated
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
                        count_HumidityGronau++;
                        sum_HumidityGronau += item.Humidity;
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

                if (sum_LightWierden == 0 || count_LightWierden == 0)
                {
                    avg_LightWierden = 0;
                }
                else
                {
                    avg_LightWierden = sum_LightWierden / count_LightWierden;
                }

                if (sum_TemperatureWierden == 0 || count_TemperatureWierden == 0)
                {
                    avg_TempWierden = 0;
                }
                else
                {
                    avg_TempWierden = sum_TemperatureWierden / count_TemperatureWierden;
                }

                if (sum_HumidityWierden == 0 || count_HumidityWierden == 0)
                {
                    avg_HumWierden = 0;
                }
                else
                {
                    avg_HumWierden = sum_HumidityWierden / count_HumidityWierden;
                }

                ///////////////////////////////////////////////////////////////////////////////

                if (sum_LightGronau == 0 || count_LightGronau == 0)
                {
                    avg_LightGronau = 0;
                }
                else
                {
                    avg_LightGronau = sum_LightGronau / count_LightGronau;
                }

                if (sum_TemperatureGronau == 0 || count_TemperatureGronau == 0)
                {
                    avg_TempGronau = 0;
                }
                else
                {
                    avg_TempGronau = sum_TemperatureGronau / count_TemperatureGronau;
                }

                if (sum_HumidityGronau == 0 || count_HumidityGronau == 0)
                {
                    avg_HumGronau = 0;
                }
                else
                {
                    avg_HumGronau = sum_HumidityGronau / count_HumidityGronau;
                }

                //-----------------------------------------------------------------------------------------------------------------------------
                // Adding the average data to a list for Wierden & Saxion
                //-----------------------------------------------------------------------------------------------------------------------------
                WeekAverageWierdenLHT.Add(new GraphData(avg_TempWierden,
                    0.0f,
                    avg_HumWierden,
                    avg_LightWierden,
                    "wierden-lht",
                    DateTime.Now.AddHours(-i).ToString("MM/dd/yyyy")));

                WeekAverageGronauLHT.Add(new GraphData(avg_TempGronau,
                    0.0f,
                    avg_HumGronau,
                    avg_LightGronau,
                    "gronau-lht",
                    DateTime.Now.AddHours(-i).ToString("MM/dd/yyyy")));
            }
        }
    }
}
