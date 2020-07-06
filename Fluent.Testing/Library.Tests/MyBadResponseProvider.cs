using Fluent.Testing.Library.Then;
using Shouldly;

namespace Fluent.Testing.Library.Tests
{
    public class MyBadResponseProvider : BadRequestResponse<MyCustomErrorMessage>
    {
        public override IBadRequestResponse ForProperty(string propertyName)
        {
            var content = ContentAsString();
            
            content.ShouldContain(propertyName);

            return this;
        }

        public override IBadRequestResponse WithMessage(string message)
        {
            var content = ContentAsString();
            
            content.ShouldContain(message);

            return this;
        }

        public override IBadRequestResponse WithErrorCode(string errorCode)
        {
            var content = ContentAsString();
            
            content.ShouldContain(errorCode);

            return this;
        }

        public override IBadRequestResponse StartsWithMessage(string message)
        {
            var content = ContentAsString();
            
            content.ShouldContain(message);

            return this;
        }

        public override IBadRequestResponse EndsWithMessage(string message)
        {
            var content = ContentAsString();
            
            content.ShouldContain(message);

            return this;
        }
    }
}