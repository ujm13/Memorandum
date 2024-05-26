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


        [Fact]
        public async Task CreateAsyncTest_��J�ݿ�ƶ���T_�s�W�N��ƶ�����_�^��MemorandumException()
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
                Status = StatusEnum.completed,
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
                Status = StatusEnum.completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = DateTime.Now
            };
            _memorandumRepository.UpdateAsync(Arg.Any<UpdateMemorandumParameterModel>()).Returns(false);

            //Actual
            Func<Task> act = () => _memorandumService.UpdateAsync(id,parameterModelDto);

            //Assert
            await act.Should().ThrowAsync<MemorandumException>().WithMessage("��s�N��ƶ�����");
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
                Status = StatusEnum.completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = nowDate
            });

            //Actual
            var actual = await _memorandumService.GetDetailAsync(id);

            //Assert
            actual.Should().BeEquivalentTo(new MemorandumDataModel
            {
                Id = id,
                Title = "�N��ƶ��W��",
                Description = "�N��ƶ����e�ԭz",
                DueDate = new DateTime(1999, 01, 01, 10, 00, 00),
                Status = StatusEnum.completed,
                Priority = PriorityEnum.Medium,
                UpdateTime = nowDate
            });
        }

        [Fact]
        public async Task GetDetailAsyncTest_��J�d��id_�Bid���w�]��_�^��MemorandumNotFountException()
        {
            //Arrange
            var id = Guid.Empty;

            //Actual
            Func<Task> act = () => _memorandumService.GetDetailAsync(id);

            //Assert
            await act.Should().ThrowAsync<MemorandumNotFountException>().WithMessage($"id {id} is empty");
        }
    }
}