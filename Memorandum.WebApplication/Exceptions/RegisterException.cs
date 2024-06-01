namespace Memorandum.Repository.Exceptions
{
    /// <summary>
    /// RegisterException
    /// </summary>
    public class RegisterException:Exception
    {
        /// <summary>
        /// RegisterException
        /// </summary>
        /// <param name="message"></param>
        public RegisterException(string message):base(message)
        { 
        }
    }
}
