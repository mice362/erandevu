using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LogoveLogoAdıIsOrtagı : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("b952603f-411c-4368-b5ff-877d64a63af0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a7977895-8beb-4be6-8c5c-323081d7adc2"));

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Clinics",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "LogoName",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BusinessPartners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    LogoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPartners", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("dc84555e-9c96-4073-8cf6-8257497cf894"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 95, 216, 6, 193, 16, 197, 6, 156, 151, 162, 203, 211, 182, 196, 226, 150, 77, 85, 134, 36, 85, 163, 122, 140, 232, 113, 4, 12, 151, 144, 197, 96, 62, 188, 251, 194, 198, 210, 254, 191, 113, 107, 177, 52, 254, 205, 71, 112, 99, 58, 223, 154, 10, 200, 4, 27, 169, 3, 155, 43, 17, 186, 31, 126 }, new byte[] { 237, 142, 102, 86, 55, 215, 117, 209, 105, 113, 132, 67, 20, 43, 112, 43, 211, 111, 88, 134, 194, 24, 64, 87, 81, 125, 1, 60, 65, 192, 4, 22, 198, 116, 197, 137, 191, 61, 191, 216, 196, 99, 93, 58, 99, 32, 40, 54, 74, 119, 243, 65, 230, 114, 230, 234, 242, 2, 53, 179, 209, 159, 182, 19, 205, 93, 195, 40, 51, 14, 226, 246, 129, 58, 31, 17, 170, 60, 7, 129, 216, 237, 60, 233, 168, 135, 70, 146, 40, 116, 135, 201, 210, 23, 93, 191, 141, 33, 121, 172, 130, 229, 111, 31, 24, 92, 61, 57, 71, 180, 96, 4, 120, 6, 189, 251, 228, 232, 211, 67, 152, 152, 83, 98, 216, 229, 246, 185 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("93ad0407-4ced-4072-9f2f-4cf8dd50cd69"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("dc84555e-9c96-4073-8cf6-8257497cf894") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessPartners");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("93ad0407-4ced-4072-9f2f-4cf8dd50cd69"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dc84555e-9c96-4073-8cf6-8257497cf894"));

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "LogoName",
                table: "Clinics");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CreatedDate", "DateOfBirth", "DeletedDate", "Email", "FirstName", "LastName", "NationalIdentity", "PasswordHash", "PasswordSalt", "Phone", "UpdatedDate" },
                values: new object[] { new Guid("a7977895-8beb-4be6-8c5c-323081d7adc2"), "Tekirdağ", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 11, 20), null, "fatmabireltr@gmail.com", "Fatma", "Birel", "12345678901", new byte[] { 25, 230, 38, 177, 13, 95, 87, 233, 181, 91, 252, 71, 20, 35, 181, 162, 202, 251, 170, 133, 209, 134, 64, 18, 200, 206, 30, 97, 7, 225, 98, 179, 0, 29, 139, 57, 255, 237, 89, 246, 119, 48, 20, 94, 108, 15, 223, 120, 22, 10, 25, 253, 233, 65, 128, 218, 52, 49, 197, 97, 69, 228, 74, 180 }, new byte[] { 135, 183, 252, 198, 62, 8, 6, 98, 55, 164, 124, 224, 56, 107, 44, 116, 24, 167, 216, 51, 63, 231, 72, 30, 129, 98, 152, 215, 153, 130, 203, 0, 103, 164, 240, 127, 114, 222, 114, 58, 227, 231, 91, 13, 112, 74, 194, 63, 130, 9, 142, 73, 132, 150, 127, 190, 72, 175, 34, 83, 23, 172, 220, 11, 210, 154, 199, 36, 86, 32, 204, 72, 46, 249, 66, 126, 209, 147, 249, 147, 123, 107, 55, 217, 142, 204, 146, 248, 110, 246, 175, 171, 3, 4, 233, 245, 56, 150, 247, 105, 94, 40, 190, 142, 115, 224, 151, 202, 57, 41, 105, 159, 58, 123, 62, 136, 224, 234, 133, 74, 134, 69, 207, 181, 113, 182, 211, 1 }, "05279563492", null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("b952603f-411c-4368-b5ff-877d64a63af0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("a7977895-8beb-4be6-8c5c-323081d7adc2") });
        }
    }
}
