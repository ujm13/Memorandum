using FluentAssertions;
using Mapster;
using MapsterMapper;
using Memorandum.Common.Enums;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Implement;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParameterDto;
using NSubstitute;
using Xunit;

namespace Memorandum.ServiceTest
{
    public class MemorandumTest
    {
        private readonly IMemorandumRepository _memorandumRepository;
        private readonly IMemorandumService _memorandumService;
        private readonly IMapper _mapper;
        public MemorandumTest()
        {
            _memorandumRepository = Substitute.For<IMemorandumRepository>();
            var config = new TypeAdapterConfig();
            config.Scan(typeof(ServiceMapperRegister).Assembly);
            _mapper = new Mapper(config);
            _memorandumService = new MemorandumService(_memorandumRepository, _mapper);
        }

        /// <summary>
        /// �ЫإN��ƶ�����
        /// </summary>
        [Fact]
        public async Task CreateAsyncTest_��J�ݿ�ƶ���T_�Ыئ��\�^��true()
        {
            //Arrange           
            var parameterModelDto = new CreateMemorandumParameterDto
            {                
                Title = "�N��ƶ��W��",
                Description = "�N��ƶ����e�ԭz",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.completed,
                Priority = PriorityEnum.Medium,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            _memorandumRepository.InsertAsync(Arg.Any<InsertMemorandumParameterModel>()).Returns(true);
            //Actual
            var actual = await _memorandumService.CreateAsync(parameterModelDto);

            //Assert
            actual.Should().BeTrue();
        }
    }
}