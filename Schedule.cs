using System;

public class Schedule
{

	DateTime experimentStartTime;
	int occurrenceDuration; //in minutes
	int numOfOccurrences; //in number of times
	int interval; //in minutes, for example - run every 30 mins for 6 hours, the interval would be 30
	int toleranceTimeslots; // in timeslots

	public Schedule(DateTime experimentStartTime, int occurrenceDuration, int numOfOccurrences, int interval) {
		this.experimentStartTime = experimentStartTime;
		this.occurrenceDuration = occurrenceDuration;
		this.numOfOccurrences = numOfOccurrences;
		this.interval = interval;
	}

	public Schedule(DateTime experimentStartTime, int occurrenceDuration, int numOfOccurrences, int interval, int toleranceTime) {
		this.experimentStartTime = experimentStartTime;
		this.occurrenceDuration = occurrenceDuration;
		this.numOfOccurrences = numOfOccurrences;
		this.interval = interval;
		this.toleranceTimeslots = toleranceTime;
	}

	public int getOccurrenceDuration() {
		return occurrenceDuration;
	}

	public int getNumOfOccurrences() {
		return numOfOccurrences;
	}

	public int getInterval() {
		return interval;
	}

	public DateTime getExperimentStartTime() {
		return experimentStartTime;
	}

	public int getToleranceTimeslots() {
		return toleranceTimeslots;
	}
}
