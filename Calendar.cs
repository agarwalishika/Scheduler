using System;
using System.Collections;
using System.Collections.Generic;

public class Calendar
{

	public static class Constants
	{
		// 60 should be divisible by the time slot duration!!
		public const int timeslotDuration = 10;
	}

	List<Timeslot> timeslots;

	public Calendar() {
		timeslots = new List<Timeslot>();
	}

	internal int numExperimentsInTimeInterval(DateTime startTime, DateTime endTime) {
		if (timeslots.Count == 0) {
			return 0;
		}

		int startindex = calculateDifferenceInTimeslots(startTime, timeslots[0].getTimeslotStartTime());
		int endIndex = calculateDifferenceInTimeslots(timeslots[timeslots.Count - 1].getTimeslotStartTime(), endTime);

		if ((startindex < 0 && endIndex < 0)
			|| (startindex >= timeslots.Count && endIndex >= timeslots.Count)){
			return 0;
		}

		int numExperiments = 0;
		for (int i = startindex; i <= endIndex; i++) {
			Schedule sch = timeslots[i].getExperimentOccurrence().getExperiment().getSchedule();
			int totalMins = sch.getNumOfOccurrences();
			DateTime expEndTime = sch.getExperimentStartTime().AddMinutes(totalMins);
		}


		return 0;
	}

	internal Boolean areTimeslotsAvailable(DateTime startTime, DateTime endTime) {
		if (timeslots.Count == 0) {
			return true;
		}

		int startIndex = calculateDifferenceInTimeslots(startTime, timeslots[0].getTimeslotStartTime());
		int endIndex = calculateDifferenceInTimeslots(endTime, timeslots[0].getTimeslotStartTime());

		if ((startIndex >= timeslots.Count && endIndex >= timeslots.Count)
			|| (startIndex < 0 && endIndex <= 0)
			|| (startIndex == 0 && endIndex == -1)) {
			return true;
		}

		if (startIndex < 0) {
			startIndex = 0;
		} if (endIndex > timeslots.Count - 1) {
			endIndex = timeslots.Count - 1;
		}

		for (int i = startIndex; i < endIndex; i++) {
			if (timeslots[i].getExperimentOccurrence() != null) {
				return false;
			}
		}

		return true;
	}

	int calculateDifferenceInTimeslots(DateTime timeTwo, DateTime timeOne) {
		/*if (timeTwo.CompareTo(timeOne) < 0) {
			DateTime t = timeTwo;
			timeTwo = timeOne;
			timeOne = t;
		}*/

		int offset = timeTwo.Minute % Constants.timeslotDuration;

		//if (offset != 0) {
			timeTwo = timeTwo.AddMinutes(Constants.timeslotDuration - offset);
		//}

		offset = timeOne.Minute % Constants.timeslotDuration;

		//if (offset != 0) {
			timeOne = timeOne.AddMinutes(-1 * offset);
		//}

		TimeSpan span = timeTwo.Subtract(timeOne);
		
		return Convert.ToInt32(span.TotalMinutes / Constants.timeslotDuration) - 1;
	}

	private void createTimeSlots2(DateTime startTime, DateTime endTime) {
		/*
		 * other cases:
		 * 1. start can be before first ts
		 *		a. end can be in bewteen first and last ts
		 *			- create in front
		 *		b. end can be after the last ts
		 *			- create in front and at the end
		 *		c. end can be before first ts
		 *			- create in front
		 * 2. start can be after last ts
		 *		d. end is also after last ts
		 *			- create only after
		 * 3. start can be in between first and last ts
		 *		e. end can be in between first and last ts
		 *			- create none
		 *		f. end can be after last ts
		 *			- create after
		 *	
		 */


		DateTime firstTime = timeslots[0].getTimeslotStartTime();
		DateTime lastTime = timeslots[timeslots.Count - 1].getTimeslotStartTime();

		int startIndex = calculateDifferenceInTimeslots(startTime, firstTime);
		int endIndex = calculateDifferenceInTimeslots(endTime, lastTime);

		if (startIndex < 0) {
			//1
			if (endIndex >= 0 && endIndex < timeslots.Count) {
				//a
			} else if (endIndex > timeslots.Count) {
				//b
			} else if (endIndex < 0) {
				//c
			}
		}
		else if (startIndex > timeslots.Count) {
			//2d (end is also after last ts)
		}
		else if (startIndex >= 0 && startIndex < timeslots.Count) {
			//3
			if (endIndex >= 0 && endIndex < timeslots.Count) {
				//e
			}
			else if (endIndex > timeslots.Count) {
				//f
			}
		}
		
	}

	private void createTimeSlots(DateTime startTime, DateTime endTime) {

		/*
		 * Start:
		 * 1. Start is between first ts and last ts
		 *		- do nothing
		 * 2. Start is before first ts
		 *		- create timeslots before
		 * 3. Start is after the last ts
		 *		- create timeslots after
		 *
		 * End:
		 * 4. Start is between first ts and last ts
		 *		- do nothing
		 * 5. End is after last ts
		 *		- create timeslots after
		 */

		if (timeslots.Count == 0) {
			int numTimeslots = calculateDifferenceInTimeslots(endTime, startTime);
			for (int i = 0; i < numTimeslots; i++) {
				DateTime newDT = startTime.AddMinutes(i * Constants.timeslotDuration);
				timeslots.Add(new Timeslot(newDT, Constants.timeslotDuration));
			}
			return;
		}

		DateTime firstTime = timeslots[0].getTimeslotStartTime();
		int startIndex = calculateDifferenceInTimeslots(startTime, firstTime);

		if (startIndex < 0) {
			// 2
			for (int i = startIndex; i < 0; i++) {
				DateTime newDT = timeslots[0].getTimeslotStartTime().AddMinutes(-1 * Constants.timeslotDuration);
				timeslots.Insert(0, new Timeslot(newDT, Constants.timeslotDuration));
			}
		}


		DateTime lastTime = timeslots[timeslots.Count - 1].getTimeslotStartTime();
		int endIndex = timeslots.Count - 1 + calculateDifferenceInTimeslots(endTime, lastTime);

		if (endIndex > timeslots.Count) {
			// 5
			for (int i = timeslots.Count; i < endIndex; i++) {
				Timeslot last = timeslots[timeslots.Count - 1];
				DateTime newDT = last.getTimeslotStartTime().AddMinutes(Constants.timeslotDuration);
				timeslots.Add(new Timeslot(newDT, Constants.timeslotDuration));
			}
		}
	}

	internal void scheduleExperimentOccurrence(ExperimentOccurrence expOcc){
		DateTime startTime = expOcc.getOccurrenceStartTime();
		DateTime endTime = startTime.AddMinutes(expOcc.getExperiment().getSchedule().getOccurrenceDuration());
		createTimeSlots(startTime, endTime);


		int startIndex = calculateDifferenceInTimeslots(startTime, timeslots[0].getTimeslotStartTime());
		int endIndex = calculateDifferenceInTimeslots(endTime, timeslots[0].getTimeslotStartTime());

		for (int i = startIndex; i < endIndex; i++) {
			timeslots[i].setExperimentOccurrence(expOcc);
		}
	}

	public void printCalendar() {
		for(int i = 0; i < timeslots.Count; i++) {
			string dateStr = timeslots[i].getTimeslotStartTime().ToString("dd/MM/yyyy HH:mm:ss");
			Console.WriteLine("Timeslot start time: {0}", dateStr);
			if (timeslots[i].getExperimentOccurrence() == null) {
				Console.WriteLine("		Experiment name:");
			} else {
				Console.WriteLine("		Experiment name: {0}", timeslots[i].getExperimentOccurrence().getExperiment().getName());
			}
			Console.WriteLine("");
		}

	}
}
