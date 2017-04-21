using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TemplateEngine.Docx.Tests
{
	public class ListItemContentTests
	{
		[Fact]
		public void ListItemContentConstructorWithEnumerable_FillNameAndValue()
		{
			var listItemContent = new ListItemContent("Name", "Value");

			Assert.Equal(listItemContent.Fields.Count(), 1);
			Assert.Equal("Name", listItemContent.Fields.First().Name);
			Assert.Equal("Value", listItemContent.Fields.First().Value);
		}

		[Fact]
		public void ListItemContentConstructorWithEnumerable_FillNameAndValueAndFields()
		{
			var listItemContent = new ListItemContent("Name", "Value", new List<ListItemContent>());

			Assert.Equal(listItemContent.Fields.Count(), 1);
			Assert.Equal("Name", listItemContent.Fields.First().Name);
			Assert.Equal("Value", listItemContent.Fields.First().Value);
			Assert.NotNull(listItemContent.NestedFields);
		}

		[Fact]
		public void ListItemContentFluentConstructorWithEnumerable_FillNameAndValue()
		{
			var listItemContent = ListItemContent.Create("Name", "Value");

			Assert.Equal(listItemContent.Fields.Count(), 1);
			Assert.Equal("Name", listItemContent.Fields.First().Name);
			Assert.Equal("Value", listItemContent.Fields.First().Value);
		}
		[Fact]
		public void ListItemContentFluentConstructorWithEnumerable_FillNameAndValueAndFields()
		{
			var listItemContent = ListItemContent.Create("Name", "Value", new List<ListItemContent>());

			Assert.Equal(listItemContent.Fields.Count(), 1);
			Assert.Equal("Name", listItemContent.Fields.First().Name);
			Assert.Equal("Value", listItemContent.Fields.First().Value);
			Assert.NotNull(listItemContent.NestedFields);
		}

		[Fact]
		public void ListItemContentFluentAddItem_FillsField()
		{
			var listItemContent = new ListItemContent("Name1", "Value1").AddField("Name2", "Value2");

			Assert.Equal(listItemContent.Fields.Count(), 2);
			Assert.Equal("Name1", listItemContent.Fields.First().Name);
			Assert.Equal("Value1", listItemContent.Fields.First().Value);
			Assert.Equal("Name2", listItemContent.Fields.Last().Name);
			Assert.Equal("Value2", listItemContent.Fields.Last().Value);
			Assert.NotNull(listItemContent.NestedFields);
		}

		[Fact]
		public void ListItemContentFluentAddNestedItem_FillsNestedField()
		{
			var listItemContent = new ListItemContent("Name1", "Value1")
				.AddNestedItem(ListItemContent.Create("NestedName", "NestedValue"));

			Assert.Equal(listItemContent.Fields.Count(), 1);
			Assert.Equal("Name1", listItemContent.Fields.First().Name);
			Assert.Equal("Value1", listItemContent.Fields.First().Value);
			Assert.Equal(listItemContent.NestedFields.Count, 1);
			Assert.Equal(listItemContent.NestedFields.First().Fields.Count, 1);
			Assert.Equal(listItemContent.NestedFields.First().Fields.First().Name, "NestedName");
			Assert.Equal(listItemContent.NestedFields.First().Fields.First().Value, "NestedValue");
		}

		[Fact]
		public void EqualsTest_ValuesAreEqual_Equals()
		{
			var firstItemContent = new ListItemContent("Name", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName", "NestedValue"));

			var secondItemContent = new ListItemContent("Name", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName", "NestedValue"));
			
			Assert.True(firstItemContent.Equals(secondItemContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByName_NotEquals()
		{
			var firstItemContent = new ListItemContent("Name1", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName", "NestedValue"));

			var secondItemContent = new ListItemContent("Name2", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName", "NestedValue"));
			
			Assert.False(firstItemContent.Equals(secondItemContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByNestedValueName_NotEquals()
		{
			var firstItemContent = new ListItemContent("Name", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName1", "NestedValue"));

			var secondItemContent = new ListItemContent("Name", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName2", "NestedValue"));
			
			Assert.False(firstItemContent.Equals(secondItemContent));
		}
		[Fact]
		public void EqualsTest_ValuesDifferByNestedValuesCounts_NotEquals()
		{
			var firstItemContent = new ListItemContent("Name", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName1", "NestedValue"));

			var secondItemContent = new ListItemContent("Name", "Value");
			
			Assert.False(firstItemContent.Equals(secondItemContent));
		}

		[Fact]
		public void EqualsTest_CompareWithNull_NotEquals()
		{
			var firstItemContent = new ListItemContent("Name", "Value")
				.AddNestedItem(ListItemContent.Create("NestedName1", "NestedValue"));
			
			Assert.False(firstItemContent.Equals(null));
		}
	}
}
