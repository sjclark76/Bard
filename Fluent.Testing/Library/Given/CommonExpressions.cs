namespace Fluent.Testing.Library.Given
{
    public static class CommonExpressions
    {
        public static T And<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("and");
            return current;
        }

        public static T The<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("the");
            return current;
        }

        public static T Then<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("then");
            return current;
        }

        public static T A<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("a");
            return current;
        }
    }
}