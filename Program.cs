using System;
using System.Collections.Generic;

namespace Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make smoe objects for a Calender and a Scheduler
            Calendar calendar = new Calendar();
            Scheduler scheduler = new Scheduler();

            // We are going to add one experiment here to the calendar
            // The experiment will be added on May 20th, 2020 at 12:00:00 AM
            // This experiment will be repeated twice, with 20 minutes in between
            //      and a protocol run time of 20 minutes.
            DateTime firstDate = new DateTime(2020, 5, 20, 0, 0, 0);
            Experiment firstExp = new Experiment("First", new Protocol(), new Schedule(firstDate, 20, 2, 20));
            scheduler.schedule(calendar, firstExp);

            // This is to print the calender so far
            //calendar.printCalendar();

            // Let's add another experiment
            // This experiment will be added to the same day, but 12:20 AM instead
            // It will be repeated 4 times, 20 minutes in between and a protocol
            //      run time of 20 minutes again.
            DateTime secondDate = new DateTime(2020, 5, 20, 0, 20, 0);
            Experiment secondExperiment = new Experiment("Second", new Protocol(), new Schedule(secondDate, 20, 3, 20));
            scheduler.schedule(calendar, secondExperiment);

            calendar.printCalendar();
        }
    }
}
