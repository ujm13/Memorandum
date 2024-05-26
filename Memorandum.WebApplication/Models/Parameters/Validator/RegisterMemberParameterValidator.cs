using FluentValidation;

namespace Memorandum.WebApplication.Models.Parameters.Validator
{
    public class RegisterMemberParameterValidator : AbstractValidator<RegisterMemberParameter>
    {
        public RegisterMemberParameterValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("使用者名稱不得為空");

            RuleFor(x => x.Birthday)
            .NotEmpty().WithMessage("生日不得為空")
            .LessThan(DateTime.Now).WithMessage("生日不得大於今天");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("電子郵件不得為空")
            .EmailAddress().WithMessage("電子郵件格式錯誤");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("密碼不得為空")
            .Length(6,15).WithMessage("密碼長度至少6個字符，最多15字符")
            .Matches(@"[A-Z]").WithMessage("密碼須包含大寫字母");

        }
    }
}
