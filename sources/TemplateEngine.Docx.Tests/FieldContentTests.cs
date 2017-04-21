using Xunit;

namespace TemplateEngine.Docx.Tests
{
    public class FieldContentTests
    {
        [Fact]
        public void FieldContentConstructorWithArguments_FillNameAndValue()
        {
            var fieldContent = new FieldContent("Name", "Value");

            Assert.Equal("Name", fieldContent.Name);
            Assert.Equal("Value", fieldContent.Value);
        }

        [Fact]
        public void EqualsTest_ValuesAreEquel_Equals()
        {
            var firstFieldContent = new FieldContent("Name", "Value");
            var secondFieldContent = new FieldContent("Name", "Value");

            Assert.True(firstFieldContent.Equals(secondFieldContent));
        }

        [Fact]
        public void EqualsTest_ValuesAreNotEqual_NotEquals()
        {
            var firstFieldContent = new FieldContent("Name", "Value");
            var secondFieldContent = new FieldContent("Name", "Value2");

            Assert.False(firstFieldContent.Equals(secondFieldContent));
        }
        [Fact]
        public void EqualsTest_CompareWithNull_NotEquals()
        {
            var firstFieldContent = new FieldContent("Name", "Value");
            var secondFieldContent = new FieldContent("Name", "Value2");

            Assert.False(firstFieldContent.Equals(null));
        }
    }
}
