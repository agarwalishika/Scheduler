using System;

public class Experiment
{

	Protocol protocols;
	Schedule recurrenceInformation;
	string name;

	public Experiment(String name, Protocol protocols, Schedule recurrenceInformation) {
		this.name = name;
		this.protocols = protocols;
		this.recurrenceInformation = recurrenceInformation;
	}

	public Schedule getSchedule() {
		return recurrenceInformation;
	}

	public string getName() {
		return name;
	}
}
