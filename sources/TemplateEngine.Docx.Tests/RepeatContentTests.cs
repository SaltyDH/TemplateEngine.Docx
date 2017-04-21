using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TemplateEngine.Docx.Tests
{
	public class RepeatContentTests
	{

		[Fact]
		public void RepeatContentConstructorWithName_FillsName()
		{
			var repeatContent = new RepeatContent("Name");

            Assert.Equal("Name", repeatContent.Name);
		}

		[Fact]
        public void RepeatContentConstructorWithNameAndEnumerableFieldContent_FillsNameAndItems()
		{
            var repeatContent = new RepeatContent("Name", new List<Content>());

            Assert.NotNull(repeatContent.Items);
            Assert.Equal("Name", repeatContent.Name);
		}

		[Fact]
        public void RepeatContentConstructorWithNameAndItems_FillsNameAndItems()
		{
            var repeatContent = new RepeatContent("Name", new Content(), new Content());

            Assert.Equal(2, repeatContent.Items.Count);
            Assert.Equal("Name", repeatContent.Name);
		}

		[Fact]
        public void RepeatContentConstructorWithNameAndEnumerableListItemContent_FillsNameAndItems()
		{
            var repeatContent = new RepeatContent("Name", new List<Content>());

            Assert.NotNull(repeatContent.Items);
            Assert.Equal("Name", repeatContent.Name);
		}

		[Fact]
        public void RepeatContentGetFieldnames()
		{
            var repeatContent = new RepeatContent("Name",
		        new Content(new FieldContent("Field1", "value"), new FieldContent("Field2", "value")),
		        new Content(new FieldContent("Field1", "value"), new FieldContent("Field2", "value")));

		    var fieldNames = repeatContent.FieldNames.ToArray();

            Assert.NotNull(repeatContent.Items);
            Assert.Equal("Name", repeatContent.Name);
            Assert.Equal(2, fieldNames.Length);
            Assert.Equal("Field1", fieldNames[0]);
            Assert.Equal("Field2", fieldNames[1]);
		}

		[Fact]
		public void ListContentFluentConstructorWithNameAndItems_FillsNameAndItems()
		{
            var repeatContent = RepeatContent.Create("Name", new Content(), new Content());

            Assert.Equal(2, repeatContent.Items.Count());
            Assert.Equal("Name", repeatContent.Name);
		}

		[Fact]
		public void ListContentFluentConstructorWithNameAndEnumerableListItemContent_FillsNameAndItems()
		{
            var repeatContent = RepeatContent.Create("Name", new List<Content>());

            Assert.NotNull(repeatContent.Items);
            Assert.Equal("Name", repeatContent.Name);
		}


		[Fact]
		public void ListContentFluentAddItem_FillsNameAndItems()
		{
            var repeatContent = RepeatContent.Create("Name", new List<Content>())
                .AddItem(new Content(new FieldContent("ItemName", "Name")));

            Assert.NotNull(repeatContent.Items);
            Assert.Equal("Name", repeatContent.Name);
            Assert.Equal(repeatContent.Items.Count, 1);
            Assert.Equal(repeatContent.Items.First().Fields.Count, 1);
            Assert.Equal(repeatContent.Items.First().Fields.First().Name, "ItemName");
            Assert.Equal(repeatContent.Items.First().Fields.First().Value, "Name");
		}

		[Fact]
		public void EqualsTest_ValuesAreEqual_Equals()
		{
			var firstRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "value2")));

            var secondRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "value2")));


            Assert.True(firstRepeatContent.Equals(secondRepeatContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByName_NotEquals()
		{
            var firstRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "value2")));

            var secondRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field2", "value")),
                new Content(new FieldContent("Field1", "value2")));


            Assert.False(firstRepeatContent.Equals(secondRepeatContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByItemValue_NotEquals()
		{
            var firstRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "value2")));

            var secondRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "anotherValue")));


            Assert.False(firstRepeatContent.Equals(secondRepeatContent));
		}

        [Fact]
        public void EqualsTest_ContentsDifferByName_NotEquals()
        {
            var firstRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "value2")));

            var secondRepeatContent = new RepeatContent("AnotherName",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "value2")));


            Assert.False(firstRepeatContent.Equals(secondRepeatContent));
        }

		[Fact]
		public void EqualsTest_CompareWithNull_NotEquals()
		{
            var firstRepeatContent = new RepeatContent("Name",
                new Content(new FieldContent("Field1", "value")),
                new Content(new FieldContent("Field1", "value2")));

            Assert.False(firstRepeatContent.Equals(null));
		}
	}
}
