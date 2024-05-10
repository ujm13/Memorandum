namespace Memorandum.WebApplication.Models.Parameters
{
    public class RegisterMemberParameter
    {
        //Id應該要自己產外面的用戶或是前端不太會產id給別人產會有重複的問題
        /// <summary>
        /// 帳戶ID
        /// </summary>
        public Guid Id { get; set; }

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
