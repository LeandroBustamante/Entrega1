using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionLimpia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    RowIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { 1, new DateTime(2026, 5, 18, 18, 1, 43, 545, DateTimeKind.Local).AddTicks(2267), "Concierto de Rock", "Active", "Estadio Unaj" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { 1, "test@correo.com", "Usuario Test", "123456" });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 50, 1, "Platea Alta", 5000m },
                    { 2, 50, 1, "Campo", 3000m }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("01486c5c-b129-41c5-b625-42e24ba37f67"), "B", 48, 2, "Available", 1 },
                    { new Guid("077a5578-5204-451d-9abe-baa3f3906ffe"), "B", 38, 2, "Available", 1 },
                    { new Guid("09f72cea-2663-4969-a08d-42812d9a2c09"), "B", 44, 2, "Available", 1 },
                    { new Guid("14634540-d1f5-4878-ac92-f45fa249492a"), "A", 42, 1, "Available", 1 },
                    { new Guid("14f3657f-03a9-4be3-bac5-f949d029f06b"), "A", 24, 1, "Available", 1 },
                    { new Guid("17b0f494-bdbf-4400-bfeb-45d2834d2574"), "A", 30, 1, "Available", 1 },
                    { new Guid("190db2a5-beb7-4a55-86e5-18e8d7bc4d64"), "A", 20, 1, "Available", 1 },
                    { new Guid("19f78b9d-f6b8-4427-b8e2-5e007810782f"), "B", 33, 2, "Available", 1 },
                    { new Guid("1ddb89c1-3345-4fa0-a831-a7ec4b47c693"), "B", 27, 2, "Available", 1 },
                    { new Guid("24b3c8d8-d4e5-4ebc-a4bf-ec51bd462d8b"), "B", 16, 2, "Available", 1 },
                    { new Guid("27e3eeda-d6fe-40d0-8657-a5a379408edb"), "A", 9, 1, "Available", 1 },
                    { new Guid("2cf102ef-0af8-4aa8-b039-6e9f14765ab9"), "B", 11, 2, "Available", 1 },
                    { new Guid("2e6fd0dd-faad-41c6-9888-2c144f491260"), "A", 18, 1, "Available", 1 },
                    { new Guid("2ed316ed-9525-41b5-9029-9e7869be9612"), "B", 7, 2, "Available", 1 },
                    { new Guid("305e8b90-5231-42cc-8ec3-6055e559118d"), "A", 44, 1, "Available", 1 },
                    { new Guid("30d25dc9-e29e-4df0-a1a7-dda2c47c2ae4"), "B", 3, 2, "Available", 1 },
                    { new Guid("35665c30-f20e-4306-9186-dd6c77c3112a"), "B", 29, 2, "Available", 1 },
                    { new Guid("39043e5e-d506-4329-907f-2d2d1e485691"), "B", 13, 2, "Available", 1 },
                    { new Guid("3bd487f1-d043-45ba-aa7a-6000321127bf"), "A", 37, 1, "Available", 1 },
                    { new Guid("3c861ffa-7a6f-451e-abf7-ab03b656a091"), "A", 39, 1, "Available", 1 },
                    { new Guid("3e07e5ee-87f6-42a6-80c9-ccfc7cbd8ac5"), "B", 25, 2, "Available", 1 },
                    { new Guid("416a93c7-9b59-4321-ab9d-f8e2ebda6488"), "A", 17, 1, "Available", 1 },
                    { new Guid("4263e4a4-9a05-41b5-9643-4a27b453fe37"), "B", 21, 2, "Available", 1 },
                    { new Guid("42d7504b-5fb2-4b4a-9886-6e0e6014834d"), "A", 5, 1, "Available", 1 },
                    { new Guid("44acbbe5-756a-445b-80d7-482602b0a1b1"), "B", 24, 2, "Available", 1 },
                    { new Guid("4570bece-e072-4c68-8890-c4527854b4ad"), "B", 34, 2, "Available", 1 },
                    { new Guid("46af2c48-0618-4341-8254-c33aaf5ad55c"), "A", 19, 1, "Available", 1 },
                    { new Guid("47e31df8-b82e-4b74-960f-1b3200ff5ef0"), "B", 5, 2, "Available", 1 },
                    { new Guid("4b32b8c1-c774-4541-878b-ce37fa87ab45"), "A", 2, 1, "Available", 1 },
                    { new Guid("4e2a01dd-e730-45b7-b558-c45f8598188b"), "A", 50, 1, "Available", 1 },
                    { new Guid("4f3adde3-c4d1-4939-a927-6999fa8d5446"), "B", 9, 2, "Available", 1 },
                    { new Guid("50745917-bf53-4ea4-babc-56e6b1ad6e1b"), "A", 41, 1, "Available", 1 },
                    { new Guid("542aefd4-0466-4064-8ed8-0dec46f7a915"), "B", 43, 2, "Available", 1 },
                    { new Guid("557a0845-5441-4c3f-9738-10c181b760d8"), "B", 22, 2, "Available", 1 },
                    { new Guid("56264dbb-92ab-457b-b893-d77108f30c5d"), "A", 26, 1, "Available", 1 },
                    { new Guid("582a289c-9d96-4c05-b4f9-610261134d12"), "A", 48, 1, "Available", 1 },
                    { new Guid("59c53937-6a16-4b5e-9ffb-975155640b5c"), "A", 38, 1, "Available", 1 },
                    { new Guid("59fffa66-bc6d-4fbc-b847-745e94856667"), "A", 43, 1, "Available", 1 },
                    { new Guid("5ea73ac2-d9a6-499d-845c-100b4714e6a1"), "A", 40, 1, "Available", 1 },
                    { new Guid("6014ed4d-6220-4150-b7a2-64363a3a2525"), "B", 20, 2, "Available", 1 },
                    { new Guid("606ac38f-1f0a-4e09-91ec-7f8d3db78fa6"), "B", 42, 2, "Available", 1 },
                    { new Guid("631e3323-465c-41a4-b017-3d92294c1dc2"), "B", 15, 2, "Available", 1 },
                    { new Guid("63acbd05-7497-4736-abb5-8fa90645d881"), "B", 2, 2, "Available", 1 },
                    { new Guid("67ac3d18-963f-4449-9fae-29478711d38e"), "A", 46, 1, "Available", 1 },
                    { new Guid("67d84a49-c25d-41e2-bc59-ab4fc2badd49"), "A", 12, 1, "Available", 1 },
                    { new Guid("6a93ca8d-6826-42f6-9397-57207d508c43"), "B", 46, 2, "Available", 1 },
                    { new Guid("6b3b8ac6-4214-4898-be66-fcd79b9bcb64"), "A", 34, 1, "Available", 1 },
                    { new Guid("6ce8cdde-97df-4a59-bc89-d97e51ca9d29"), "B", 35, 2, "Available", 1 },
                    { new Guid("74a3fd50-601f-4506-bb28-1e16cbb186e7"), "B", 32, 2, "Available", 1 },
                    { new Guid("74c37db6-84c2-4c63-b8d1-8354359a0e2d"), "B", 10, 2, "Available", 1 },
                    { new Guid("755f26fa-42ce-42a0-b1ca-2503e6b16880"), "B", 1, 2, "Available", 1 },
                    { new Guid("7c8584c5-6b26-453e-9589-5a9f6eb52e26"), "B", 4, 2, "Available", 1 },
                    { new Guid("80ecca70-84eb-4518-acdd-a2eb0e8fb488"), "A", 21, 1, "Available", 1 },
                    { new Guid("81fafcad-2c17-4073-a480-5bce596f7e4e"), "B", 37, 2, "Available", 1 },
                    { new Guid("82e78be8-d97b-42c5-a9e9-8da0833949f3"), "A", 36, 1, "Available", 1 },
                    { new Guid("8434c750-cf5f-4c3e-8537-23eecd2f710e"), "B", 40, 2, "Available", 1 },
                    { new Guid("89542451-a0e8-48e3-b9de-5f9ec1828014"), "B", 50, 2, "Available", 1 },
                    { new Guid("89d81ffc-6612-4cb3-b90b-2be510cd5b3e"), "A", 13, 1, "Available", 1 },
                    { new Guid("95578892-198d-4b25-ac75-e31a4db95085"), "A", 15, 1, "Available", 1 },
                    { new Guid("9ed5a9dc-cd62-42e1-b688-4de841af5f02"), "A", 27, 1, "Available", 1 },
                    { new Guid("a3246d9a-c86b-43bc-bed6-49820fecc00c"), "A", 32, 1, "Available", 1 },
                    { new Guid("a4da2ba1-2a99-49dd-9211-e8ecc3796e68"), "A", 28, 1, "Available", 1 },
                    { new Guid("a60070fd-1dc6-469b-bc2a-a2d5dd77f39b"), "A", 49, 1, "Available", 1 },
                    { new Guid("a680351e-b881-482a-9e0f-85c63e4d5283"), "A", 45, 1, "Available", 1 },
                    { new Guid("a9b2c7ae-bba6-44bd-8c2c-88a233fe806a"), "B", 47, 2, "Available", 1 },
                    { new Guid("ac0a9568-b1eb-4451-a228-f885a3d4facc"), "A", 23, 1, "Available", 1 },
                    { new Guid("b03bda26-ef81-41ba-8edf-e8821ea8aa45"), "B", 17, 2, "Available", 1 },
                    { new Guid("b262271f-e918-489c-bc83-eb8dc2220d5e"), "A", 4, 1, "Available", 1 },
                    { new Guid("b478e39a-fd27-4638-8d42-effd82b7ff58"), "B", 39, 2, "Available", 1 },
                    { new Guid("b642a175-5cc2-4d73-9fc6-877cb6405da5"), "A", 47, 1, "Available", 1 },
                    { new Guid("b682d3da-87f3-4c09-953f-2d03faad3ff3"), "A", 11, 1, "Available", 1 },
                    { new Guid("b8804dec-694d-49a8-8f0b-904ee27adf99"), "A", 10, 1, "Available", 1 },
                    { new Guid("baa17206-85e2-484b-9455-822dc4273d96"), "A", 7, 1, "Available", 1 },
                    { new Guid("baac7554-a2c4-4228-af77-9bba2c08bcbe"), "A", 6, 1, "Available", 1 },
                    { new Guid("bb3e824a-0f16-492f-b77f-8eda305f3f5d"), "B", 6, 2, "Available", 1 },
                    { new Guid("bea3dc0e-ad54-4021-b129-83497de7fdc0"), "B", 12, 2, "Available", 1 },
                    { new Guid("c0c85095-6ccf-4f05-9bf2-51708e0c9b5a"), "A", 8, 1, "Available", 1 },
                    { new Guid("c0ef9ecd-709b-4dc2-acf2-c09a4e7a3feb"), "A", 3, 1, "Available", 1 },
                    { new Guid("c14eaadf-6dfb-4528-9faf-bb2f304359c8"), "A", 14, 1, "Available", 1 },
                    { new Guid("c4f38e7b-ebda-47a9-9304-58ed97ab6077"), "A", 16, 1, "Available", 1 },
                    { new Guid("c57ed211-dd84-4253-8445-da0c531c8ce1"), "B", 23, 2, "Available", 1 },
                    { new Guid("c98571ab-08bb-4778-9711-c32a036c7367"), "B", 14, 2, "Available", 1 },
                    { new Guid("cc44e621-3f25-47d5-95b2-11953824b7f9"), "A", 33, 1, "Available", 1 },
                    { new Guid("d383a0cf-f118-47d1-a120-15c434732841"), "A", 25, 1, "Available", 1 },
                    { new Guid("d3e0b795-e54e-478d-ae22-1890422a9530"), "B", 31, 2, "Available", 1 },
                    { new Guid("d60cc695-53bb-4444-98fd-855cb3dd70e4"), "A", 29, 1, "Available", 1 },
                    { new Guid("d680511a-673e-4c6c-8573-41e898ba23ce"), "A", 35, 1, "Available", 1 },
                    { new Guid("d92b90ed-7c9e-4a1a-b11e-329158cc492a"), "B", 41, 2, "Available", 1 },
                    { new Guid("d9b7fd64-d5c5-45f6-8147-14d4d442e605"), "B", 30, 2, "Available", 1 },
                    { new Guid("dcd1cdae-7fe1-44f0-91f3-1f4c99198439"), "B", 49, 2, "Available", 1 },
                    { new Guid("e111c30c-71f9-4818-bb6b-fb47d07dbaa2"), "B", 8, 2, "Available", 1 },
                    { new Guid("e3e42ce2-9008-4aaf-b3af-96606289ea13"), "A", 1, 1, "Available", 1 },
                    { new Guid("e66bd073-19ed-48d8-b669-d7739838426c"), "B", 26, 2, "Available", 1 },
                    { new Guid("e956b94f-6e3d-42b8-8dcc-637f757edf1e"), "A", 22, 1, "Available", 1 },
                    { new Guid("ea11b925-3002-4b46-a7b8-b064535cb236"), "B", 19, 2, "Available", 1 },
                    { new Guid("f017bb60-1687-413f-b755-99fea73ebbca"), "A", 31, 1, "Available", 1 },
                    { new Guid("f104cacc-3c07-4254-8191-40c96b53c57d"), "B", 28, 2, "Available", 1 },
                    { new Guid("f3d751e4-2e24-4b5b-868d-ec9b1e8d9ec3"), "B", 36, 2, "Available", 1 },
                    { new Guid("fdd0f465-c118-4fd5-8efe-86cb77c175b2"), "B", 45, 2, "Available", 1 },
                    { new Guid("fe163b0d-9706-4445-9f75-bfecb4bd4186"), "B", 18, 2, "Available", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatId",
                table: "Reservations",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectorId",
                table: "Seats",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_EventId",
                table: "Sectors",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
