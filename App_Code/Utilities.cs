﻿using System;
using System.Collections.Generic;
using System.Web;

namespace App_Code.Utility
{
    public class Utilities
    {
        public Utilities()
        {

        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private int epoch;
        public int Epoch
        {
            get { return epoch; }
            set { epoch = value; }
        }

        private int sampleCount = 1;
        public int SampleCount
        {
            get { return sampleCount; }
            set { sampleCount = value; }
        }
    }

    public static class Utilitie_S
    {
        public static Utilities EpochToDateTime(int unixTime)
        {
            Utilities ep = new Utilities();
            
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0,0);
            dateTime = dateTime.AddSeconds(unixTime);

            ep.Date = dateTime;
            return ep;
        }

        public static Utilities DateTimeToEpoch(DateTime time)
        {
            Utilities ep = new Utilities();
            DateTime epoch = new DateTime(1970, 1, 1);

            ep.Epoch = Convert.ToInt32(time.Subtract(epoch).TotalSeconds);
            return ep;
        }

        public static int Sampler(int fromTime, int toTime)
        {
            int sample = 1;
            int interval = toTime - fromTime;
            if (interval > 20)
            {
                sample = interval / 20;
            }
            else
            {
                sample = interval;
            }
            return sample;
        }

        public static IEnumerable<DateTime> GetHours(this DateTime date)
        {
            date = date.Date; // truncate hours
            for (int i = 0; i < 24; i++)
            {
                yield return date.AddHours(i);
            }
        }
        public static IEnumerable<DateTime> AllDatesInMonth(this DateTime date)
        {
            date = date.Date;
            int year = date.Year;
            int month = date.Month;
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }
        public static IEnumerable<DateTime> NextSevenDays(this DateTime date)
        {
            date = date.Date;
            for (int i = 0; i < 8; i++)
            {
                yield return date;

                date = date.AddDays(1);
            }

        }

        public static void Return_Line_Time(string plotType, out int fromTime, out int toTime)
        {
            DateTime frTime = DateTime.Today;
            DateTime tTime = DateTime.Now;
            int day = tTime.Day;
            int month = tTime.Month;
            int year = tTime.Year;

            if (plotType == "LNT")
            {
                tTime = new DateTime(year, month, day, 7, 0, 0);
                frTime = tTime.Add(new TimeSpan(-12, 0, 0));
            }
            if (plotType == "LDY")
            {
                tTime = new DateTime(year, month, day, 0, 0, 1);
                frTime = tTime.Add(new TimeSpan(-24, 0, 0));
            }
            if (plotType == "SIXHR")
            {
                tTime = DateTime.Now;
                frTime = tTime.Add(new TimeSpan(-6, 0, 0));
            }
            if (plotType == "LWK")
            {
                tTime = DateTime.Now;
                frTime = tTime.Add(new TimeSpan(-6, 0, 0, 1));
            }
            if (plotType == "THMNT")
            {
                frTime = new DateTime(year, month, 1, 0, 0, 1);
                tTime = DateTime.Now;
            }
            if (plotType == "LMNTH")
            {
                if (month > 1)
                {
                    frTime = new DateTime(year, month - 1, 1, 0, 0, 1);
                }
                else
                {
                    frTime = new DateTime(year - 1, 12, 1, 0, 0, 1);
                }
                tTime = new DateTime(year, month, 1, 0, 0, 1);
            }
            if (plotType == "THYR")
            {
                frTime = new DateTime(year, 1, 1, 0 , 0, 1);
                tTime = DateTime.Now;
            }

            Utilities ut1 = DateTimeToEpoch(frTime);
            fromTime = ut1.Epoch;
            Utilities ut2 = DateTimeToEpoch(tTime);
            toTime = ut2.Epoch;            
        }

        public static List<int> Return_Bar_Time(DateTime fromTime, string comparisonType)
        {
            
            if (comparisonType == "DTD")        // will return epochs of all days coming between two dates for day to day comparisons
            {
                List<int> allDays = new List<int>();
                int year = fromTime.Year;
                int month = fromTime.Month;
                foreach (DateTime date in fromTime.AllDatesInMonth())
                {
                    Utilities ut = DateTimeToEpoch(date);
                    int ep = ut.Epoch;
                    ep = ep + 1;
                    allDays.Add(ep);
                }
                return allDays;
            }
            if (comparisonType == "HBH")        // will return epochs for all hours coming between two dates for hour by hour comparisons
            {
                List<int> allHours = new List<int>();
               
                foreach (DateTime time in fromTime.GetHours())
                {
                    Utilities ut = DateTimeToEpoch(time);
                    int ep = ut.Epoch;
                    ep = ep + 1;
                    allHours.Add(ep);
                }
                return allHours;
            }
            if (comparisonType == "7Days")        //Week comparisons
            {
                List<int> sevenDays = new List<int>();
                foreach (DateTime time in fromTime.NextSevenDays())
                {
                    Utilities ut = DateTimeToEpoch(time);
                    int ep = ut.Epoch;
                    ep = ep + 1;
                    sevenDays.Add(ep); 
                }
                return sevenDays;
            }
            if (comparisonType == "WKND")
            {
                List<int> weekends = new List<int>();
                foreach (DateTime date in fromTime.AllDatesInMonth())
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        Utilities ut = DateTimeToEpoch(date);
                        int ep = ut.Epoch;
                        ep = ep + 1;
                        weekends.Add(ep);
                    }
                }
                return weekends;
            }
            if (comparisonType == "WKDY")
            {
                List<int> weekdays = new List<int>();
                foreach (DateTime date in fromTime.AllDatesInMonth())
                {
                    if (date.DayOfWeek.ToString() != DayOfWeek.Saturday.ToString() || date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        Utilities ut = DateTimeToEpoch(date);
                        int ep = ut.Epoch;
                        ep = ep + 1;
                        weekdays.Add(ep);
                    }
                }
                return weekdays;
            }

            return null;
        }

        public static List<int> LastDashMonths(int lastMonths)
        {
            List<int> epochs = new List<int>();
            List<DateTime> timeListing = new List<DateTime>();

            DateTime now = DateTime.Today.AddDays(-1);
            timeListing.Add(now);
            DateTime newTime = new DateTime();

            int day = now.Day;
            int month = now.Month;
            int year = now.Year;

            newTime=new DateTime(year,month,1,0,0,1);
            timeListing.Add(newTime);                   //this month is added

            

            for (int i = 0; i < lastMonths - 1;i++ )
            {
                if (month == 1)
                {
                    month = 12;
                    year = year - 1;
                }
                else
                {
                    month=month-1;
                }
                newTime = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
                timeListing.Add(newTime);
                newTime = new DateTime(year, month, 1, 0, 0, 1);
                timeListing.Add(newTime);

            }

            for (int j = 0; j < timeListing.Count; j++)
            {
                Utilities ut = DateTimeToEpoch(timeListing[j]);
                epochs.Add(ut.Epoch);
            }

            return epochs;
        }

        public static List<int> DashDaysEpochs(int fromtime, int numberOfDashs, int dashDays)
        {
            List<int> epochs = new List<int>();

            epochs.Add(fromtime);
            for (int i = 1; i < numberOfDashs; i++)
            {
                int ep = fromtime + (i *dashDays *86400);
                epochs.Add(ep);
            }

            return epochs;
        }

        public static string [] TimeFormatterBar(int [] epochs)
        {
            string [] timeSeries = new string[epochs.Length];
            int timeDiff = 0;
            if (epochs.Length > 1)
            {
                timeDiff = epochs[1] - epochs[0];

                if (timeDiff < 100000)
                {
                    Utilities ut = Utilitie_S.EpochToDateTime(epochs[0]);
                    timeSeries[0] = (ut.Date.ToString("dd MMM HH:mm"));

                    for (int ep=1;ep<epochs.Length;ep++)
                    {
                        ut=Utilitie_S.EpochToDateTime(epochs[ep]);
                        timeSeries[ep] = (ut.Date.ToString("dd MMM HH:mm"));
                    }
                }

                if ( (timeDiff>100000 && timeDiff < 2600000) || (timeDiff > 2600000 && timeDiff < 31100000))
                {//days and months
                    Utilities ut = Utilitie_S.EpochToDateTime(epochs[0]);
                    timeSeries[0]=(ut.Date.ToString("dd MMM yyyy"));

                    for (int ep = 1; ep < epochs.Length; ep++)
                    {
                        ut = Utilitie_S.EpochToDateTime(epochs[ep]);
                        timeSeries[ep]=(ut.Date.ToString("dd MMM"));
                    }

                }

                if (timeDiff > 31100000)
                {
                    Utilities ut = Utilitie_S.EpochToDateTime(epochs[0]);
                    timeSeries[0] = (ut.Date.ToString("MMM yyyy"));

                    for (int ep = 1; ep < epochs.Length; ep++)
                    {
                        ut = Utilitie_S.EpochToDateTime(epochs[ep]);
                        timeSeries[ep]=(ut.Date.ToString("MMM yyyy"));
                    }
                }

            }
            return timeSeries;
        }

        public static string[] TimeFormatter(int[] epochs)
        {
            string[] timeSeries = new string[epochs.Length - 1];
            int timeDiff = 0;
            if (epochs.Length > 1)
            {
                timeDiff = epochs[1] - epochs[0];

                if (timeDiff < 100000)
                {
                    Utilities ut1 = Utilitie_S.EpochToDateTime(epochs[0]);
                    Utilities ut2 = Utilitie_S.EpochToDateTime(epochs[1]);


                    timeSeries[0] = (ut1.Date.ToString("dd MMM HH:mm") + " - " + ut2.Date.ToString("dd MMM HH:mm"));

                    for (int ep = 1; ep < epochs.Length - 1; ep++)
                    {
                        ut1 = Utilitie_S.EpochToDateTime(epochs[ep]);
                        ut2 = Utilitie_S.EpochToDateTime(epochs[ep + 1]);
                        timeSeries[ep] = (ut1.Date.ToString("dd MMM HH:mm") + " - " + ut2.Date.ToString("dd MMM HH:mm"));
                    }
                }

                if ((timeDiff > 100000 && timeDiff < 2600000) || (timeDiff > 2600000 && timeDiff < 31100000))
                {//days and months
                    Utilities ut1 = Utilitie_S.EpochToDateTime(epochs[0]);
                    Utilities ut2 = Utilitie_S.EpochToDateTime(epochs[1]);

                    timeSeries[0] = (ut1.Date.ToString("dd MMM") + " - " + ut2.Date.ToString("dd MMM yyyy"));

                    for (int ep = 1; ep < epochs.Length - 1; ep++)
                    {
                        ut1 = Utilitie_S.EpochToDateTime(epochs[ep]);
                        ut2 = Utilitie_S.EpochToDateTime(epochs[ep + 1]);
                        timeSeries[ep] = (ut1.Date.ToString("dd MMM") + " - " + ut2.Date.ToString("dd MMM"));
                    }

                }

                if (timeDiff > 31100000)
                {
                    Utilities ut1 = Utilitie_S.EpochToDateTime(epochs[0]);
                    Utilities ut2 = Utilitie_S.EpochToDateTime(epochs[1]);
                    timeSeries[0] = (ut1.Date.ToString("MMM yyyy") + " - " + ut2.Date.ToString("MMM yyyy"));

                    for (int ep = 1; ep < epochs.Length - 1; ep++)
                    {
                        ut1 = Utilitie_S.EpochToDateTime(epochs[ep]);
                        ut2 = Utilitie_S.EpochToDateTime(epochs[ep + 1]);
                        timeSeries[ep] = (ut1.Date.ToString("MMM yyyy") + " - " + ut2.Date.ToString("MMM yyyy"));
                    }
                }

            }
            return timeSeries;
        }

    } 
}