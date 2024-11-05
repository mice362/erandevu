namespace Application.Features.Branches.Constants;

public static class BranchesBusinessMessages
{
    public const string SectionName = "Branch";

    public const string BranchNotExists = "Böyle bir branş bulunmamaktadır";

    public static string BranchAlreadyExists = "Bu isimde branş zaten mevcut";

    public static string CannotDeleteBranchWithDoctors = "Bu branşa ait doktor bulunmaktadır. Branş silinemez.";
}