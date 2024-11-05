namespace Application.Features.DoctorSchedules.Constants;

public static class DoctorSchedulesBusinessMessages
{
    public const string SectionName = "DoctorSchedule";


    public const string DoctorScheduleNotExists = "Böyle bir doktor takvimi bulunamadı";

    public const string DoctorScheduleCannotBeDeletedDueToExistingAppointments = "Bu takvim çizelgesi mevcut randevular nedeniyle silinemez.";

    public const string DoctorScheduleAlreadyExistsForThisDate = "Bu tarih için doktor takvim çizelgeniz zaten mevcut.";

    public const string DoctorScheduleIsSoftDeletedAndCannotBeUpdated = "Böyle bir doktor takvim çizelgesi bulunmamaktadır.";

    public const string CheckIfAppointmentsExistOnDate = "Bu tarihe ait hastalar tarafında alınmış randevular bulunmaktadır.Tarihi güncelleyemezsiniz";
}





