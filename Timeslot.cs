using System;

public class Timeslot
{
	private ExperimentOccurrence run;
	private DateTime timeslotStartTime;
	private int duration;

	public Timeslot(DateTime timeslotStartTime, int duration)
	{
		run = null;
		this.timeslotStartTime = timeslotStartTime;
		this.duration = duration;
	}

	public DateTime getTimeslotStartTime() {
		return timeslotStartTime;
	}

	public ExperimentOccurrence getExperimentOccurrence() {
		return run;
	}

	public void setExperimentOccurrence(ExperimentOccurrence run) {
		this.run = run;
	}


}
