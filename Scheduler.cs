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

		int y = expSch.getOccurrenceDuration();
		int z = expSch.getInterval();

		for (int i = 0; i < expOccs.Length; i++) {
			DateTime newStart = expSch.getExperimentStartTime().AddMinutes((i * y) + (i * z));
			DateTime newEnd = expSch.getExperimentStartTime().AddMinutes(((i + 1) * y) + (i * z));
			expOccs[i] = new ExperimentOccurrence(exp, newStart);
			if (!cal.areTimeslotsAvailable(newStart, newEnd)) {
				DateTime[] times = checkTolerance(cal, expOccs[i], newStart, newEnd);
				if (times == null) {
					return false;
				}
				expOccs[i].setOccurrenceStartTime(times[0]);
			}
		}

		for (int i = 0; i < expOccs.Length; i++) {
			cal.scheduleExperimentOccurrence(expOccs[i]);
		}

		return true;
    }

	private DateTime[] checkTolerance(Calendar cal, ExperimentOccurrence expOcc, DateTime origStart, DateTime origEnd) {
		Schedule sch = expOcc.getExperiment().getSchedule();
		int tolTimeslots = sch.getToleranceTimeslots();
		int tsTime = Calendar.Constants.timeslotDuration;

		for (int i = 1; i <= tolTimeslots; i++) {
			DateTime olderStart = origStart.AddMinutes(-1 * i * tsTime);
			DateTime olderEnd = origEnd.AddMinutes(-1 * i * tsTime);
			if (cal.areTimeslotsAvailable(olderStart, olderEnd)) {
				return new DateTime[] { olderStart };
			}

			DateTime newerStart = origStart.AddMinutes(i * tsTime);
			DateTime newerEnd = origEnd.AddMinutes(i * tsTime);
			if (cal.areTimeslotsAvailable(newerStart, newerEnd)) {
				return new DateTime[] { newerStart };
			}
		}

		return null;
	}
}
