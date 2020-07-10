namespace Fluent.Testing.Library.Given
{
    public static class CommonExpressions
    {
        public static T And<T>(this T current) where T : BeginAScenario
        {
            Scenario.PrependToNextLogMessage("and");
            return current;
        }

        public static T The<T>(this T current) where T : BeginAScenario
        {
            Scenario.PrependToNextLogMessage("the");
            return current;
        }

        public static T Then<T>(this T current) where T : BeginAScenario
        {
            Scenario.PrependToNextLogMessage("then");
            return current;
        }

        public static T A<T>(this T current) where T : BeginAScenario
        {
            Scenario.PrependToNextLogMessage("a");
            return current;
        }
    }
}