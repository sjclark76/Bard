// namespace Fluent.Testing.Library.Given
// {
//     public class Scenario
//     {
//         private static string _logTextToPrepend = "";
//
//         // public static T And<T>(this T current) where T:Scenario
//         // {
//         //     Scenario.PrependToNextLogMessage("and");
//         //     return current;
//         // }
//         //
//         // public static T The<T>(this T current) where T:Scenario
//         // {
//         //     Scenario.PrependToNextLogMessage("the");
//         //     return current;
//         // }
//         //
//         // public static T Then<T>(this T current) where T:Scenario
//         // {
//         //     Scenario.PrependToNextLogMessage("then");
//         //     return current;
//         // }
//
//         // public Scenario<TScenario> The => ApplyAdjective("the");
//         // public Scenario<TScenario> And => ApplyAdjective("and");
//         //
//         // public Scenario<TScenario> A => ApplyAdjective("a");
//         //
//         // private Scenario<TScenario> ApplyAdjective(string adjective)
//         // {
//         //     PrependToNextLogMessage(adjective);
//         //     return this;
//         // }
//
//         public static void PrependToNextLogMessage(string text)
//         {
//             if (_logTextToPrepend.Length > 0) _logTextToPrepend += " ";
//
//             _logTextToPrepend += text;
//         }
//     }
// }