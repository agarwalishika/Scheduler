using System;
using System.Collections.Generic;

namespace Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] months = {"January", "February", "March", "April", "May",
                    "June", "July", "September", "October", "November", "December"};


            List<Timeslot> ts = new List<Timeslot>();
            Console.WriteLine(ts.Count);


            /*
            DateTime now = DateTime.Now;

            DateTime beginTime = new DateTime(2020, 1, 1, 0, 0, 0);
            DateTime time = new DateTime(2020, 1, 1, 0, 10, 0);

            int numTimeslotsDifference = (time.DayOfYear - beginTime.DayOfYear) * (24 * 60) / 10;
            numTimeslotsDifference += (time.Hour - beginTime.Hour) * (60 / 10);
            numTimeslotsDifference += (time.Minute - beginTime.Minute) / 10;
            Console.WriteLine("Difference is: {0}", numTimeslotsDifference);

            Console.WriteLine("Today's date: {0}", now.Date);
            Console.WriteLine("Today is {0} day of {1}", now.Day, months[now.Month - 1]);
            Console.WriteLine("Today is {0} day of {1}", now.DayOfYear, now.Year);
            Console.WriteLine("Today's time: {0}", now.TimeOfDay);
            Console.WriteLine("Hour: {0}", now.Hour);
            Console.WriteLine("Minute: {0}", now.Minute);
            Console.WriteLine("Second: {0}", now.Second);
            Console.WriteLine("Millisecond: {0}", now.Millisecond);
            Console.WriteLine("The day of week: {0}", now.DayOfWeek);
            Console.WriteLine("Kind: {0}", now.Kind);*/
        }
    }
}
