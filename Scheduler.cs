using System;

public class Scheduler
{
	public Scheduler() {
	}

	public Boolean schedule(Calendar cal, Experiment exp)
    {
		// make all the occurrences
		// see if the timeslots are available
		ExperimentOccurrence[] expOccs = new ExperimentOccurrence[exp.getSchedule().getNumOfOccurrences()];
		Schedule expSch = exp.getSchedule();
		for (int i = 0; i < expOccs.Length; i++) {
			DateTime newDt = expSch.getExperimentStartTime().AddMinutes(expSch.getInterval());
			DateTime newEnd = newDt.AddMinutes(expSch.getOccurrenceDuration());
			expOccs[i] = new ExperimentOccurrence(exp, newDt);
			if (!cal.areTimeslotsAvailable(newDt, newEnd)) {
				return false;
			}
		}

		for (int i = 0; i < expOccs.Length; i++) {
			cal.scheduleExperimentOccurrence(expOccs[i]);
		}

		return true;
    }
}
