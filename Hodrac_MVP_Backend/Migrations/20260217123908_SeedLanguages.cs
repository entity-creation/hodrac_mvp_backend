using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hodrac_MVP_Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
