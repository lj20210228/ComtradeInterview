using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAgents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nomination_Agent_AgentId",
                table: "Nomination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nomination",
                table: "Nomination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "Nomination",
                newName: "Nominations");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameIndex(
                name: "IX_Nomination_AgentId_NominatedAt",
                table: "Nominations",
                newName: "IX_Nominations_AgentId_NominatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nominations",
                table: "Nominations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "lazar123");

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { 2, "nikola@telekom.rs", "Agent Nikola", "nikola123" });

            migrationBuilder.AddForeignKey(
                name: "FK_Nominations_Agents_AgentId",
                table: "Nominations",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nominations_Agents_AgentId",
                table: "Nominations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nominations",
                table: "Nominations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Nominations",
                newName: "Nomination");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameIndex(
                name: "IX_Nominations_AgentId_NominatedAt",
                table: "Nomination",
                newName: "IX_Nomination_AgentId_NominatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nomination",
                table: "Nomination",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Nomination_Agent_AgentId",
                table: "Nomination",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
