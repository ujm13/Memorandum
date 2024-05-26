using FluentAssertions;
using Mapster;
using MapsterMapper;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.Implement;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;
using NSubstitute;

namespace Member.ServiceTest
{
    public class MemberTest
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberTest()
        {
            _memberRepository = Substitute.For<IMemberRepository>();
            var config = new TypeAdapterConfig();
            config.Scan(typeof(ServiceMapperRegister).Assembly);
            _mapper = new Mapper(config);
            _memberService = new MemberService(_memberRepository, _mapper);
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
            _memberRepository.InsertAsync(Arg.Any<RegisterMemberParameterModel>()).Returns(true);

            //Actual
            var actual = await _memberService.RegisterAsync(parameterDto);

            //Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterAsyncTest_輸入會員資訊_且會員資料插入失敗_拋出RegisterException()
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
            await act.Should().ThrowAsync<RegisterException>().WithMessage("會員註冊資料插入失敗");
        }



        /// <summary>
        /// 會員登入測試
        /// </summary>
        [Fact]
        public async Task LoginAsync_輸入帳號密碼_登入成功回傳LoginMemberResultDto()
        {
            //Arrange
            var parameterDto = new LoginMemberParameterDto
            {
                Account = "qqq123",
                Password = "00000"
            };

            _memberRepository.GetAsync(Arg.Any<LoginMemberParameterModel>()).Returns(new LoginMemberDataModel
            {
                Account = "qqq123",
                Password = "00000",
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
    }
}