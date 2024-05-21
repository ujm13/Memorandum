namespace Memorandum.WebApplication.Models.ViewModels
{
    public class ResultViewModel<T>
    {
        /// <summary>
        /// 狀態碼
        /// </summary>
        public int StatuesCode { get; set; }

        /// <summary>
        /// 狀態訊息
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public T Data{get; set;}
}
}
