using FluentAssertions;
using Mapster;
using MapsterMapper;
using Memorandum.Common.Enums;
using Memorandum.Repository.infrastructure.MapperRegisters;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Exceptions;
using Memorandum.Service.Implement;
using Memorandum.Service.Interfaces;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
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
                Status = StatusEnum.Completed,
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
                Status = StatusEnum.Completed,
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

        /// <summary>
        /// 更新代辦事項測試
        /// </summary>
        [Fact]
        public async Task UpdateAsyncTest_輸入更新資訊_更新成功回傳true()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "更新代辦事項名稱",
                Description = "更新代辦事項內容敘述",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = DateTime.Now
            };
            _memorandumRepository.UpdateAsync(Arg.Any<UpdateMemorandumParameterModel>()).Returns(true);

            //Actual
            var actual = await _memorandumService.UpdateAsync(id, parameterModelDto);

            //Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsyncTest_輸入更新資訊_更新代辦事項失敗_回傳MemorandumException()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "更新代辦事項名稱",
                Description = "更新代辦事項內容敘述",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = DateTime.Now
            };
            _memorandumRepository.UpdateAsync(Arg.Any<UpdateMemorandumParameterModel>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.UpdateAsync(id, parameterModelDto);

            //Assert
            await act.Should().ThrowAsync<MemorandumException>().WithMessage("更新代辦事項失敗");
        }

        [Fact]
        public async Task UpdateAsyncTest_輸入查詢id_且id為預設值_回傳MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.Empty;
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "更新代辦事項名稱",
                Description = "更新代辦事項內容敘述",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = DateTime.Now
            };

            //Actual
            Func<Task> act = () => _memorandumService.UpdateAsync(id, parameterModelDto);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} is empty");
        }

        [Fact]
        public async Task UpdateAsyncTest_輸入查詢id_查詢結果為false_回傳MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "更新代辦事項名稱",
                Description = "更新代辦事項內容敘述",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = DateTime.Now
            };
            _memorandumRepository.IsExistIdAsync(Arg.Any<Guid>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.UpdateAsync(id, parameterModelDto);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} not found");

        }

        /// <summary>
        /// 取得明細資料測試
        /// </summary>
        [Fact]
        public async Task GetDetailAsyncTest_輸入查詢id_查詢成功回傳明細資料()
        {
            //Arrange
            var id = Guid.NewGuid();

            var nowDate = DateTime.Now;
            _memorandumRepository.GetDetailAsync(Arg.Any<Guid>()).Returns(new MemorandumDataModel
            {
                Id = id,
                Title = "代辦事項名稱",
                Description = "代辦事項內容敘述",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = nowDate
            });

            //Actual
            var actual = await _memorandumService.GetDetailAsync(id);

            //Assert
            actual.Should().BeEquivalentTo(new MemorandumResultModelDto
            {
                Id = id,
                Title = "代辦事項名稱",
                Description = "代辦事項內容敘述",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = nowDate
            });
        }

        [Fact]
        public async Task GetDetailAsyncTest_輸入查詢id_且id為預設值_回傳MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.Empty;

            //Actual
            Func<Task> act = () => _memorandumService.GetDetailAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} is empty");
        }

        [Fact]
        public async Task GetDetailAsyncTest_輸入查詢id_查詢結果為Null_回傳MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();
            _memorandumRepository.GetDetailAsync(Arg.Any<Guid>()).ReturnsNull();

            //Actual
            Func<Task> act = () => _memorandumService.GetDetailAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} not found");

        }


        /// <summary>
        /// 取得所有待辦事項測試
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllAsyncTest_查詢所有代辦事項_查詢成功回傳所有資料()
        {
            //Arrange
            var id = Guid.NewGuid();
            var nowDate = DateTime.Now;
            _memorandumRepository.GetAllAsync().Returns(new List<MemorandumDataModel>
            {
                new MemorandumDataModel
                {
                    Id = id,
                    Title = "代辦事項名稱",
                    Description = "代辦事項內容敘述",
                    DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                    Status = StatusEnum.Completed,
                    Priority = PriorityEnum.Medium,
                    CreateTime=new DateTime(2024, 05, 26, 10, 00, 00),
                    UpdateTime = nowDate
                }
            });

            //Actual
            var actual = await _memorandumService.GetAllAsync();

            //Assert
            actual.Should().BeEquivalentTo(new List<MemorandumResultModelDto>
            {
                new MemorandumResultModelDto
                {
                    Id = id,
                    Title = "代辦事項名稱",
                    Description = "代辦事項內容敘述",
                    DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                    Status = StatusEnum.Completed,
                    Priority = PriorityEnum.Medium,
                    CreateTime=new DateTime(2024, 05, 26, 10, 00, 00),
                    UpdateTime = nowDate
                }
            });
        }

        /// <summary>
        /// 刪除代辦事項測試
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteAsyncTest_輸入要刪除的id_刪除成功回傳true()
        {
            //Arrange
            var id = Guid.NewGuid();
            _memorandumRepository.DeleteAsync(Arg.Any<Guid>()).Returns(true);

            //Actual
            var actual = await _memorandumService.DeleteAsync(id);

            //Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsyncTest_輸入要刪除的id_id為預設值_回傳MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.Empty;

            //Actual
            Func<Task> act =()=> _memorandumService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} is empty");
        }

        [Fact]
        public async Task DeleteAsyncTest_輸入查詢id_查詢結果為false_回傳MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();
            _memorandumRepository.IsExistIdAsync(Arg.Any<Guid>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} not found");

        }

        [Fact]
        public async Task DeleteAsyncTest_輸入要刪除的id_刪除代辦事項失敗_回傳MemorandumException()
        {
            //Arrange
            var id = Guid.NewGuid();
            _memorandumRepository.DeleteAsync(Arg.Any<Guid>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumException>().WithMessage("刪除代辦事項失敗");
        }
    }
}