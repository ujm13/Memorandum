using FluentAssertions;
using Mapster;
using MapsterMapper;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.Implement;
using Memorandum.Service.infrastructure.Helpers;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Member.ServiceTest
{
    public class MemberTest
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        private readonly IEncryptHelper _encryptHelper;

        public MemberTest()
        {
            _memberRepository = Substitute.For<IMemberRepository>();
            _encryptHelper = Substitute.For<IEncryptHelper>();
            var config = new TypeAdapterConfig();
            config.Scan(typeof(ServiceMapperRegister).Assembly);
            _mapper = new Mapper(config);
            _memberService = new MemberService(_memberRepository, _mapper, _encryptHelper);
        }

        /// <summary>
        /// 會員建立測試
        /// </summary>
        [Fact]
        public async Task RegisterAsyncTest_輸入會員資訊_回傳true()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterDto = new RegisterMemberParameterDto
            {
                Id = id,
                UserName = "使用者名稱",
                Account = "qqq123",
                Password = "00000",
                Email = "hgujgy@gmail.com",
                Phone = "0912345678",
                Birthday = new DateTime(1999, 01, 01)
            };
            _encryptHelper.HashPassword(Arg.Any<string>()).Returns("456fdg465rgegf");
            _memberRepository.InsertAsync(Arg.Any<RegisterMemberParameterModel>()).Returns(true);

            //Actual
            var actual = await _memberService.RegisterAsync(parameterDto);

            //Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterAsyncTest_輸入會員資訊_且會員註冊失敗_拋出RegisterException()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterDto = new RegisterMemberParameterDto
            {
                Id = id,
                UserName = "使用者名稱",
                Account = "qqq123",
                Password = "00000",
                Email = "hgujgy@gmail.com",
                Phone = "0912345678",
                Birthday = new DateTime(1999, 01, 01)
            };
            _memberRepository.InsertAsync(Arg.Any<RegisterMemberParameterModel>()).Returns(false);

            //Actual
            Func<Task> act = () => _memberService.RegisterAsync(parameterDto);

            //Assert
            await act.Should().ThrowAsync<RegisterException>().WithMessage("會員註冊失敗");
        }



        /// <summary>
        /// 會員登入測試
        /// </summary>
        [Fact]
        public async Task LoginAsyncTest_輸入帳號密碼_登入成功回傳LoginMemberResultDto()
        {
            //Arrange
            var parameterDto = new LoginMemberParameterDto
            {
                Account = "qqq123",
                Password = "00000"
            };
            _encryptHelper.HashPassword(Arg.Any<string>()).Returns("456fdg465rgegf");
            _memberRepository.GetAsync(Arg.Any<LoginMemberParameterModel>()).Returns(new LoginMemberDataModel
            {
                Account = "qqq123",
                Password = "456fdg465rgegf",
                UserName = "使用者名稱",
                Email = "hgujgy@gmail.com"
            });

            //Actual
            var actual = await _memberService.LoginAsync(parameterDto);

            //Assert
            actual.Should().BeEquivalentTo(new LoginMemberResultDto
            {
                Account = "qqq123",
                UserName = "使用者名稱",
                Email = "hgujgy@gmail.com"
            });
        }

        [Fact]
        public async Task LoginAsyncTest_輸入帳號密碼_查詢結果無此會員_回傳MemberNotFoundException()
        {
            //Arrange
            var parameterDto = new LoginMemberParameterDto
            {
                Account = "qqq123",
                Password = "00000"
            };

            _memberRepository.GetAsync(Arg.Any<LoginMemberParameterModel>()).ReturnsNull();

            //actual
            Func<Task> act = () => _memberService.LoginAsync(parameterDto);

            //assert
            await act.Should().ThrowAsync<MemberNotFoundException>().WithMessage("查無此會員");
        }

        [Fact]
        public async Task LoginAsyncTest_輸入帳號密碼_會員密碼錯誤_回傳LoginFailedException()
        {
            //Arrange
            var parameterDto = new LoginMemberParameterDto
            {
                Account = "qqq123",
                Password = "0000o"
            };

            _memberRepository.GetAsync(Arg.Any<LoginMemberParameterModel>()).Returns(new LoginMemberDataModel
            {
                Account = "qqq123",
                Password = "00000",
                UserName = "使用者名稱",
                Email = "hgujgy@gmail.com"
            });

            //actual
            Func<Task> act = () => _memberService.LoginAsync(parameterDto);

            //assert 
            await act.Should().ThrowAsync<LoginFailedException>().WithMessage("會員密碼錯誤");
        }
    }
}