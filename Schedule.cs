using System;

public class Schedule
{

	DateTime experimentStartTime;
	int occurrenceDuration;
	int numOfOccurrences;
	int interval;

	public Schedule(DateTime experimentStartTime, int occurrenceDuration, int numOfOccurrences, int interval) {
		this.experimentStartTime = experimentStartTime;
		this.occurrenceDuration = occurrenceDuration;
		this.numOfOccurrences = numOfOccurrences;
		this.interval = interval;
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
}
