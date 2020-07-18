using Shouldly;

namespace Fluent.Testing.Library.Tests
{
    public class MyBadRequestProvider : BadRequestProvider<MyCustomErrorMessage>
    {
        public override IBadRequestProvider ForProperty(string propertyName)
        {
            var content = StringContent;

            content.ShouldContain(propertyName);

            return this;
        }

        public override IBadRequestProvider WithMessage(string message)
        {
            var content = StringContent;

            content.ShouldContain(message);

            return this;
        }

        public override IBadRequestProvider WithErrorCode(string errorCode)
        {
            var content = StringContent;

            content.ShouldContain(errorCode);

            return this;
        }

        public override IBadRequestProvider StartsWithMessage(string message)
        {
            var content = StringContent;

            content.ShouldContain(message);

            return this;
        }

        public override IBadRequestProvider EndsWithMessage(string message)
        {
            var content = StringContent;

            content.ShouldContain(message);

            return this;
        }
    }
}