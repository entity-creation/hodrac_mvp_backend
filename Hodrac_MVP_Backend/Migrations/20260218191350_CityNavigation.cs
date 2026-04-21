using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hodrac_MVP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CityNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_City_Countries_CityId", table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_DestinationCities_City_CityId",
                table: "DestinationCities"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_City", table: "City");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("009b909e-829e-483b-a24e-818beda12c5c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("01a8671b-ec40-48d4-9259-5595ccfd537c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("02e85773-e791-4685-a043-20141f6fe48e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("072b0b50-b474-406e-8de6-6e21ca42c49c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("0969a63b-881d-4a4f-b4af-40eb64933218")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("0ccb123c-7a0d-4ffc-8614-137a8d0739cc")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("0d8511f5-8136-47c8-9d61-bbdf168f6803")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("162ec2f0-b7bb-4fab-b8a8-be998e7a79a2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("16e3c394-9486-4543-a919-3455a59d0cce")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("17a90c16-7850-4f5d-ac69-0ff0168c2a40")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("182d54bd-943f-4e5d-a743-ed21d4393326")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("19a47393-1bf1-4c92-966f-d7fd83ef82c3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("1ad1d234-65ba-4f5c-a724-576cc73ed2f2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("1c84e4bd-4b3c-453f-a502-1d496763f1ac")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("1e939af9-ab04-44d9-beff-e69eed27f7ce")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("23cfdace-207c-4ebf-b876-8ee4a5f14b23")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("26b5b8a6-46c9-45ef-9d36-5ac92304f52f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2768921d-570b-4af7-a413-cda99435eea1")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2936d03d-fa31-4ce8-9c95-a3e1015b2a8c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2b08d03d-4efa-4d1b-8873-76aa0f0adcd2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("341224f5-f74a-40dd-83c4-fcc36ca83d1f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("36bf985e-c569-469b-bd72-de3616fe706b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("39931ae3-deba-43ee-a448-e20181c8e76a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3bb8dc11-d2a8-41dd-95cf-5b0492e6eda4")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3bc56107-d777-4035-9df2-a0f745f74899")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3cb019ac-b82a-48a6-ad6c-a7f2f2353923")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("45fd39aa-d9dc-416d-b5df-809f41a692f5")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("46b157f4-f6e2-4208-b710-6cc5131f001d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("4a92fb57-8ef9-41a8-8b0b-e93d2f3f2f34")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("4b02dde5-b942-4872-8478-8bd120e06ace")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("4bc6f917-0b5f-499f-b556-68316cd682a8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("4dee3394-bbc7-4ed0-9541-7c265d43f7ec")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("514afbbd-37fd-4d20-bff4-a0574a67127d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("515582ec-6cee-410b-b4d8-55b888dccf89")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("539d2e24-fc8a-4b61-8788-a6be445adcf8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("543cbd15-aa33-452f-90b0-81a340a916b0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5620f9a3-6f2c-4089-947e-192a044a8db7")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("56d407f1-1bc0-4a73-a988-c8bd0c2666b6")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5810fdb6-316b-4974-841f-28df01e8115a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("582645f6-bff3-4f13-aa0a-9190bf2090c4")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("58e65cff-af0d-4750-932f-04ac9203b202")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5db9c67b-cbf3-4943-9ae6-5db1a9cd95d3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5f6bc256-5996-4c84-a3da-62c5cbeb4cfa")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5f9b3002-15e5-42a9-b5bf-98fb66276925")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("62fccf19-1d43-462a-9768-00cd9f326fec")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("65ff9b9c-c6f6-457a-a3bf-510d9b6c0e1b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("663613d4-14ac-4d78-8bca-08fb49e314eb")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("67904680-0c9b-4417-997e-e45f51c8c618")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6826088a-bd79-44f3-826d-34391aa59f44")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("687db95a-2000-4c51-9d70-41fef88169cf")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6cd829fd-629f-4de8-9afd-6985496c4051")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6d57f9a1-abd7-4f07-94dc-a4aecaad6fec")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6d9ebdf3-130a-4539-9f01-2b153d4b4e28")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6e5821e3-7acc-4c76-81f2-d05468a42e0f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7284fb11-fc82-490b-b8be-f73b0398ccde")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("74ea65eb-ed99-437b-8f61-5c5c9d79edda")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("772c4036-d0e7-49ee-b5c6-e9c29d13b8f0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("77f3b37a-2745-4c54-8fa9-39b00d2ab34f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("79e0ccaf-0dd3-4685-bae9-6e871469a166")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7fcb1776-b96a-4872-b600-d898a49b3d25")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8130e7eb-3971-4ff4-aa01-3b58dad382cf")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("816894d2-91c2-45d2-a201-724e5e694e83")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("834796dc-2f71-4b10-9096-bc48951940a5")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8b51f7ed-9441-4cd1-911c-6b177546ea3c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8dfef08f-a18a-4a7e-b9d2-9360e37dc804")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8e76d742-f96a-41fe-815d-e0df13b7af4e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8e8bee65-ee49-490e-bce6-3b4dea52964c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8ef81c00-dc64-450c-aa0b-b12682bc0777")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8fe5deaf-d519-4e34-b830-c869024773ba")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9025e5b8-f0d3-47dc-a8b6-08406c0f81cb")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("931249e8-a620-4ad4-9628-3d9ec96019f3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("93456d8b-86fe-4219-9448-05e0178b47d5")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("98e96c89-9875-44b0-a565-fb8c07c4afaf")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9c4b564d-7288-4771-9d68-beccd42cab57")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9e3615a6-5576-40c1-a877-614f64440727")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9ecc72d2-a48b-4f4c-9d4e-6e19fadfd130")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a173504f-be00-4982-be88-4a50d9271c24")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a6eba4d0-41ec-4318-a4e0-6caea6e5d44a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a815e0e0-1809-454a-9840-db3a87678646")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a966aec5-e74b-46ff-90b4-027f259e19a6")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ae018ced-a132-4027-a5af-ad0a32528b9e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b2ddf799-146a-4cc6-9eba-97cd702682a6")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b41e7e8c-275c-400b-8114-7c00246078f7")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b48c2526-db17-4569-a679-29250b4dcebc")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b5531560-d7ed-4765-b435-0d43ea8b3b8a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b94059a8-cdf7-418e-b0d2-a47e7a42512e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("bbfcd81a-ed5e-4b87-a0d3-876726160ddf")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("bd52bdbe-c4bb-4449-9e52-6169c9304d3b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c35571b9-65be-4a85-aadc-1b578ef1a736")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c390f2a9-9120-473d-bf12-937ed076bfe0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c3e535ad-8b05-4ce6-a302-fdb830605ea3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c7d339b2-63bb-4a70-9727-032ca4c987eb")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ccb8a8fa-0c84-4e51-8274-78c5e3f207d0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ced40db0-363f-4f93-a01f-91b30175c70e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d0ffc022-b927-4e63-b987-b85e02ee0cd9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d43315a9-b5f0-472a-a59a-4952395fcb21")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d6544404-3f7b-4ae8-bb7d-e0398c761e0e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d80d04e2-c425-4291-9c78-2e4b53e4c8cb")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d900f2dc-8017-4de3-a64c-a9c112628457")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d9864a21-5957-4bb3-8f55-ebc35914bce8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("db6ef275-14d8-40ec-a0bd-6ee9be77ae9b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("dca56cfa-838c-49ba-b88f-74ef5c048f33")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e57e78df-6192-423c-9d3f-913ca66701b2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e716b847-432c-4d44-803e-dc1700f18144")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e8637d8a-eee1-46ae-bb87-9f1c1fa74109")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("eb357206-83f2-412a-89db-cb2645321562")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f3707b69-47c4-44f8-b462-cbcad60b15c3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f537c46f-89a4-4b25-91f0-68a375118db0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f6756ae1-fade-422b-837b-e1aae84d152d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f6d0acba-f8dd-41ef-913d-765fdf871e4c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("fb99a084-a3d3-4d0e-8f2f-3a5dd6536759")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ffe2c1b6-ebdb-4b47-bca7-072509dff172")
            );

            migrationBuilder.RenameTable(name: "City", newName: "Cities");

            migrationBuilder.AddPrimaryKey(name: "PK_Cities", table: "Cities", column: "CityId");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "LanguageName" },
                values: new object[,]
                {
                    { new Guid("00d1d0ba-28df-417c-b586-dabed832fd63"), "Urdu" },
                    { new Guid("04621fff-1499-49fc-87f2-a5fb6f00982e"), "Greek" },
                    { new Guid("04956d76-f163-4097-ae47-a789c1312f78"), "Yiddish" },
                    { new Guid("0ca38f07-32c7-4dfb-b3fc-9b19847a4069"), "Turkish" },
                    { new Guid("0f93255e-1961-4b67-ad82-557f82e08682"), "Indonesian" },
                    { new Guid("13c5827b-84e6-49de-b63a-5bd8679e2d83"), "English" },
                    { new Guid("13f9c72b-a447-44a6-9fed-7936a18d0c61"), "Gujarati" },
                    { new Guid("167f69e6-96d0-4861-b31c-08f2fadfde9f"), "Latvian" },
                    { new Guid("16b9e2d9-b10f-49d6-9af7-ea7e1708902c"), "Frisian" },
                    { new Guid("16f8bbdd-edc6-47c4-938d-8bd733e3211c"), "Sundanese" },
                    { new Guid("183fc9cb-3b11-4e65-9765-a54831e3db48"), "Amharic" },
                    { new Guid("1920dd53-903b-4c4c-b07e-61147f45ffbd"), "Cebuano" },
                    { new Guid("252c2c9e-1621-49db-8c98-aef84073c8cf"), "Czech" },
                    { new Guid("268b2d6d-c594-4254-868e-b03a46a8ad8a"), "Danish" },
                    { new Guid("2cefb843-9d27-4b22-9c8b-f51c9abb2de4"), "Slovenian" },
                    { new Guid("2d4d978a-6698-4e5d-8f91-ee1b7519c243"), "Vietnamese" },
                    { new Guid("3567b431-947d-4856-b8e9-7cf85e99a7c2"), "Belarusian" },
                    { new Guid("35be57bb-c8d3-46df-8922-6c0be9caf5e3"), "Hindi" },
                    { new Guid("368620c4-9560-4a41-9e4b-912aa689f6e9"), "Chinese (Cantonese)" },
                    { new Guid("37fb9e1a-74b4-4897-9e16-ab3ea80e6722"), "Igbo" },
                    { new Guid("385d889d-b6d1-4129-ba88-216f61d6b214"), "Mongolian" },
                    { new Guid("3b91ddde-d594-41fd-9fe8-5fdc12e25c79"), "Norwegian" },
                    { new Guid("3bfa246e-559e-47da-bec3-51bf02872675"), "Korean" },
                    { new Guid("3d91639b-5cf5-4a8b-8783-fe09f76b5810"), "Bengali" },
                    { new Guid("40fb67db-76d6-4166-9141-f8d332448f18"), "Samoan" },
                    { new Guid("44f1de5e-796f-4d1d-907c-156a772376b5"), "Punjabi" },
                    { new Guid("4f5b852a-f77e-4d0c-be0f-29017a39cc8c"), "Swahili" },
                    { new Guid("51ed2ab6-a9df-4008-88c3-ba92ba4b3dd3"), "Sinhala" },
                    { new Guid("55f1ff80-1afa-49b5-ab5f-22f81fe4daf2"), "Xhosa" },
                    { new Guid("57710848-fbfe-4a14-bdde-d0a5f1b648b7"), "Bulgarian" },
                    { new Guid("5b1c4a26-d170-4946-abae-f72aa1885c25"), "Dari" },
                    { new Guid("60a1888d-00c6-4b8d-95b3-c32705eb1d1c"), "Filipino" },
                    { new Guid("636a9c34-819e-4c94-b46b-bdc740e42e9b"), "Nepali" },
                    { new Guid("6566c7b8-ad31-46a6-a59f-abecf19fdb9b"), "Basque" },
                    { new Guid("65c1c4ab-df62-4e09-8b53-d311c93aceba"), "Pashto" },
                    { new Guid("6624a2a6-0d1e-42ba-b22f-67e273cde2b2"), "Esperanto" },
                    { new Guid("67b9aa89-d6c2-4109-b458-dc1e34582479"), "Bosnian" },
                    { new Guid("6b339651-0e74-437c-830c-56e12e2d413a"), "Kazakh" },
                    { new Guid("6b5893dc-3353-4447-9c9e-7f8c49f89e17"), "Marathi" },
                    { new Guid("6b6ba918-c645-4d25-87c9-91ed2525f713"), "Luxembourgish" },
                    { new Guid("6e14209d-fc86-4729-9a1c-61b6efd42f22"), "Icelandic" },
                    { new Guid("6fd07b7d-60c9-4b09-a46f-a7a07130c4ca"), "Sindhi" },
                    { new Guid("733b8a9b-d37f-4562-84c4-c7fc1971484d"), "Tajik" },
                    { new Guid("774cd54e-9f09-4813-a7fc-7cba3f89abfc"), "Uzbek" },
                    { new Guid("7ba278f9-df36-4b64-8364-8382f21df366"), "Malay" },
                    { new Guid("7d03694e-40db-409f-8ccc-83425d6c091b"), "Telugu" },
                    { new Guid("7d1eeee7-bacf-4495-af0f-2cd0d7952c58"), "Assamese" },
                    { new Guid("803e502e-bd54-4e4a-8d54-c6467502c314"), "Maltese" },
                    { new Guid("84952a9d-a2b9-4c00-8450-becb223d17ce"), "Romanian" },
                    { new Guid("853a11b2-d0e3-4d2f-aa1a-e30b0494af67"), "Catalan" },
                    { new Guid("85a65f66-c25f-46e5-9598-5fec6b57e626"), "Kinyarwanda" },
                    { new Guid("86b504ca-9d9d-4d17-b3e8-b1cf6fe00a02"), "Hawaiian" },
                    { new Guid("86c866df-c981-413a-97fe-dffdc6f531fb"), "Kannada" },
                    { new Guid("8a185d8a-5332-4980-bf0c-e43414b009ec"), "Finnish" },
                    { new Guid("8ad59034-c060-4a7f-8b0d-e4375af0a443"), "Hausa" },
                    { new Guid("8c074c9d-36f2-4dba-80c5-74556e624674"), "Dutch" },
                    { new Guid("8c439f2d-a8b5-4f0c-af48-aa180821491e"), "Lao" },
                    { new Guid("91663350-d3a2-4916-89d6-80aaee11cdd3"), "Chichewa" },
                    { new Guid("926bfc9b-5050-4085-8fdc-4f4282a1b33a"), "Ukrainian" },
                    { new Guid("9300051e-5562-4c20-80c4-11cc2326e644"), "Somali" },
                    { new Guid("9430d583-faaf-4b91-acb1-5b697d7ee95c"), "Hebrew" },
                    { new Guid("9582f7e4-1680-4dd2-8d1f-c005f63c469b"), "Polish" },
                    { new Guid("9a953e42-a2c9-4cba-b709-0614fa676978"), "German" },
                    { new Guid("9f948337-fcd2-449f-9cff-4c8c5c590540"), "Chinese (Mandarin)" },
                    { new Guid("a00f7e80-9c94-49e4-8fa7-c134d0d66858"), "Fijian" },
                    { new Guid("a204ffcd-cf3c-41a9-8dc7-5d33eab609e6"), "Swedish" },
                    { new Guid("a20c5e5c-0d2b-47ee-b64b-e081deedbbe1"), "Portuguese" },
                    { new Guid("a2e524cd-acb5-4ec7-becc-c502df09571d"), "Italian" },
                    { new Guid("a39cb1f7-ee0d-4c77-ab0a-46b66d6cd84c"), "Georgian" },
                    { new Guid("a77cad8e-3b23-4be0-ad0b-6cadbe0890a2"), "Khmer" },
                    { new Guid("a7d18ade-d543-40d3-99e4-05034cd8554f"), "Yoruba" },
                    { new Guid("a9adcb03-7d1c-4829-a764-eb2cbfea0d88"), "Arabic" },
                    { new Guid("b3692789-7d7b-4f57-9597-d3b4e6d1480e"), "Welsh" },
                    { new Guid("b49ad8b2-1e23-45a2-98b5-9f55952edc5f"), "Afrikaans" },
                    { new Guid("b72e3ef9-b997-4bcf-b7df-babd972a4776"), "Albanian" },
                    { new Guid("b745029f-7f5a-403a-8aca-89beca7ffcd1"), "Serbian" },
                    { new Guid("ba878f1b-228e-4a24-a24c-c98305dcd99a"), "Kurdish" },
                    { new Guid("bd411587-61a9-42ce-bc73-12f1c6550596"), "Galician" },
                    { new Guid("bfcb489b-73d7-4ec0-a754-1e7cc307f46d"), "Malagasy" },
                    { new Guid("c58a45da-ad65-49bd-8191-fd76df3d6171"), "Tamil" },
                    { new Guid("c5b66fbc-73f6-4442-bcba-fbc635f4aeea"), "Persian" },
                    { new Guid("ca8428a0-e7f3-4905-b950-69d42d106f30"), "Sesotho" },
                    { new Guid("caf74d0c-a7b3-4712-9948-e16bd74bb3de"), "Hmong" },
                    { new Guid("ce301c7b-76c5-4329-898b-48c8f1e592cd"), "Japanese" },
                    { new Guid("cf0e0e9c-2b67-42b5-8514-1ed767627df2"), "Turkmen" },
                    { new Guid("d0a42bcb-e752-46af-a713-6159bd348f2e"), "Thai" },
                    { new Guid("d5e6ceb1-70f7-45a5-aacf-9c3fc4d75e54"), "Croatian" },
                    { new Guid("d8ab352f-6056-4fcb-975a-e277bc678870"), "Zulu" },
                    { new Guid("d8af3c6b-36d6-42f2-94f0-92a1783c42e7"), "Burmese" },
                    { new Guid("da125b12-ee9d-48bc-93a8-ff705d975393"), "Estonian" },
                    { new Guid("db2ac75d-296e-481f-9e5d-00f6891b5ce8"), "Malayalam" },
                    { new Guid("db593a90-0af0-4265-ab02-3210a5f23a54"), "Hungarian" },
                    { new Guid("de6eeafd-743a-44fb-b6de-ac86bd78dad8"), "Uyghur" },
                    { new Guid("e1cb74ad-13fc-4e4c-ab8e-844a4dc3398b"), "Corsican" },
                    { new Guid("e1fc19ab-93e2-4f51-860d-6dcf6874b607"), "Maori" },
                    { new Guid("e21399a8-da52-45e2-9bb3-241874492dcd"), "Shona" },
                    { new Guid("e3ae230f-1776-4f73-a4c6-ed1a1a681634"), "Haitian Creole" },
                    { new Guid("e58cda90-0d67-49ea-af73-f6884c332b72"), "Russian" },
                    { new Guid("e660ad9d-859e-43dd-a0fb-7079e6cf7da3"), "Azerbaijani" },
                    { new Guid("e74a0eb0-c2ee-4335-ab10-588566899ade"), "Irish" },
                    { new Guid("e74dd91d-2a7f-4632-9969-6a067c135e08"), "French" },
                    { new Guid("e77a0465-f965-4582-b821-c01415ab6e3d"), "Tatar" },
                    { new Guid("efda5407-4ade-4de0-b4c2-301876e1488e"), "Slovak" },
                    { new Guid("f139a17b-95fe-4910-8fd2-91ec4ef51c65"), "Javanese" },
                    { new Guid("f3e84e2a-3175-4b7e-a8f9-3b9acf76df87"), "Spanish" },
                    { new Guid("f3fbf82c-f16c-4689-a87c-8752767dd25e"), "Kyrgyz" },
                    { new Guid("f43b7c9f-53a8-4809-99b1-aaf318639efd"), "Macedonian" },
                    { new Guid("f4f9439b-23cc-4bcb-9d17-0b3737459130"), "Odia" },
                    { new Guid("f515e31b-1b1a-4544-a1af-43f671ac5993"), "Armenian" },
                    { new Guid("f5f9ec46-be23-4f54-b7dd-5f6eb3e17be9"), "Scottish Gaelic" },
                    { new Guid("f837b497-4b57-4aa7-a44d-81e0aab0a469"), "Lithuanian" },
                    { new Guid("fe94e37c-e7ec-4a21-b891-8d78f11b604b"), "Latin" },
                }
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CityId",
                table: "Cities",
                column: "CityId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationCities_Cities_CityId",
                table: "DestinationCities",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Cities_Countries_CityId", table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_DestinationCities_Cities_CityId",
                table: "DestinationCities"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Cities", table: "Cities");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("00d1d0ba-28df-417c-b586-dabed832fd63")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("04621fff-1499-49fc-87f2-a5fb6f00982e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("04956d76-f163-4097-ae47-a789c1312f78")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("0ca38f07-32c7-4dfb-b3fc-9b19847a4069")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("0f93255e-1961-4b67-ad82-557f82e08682")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("13c5827b-84e6-49de-b63a-5bd8679e2d83")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("13f9c72b-a447-44a6-9fed-7936a18d0c61")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("167f69e6-96d0-4861-b31c-08f2fadfde9f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("16b9e2d9-b10f-49d6-9af7-ea7e1708902c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("16f8bbdd-edc6-47c4-938d-8bd733e3211c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("183fc9cb-3b11-4e65-9765-a54831e3db48")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("1920dd53-903b-4c4c-b07e-61147f45ffbd")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("252c2c9e-1621-49db-8c98-aef84073c8cf")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("268b2d6d-c594-4254-868e-b03a46a8ad8a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2cefb843-9d27-4b22-9c8b-f51c9abb2de4")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2d4d978a-6698-4e5d-8f91-ee1b7519c243")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3567b431-947d-4856-b8e9-7cf85e99a7c2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("35be57bb-c8d3-46df-8922-6c0be9caf5e3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("368620c4-9560-4a41-9e4b-912aa689f6e9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("37fb9e1a-74b4-4897-9e16-ab3ea80e6722")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("385d889d-b6d1-4129-ba88-216f61d6b214")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3b91ddde-d594-41fd-9fe8-5fdc12e25c79")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3bfa246e-559e-47da-bec3-51bf02872675")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3d91639b-5cf5-4a8b-8783-fe09f76b5810")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("40fb67db-76d6-4166-9141-f8d332448f18")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("44f1de5e-796f-4d1d-907c-156a772376b5")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("4f5b852a-f77e-4d0c-be0f-29017a39cc8c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("51ed2ab6-a9df-4008-88c3-ba92ba4b3dd3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("55f1ff80-1afa-49b5-ab5f-22f81fe4daf2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("57710848-fbfe-4a14-bdde-d0a5f1b648b7")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5b1c4a26-d170-4946-abae-f72aa1885c25")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("60a1888d-00c6-4b8d-95b3-c32705eb1d1c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("636a9c34-819e-4c94-b46b-bdc740e42e9b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6566c7b8-ad31-46a6-a59f-abecf19fdb9b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("65c1c4ab-df62-4e09-8b53-d311c93aceba")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6624a2a6-0d1e-42ba-b22f-67e273cde2b2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("67b9aa89-d6c2-4109-b458-dc1e34582479")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6b339651-0e74-437c-830c-56e12e2d413a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6b5893dc-3353-4447-9c9e-7f8c49f89e17")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6b6ba918-c645-4d25-87c9-91ed2525f713")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6e14209d-fc86-4729-9a1c-61b6efd42f22")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6fd07b7d-60c9-4b09-a46f-a7a07130c4ca")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("733b8a9b-d37f-4562-84c4-c7fc1971484d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("774cd54e-9f09-4813-a7fc-7cba3f89abfc")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7ba278f9-df36-4b64-8364-8382f21df366")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7d03694e-40db-409f-8ccc-83425d6c091b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7d1eeee7-bacf-4495-af0f-2cd0d7952c58")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("803e502e-bd54-4e4a-8d54-c6467502c314")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("84952a9d-a2b9-4c00-8450-becb223d17ce")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("853a11b2-d0e3-4d2f-aa1a-e30b0494af67")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("85a65f66-c25f-46e5-9598-5fec6b57e626")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("86b504ca-9d9d-4d17-b3e8-b1cf6fe00a02")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("86c866df-c981-413a-97fe-dffdc6f531fb")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8a185d8a-5332-4980-bf0c-e43414b009ec")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8ad59034-c060-4a7f-8b0d-e4375af0a443")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8c074c9d-36f2-4dba-80c5-74556e624674")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8c439f2d-a8b5-4f0c-af48-aa180821491e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("91663350-d3a2-4916-89d6-80aaee11cdd3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("926bfc9b-5050-4085-8fdc-4f4282a1b33a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9300051e-5562-4c20-80c4-11cc2326e644")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9430d583-faaf-4b91-acb1-5b697d7ee95c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9582f7e4-1680-4dd2-8d1f-c005f63c469b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9a953e42-a2c9-4cba-b709-0614fa676978")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9f948337-fcd2-449f-9cff-4c8c5c590540")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a00f7e80-9c94-49e4-8fa7-c134d0d66858")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a204ffcd-cf3c-41a9-8dc7-5d33eab609e6")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a20c5e5c-0d2b-47ee-b64b-e081deedbbe1")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a2e524cd-acb5-4ec7-becc-c502df09571d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a39cb1f7-ee0d-4c77-ab0a-46b66d6cd84c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a77cad8e-3b23-4be0-ad0b-6cadbe0890a2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a7d18ade-d543-40d3-99e4-05034cd8554f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a9adcb03-7d1c-4829-a764-eb2cbfea0d88")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b3692789-7d7b-4f57-9597-d3b4e6d1480e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b49ad8b2-1e23-45a2-98b5-9f55952edc5f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b72e3ef9-b997-4bcf-b7df-babd972a4776")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b745029f-7f5a-403a-8aca-89beca7ffcd1")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ba878f1b-228e-4a24-a24c-c98305dcd99a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("bd411587-61a9-42ce-bc73-12f1c6550596")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("bfcb489b-73d7-4ec0-a754-1e7cc307f46d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c58a45da-ad65-49bd-8191-fd76df3d6171")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c5b66fbc-73f6-4442-bcba-fbc635f4aeea")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ca8428a0-e7f3-4905-b950-69d42d106f30")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("caf74d0c-a7b3-4712-9948-e16bd74bb3de")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ce301c7b-76c5-4329-898b-48c8f1e592cd")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("cf0e0e9c-2b67-42b5-8514-1ed767627df2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d0a42bcb-e752-46af-a713-6159bd348f2e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d5e6ceb1-70f7-45a5-aacf-9c3fc4d75e54")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d8ab352f-6056-4fcb-975a-e277bc678870")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d8af3c6b-36d6-42f2-94f0-92a1783c42e7")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("da125b12-ee9d-48bc-93a8-ff705d975393")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("db2ac75d-296e-481f-9e5d-00f6891b5ce8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("db593a90-0af0-4265-ab02-3210a5f23a54")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("de6eeafd-743a-44fb-b6de-ac86bd78dad8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e1cb74ad-13fc-4e4c-ab8e-844a4dc3398b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e1fc19ab-93e2-4f51-860d-6dcf6874b607")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e21399a8-da52-45e2-9bb3-241874492dcd")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e3ae230f-1776-4f73-a4c6-ed1a1a681634")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e58cda90-0d67-49ea-af73-f6884c332b72")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e660ad9d-859e-43dd-a0fb-7079e6cf7da3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e74a0eb0-c2ee-4335-ab10-588566899ade")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e74dd91d-2a7f-4632-9969-6a067c135e08")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e77a0465-f965-4582-b821-c01415ab6e3d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("efda5407-4ade-4de0-b4c2-301876e1488e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f139a17b-95fe-4910-8fd2-91ec4ef51c65")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f3e84e2a-3175-4b7e-a8f9-3b9acf76df87")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f3fbf82c-f16c-4689-a87c-8752767dd25e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f43b7c9f-53a8-4809-99b1-aaf318639efd")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f4f9439b-23cc-4bcb-9d17-0b3737459130")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f515e31b-1b1a-4544-a1af-43f671ac5993")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f5f9ec46-be23-4f54-b7dd-5f6eb3e17be9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f837b497-4b57-4aa7-a44d-81e0aab0a469")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("fe94e37c-e7ec-4a21-b891-8d78f11b604b")
            );

            migrationBuilder.RenameTable(name: "Cities", newName: "City");

            migrationBuilder.AddPrimaryKey(name: "PK_City", table: "City", column: "CityId");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "LanguageName" },
                values: new object[,]
                {
                    { new Guid("009b909e-829e-483b-a24e-818beda12c5c"), "Afrikaans" },
                    { new Guid("01a8671b-ec40-48d4-9259-5595ccfd537c"), "Chinese (Mandarin)" },
                    { new Guid("02e85773-e791-4685-a043-20141f6fe48e"), "Russian" },
                    { new Guid("072b0b50-b474-406e-8de6-6e21ca42c49c"), "Ukrainian" },
                    { new Guid("0969a63b-881d-4a4f-b4af-40eb64933218"), "Hawaiian" },
                    { new Guid("0ccb123c-7a0d-4ffc-8614-137a8d0739cc"), "Azerbaijani" },
                    { new Guid("0d8511f5-8136-47c8-9d61-bbdf168f6803"), "Italian" },
                    { new Guid("162ec2f0-b7bb-4fab-b8a8-be998e7a79a2"), "Marathi" },
                    { new Guid("16e3c394-9486-4543-a919-3455a59d0cce"), "Hmong" },
                    { new Guid("17a90c16-7850-4f5d-ac69-0ff0168c2a40"), "Portuguese" },
                    { new Guid("182d54bd-943f-4e5d-a743-ed21d4393326"), "Gujarati" },
                    { new Guid("19a47393-1bf1-4c92-966f-d7fd83ef82c3"), "Kurdish" },
                    { new Guid("1ad1d234-65ba-4f5c-a724-576cc73ed2f2"), "Tajik" },
                    { new Guid("1c84e4bd-4b3c-453f-a502-1d496763f1ac"), "Sundanese" },
                    { new Guid("1e939af9-ab04-44d9-beff-e69eed27f7ce"), "Hebrew" },
                    { new Guid("23cfdace-207c-4ebf-b876-8ee4a5f14b23"), "Irish" },
                    { new Guid("26b5b8a6-46c9-45ef-9d36-5ac92304f52f"), "Corsican" },
                    { new Guid("2768921d-570b-4af7-a413-cda99435eea1"), "Lao" },
                    { new Guid("2936d03d-fa31-4ce8-9c95-a3e1015b2a8c"), "Basque" },
                    { new Guid("2b08d03d-4efa-4d1b-8873-76aa0f0adcd2"), "Sesotho" },
                    { new Guid("341224f5-f74a-40dd-83c4-fcc36ca83d1f"), "Khmer" },
                    { new Guid("36bf985e-c569-469b-bd72-de3616fe706b"), "Pashto" },
                    { new Guid("39931ae3-deba-43ee-a448-e20181c8e76a"), "Japanese" },
                    { new Guid("3bb8dc11-d2a8-41dd-95cf-5b0492e6eda4"), "Korean" },
                    { new Guid("3bc56107-d777-4035-9df2-a0f745f74899"), "Samoan" },
                    { new Guid("3cb019ac-b82a-48a6-ad6c-a7f2f2353923"), "Javanese" },
                    { new Guid("45fd39aa-d9dc-416d-b5df-809f41a692f5"), "Yiddish" },
                    { new Guid("46b157f4-f6e2-4208-b710-6cc5131f001d"), "Mongolian" },
                    { new Guid("4a92fb57-8ef9-41a8-8b0b-e93d2f3f2f34"), "Thai" },
                    { new Guid("4b02dde5-b942-4872-8478-8bd120e06ace"), "Romanian" },
                    { new Guid("4bc6f917-0b5f-499f-b556-68316cd682a8"), "Amharic" },
                    { new Guid("4dee3394-bbc7-4ed0-9541-7c265d43f7ec"), "Sinhala" },
                    { new Guid("514afbbd-37fd-4d20-bff4-a0574a67127d"), "Shona" },
                    { new Guid("515582ec-6cee-410b-b4d8-55b888dccf89"), "Latvian" },
                    { new Guid("539d2e24-fc8a-4b61-8788-a6be445adcf8"), "Assamese" },
                    { new Guid("543cbd15-aa33-452f-90b0-81a340a916b0"), "Kyrgyz" },
                    { new Guid("5620f9a3-6f2c-4089-947e-192a044a8db7"), "Welsh" },
                    { new Guid("56d407f1-1bc0-4a73-a988-c8bd0c2666b6"), "Serbian" },
                    { new Guid("5810fdb6-316b-4974-841f-28df01e8115a"), "Igbo" },
                    { new Guid("582645f6-bff3-4f13-aa0a-9190bf2090c4"), "Arabic" },
                    { new Guid("58e65cff-af0d-4750-932f-04ac9203b202"), "Macedonian" },
                    { new Guid("5db9c67b-cbf3-4943-9ae6-5db1a9cd95d3"), "Swedish" },
                    { new Guid("5f6bc256-5996-4c84-a3da-62c5cbeb4cfa"), "Slovak" },
                    { new Guid("5f9b3002-15e5-42a9-b5bf-98fb66276925"), "Indonesian" },
                    { new Guid("62fccf19-1d43-462a-9768-00cd9f326fec"), "Spanish" },
                    { new Guid("65ff9b9c-c6f6-457a-a3bf-510d9b6c0e1b"), "Belarusian" },
                    { new Guid("663613d4-14ac-4d78-8bca-08fb49e314eb"), "Somali" },
                    { new Guid("67904680-0c9b-4417-997e-e45f51c8c618"), "Swahili" },
                    { new Guid("6826088a-bd79-44f3-826d-34391aa59f44"), "Finnish" },
                    { new Guid("687db95a-2000-4c51-9d70-41fef88169cf"), "Kazakh" },
                    { new Guid("6cd829fd-629f-4de8-9afd-6985496c4051"), "Maori" },
                    { new Guid("6d57f9a1-abd7-4f07-94dc-a4aecaad6fec"), "Kinyarwanda" },
                    { new Guid("6d9ebdf3-130a-4539-9f01-2b153d4b4e28"), "Uyghur" },
                    { new Guid("6e5821e3-7acc-4c76-81f2-d05468a42e0f"), "Latin" },
                    { new Guid("7284fb11-fc82-490b-b8be-f73b0398ccde"), "Dari" },
                    { new Guid("74ea65eb-ed99-437b-8f61-5c5c9d79edda"), "Malay" },
                    { new Guid("772c4036-d0e7-49ee-b5c6-e9c29d13b8f0"), "Uzbek" },
                    { new Guid("77f3b37a-2745-4c54-8fa9-39b00d2ab34f"), "Scottish Gaelic" },
                    { new Guid("79e0ccaf-0dd3-4685-bae9-6e871469a166"), "Kannada" },
                    { new Guid("7fcb1776-b96a-4872-b600-d898a49b3d25"), "Turkish" },
                    { new Guid("8130e7eb-3971-4ff4-aa01-3b58dad382cf"), "Turkmen" },
                    { new Guid("816894d2-91c2-45d2-a201-724e5e694e83"), "Albanian" },
                    { new Guid("834796dc-2f71-4b10-9096-bc48951940a5"), "Croatian" },
                    { new Guid("8b51f7ed-9441-4cd1-911c-6b177546ea3c"), "Polish" },
                    { new Guid("8dfef08f-a18a-4a7e-b9d2-9360e37dc804"), "Odia" },
                    { new Guid("8e76d742-f96a-41fe-815d-e0df13b7af4e"), "Persian" },
                    { new Guid("8e8bee65-ee49-490e-bce6-3b4dea52964c"), "Yoruba" },
                    { new Guid("8ef81c00-dc64-450c-aa0b-b12682bc0777"), "English" },
                    { new Guid("8fe5deaf-d519-4e34-b830-c869024773ba"), "German" },
                    { new Guid("9025e5b8-f0d3-47dc-a8b6-08406c0f81cb"), "Icelandic" },
                    { new Guid("931249e8-a620-4ad4-9628-3d9ec96019f3"), "Georgian" },
                    { new Guid("93456d8b-86fe-4219-9448-05e0178b47d5"), "Czech" },
                    { new Guid("98e96c89-9875-44b0-a565-fb8c07c4afaf"), "Chichewa" },
                    { new Guid("9c4b564d-7288-4771-9d68-beccd42cab57"), "Malagasy" },
                    { new Guid("9e3615a6-5576-40c1-a877-614f64440727"), "Tamil" },
                    { new Guid("9ecc72d2-a48b-4f4c-9d4e-6e19fadfd130"), "Haitian Creole" },
                    { new Guid("a173504f-be00-4982-be88-4a50d9271c24"), "Norwegian" },
                    { new Guid("a6eba4d0-41ec-4318-a4e0-6caea6e5d44a"), "Greek" },
                    { new Guid("a815e0e0-1809-454a-9840-db3a87678646"), "Hausa" },
                    { new Guid("a966aec5-e74b-46ff-90b4-027f259e19a6"), "Estonian" },
                    { new Guid("ae018ced-a132-4027-a5af-ad0a32528b9e"), "Bulgarian" },
                    { new Guid("b2ddf799-146a-4cc6-9eba-97cd702682a6"), "Armenian" },
                    { new Guid("b41e7e8c-275c-400b-8114-7c00246078f7"), "Punjabi" },
                    { new Guid("b48c2526-db17-4569-a679-29250b4dcebc"), "Galician" },
                    { new Guid("b5531560-d7ed-4765-b435-0d43ea8b3b8a"), "Hindi" },
                    { new Guid("b94059a8-cdf7-418e-b0d2-a47e7a42512e"), "Frisian" },
                    { new Guid("bbfcd81a-ed5e-4b87-a0d3-876726160ddf"), "Catalan" },
                    { new Guid("bd52bdbe-c4bb-4449-9e52-6169c9304d3b"), "Nepali" },
                    { new Guid("c35571b9-65be-4a85-aadc-1b578ef1a736"), "Telugu" },
                    { new Guid("c390f2a9-9120-473d-bf12-937ed076bfe0"), "Slovenian" },
                    { new Guid("c3e535ad-8b05-4ce6-a302-fdb830605ea3"), "Danish" },
                    { new Guid("c7d339b2-63bb-4a70-9727-032ca4c987eb"), "Urdu" },
                    { new Guid("ccb8a8fa-0c84-4e51-8274-78c5e3f207d0"), "Cebuano" },
                    { new Guid("ced40db0-363f-4f93-a01f-91b30175c70e"), "Lithuanian" },
                    { new Guid("d0ffc022-b927-4e63-b987-b85e02ee0cd9"), "Zulu" },
                    { new Guid("d43315a9-b5f0-472a-a59a-4952395fcb21"), "Tatar" },
                    { new Guid("d6544404-3f7b-4ae8-bb7d-e0398c761e0e"), "Esperanto" },
                    { new Guid("d80d04e2-c425-4291-9c78-2e4b53e4c8cb"), "Luxembourgish" },
                    { new Guid("d900f2dc-8017-4de3-a64c-a9c112628457"), "Hungarian" },
                    { new Guid("d9864a21-5957-4bb3-8f55-ebc35914bce8"), "Xhosa" },
                    { new Guid("db6ef275-14d8-40ec-a0bd-6ee9be77ae9b"), "French" },
                    { new Guid("dca56cfa-838c-49ba-b88f-74ef5c048f33"), "Filipino" },
                    { new Guid("e57e78df-6192-423c-9d3f-913ca66701b2"), "Maltese" },
                    { new Guid("e716b847-432c-4d44-803e-dc1700f18144"), "Burmese" },
                    { new Guid("e8637d8a-eee1-46ae-bb87-9f1c1fa74109"), "Sindhi" },
                    { new Guid("eb357206-83f2-412a-89db-cb2645321562"), "Fijian" },
                    { new Guid("f3707b69-47c4-44f8-b462-cbcad60b15c3"), "Dutch" },
                    { new Guid("f537c46f-89a4-4b25-91f0-68a375118db0"), "Bosnian" },
                    { new Guid("f6756ae1-fade-422b-837b-e1aae84d152d"), "Malayalam" },
                    { new Guid("f6d0acba-f8dd-41ef-913d-765fdf871e4c"), "Bengali" },
                    { new Guid("fb99a084-a3d3-4d0e-8f2f-3a5dd6536759"), "Chinese (Cantonese)" },
                    { new Guid("ffe2c1b6-ebdb-4b47-bca7-072509dff172"), "Vietnamese" },
                }
            );

            migrationBuilder.AddForeignKey(
                name: "FK_City_Countries_CityId",
                table: "City",
                column: "CityId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationCities_City_CityId",
                table: "DestinationCities",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict
            );
        }
    }
}
