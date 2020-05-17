using System;

public class Experiment
{

	Protocol protocols;
	Schedule recurrenceInformation;

	public Experiment(Protocol protocols, Schedule recurrenceInformation) {
		this.protocols = protocols;
		this.recurrenceInformation = recurrenceInformation;
	}

	public Schedule getSchedule() {
		return recurrenceInformation;
	}
}
