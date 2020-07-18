namespace Fluent.Testing.Library
{
    public static class CommonExpressions
    {
        public static T And<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("And");
            return current;
        }

        public static T The<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("The");
            return current;
        }

        public static T Then<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("Then");
            return current;
        }

        public static T A<T>(this T current) where T : ScenarioBase
        {
            current.AddMessage("A");
            return current;
        }
    }
}