using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TemplateEngine.Docx.Tests
{
    public class TableContentTests
    {
        [Fact]
        public void TableContentConstrictorWithName_FillsName()
        {
            var tableContent = new TableContent("Name");

            Assert.Equal("Name", tableContent.Name);
        }

        [Fact]
        public void TableContentConstructorWithNameAndEnumerable_FillsNameAndRows()
        {
            var tableContent = new TableContent("Name", new List<TableRowContent>());

            Assert.NotNull(tableContent.Rows);
            Assert.Equal("Name", tableContent.Name);
        }

        [Fact]
        public void TableContentConstructorWithNameAndRows_FillsNameAndRows()
        {
            var tableContent = new TableContent("Name", new TableRowContent(), new TableRowContent());

            Assert.Equal(2, tableContent.Rows.Count());
            Assert.Equal("Name", tableContent.Name);
        }

		[Fact]
		public void TableContentFluentConstructorWithNameAndEnumerable_FillsNameAndRows()
		{
			var tableContent = TableContent.Create("Name", new List<TableRowContent>());

			Assert.NotNull(tableContent.Rows);
			Assert.Equal("Name", tableContent.Name);
		}

        [Fact]
        public void TableContentFluentConstructorWithNameAndRows_FillsNameAndRows()
        {
            var tableContent = TableContent.Create("Name", new TableRowContent(), new TableRowContent());

            Assert.Equal(2, tableContent.Rows.Count());
            Assert.Equal("Name", tableContent.Name);
        }
        [Fact]
        public void TableAddRowFluent_AddsRow()
        {
            var tableContent = TableContent.Create("Name")
				.AddRow(new FieldContent());

            Assert.Equal(1, tableContent.Rows.Count());
            Assert.Equal("Name", tableContent.Name);
        }


		[Fact]
		public void EqualsTest_ValuesAreEqual_Equals()
		{
			var firstTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			var secondTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			
			Assert.True(firstTableContent.Equals(secondTableContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByName_NotEquals()
		{
			var firstTableContent =
				new TableContent("Team Members Table1")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			var secondTableContent =
				new TableContent("Team Members Table2")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			
			Assert.False(firstTableContent.Equals(secondTableContent));
		}
		[Fact]
		public void EqualsTest_ValuesDifferByFieldName_NotEquals()
		{
			var firstTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name1", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			var secondTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name2", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			
			Assert.False(firstTableContent.Equals(secondTableContent));
		}
		[Fact]
		public void EqualsTest_ValuesDifferByFieldCount_NotEquals()
		{
			var firstTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			var secondTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			
			Assert.False(firstTableContent.Equals(secondTableContent));
		}

		[Fact]
		public void EqualsTest_ValuesDifferByRowCount_NotEquals()
		{
			var firstTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			var secondTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Eric"),
						new FieldContent("Role", "Program Manager"))
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));

			
			Assert.False(firstTableContent.Equals(secondTableContent));
		}

		[Fact]
		public void EqualsTest_CompareWithNull_NotEquals()
		{
			var firstTableContent =
				new TableContent("Team Members Table")
					.AddRow(
						new FieldContent("Name", "Bob"),
						new FieldContent("Role", "Developer"));
			
			Assert.False(firstTableContent.Equals(null));
		}

    }
}
