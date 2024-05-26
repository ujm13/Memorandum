using FluentAssertions;
using Mapster;
using MapsterMapper;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Implement;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParamaterModelDto;
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
    }
}