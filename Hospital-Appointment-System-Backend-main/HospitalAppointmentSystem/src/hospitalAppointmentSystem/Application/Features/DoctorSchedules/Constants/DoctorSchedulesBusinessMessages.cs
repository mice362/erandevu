namespace Application.Features.DoctorSchedules.Constants;

public static class DoctorSchedulesBusinessMessages
{
    public const string SectionName = "DoctorSchedule";


    public const string DoctorScheduleNotExists = "B�yle bir doktor takvimi bulunamad�";

    public const string DoctorScheduleCannotBeDeletedDueToExistingAppointments = "Bu takvim �izelgesi mevcut randevular nedeniyle silinemez.";

    public const string DoctorScheduleAlreadyExistsForThisDate = "Bu tarih i�in doktor takvim �izelgeniz zaten mevcut.";

    public const string DoctorScheduleIsSoftDeletedAndCannotBeUpdated = "B�yle bir doktor takvim �izelgesi bulunmamaktad�r.";

    public const string CheckIfAppointmentsExistOnDate = "Bu tarihe ait hastalar taraf�nda al�nm�� randevular bulunmaktad�r.Tarihi g�ncelleyemezsiniz";
}





