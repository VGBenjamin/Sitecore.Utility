using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.Utility.Extensions;
using Xunit;

namespace Sitecore.Utility.Test
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("sitecore", "#sitecore#")]
        [InlineData("/sitecore", "/#sitecore#")]
        [InlineData("/sitecore/content/Test-Site", "/#sitecore#/#content#/#Test-Site#")]
        [InlineData("/sitecore/content/TestSites/Test/Data/Ambiances/2016/09/28/15/56/STRASS BLANC 1 SDB/ancestor::*[@@templateid='{54A1C811-C8A6-40D9-B6C3-00930EFF1CDE}']/*[@@templateid='{C79967AC-E386-4AAA-B5CF-78184172E138}']", "/#sitecore#/#content#/#TestSites#/#Test#/#Data#/#Ambiances#/#2016#/#09#/#28#/#15#/#56#/#STRASS BLANC 1 SDB#/ancestor::*[@@templateid='{54A1C811-C8A6-40D9-B6C3-00930EFF1CDE}']/*[@@templateid='{C79967AC-E386-4AAA-B5CF-78184172E138}']")]
        [InlineData("/sitecore/content/TestSites/Test/Data/Ambiances/2016/09/28/15/56/STRASS BLANC 1 SDB/ancestor::*[@@templateid='{54A1C811-C8A6-40D9-B6C3-00930EFF1CDE}']/Test[@@templateid='{C79967AC-E386-4AAA-B5CF-78184172E138}']", "/#sitecore#/#content#/#TestSites#/#Test#/#Data#/#Ambiances#/#2016#/#09#/#28#/#15#/#56#/#STRASS BLANC 1 SDB#/ancestor::*[@@templateid='{54A1C811-C8A6-40D9-B6C3-00930EFF1CDE}']/#Test#[@@templateid='{C79967AC-E386-4AAA-B5CF-78184172E138}']")]

        public void SanitizeQuery_ValidData(string query, string expected)
        {
            //Assign

            //Act
            var returned = query.SanitizeQuery();

            //Assert
            returned.Should().Be(expected);
        }
    }
}
