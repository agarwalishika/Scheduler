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

	public Experiment getExperiment() {
		return experimentBlueprint;
	}
}
