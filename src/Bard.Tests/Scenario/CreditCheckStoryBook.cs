using Bard;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class CreditCheckStoryBook : StoryBook
    {
        public EndChapter<object> Nothing_much_happens()
        {
            return When(context =>
            {
                return new object();
            }).End();

        }
    }
}