namespace Memorandum.Service.Exceptions
{
    public class MemorandumNotFoundException:Exception
    {
        public MemorandumNotFoundException(string message):base(message) 
        {
        }
    }
}
