using FSH.WebApi.Domain.Article;
using Moq;
using NUnit.Framework;
using System;

namespace Domain.Test.Article
{
    [TestFixture]
    public class CategoryTests
    {
        private MockRepository mockRepository;



        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private Category CreateCategory()
        {
            return Category.Create("en", "", "", "");
        }

        [Test]
        public void Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var category = this.CreateCategory();
            string? cultureCode = null;
            string? name = null;
            string? description = null;
            string? color = null;

            // Act
            var result = category.Update(
                cultureCode,
                name,
                description,
                color);

            // Assert
            Assert.Fail();
            this.mockRepository.Create<News>().VerifyAll();
        }
    }
}
