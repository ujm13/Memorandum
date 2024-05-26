using FluentAssertions;
using Mapster;
using MapsterMapper;
using Memorandum.Common.Enums;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
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
        /// 創建代辦事項測試
        /// </summary>
        [Fact]
        public async Task CreateAsyncTest_輸入待辦事項資訊_創建成功回傳true()
        {
            //Arrange           
            var parameterModelDto = new CreateMemorandumParameterDto
            {                
                Title = "代辦事項名稱",
                Description = "代辦事項內容敘述",
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


        [Fact]
        public async Task CreateAsyncTest_輸入待辦事項資訊_新增代辦事項失敗_回傳MemorandumException()
        {
            //Arrange
            var parameterModelDto = new CreateMemorandumParameterDto
            {               
                Title = "代辦事項名稱",
                Description = "代辦事項內容敘述",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.completed,
                Priority = PriorityEnum.Medium,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            _memorandumRepository.InsertAsync(Arg.Any<InsertMemorandumParameterModel>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.CreateAsync(parameterModelDto);

            //Assert
            await act.Should().ThrowAsync<MemorandumException>().WithMessage("新增代辦事項失敗");
        }
    }
}