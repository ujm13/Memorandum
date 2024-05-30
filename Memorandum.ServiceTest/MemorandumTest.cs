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
        public async Task CreateAsyncTest_��J�ݿ�ƶ���T_�s�W�N��ƶ�����_�^��MemorandumException()
        {
            //Arrange
            var parameterModelDto = new CreateMemorandumParameterDto
            {
                Title = "�N��ƶ��W��",
                Description = "�N��ƶ����e�ԭz",
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
            await act.Should().ThrowAsync<MemorandumException>().WithMessage("�s�W�N��ƶ�����");
        }

        /// <summary>
        /// ��s�N��ƶ�����
        /// </summary>
        [Fact]
        public async Task UpdateAsyncTest_��J��s��T_��s���\�^��true()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "��s�N��ƶ��W��",
                Description = "��s�N��ƶ����e�ԭz",
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
        public async Task UpdateAsyncTest_��J��s��T_��s�N��ƶ�����_�^��MemorandumException()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "��s�N��ƶ��W��",
                Description = "��s�N��ƶ����e�ԭz",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = DateTime.Now
            };
            _memorandumRepository.UpdateAsync(Arg.Any<UpdateMemorandumParameterModel>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.UpdateAsync(id, parameterModelDto);

            //Assert
            await act.Should().ThrowAsync<MemorandumException>().WithMessage("��s�N��ƶ�����");
        }

        [Fact]
        public async Task UpdateAsyncTest_��J�d��id_�Bid���w�]��_�^��MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.Empty;
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "��s�N��ƶ��W��",
                Description = "��s�N��ƶ����e�ԭz",
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
        public async Task UpdateAsyncTest_��J�d��id_�d�ߵ��G��false_�^��MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();
            var parameterModelDto = new UpdateMemorandumParameterDto
            {
                Title = "��s�N��ƶ��W��",
                Description = "��s�N��ƶ����e�ԭz",
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
        /// ���o���Ӹ�ƴ���
        /// </summary>
        [Fact]
        public async Task GetDetailAsyncTest_��J�d��id_�d�ߦ��\�^�ǩ��Ӹ��()
        {
            //Arrange
            var id = Guid.NewGuid();

            var nowDate = DateTime.Now;
            _memorandumRepository.GetDetailAsync(Arg.Any<Guid>()).Returns(new MemorandumDataModel
            {
                Id = id,
                Title = "�N��ƶ��W��",
                Description = "�N��ƶ����e�ԭz",
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
                Title = "�N��ƶ��W��",
                Description = "�N��ƶ����e�ԭz",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.Completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = nowDate
            });
        }

        [Fact]
        public async Task GetDetailAsyncTest_��J�d��id_�Bid���w�]��_�^��MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.Empty;

            //Actual
            Func<Task> act = () => _memorandumService.GetDetailAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} is empty");
        }

        [Fact]
        public async Task GetDetailAsyncTest_��J�d��id_�d�ߵ��G��Null_�^��MemorandumNotFoundException()
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
        /// ���o�Ҧ��ݿ�ƶ�����
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllAsyncTest_�d�ߩҦ��N��ƶ�_�d�ߦ��\�^�ǩҦ����()
        {
            //Arrange
            var id = Guid.NewGuid();
            var nowDate = DateTime.Now;
            _memorandumRepository.GetAllAsync().Returns(new List<MemorandumDataModel>
            {
                new MemorandumDataModel
                {
                    Id = id,
                    Title = "�N��ƶ��W��",
                    Description = "�N��ƶ����e�ԭz",
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
                    Title = "�N��ƶ��W��",
                    Description = "�N��ƶ����e�ԭz",
                    DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                    Status = StatusEnum.Completed,
                    Priority = PriorityEnum.Medium,
                    CreateTime=new DateTime(2024, 05, 26, 10, 00, 00),
                    UpdateTime = nowDate
                }
            });
        }

        /// <summary>
        /// �R���N��ƶ�����
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteAsyncTest_��J�n�R����id_�R�����\�^��true()
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
        public async Task DeleteAsyncTest_��J�n�R����id_id���w�]��_�^��MemorandumNotFoundException()
        {
            //Arrange
            var id = Guid.Empty;

            //Actual
            Func<Task> act =()=> _memorandumService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFoundException>().WithMessage($"id {id} is empty");
        }

        [Fact]
        public async Task DeleteAsyncTest_��J�d��id_�d�ߵ��G��false_�^��MemorandumNotFoundException()
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
        public async Task DeleteAsyncTest_��J�n�R����id_�R���N��ƶ�����_�^��MemorandumException()
        {
            //Arrange
            var id = Guid.NewGuid();
            _memorandumRepository.DeleteAsync(Arg.Any<Guid>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumException>().WithMessage("�R���N��ƶ�����");
        }
    }
}