using Memorandum.Common.Enums;

namespace Memorandum.WebApplication.Models.ViewModels
{
    public class MemorandumResultViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 敘述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 優先級
        /// </summary>
        public PriorityEnum Priority { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最後更新時間
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
