namespace Memorandum.WebApplication.Models.Parameters
{
    /// <summary>
    /// RegisterMemberParameter
    /// </summary>
    public class RegisterMemberParameter
    {

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

    }
}
