using System;
using System.Collections;
using System.Collections.Generic;

public class Calendar
{

	static class Constants
	{
		public const int timeslotDuration = 10;
	}

	List<Timeslot> timeslots;

	public Calendar() {
		timeslots = new List<Timeslot>();
	}

	internal Boolean areTimeslotsAvailable(DateTime startTime, DateTime endTime) {
		int startIndex = calculateDifferenceInTimeslots(startTime, timeslots[0].getTimeslotStartTime());
		int endIndex = startIndex + calculateDifferenceInTimeslots(endTime, startTime);

		if (startIndex < timeslots.Count || endIndex < timeslots.Count) {
			return false;
		} else if (timeslots[startIndex].getExperimentOccurrence() != null) {
			return false;
		} else if (timeslots[endIndex].getExperimentOccurrence() != null){
			return false;
		}

		return true;
	}

	private int calculateDifferenceInTimeslots(DateTime timeOne, DateTime timeTwo) {
		int offset = timeOne.Minute % 10;

		if (offset != 0) {
			timeOne.AddMinutes(-1 * offset);
		}

		// assuming that the every experiment will finish within a month
		//DateTime timeTwo = timeslots[0].getTimeslotStartTime();

		// each day has 24 hours, each hour has 60 minutes
		int numTimeslotsDifference = (timeOne.DayOfYear - timeTwo.DayOfYear) * (24 * 60) / Constants.timeslotDuration;
		numTimeslotsDifference += (timeOne.Hour - timeTwo.Hour) * (60 / Constants.timeslotDuration);
		numTimeslotsDifference += (timeOne.Minute - timeTwo.Minute) / Constants.timeslotDuration;

		return numTimeslotsDifference;
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
			int numTimeslots = calculateDifferenceInTimeslots(startTime, endTime);
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
		else if (startIndex > timeslots.Count) {
			// 3
			for (int i = timeslots.Count; i < startIndex; i++) {
				Timeslot last = timeslots[timeslots.Count - 1];
				DateTime newDT = last.getTimeslotStartTime().AddMinutes(Constants.timeslotDuration);
				timeslots.Add(new Timeslot(newDT, Constants.timeslotDuration));
			}
		}


		DateTime lastTime = timeslots[timeslots.Count].getTimeslotStartTime();
		int endIndex = calculateDifferenceInTimeslots(endTime, lastTime);

		if (endIndex > timeslots.Count) {
			// 5
			for (int i = timeslots.Count; i < endIndex; i++) {
				Timeslot last = timeslots[timeslots.Count - 1];
				DateTime newDT = last.getTimeslotStartTime().AddMinutes(Constants.timeslotDuration);
				timeslots.Add(new Timeslot(newDT, Constants.timeslotDuration));
			}
		}
	}

	internal Boolean scheduleExperimentOccurrence(ExperimentOccurrence expOcc){
		DateTime startTime = expOcc.getOccurrenceStartTime();
		DateTime endTime = startTime.AddMinutes(expOcc.getExperiment().getSchedule().getOccurrenceDuration());
		createTimeSlots(startTime, endTime);

		int startIndex = calculateDifferenceInTimeslots(timeslots[0].getTimeslotStartTime(), startTime);
		int endIndex = calculateDifferenceInTimeslots(startTime, endTime);

		for (int i = startIndex; i <= endIndex; i++) {
			timeslots[i].setExperimentOccurrence(expOcc);
		}

		return true;
	}

	public void printCalendar() {

	}
}
