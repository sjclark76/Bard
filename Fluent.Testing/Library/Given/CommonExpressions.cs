namespace Fluent.Testing.Library.Given
{
    public static class CommonExpressions
    {
        public static T And<T>(this T current) where T : IBeginAScenario
        {
            Scenario.PrependToNextLogMessage("and");
            return current;
        }

        public static T The<T>(this T current) where T : IBeginAScenario
        {
            Scenario.PrependToNextLogMessage("the");
            return current;
        }

        public static T Then<T>(this T current) where T : IBeginAScenario
        {
            Scenario.PrependToNextLogMessage("then");
            return current;
        }

        public static T A<T>(this T current) where T : IBeginAScenario
        {
            Scenario.PrependToNextLogMessage("a");
            return current;
        }
    }
}