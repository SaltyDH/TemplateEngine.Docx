using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TemplateEngine.Docx.Tests
{
    public class TableRowContentTests
    {
        [Fact]
        public void TableRowContentConstructorWithEnumerable_FillsFields()
        {
            var tableRowContent = new TableRowContent(new List<FieldContent>());

            Assert.NotNull(tableRowContent.Fields);
        }

        [Fact]
        public void TableRowContentConstructorWithFields_FillsFields()
        {
            var tableRowContent = new TableRowContent(new FieldContent(), new FieldContent());

            Assert.Equal(2, tableRowContent.Fields.Count());
        }

		[Fact]
		public void EqualsTest_ValuesAreEqual_Equals()
		{
			var firstTableRow = new TableRowContent(
				new FieldContent("Name", "Eric"),
				new FieldContent("Role", "Program Manager"));

			var secondTableRow = new TableRowContent(
				new FieldContent("Name", "Eric"),
				new FieldContent("Role", "Program Manager"));

			Assert.True(firstTableRow.Equals(secondTableRow));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByFieldName_NotEquals()
		{
			var firstTableRow = new TableRowContent(
				new FieldContent("Name1", "Eric"),
				new FieldContent("Role", "Program Manager"));

			var secondTableRow = new TableRowContent(
				new FieldContent("Name2", "Eric"),
				new FieldContent("Role", "Program Manager"));

			Assert.False(firstTableRow.Equals(secondTableRow));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByFieldValue_NotEquals()
		{
			var firstTableRow = new TableRowContent(
				new FieldContent("Name", "Eric1"),
				new FieldContent("Role", "Program Manager"));

			var secondTableRow = new TableRowContent(
				new FieldContent("Name", "Eric2"),
				new FieldContent("Role", "Program Manager"));

			Assert.False(firstTableRow.Equals(secondTableRow));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByFieldsCount_NotEquals()
		{
			var firstTableRow = new TableRowContent(
				new FieldContent("Name", "Eric"));

			var secondTableRow = new TableRowContent(
				new FieldContent("Name", "Eric"),
				new FieldContent("Role", "Program Manager"));

			Assert.False(firstTableRow.Equals(secondTableRow));
		}

		[Fact]
		public void EqualsTest_CompareWithNull_NotEquals()
		{
			var firstTableRow = new TableRowContent(
				new FieldContent("Name", "Eric"));
			
			Assert.False(firstTableRow.Equals(null));
		}
    }
}
