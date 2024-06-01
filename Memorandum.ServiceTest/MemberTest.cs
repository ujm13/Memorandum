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
        /// �|���إߴ���
        /// </summary>
        [Fact]
        public async Task RegisterAsyncTest_��J�|����T_�^��true()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterDto = new RegisterMemberParameterDto
            {
                Id = id,
                UserName = "�ϥΪ̦W��",
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
        public async Task RegisterAsyncTest_��J�|����T_�B�|�����U����_�ߥXRegisterException()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterDto = new RegisterMemberParameterDto
            {
                Id = id,
                UserName = "�ϥΪ̦W��",
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
            await act.Should().ThrowAsync<RegisterException>().WithMessage("�|�����U����");
        }



        /// <summary>
        /// �|���n�J����
        /// </summary>
        [Fact]
        public async Task LoginAsyncTest_��J�b���K�X_�n�J���\�^��LoginMemberResultDto()
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
                UserName = "�ϥΪ̦W��",
                Email = "hgujgy@gmail.com"
            });

            //Actual
            var actual = await _memberService.LoginAsync(parameterDto);

            //Assert
            actual.Should().BeEquivalentTo(new LoginMemberResultDto
            {
                Account = "qqq123",
                UserName = "�ϥΪ̦W��",
                Email = "hgujgy@gmail.com"
            });
        }

        [Fact]
        public async Task LoginAsyncTest_��J�b���K�X_�d�ߵ��G�L���|��_�^��MemberNotFoundException()
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
            await act.Should().ThrowAsync<MemberNotFoundException>().WithMessage("�d�L���|��");
        }

        [Fact]
        public async Task LoginAsyncTest_��J�b���K�X_�|���K�X���~_�^��LoginFailedException()
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
                UserName = "�ϥΪ̦W��",
                Email = "hgujgy@gmail.com"
            });

            //actual
            Func<Task> act = () => _memberService.LoginAsync(parameterDto);

            //assert 
            await act.Should().ThrowAsync<LoginFailedException>().WithMessage("�|���K�X���~");
        }
    }
}