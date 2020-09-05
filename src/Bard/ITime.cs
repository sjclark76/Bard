namespace Bard
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITime
    {
        /// <summary>
        /// Assert the response is returned within the specified time
        /// </summary>
        /// <param name="milliseconds"></param>
        void LessThan(int milliseconds);
    }
}