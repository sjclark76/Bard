using System.Text;

namespace Bard.Internal
{
    internal class Humanize
    {
        internal static string MethodName(string name)
        { 
            StringBuilder humanizedMethodName = new StringBuilder();
            foreach (var character in name)
            {
                if (character == '_') 
                {
                    humanizedMethodName.Append(" ");
                }
                else if (char.IsUpper(character))
                {
                    humanizedMethodName.Append(" ");
                    humanizedMethodName.Append(char.ToUpper(character));
                }
                else
                {
                    humanizedMethodName.Append(char.ToUpper(character));
                }
            }

            return humanizedMethodName.ToString();
            
        }
    }
}