using System;
using System.Collections.Generic;

namespace Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {







            Calendar cal = new Calendar();
            Console.WriteLine("ORIGINAL");
            Scheduler sch = new Scheduler();
            
            DateTime newDt = new DateTime(2020, 1, 1, 0, 20, 0);
            Experiment exp = new Experiment("annoying", new Protocol(), new Schedule(newDt, 10, 1, 0));
            sch.schedule(cal, exp);

            DateTime nwDt = new DateTime(2020, 1, 1, 0, 10, 0);
            Experiment ep = new Experiment("barf", new Protocol(), new Schedule(nwDt, 10, 1, 0));
            sch.schedule(cal, ep);

            DateTime nwD = new DateTime(2020, 1, 1, 0, 30, 0);
            Experiment p = new Experiment("yuckie", new Protocol(), new Schedule(nwD, 10, 1, 0));
            sch.schedule(cal, p);

            DateTime dt = new DateTime(2020, 1, 1, 0, 0, 0);
            Experiment e = new Experiment("two", new Protocol(), new Schedule(dt, 10, 3, 10, 2));
            sch.schedule(cal, e);


            cal.printCalendar();

            /*

            DateTime d = new DateTime(2020, 1, 1, 2, 0, 0);
            Experiment x = new Experiment("three", new Protocol(), new Schedule(d, 20, 3, 20));
            sch.schedule(cal, x);
            cal.printCalendar();*/

            /*DateTime newDt = new DateTime(2020, 1, 1, 0, 0, 0);
            Experiment exp = new Experiment("one", new Protocol(), new Schedule(newDt, 30, 3, 30));
            Scheduler sch = new Scheduler();
            sch.schedule(cal, exp);

            DateTime dt = new DateTime(2020, 1, 1, 0, 30, 0);
            Experiment e = new Experiment("two", new Protocol(), new Schedule(dt, 30, 3, 30));
            sch.schedule(cal, e);

            DateTime d = new DateTime(2020, 1, 1, 2, 0, 0);
            Experiment x = new Experiment("three", new Protocol(), new Schedule(d, 20, 3, 20));
            sch.schedule(cal, x);
            cal.printCalendar();*/
        }
    }
}
