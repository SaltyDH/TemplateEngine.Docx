﻿using System;
using Newtonsoft.Json;
using Xunit;

namespace TemplateEngine.Docx.Tests
{
	public class ContentTests
	{
		[Fact]
		public void ContentSerializationTest_SerializeToJson_Success()
		{
			var valuesToFill = new Content(
				// Add field.
				new FieldContent("Report date", new DateTime(2000, 01, 01).ToString("d")),
				
				// Add table.
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer")),
				// Add nested list.	
				new ListContent("Team Members Nested List")
					.AddItem(new ListItemContent("Role", "Program Manager")
						.AddNestedItem(new FieldContent("Name", "Eric"))
						.AddNestedItem(new FieldContent("Name", "Ann")))
					.AddItem(new ListItemContent("Role", "Developer")
						.AddNestedItem(new FieldContent("Name", "Bob"))
						.AddNestedItem(new FieldContent("Name", "Richard"))),
				// Add image
				new ImageContent("photo", new byte[]{1, 2, 3}),

                // Add repeat content// Add image
				new RepeatContent("Repeat")
                    .AddItem(new FieldContent("Weekend", "Saturday"))
                    .AddItem(new FieldContent("Weekend", "Sunday"))
				
				);


			var serialized = JsonConvert.SerializeObject(valuesToFill);
			
			Assert.Equal("{\"Repeats\":[{\"Name\":\"Repeat\",\"Items\":[{\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Weekend\",\"Value\":\"Saturday\"}],\"Images\":[]},{\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Weekend\",\"Value\":\"Sunday\"}],\"Images\":[]}],\"FieldNames\":[\"Weekend\"]}],\"Tables\":[{\"Name\":\"Team Members Table\",\"Rows\":[{\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Eric\"},{\"Name\":\"Role\",\"Value\":\"Program Manager\"}],\"Images\":[]},{\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Bob\"},{\"Name\":\"Role\",\"Value\":\"Developer\"}],\"Images\":[]}],\"FieldNames\":[\"Name\",\"Role\"]}],\"Lists\":[{\"Name\":\"Team Members Nested List\",\"Items\":[{\"NestedFields\":[{\"NestedFields\":null,\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Eric\"}],\"Images\":[]},{\"NestedFields\":null,\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Ann\"}],\"Images\":[]}],\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Role\",\"Value\":\"Program Manager\"}],\"Images\":[]},{\"NestedFields\":[{\"NestedFields\":null,\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Bob\"}],\"Images\":[]},{\"NestedFields\":null,\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Richard\"}],\"Images\":[]}],\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Role\",\"Value\":\"Developer\"}],\"Images\":[]}],\"FieldNames\":[\"Role\",\"Name\"]}],\"Fields\":[{\"Name\":\"Report date\",\"Value\":\"01.01.2000\"}],\"Images\":[{\"Name\":\"photo\",\"Binary\":\"AQID\"}]}", serialized);

		}

        [Fact]
		public void ContentDeserializationTest_DeserializeFromJson_Success()
		{
			var valuesToFill = new Content(
				// Add field.
				new FieldContent("Report date", new DateTime(2000, 01, 01).ToString("d")),
				// Add table.
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer")),
				// Add nested list.	
				new ListContent("Team Members Nested List")
					.AddItem(new ListItemContent("Role", "Program Manager")
						.AddNestedItem(new FieldContent("Name", "Eric"))
						.AddNestedItem(new FieldContent("Name", "Ann")))
					.AddItem(new ListItemContent("Role", "Developer")
						.AddNestedItem(new FieldContent("Name", "Bob"))
						.AddNestedItem(new FieldContent("Name", "Richard"))),
				// Add image
				new ImageContent("photo", new byte[] { 1, 2, 3 })
				);


			const string serialized = "{\"Tables\":[{\"Name\":\"Team Members Table\",\"Rows\":[{\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Eric\"},{\"Name\":\"Role\",\"Value\":\"Program Manager\"}],\"Images\":[]},{\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Bob\"},{\"Name\":\"Role\",\"Value\":\"Developer\"}],\"Images\":[]}],\"FieldNames\":[\"Name\",\"Role\"]}],\"Lists\":[{\"Name\":\"Team Members Nested List\",\"Items\":[{\"NestedFields\":[{\"NestedFields\":null,\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Eric\"}],\"Images\":[]},{\"NestedFields\":null,\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Ann\"}],\"Images\":[]}],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Role\",\"Value\":\"Program Manager\"}],\"Images\":[]},{\"NestedFields\":[{\"NestedFields\":null,\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Bob\"}],\"Images\":[]},{\"NestedFields\":null,\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Name\",\"Value\":\"Richard\"}],\"Images\":[]}],\"Tables\":[],\"Lists\":[],\"Fields\":[{\"Name\":\"Role\",\"Value\":\"Developer\"}],\"Images\":[]}],\"FieldNames\":[\"Role\",\"Name\"]}],\"Fields\":[{\"Name\":\"Report date\",\"Value\":\"01.01.2000\"}],\"Images\":[{\"Name\":\"photo\",\"Binary\":\"AQID\"}]}";

			var deserialized = JsonConvert.DeserializeObject<Content>(serialized);

			Assert.True(valuesToFill.Equals(deserialized));
		}

		[Fact]
		public void ContentSerializationTest_EmptyContentSerializeToJson_Success()
		{
			var valuesToFill = new Content();

			var serialized = JsonConvert.SerializeObject(valuesToFill);

			Assert.Equal("{\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[],\"Images\":[]}", serialized);
		}

		[Fact]
		public void ContentDeserializationTest_EmptyContentDeserializeFromJson_Success()
		{
			var valuesToFill = new Content();

            const string serialized = "{\"Repeats\":[],\"Tables\":[],\"Lists\":[],\"Fields\":[],\"Images\":[]}";

			var deserialized = JsonConvert.DeserializeObject<Content>(serialized);

			Assert.True(valuesToFill.Equals(deserialized));
		}

		[Fact]
		public void EqualsTest_ObjectsAreEqual_Equals()
		{
			
			var firstValuesToFill = new Content(
				// Add field.
				new FieldContent("Report date", new DateTime(2000, 01, 01).ToString("d")),
				// Add table.
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer")),
				// Add nested list.	
				new ListContent("Team Members Nested List")
					.AddItem(new ListItemContent("Role", "Program Manager")
						.AddNestedItem(new FieldContent("Name", "Eric"))
						.AddNestedItem(new FieldContent("Name", "Ann")))
					.AddItem(new ListItemContent("Role", "Developer")
						.AddNestedItem(new FieldContent("Name", "Bob"))
						.AddNestedItem(new FieldContent("Name", "Richard"))),
				// Add image
				new ImageContent("photo", new byte[] { 1, 2, 3 })
				);

			var secondValuesToFill = new Content(
				// Add field.
				new FieldContent("Report date", new DateTime(2000, 01, 01).ToString("d")),
				// Add table.
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer")),
				// Add nested list.	
				new ListContent("Team Members Nested List")
					.AddItem(new ListItemContent("Role", "Program Manager")
						.AddNestedItem(new FieldContent("Name", "Eric"))
						.AddNestedItem(new FieldContent("Name", "Ann")))
					.AddItem(new ListItemContent("Role", "Developer")
						.AddNestedItem(new FieldContent("Name", "Bob"))
						.AddNestedItem(new FieldContent("Name", "Richard"))),
				// Add image
				new ImageContent("photo", new byte[] { 1, 2, 3 })
				);

			Assert.True(firstValuesToFill.Equals(secondValuesToFill));

		}


		[Fact]
		public void EqualsTest_ObjectsDifferByItemsCount_NotEquals()
		{

			var firstValuesToFill = new Content(
				// Add field.
				new FieldContent("Report date", new DateTime(2000, 01, 01).ToString("d")),
				// Add table.
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"))
				);

			var secondValuesToFill = new Content(
				// Add field.
				new FieldContent("Report date", new DateTime(2000, 01, 01).ToString("d"))
				);

			Assert.False(firstValuesToFill.Equals(secondValuesToFill));

		}

		[Fact]
		public void EqualsTest_CopareWithNull_NotEquals()
		{

			var firstValuesToFill = new Content(
				// Add field.
				new FieldContent("Report date", new DateTime(2000, 01, 01).ToString("d")),
				// Add table.
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"))
				);

			Assert.False(firstValuesToFill.Equals(null));

		}
	}
}
