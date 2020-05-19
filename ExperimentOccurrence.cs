using System;

public class ExperimentOccurrence
{
	Experiment experimentBlueprint;
	DateTime occurrenceStartTime;

	public ExperimentOccurrence(Experiment experimentBlueprint, DateTime occurrenceStartTime)
	{
		this.experimentBlueprint = experimentBlueprint;
		this.occurrenceStartTime = occurrenceStartTime;
	}

	public DateTime getOccurrenceStartTime() {
		return occurrenceStartTime;
	}

	public void setOccurrenceStartTime(DateTime occurrenceStartTime) {
		this.occurrenceStartTime = occurrenceStartTime;
	}

	public Experiment getExperiment() {
		return experimentBlueprint;
	}
}
