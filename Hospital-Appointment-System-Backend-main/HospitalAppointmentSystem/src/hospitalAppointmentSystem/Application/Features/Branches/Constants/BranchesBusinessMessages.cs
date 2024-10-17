namespace Application.Features.Branches.Constants;

public static class BranchesBusinessMessages
{
    public const string SectionName = "Branch";

    public const string BranchNotExists = "B�yle bir bran� bulunmamaktad�r";

    public static string BranchAlreadyExists = "Bu isimde bran� zaten mevcut";

    public static string CannotDeleteBranchWithDoctors = "Bu bran�a ait doktor bulunmaktad�r. Bran� silinemez.";
}