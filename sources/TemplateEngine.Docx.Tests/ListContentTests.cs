using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TemplateEngine.Docx.Tests
{
	public class ListContentTests
	{

		[Fact]
		public void ListContentConstructorWithName_FillsName()
		{
			var listContent = new ListContent("Name");

			Assert.Equal("Name", listContent.Name);
		}

		[Fact]
		public void ListContentConstructorWithNameAndEnumerableFieldContent_FillsNameAndItems()
		{
			var listContent = new ListContent("Name", new List<ListItemContent>());

			Assert.NotNull(listContent.Items);
			Assert.Equal("Name", listContent.Name);
		}

		[Fact]
		public void ListContentConstructorWithNameAndItems_FillsNameAndItems()
		{
			var listContent = new ListContent("Name", new ListItemContent(), new ListItemContent());

			Assert.Equal(2, listContent.Items.Count());
			Assert.Equal("Name", listContent.Name);
		}

		[Fact]
		public void ListContentConstructorWithNameAndEnumerableListItemContent_FillsNameAndItems()
		{
			var listContent = new ListContent("Name", new List<ListItemContent>());

			Assert.NotNull(listContent.Items);
			Assert.Equal("Name", listContent.Name);
		}

		[Fact]
		public void ListContentGetFieldnames()
		{
			var listContent = new ListContent("Name", 
				new ListItemContent("Header", "value",
					new ListItemContent("Subheader", "value")),
				new ListItemContent("Header", "value",
					new ListItemContent("Subheader", "value"),
					new ListItemContent("Subheader", "value2",
						new ListItemContent("Subsubheader", "value"))));

			Assert.NotNull(listContent.Items);
			Assert.Equal("Name", listContent.Name);
			Assert.Equal(3, listContent.FieldNames.Count());
			Assert.Equal("Header", listContent.FieldNames.ToArray()[0]);
			Assert.Equal("Subheader", listContent.FieldNames.ToArray()[1]);
			Assert.Equal("Subsubheader", listContent.FieldNames.ToArray()[2]);
		}

		[Fact]
		public void ListContentFluentConstructorWithNameAndItems_FillsNameAndItems()
		{
			var listContent = ListContent.Create("Name", new ListItemContent(), new ListItemContent());

			Assert.Equal(2, listContent.Items.Count());
			Assert.Equal("Name", listContent.Name);
		}

		[Fact]
		public void ListContentFluentConstructorWithNameAndEnumerableListItemContent_FillsNameAndItems()
		{
			var listContent = ListContent.Create("Name", new List<ListItemContent>());

			Assert.NotNull(listContent.Items);
			Assert.Equal("Name", listContent.Name);
		}


		[Fact]
		public void ListContentFluentAddItem_FillsNameAndItems()
		{
			var listContent = ListContent.Create("Name", new List<ListItemContent>())
				.AddItem(ListItemContent.Create("ItemName", "Name"));
			
			Assert.NotNull(listContent.Items);
			Assert.Equal("Name", listContent.Name);
			Assert.Equal(listContent.Items.Count, 1);
			Assert.Equal(listContent.Items.First().Fields.Count, 1);
			Assert.Equal(listContent.Items.First().Fields.First().Name, "ItemName");
			Assert.Equal(listContent.Items.First().Fields.First().Value, "Name");
		}

		[Fact]
		public void EqualsTest_ValuesAreEqual_Equals()
		{
			var firstListContent = new ListContent("Name",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value2",
					new ListItemContent("Subsubheader", "value"))));

			var secondListContent = new ListContent("Name",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value2",
					new ListItemContent("Subsubheader", "value"))));


			Assert.True(firstListContent.Equals(secondListContent));
		}


		[Fact]
		public void EqualsTest_ValuesDifferByName_NotEquals()
		{
			var firstListContent = new ListContent("Name1",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value2",
					new ListItemContent("Subsubheader", "value"))));

			var secondListContent = new ListContent("Name2",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value2",
					new ListItemContent("Subsubheader", "value"))));


			Assert.False(firstListContent.Equals(secondListContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByListItemValue_NotEquals()
		{
			var firstListContent = new ListContent("Name",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value1",
					new ListItemContent("Subsubheader", "value"))));

			var secondListContent = new ListContent("Name",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value2",
					new ListItemContent("Subsubheader", "value"))));


			Assert.False(firstListContent.Equals(secondListContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByNestedValue_NotEquals()
		{
			var firstListContent = new ListContent("Name",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value",
					new ListItemContent("Subsubheader1", "value"))));

			var secondListContent = new ListContent("Name",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value",
					new ListItemContent("Subsubheader2", "value"))));


			Assert.False(firstListContent.Equals(secondListContent));
		}
		[Fact]
		public void EqualsTest_CompareWithNull_NotEquals()
		{
			var firstListContent = new ListContent("Name",
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value")),
			new ListItemContent("Header", "value",
				new ListItemContent("Subheader", "value"),
				new ListItemContent("Subheader", "value",
					new ListItemContent("Subsubheader1", "value"))));
			
			Assert.False(firstListContent.Equals(null));
		}
	}
}
