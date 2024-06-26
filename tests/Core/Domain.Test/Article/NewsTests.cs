﻿using FSH.WebApi.Domain.Article;
using Moq;
using NUnit.Framework;
using System;

namespace Domain.Test.Article
{
    [TestFixture]
    public class NewsTests
    {
        private MockRepository mockRepository;



        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private News CreateNews()
        {
            return News.Create("eg", "", "", "", "", "", "", "", "", Guid.NewGuid());
        }

        [Test]
        public void Update_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var news = this.CreateNews();
            string? title = null;
            string? description = null;
            string? body = null;
            string? subTitle = null;
            string? seoTitle = null;
            string? socialTitle = null;
            string? cultureCode = null;
            string? mainImagePath = null;
            Guid? categoryId = Guid.Empty;

            // Act
            var result = news.Update(
                title,
                description,
                body,
                subTitle,
                seoTitle,
                socialTitle,
                cultureCode,
                mainImagePath,
                (Guid)categoryId);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}
