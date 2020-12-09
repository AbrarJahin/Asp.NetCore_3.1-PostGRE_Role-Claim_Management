using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Migrations
{
    public partial class LeaveApplicationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRole",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934") });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"));

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "XmlFiles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    FileContent = table.Column<string>(type: "text", nullable: false),
                    FileRealName = table.Column<string>(maxLength: 32767, nullable: false),
                    TableName = table.Column<int>(nullable: false),
                    DbEntryId = table.Column<long>(nullable: false),
                    IsAlreadyUsed = table.Column<bool>(nullable: false),
                    SignerId = table.Column<Guid>(nullable: true),
                    PreviousFileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XmlFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XmlFiles_XmlFiles_PreviousFileId",
                        column: x => x.PreviousFileId,
                        principalSchema: "public",
                        principalTable: "XmlFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_XmlFiles_User_SignerId",
                        column: x => x.SignerId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplications",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ApplicantId = table.Column<Guid>(nullable: true),
                    ApplicantName = table.Column<string>(maxLength: 32767, nullable: false),
                    Designation = table.Column<string>(maxLength: 32767, nullable: false),
                    LeaveStart = table.Column<DateTime>(nullable: false),
                    LeaveEnd = table.Column<DateTime>(nullable: false),
                    LeaveType = table.Column<int>(nullable: false),
                    PurposeOfLeave = table.Column<string>(maxLength: 32767, nullable: false),
                    AddressDuringLeave = table.Column<string>(maxLength: 32767, nullable: false),
                    PhoneNoDuringLeave = table.Column<string>(maxLength: 11, nullable: false),
                    ApplicationStatus = table.Column<int>(nullable: false),
                    LastSignedId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_User_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_XmlFiles_LastSignedId",
                        column: x => x.LastSignedId,
                        principalSchema: "public",
                        principalTable: "XmlFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec"), "c368e57d-87aa-49b3-a72a-26822535514b", "12/9/2020 3:35:51 PM", "Super-Admin", "SUPER-ADMIN" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "UsernameChangeLimit" },
                values: new object[] { new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), 0, "1230398c-0a79-4bad-8174-7d4f41916993", "abrar@jahin.com", true, "Abrar", "Jahin", false, null, "ABRAR@JAHIN.COM", "ABRAR", "AQAAAAEAACcQAAAAEMzCrVPdJnDVDY/Z3GJ3SKyVP/LuFiX+oqyPF//0/eFMsumquSXdRF24MlFfqB3TWQ==", null, false, null, "637431465512677133_65a02eab-34ed-4845-8bd4-4e6a92cb20a3", false, "abrar", 10 });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "RoleClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -6, "Claim_Create", "Claim.Create", new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec") },
                    { -5, "Role_Delete", "Role.Delete", new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec") },
                    { -4, "Role_Update", "Role.Update", new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec") },
                    { -3, "Role_Read", "Role.Read", new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec") },
                    { -2, "Role_Create", "Role.Create", new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec") },
                    { -1, "SuperAdmin_All", "SuperAdmin.All", new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec") }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId", "UserId1" },
                values: new object[,]
                {
                    { -6, "Claim_Create", "Claim.Create", new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), null },
                    { -5, "Role_Delete", "Role.Delete", new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), null },
                    { -4, "Role_Update", "Role.Update", new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), null },
                    { -3, "Role_Read", "Role.Read", new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), null },
                    { -2, "Role_Create", "Role.Create", new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), null },
                    { -1, "SuperAdmin_All", "SuperAdmin.All", new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), null }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId", "ReasonForAdding", "RoleId1", "UserId1" },
                values: new object[] { new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec"), "Created During Migration", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_ApplicantId",
                schema: "public",
                table: "LeaveApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_LastSignedId",
                schema: "public",
                table: "LeaveApplications",
                column: "LastSignedId");

            migrationBuilder.CreateIndex(
                name: "IX_XmlFiles_PreviousFileId",
                schema: "public",
                table: "XmlFiles",
                column: "PreviousFileId");

            migrationBuilder.CreateIndex(
                name: "IX_XmlFiles_SignerId",
                schema: "public",
                table: "XmlFiles",
                column: "SignerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveApplications",
                schema: "public");

            migrationBuilder.DropTable(
                name: "XmlFiles",
                schema: "public");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "RoleClaim",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "UserRole",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"), new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec") });

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("537af2a0-eb47-45d5-9dae-9d7ab09cdeec"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bda81396-6b95-4e10-b3f0-9f5ed69b51c9"));

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934"), "9d3a576f-a9c0-44c7-9f48-788680e06377", "12/8/2020 9:27:07 AM", "Super-Admin", "SUPER-ADMIN" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "UsernameChangeLimit" },
                values: new object[] { new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), 0, "66d8c20a-b7ad-4c25-8bae-afe301f3a72d", "abrar@jahin.com", true, "Abrar", "Jahin", false, null, "ABRAR@JAHIN.COM", "ABRAR", "AQAAAAEAACcQAAAAEIvZj1REJRr5G9ehJD0eYPfa4BxlVngQ6ZcUsitZ4PMBTwpKDeR2T6Rv2rITpwLzEg==", null, false, null, "637430380275976571_dbf7cdff-e9a1-411b-93a5-d88d5bfc65e8", false, "abrar", 10 });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "RoleClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -6, "Claim_Create", "Claim.Create", new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934") },
                    { -5, "Role_Delete", "Role.Delete", new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934") },
                    { -4, "Role_Update", "Role.Update", new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934") },
                    { -3, "Role_Read", "Role.Read", new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934") },
                    { -2, "Role_Create", "Role.Create", new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934") },
                    { -1, "SuperAdmin_All", "SuperAdmin.All", new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934") }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId", "UserId1" },
                values: new object[,]
                {
                    { -6, "Claim_Create", "Claim.Create", new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), null },
                    { -5, "Role_Delete", "Role.Delete", new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), null },
                    { -4, "Role_Update", "Role.Update", new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), null },
                    { -3, "Role_Read", "Role.Read", new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), null },
                    { -2, "Role_Create", "Role.Create", new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), null },
                    { -1, "SuperAdmin_All", "SuperAdmin.All", new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), null }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId", "ReasonForAdding", "RoleId1", "UserId1" },
                values: new object[] { new Guid("503c13fb-3f62-43ee-a42b-9beb5b678939"), new Guid("e507c211-0ff1-4a1e-a744-c0cf30e96934"), "Created During Migration", null, null });
        }
    }
}
