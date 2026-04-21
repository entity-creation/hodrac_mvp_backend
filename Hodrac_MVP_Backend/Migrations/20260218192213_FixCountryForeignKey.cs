using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hodrac_MVP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class FixCountryForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Cities_Countries_CityId", table: "Cities");

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

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "LanguageName" },
                values: new object[,]
                {
                    { new Guid("003bd571-4d0d-4234-a667-3df269bc6aa9"), "Hungarian" },
                    { new Guid("02b31e5e-56cf-4c7f-ae6c-abbc9be8bde2"), "Xhosa" },
                    { new Guid("071523b9-818c-4bdb-a865-a4a13844c078"), "Turkmen" },
                    { new Guid("074a8236-c26b-4c95-8827-b055fd635ea0"), "Scottish Gaelic" },
                    { new Guid("118ca133-1cdb-4335-82b1-4b9e5924dc6a"), "Arabic" },
                    { new Guid("146caa8c-09e1-4f13-8b00-ca219b30140e"), "Macedonian" },
                    { new Guid("1ec7d542-271c-44b9-ab10-530b85b67d9d"), "Turkish" },
                    { new Guid("1ff305eb-79d3-4e3f-bdd7-14d701676af9"), "Assamese" },
                    { new Guid("1ffbbec6-6491-4f19-b4bd-de0331c5520a"), "Urdu" },
                    { new Guid("203fbdf2-f1ce-4468-8a71-99915a77dada"), "Sundanese" },
                    { new Guid("21205e99-e7db-47bf-9de3-af16ef66edca"), "Bulgarian" },
                    { new Guid("2425aa5d-2759-4f04-8edf-5ba666b626fb"), "Tajik" },
                    { new Guid("24747e82-c82e-44ca-a835-11a8ec29095b"), "Chinese (Cantonese)" },
                    { new Guid("2518bc66-4eb0-47f7-9125-9853c5ed5597"), "Korean" },
                    { new Guid("28bb83f8-71c9-487c-8772-3f12f07ae2d1"), "Thai" },
                    { new Guid("2a13b85d-1fc8-4a18-bf3b-6dfdf02c65c3"), "Tamil" },
                    { new Guid("2dd276b8-6ba6-42e5-a4c8-51af8b784c63"), "Irish" },
                    { new Guid("31fa0ece-055d-4767-98a5-b2ddd659dafe"), "Marathi" },
                    { new Guid("326150c2-5f36-4289-8af2-5279d8ef5712"), "Dutch" },
                    { new Guid("32b4f564-661a-456f-9357-c775ad40d69c"), "Indonesian" },
                    { new Guid("33a70d06-a8e2-4425-abae-f4440f095b2e"), "Icelandic" },
                    { new Guid("374d0e46-e8d3-4814-985c-9fb020a3a1b8"), "Malagasy" },
                    { new Guid("39768953-b5c0-48cf-b52b-593114205e93"), "Hausa" },
                    { new Guid("3aa1cd62-2dc4-497d-aced-ed15e9b991c1"), "Albanian" },
                    { new Guid("3d640ee8-9bc4-4cb5-9602-0193845ac42a"), "Shona" },
                    { new Guid("3efd64ba-dbbf-4300-a49d-52f16b8cca0c"), "Georgian" },
                    { new Guid("3f2d5c85-0f61-461e-94d8-052bbb32d4f8"), "Burmese" },
                    { new Guid("3fada422-80c8-4ec6-ac11-c15aa35fa096"), "Kannada" },
                    { new Guid("40cdc829-29d4-43ab-b62a-3ec640af0e4e"), "Dari" },
                    { new Guid("40ffe8e3-54ee-4d8f-b364-c955248086ab"), "Haitian Creole" },
                    { new Guid("41cf5c42-c290-4e32-a367-e0b1941b97b0"), "Fijian" },
                    { new Guid("458264d7-7305-437a-81dd-0a4fe9e76fc9"), "Ukrainian" },
                    { new Guid("46f606ab-4c2f-4065-af76-50b80d33e29f"), "Swedish" },
                    { new Guid("47740e21-c441-4abb-a7be-2e6278b10745"), "Russian" },
                    { new Guid("52f18624-7555-43d8-825e-79531f92b9a9"), "Serbian" },
                    { new Guid("534e3d0b-a172-438f-a349-e8a6ae6caa11"), "Kazakh" },
                    { new Guid("53bff35f-1b48-4620-a75d-7c0589ced6fa"), "Chinese (Mandarin)" },
                    { new Guid("54dee145-3967-42b0-8253-8da3edef4217"), "Hawaiian" },
                    { new Guid("55c07e1b-5fb0-4aee-995a-49f134531152"), "Malayalam" },
                    { new Guid("56aa45c9-5c5d-4e13-8696-c65badbcf782"), "Welsh" },
                    { new Guid("577ff31e-9b9a-46c4-a328-80197c296d94"), "Khmer" },
                    { new Guid("59409e6b-9170-4b3e-8cea-9a69192ffd19"), "Maori" },
                    { new Guid("5a36f6ec-8abb-4809-89c2-df24779784d0"), "Italian" },
                    { new Guid("5c6e37b0-7581-43e5-8986-9a6f8cc72e28"), "Filipino" },
                    { new Guid("61c73526-089d-4d52-8124-5dd8c187a93a"), "Igbo" },
                    { new Guid("628b80b0-c656-43a9-95fe-a05838e7c26e"), "Azerbaijani" },
                    { new Guid("65aeca4d-29ba-4542-837c-e3f99eb00eea"), "Mongolian" },
                    { new Guid("67768d40-63c4-4ae3-992a-0e8c3b174c6a"), "Catalan" },
                    { new Guid("67fa8201-e376-45c6-8bfa-e8564517da2c"), "Bengali" },
                    { new Guid("6d650958-6a36-4373-9741-62646afa5f92"), "Lao" },
                    { new Guid("6d66403d-37cc-4152-be92-19dd40134b8d"), "Javanese" },
                    { new Guid("6d7a2873-e2e9-4ce5-b17a-f6501fa34df7"), "Latin" },
                    { new Guid("6e049c74-a532-4a41-8ee6-279631faedf0"), "Czech" },
                    { new Guid("7215035c-d863-493a-bc0a-7fb350411c98"), "Odia" },
                    { new Guid("72d7bec3-3648-480a-8195-42763119248a"), "Greek" },
                    { new Guid("7a28dacf-b9b3-41e8-9c8c-e3628382ac42"), "Kyrgyz" },
                    { new Guid("7aaa2abd-d299-499a-837f-408bde3a2773"), "Hindi" },
                    { new Guid("81e191f6-1f32-419c-a109-88d5d7bd0571"), "Armenian" },
                    { new Guid("85064b65-c48a-4c2b-af4b-8d48d4aa6cbd"), "Lithuanian" },
                    { new Guid("85de35b2-2db5-4893-83b7-d42d66d21f6e"), "Galician" },
                    { new Guid("88da282b-96e2-423b-8a4b-ea4d45ac7061"), "Maltese" },
                    { new Guid("897e9258-c816-469c-a666-7bc8b1630d7a"), "Yoruba" },
                    { new Guid("8cb726c0-354c-4370-97c1-2b35efde6107"), "Pashto" },
                    { new Guid("90fbac0e-b6ca-46f7-854b-6e1fb08257f5"), "Croatian" },
                    { new Guid("97a1c826-ca50-4f95-84b1-23ac2503699b"), "Latvian" },
                    { new Guid("9b1a2fd4-a2a8-48d9-8803-97e9ac84d09b"), "Sindhi" },
                    { new Guid("9b3bc1a1-4a07-45b0-a463-4cd1e57ea8ee"), "Finnish" },
                    { new Guid("9c477989-0335-460d-b9c2-65ba17f42af7"), "Luxembourgish" },
                    { new Guid("a0d30334-47ec-4566-b172-f05edcec5369"), "Nepali" },
                    { new Guid("a49ad3fa-50ae-45f5-86ae-f105fac333f4"), "Punjabi" },
                    { new Guid("a733525c-80a6-4327-9879-dce477211162"), "Chichewa" },
                    { new Guid("a86e1bbd-7b79-4114-a2a3-3ef70e44264a"), "Malay" },
                    { new Guid("a9c5854d-a5b5-42cf-953e-df05a1ca9787"), "Bosnian" },
                    { new Guid("aa5fc42a-92df-4369-bc2a-46916175deda"), "Cebuano" },
                    { new Guid("ab74b800-6a20-4933-8b71-6d65dc8856a3"), "Frisian" },
                    { new Guid("afea1cdc-adb5-4745-86a5-1047e6e9b116"), "Japanese" },
                    { new Guid("b2744ae8-935c-4435-ad88-7fb839a9f25f"), "Spanish" },
                    { new Guid("b4120956-850f-4468-9c4e-9fb36e9c98f9"), "Tatar" },
                    { new Guid("b5d5437d-d615-4392-b9be-3e45e67471e5"), "French" },
                    { new Guid("b7ad3af9-a8d9-46b0-9b9c-103967ce93b2"), "Sinhala" },
                    { new Guid("bc66dfde-e930-4f10-9619-7ec3c3cbfc4b"), "Esperanto" },
                    { new Guid("bc8b06c2-d6b2-4dd6-8ee7-c1dda53bc2a4"), "Basque" },
                    { new Guid("bf8a5288-b030-4e44-95c9-67584e18a1b8"), "Portuguese" },
                    { new Guid("c344db8f-6963-462a-8736-0fccd75d0070"), "Corsican" },
                    { new Guid("c3faa26b-6cb9-47b0-9fd5-4436fea79d72"), "Amharic" },
                    { new Guid("c98ec2a6-6c43-4d42-b817-567bcb0bf08d"), "Uyghur" },
                    { new Guid("cbb6e42a-8e4c-499b-ad0a-4f6f60565cfa"), "Telugu" },
                    { new Guid("cc5c230d-87ed-4223-b728-4c66e6d7a184"), "Uzbek" },
                    { new Guid("cd791d51-0a13-406a-9e27-966ba5566937"), "Somali" },
                    { new Guid("cdbbacdf-77d5-43a6-a9fe-2b0becde506f"), "German" },
                    { new Guid("cddff1cf-e5b8-4994-ab6c-524275465c4b"), "Hmong" },
                    { new Guid("d0a310c5-ea2b-4db2-b6d1-e05441bd9b38"), "Vietnamese" },
                    { new Guid("d0b1f016-c29c-4622-b04a-2d054c4ff668"), "Belarusian" },
                    { new Guid("d45d1268-de87-4b0a-b1b9-a969cd20a498"), "Gujarati" },
                    { new Guid("d9bf915b-7846-4494-8956-34fe6a0fe977"), "Swahili" },
                    { new Guid("dca3cb61-c91c-4729-8b0c-570586fe2c89"), "Yiddish" },
                    { new Guid("df4ce3c5-0ce8-44e7-8d96-3f766f69ffcc"), "Polish" },
                    { new Guid("dfb69a10-73e0-48b7-b4cc-c84eef44b88f"), "Romanian" },
                    { new Guid("e023c813-a22e-425b-a82b-9e669cb3a992"), "Norwegian" },
                    { new Guid("e0a81968-6e3f-4846-99fc-b53450f56cc3"), "Hebrew" },
                    { new Guid("e546b6f6-b952-493c-b505-be4c255d551e"), "Kurdish" },
                    { new Guid("e7e8db2b-f093-415a-87f2-dc41ca3c0587"), "Slovenian" },
                    { new Guid("e8f328fc-cb40-44ab-b293-68240aeb1b49"), "Kinyarwanda" },
                    { new Guid("ecae7de0-3830-41de-a97f-108e07115604"), "Estonian" },
                    { new Guid("ef1e3fe8-fdf2-4240-adcf-7d4fd5593bf6"), "Danish" },
                    { new Guid("ef9f6716-0fe9-4d21-aebd-7edb6bd0c79d"), "Afrikaans" },
                    { new Guid("f0d6c937-4ad3-4ba8-a4a0-6aff113eedca"), "Samoan" },
                    { new Guid("f1a22541-852c-40b0-8b7f-8d657aa18638"), "Sesotho" },
                    { new Guid("f40999f3-d416-4c63-8c4e-4d223f7f1310"), "Persian" },
                    { new Guid("fa6cc8aa-777c-407f-ac6b-8b41146a179b"), "Zulu" },
                    { new Guid("fc3a9739-e288-4f11-93e1-a1885ad61053"), "Slovak" },
                    { new Guid("fcf13ee6-e817-4345-aedf-bf4e98e84316"), "English" },
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Cities_Countries_CountryId", table: "Cities");

            migrationBuilder.DropIndex(name: "IX_Cities_CountryId", table: "Cities");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("003bd571-4d0d-4234-a667-3df269bc6aa9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("02b31e5e-56cf-4c7f-ae6c-abbc9be8bde2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("071523b9-818c-4bdb-a865-a4a13844c078")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("074a8236-c26b-4c95-8827-b055fd635ea0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("118ca133-1cdb-4335-82b1-4b9e5924dc6a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("146caa8c-09e1-4f13-8b00-ca219b30140e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("1ec7d542-271c-44b9-ab10-530b85b67d9d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("1ff305eb-79d3-4e3f-bdd7-14d701676af9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("1ffbbec6-6491-4f19-b4bd-de0331c5520a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("203fbdf2-f1ce-4468-8a71-99915a77dada")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("21205e99-e7db-47bf-9de3-af16ef66edca")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2425aa5d-2759-4f04-8edf-5ba666b626fb")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("24747e82-c82e-44ca-a835-11a8ec29095b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2518bc66-4eb0-47f7-9125-9853c5ed5597")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("28bb83f8-71c9-487c-8772-3f12f07ae2d1")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2a13b85d-1fc8-4a18-bf3b-6dfdf02c65c3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("2dd276b8-6ba6-42e5-a4c8-51af8b784c63")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("31fa0ece-055d-4767-98a5-b2ddd659dafe")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("326150c2-5f36-4289-8af2-5279d8ef5712")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("32b4f564-661a-456f-9357-c775ad40d69c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("33a70d06-a8e2-4425-abae-f4440f095b2e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("374d0e46-e8d3-4814-985c-9fb020a3a1b8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("39768953-b5c0-48cf-b52b-593114205e93")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3aa1cd62-2dc4-497d-aced-ed15e9b991c1")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3d640ee8-9bc4-4cb5-9602-0193845ac42a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3efd64ba-dbbf-4300-a49d-52f16b8cca0c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3f2d5c85-0f61-461e-94d8-052bbb32d4f8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("3fada422-80c8-4ec6-ac11-c15aa35fa096")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("40cdc829-29d4-43ab-b62a-3ec640af0e4e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("40ffe8e3-54ee-4d8f-b364-c955248086ab")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("41cf5c42-c290-4e32-a367-e0b1941b97b0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("458264d7-7305-437a-81dd-0a4fe9e76fc9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("46f606ab-4c2f-4065-af76-50b80d33e29f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("47740e21-c441-4abb-a7be-2e6278b10745")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("52f18624-7555-43d8-825e-79531f92b9a9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("534e3d0b-a172-438f-a349-e8a6ae6caa11")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("53bff35f-1b48-4620-a75d-7c0589ced6fa")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("54dee145-3967-42b0-8253-8da3edef4217")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("55c07e1b-5fb0-4aee-995a-49f134531152")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("56aa45c9-5c5d-4e13-8696-c65badbcf782")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("577ff31e-9b9a-46c4-a328-80197c296d94")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("59409e6b-9170-4b3e-8cea-9a69192ffd19")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5a36f6ec-8abb-4809-89c2-df24779784d0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("5c6e37b0-7581-43e5-8986-9a6f8cc72e28")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("61c73526-089d-4d52-8124-5dd8c187a93a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("628b80b0-c656-43a9-95fe-a05838e7c26e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("65aeca4d-29ba-4542-837c-e3f99eb00eea")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("67768d40-63c4-4ae3-992a-0e8c3b174c6a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("67fa8201-e376-45c6-8bfa-e8564517da2c")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6d650958-6a36-4373-9741-62646afa5f92")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6d66403d-37cc-4152-be92-19dd40134b8d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6d7a2873-e2e9-4ce5-b17a-f6501fa34df7")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("6e049c74-a532-4a41-8ee6-279631faedf0")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7215035c-d863-493a-bc0a-7fb350411c98")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("72d7bec3-3648-480a-8195-42763119248a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7a28dacf-b9b3-41e8-9c8c-e3628382ac42")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("7aaa2abd-d299-499a-837f-408bde3a2773")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("81e191f6-1f32-419c-a109-88d5d7bd0571")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("85064b65-c48a-4c2b-af4b-8d48d4aa6cbd")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("85de35b2-2db5-4893-83b7-d42d66d21f6e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("88da282b-96e2-423b-8a4b-ea4d45ac7061")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("897e9258-c816-469c-a666-7bc8b1630d7a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("8cb726c0-354c-4370-97c1-2b35efde6107")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("90fbac0e-b6ca-46f7-854b-6e1fb08257f5")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("97a1c826-ca50-4f95-84b1-23ac2503699b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9b1a2fd4-a2a8-48d9-8803-97e9ac84d09b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9b3bc1a1-4a07-45b0-a463-4cd1e57ea8ee")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("9c477989-0335-460d-b9c2-65ba17f42af7")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a0d30334-47ec-4566-b172-f05edcec5369")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a49ad3fa-50ae-45f5-86ae-f105fac333f4")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a733525c-80a6-4327-9879-dce477211162")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a86e1bbd-7b79-4114-a2a3-3ef70e44264a")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("a9c5854d-a5b5-42cf-953e-df05a1ca9787")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("aa5fc42a-92df-4369-bc2a-46916175deda")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ab74b800-6a20-4933-8b71-6d65dc8856a3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("afea1cdc-adb5-4745-86a5-1047e6e9b116")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b2744ae8-935c-4435-ad88-7fb839a9f25f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b4120956-850f-4468-9c4e-9fb36e9c98f9")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b5d5437d-d615-4392-b9be-3e45e67471e5")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("b7ad3af9-a8d9-46b0-9b9c-103967ce93b2")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("bc66dfde-e930-4f10-9619-7ec3c3cbfc4b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("bc8b06c2-d6b2-4dd6-8ee7-c1dda53bc2a4")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("bf8a5288-b030-4e44-95c9-67584e18a1b8")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c344db8f-6963-462a-8736-0fccd75d0070")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c3faa26b-6cb9-47b0-9fd5-4436fea79d72")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("c98ec2a6-6c43-4d42-b817-567bcb0bf08d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("cbb6e42a-8e4c-499b-ad0a-4f6f60565cfa")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("cc5c230d-87ed-4223-b728-4c66e6d7a184")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("cd791d51-0a13-406a-9e27-966ba5566937")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("cdbbacdf-77d5-43a6-a9fe-2b0becde506f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("cddff1cf-e5b8-4994-ab6c-524275465c4b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d0a310c5-ea2b-4db2-b6d1-e05441bd9b38")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d0b1f016-c29c-4622-b04a-2d054c4ff668")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d45d1268-de87-4b0a-b1b9-a969cd20a498")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("d9bf915b-7846-4494-8956-34fe6a0fe977")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("dca3cb61-c91c-4729-8b0c-570586fe2c89")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("df4ce3c5-0ce8-44e7-8d96-3f766f69ffcc")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("dfb69a10-73e0-48b7-b4cc-c84eef44b88f")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e023c813-a22e-425b-a82b-9e669cb3a992")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e0a81968-6e3f-4846-99fc-b53450f56cc3")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e546b6f6-b952-493c-b505-be4c255d551e")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e7e8db2b-f093-415a-87f2-dc41ca3c0587")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("e8f328fc-cb40-44ab-b293-68240aeb1b49")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ecae7de0-3830-41de-a97f-108e07115604")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ef1e3fe8-fdf2-4240-adcf-7d4fd5593bf6")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("ef9f6716-0fe9-4d21-aebd-7edb6bd0c79d")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f0d6c937-4ad3-4ba8-a4a0-6aff113eedca")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f1a22541-852c-40b0-8b7f-8d657aa18638")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("f40999f3-d416-4c63-8c4e-4d223f7f1310")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("fa6cc8aa-777c-407f-ac6b-8b41146a179b")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("fc3a9739-e288-4f11-93e1-a1885ad61053")
            );

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: new Guid("fcf13ee6-e817-4345-aedf-bf4e98e84316")
            );

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
        }
    }
}
