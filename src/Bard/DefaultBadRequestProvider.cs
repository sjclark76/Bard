using System;
using Bard.Internal;

namespace Bard
{
    public class DefaultBadRequestProvider : BadRequestProviderBase
    {
        public override IBadRequestProvider ForProperty(string propertyName)
        {
            ShouldContain(propertyName);

            return this;
        }

        public override IBadRequestProvider WithMessage(string message)
        {
            ShouldContain(message);

            return this;
        }

        public override IBadRequestProvider WithErrorCode(string errorCode)
        {
            ShouldContain(errorCode);

            return this;
        }

        public override IBadRequestProvider StartsWithMessage(string message)
        {
            var content = StringContent;

            if (content.StartsWith(message) == false)
                throw new BardException($"The received response did not start with the message:{message}");
                
            return this;
        }

        public override IBadRequestProvider EndsWithMessage(string message)
        {
            var content = StringContent;

            if (content.Equals(message) == false)
                throw new BardException($"The received response did not end with the message:{message}");

            return this;
        }
        private void ShouldContain(string value)
        {
            if (StringContent.Contains(value, StringComparison.InvariantCultureIgnoreCase) == false)
            {
                throw new BardException($"The received response did not contain the message:{value}");
            }
        }
    }
}