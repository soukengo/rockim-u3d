using System;
using NUnit.Framework;
using RockIM.Sdk.Utils;
using RockIM.Shared.Reasons;

namespace RockIM.Tests
{
    public class RockImTest
    {
        [Test]
        public void TestEnum()
        {
            var attribute = ProtobufUtils.GetEnumName(User.Types.ErrorReason.AccessTokenInvalid);
            Console.WriteLine(attribute);
        }
    }
}