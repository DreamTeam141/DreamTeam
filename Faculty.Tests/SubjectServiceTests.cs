using System.Collections.Generic;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Services;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;
using Faculty.DAL.Repositories;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Faculty.Tests
{
    [TestFixture]
    public class SubjectServiceTests
    {
        private Subject _subject;
        private Subject _subject2;
        private SubjectDTO _subjectDto;
        private SubjectDTO _subjectDto2;
        private List<Subject> _subjectsList;
        private List<SubjectDTO> _subjectsDtoList;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<Subject>> _subjectRepositoryMock;
        private ISubjectService _subjectService;


        [SetUp]
        public void SetUp()
        {
            _subject = new Subject
            {
                Id = 1,
                Title = "Title"
            };

            _subject2 = new Subject
            {
                Id = 2,
                Title = "Title2"
            };

            _subjectDto = new SubjectDTO
            {
                Id = 1,
                Title = "Title"
            };

            _subjectDto2 = new SubjectDTO
            {
                Id = 2,
                Title = "Title2"
            };

            _subjectsList = new List<Subject> { _subject, _subject2 };
            _subjectsDtoList = new List<SubjectDTO> { _subjectDto, _subjectDto2 };

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(x => x.Map<Subject>(_subjectDto))
                .Returns(_subject);
            _mapperMock.Setup(x => x.Map<SubjectDTO>(_subject))
                .Returns(_subjectDto);
            _mapperMock.Setup(x => x.Map<List<Subject>>(_subjectsDtoList))
                .Returns(_subjectsList);
            _mapperMock.Setup(x => x.Map<List<SubjectDTO>>(_subjectsList))
                .Returns(_subjectsDtoList);

            _subjectRepositoryMock = new Mock<IRepository<Subject>>();
            _subjectRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(_subject);
            _subjectRepositoryMock.Setup(x => x.GetAll())
                .Returns(_subjectsList);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.SubjectRepository)
                .Returns(_subjectRepositoryMock.Object);

            _subjectService = new SubjectService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public void GetSubjectDtoById_GetSubject_Succes()
        {
            _subjectService.GetSubjectDtoById(It.IsAny<int>());
            _subjectRepositoryMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Create_CreateSubjects_Success()
        {
            _subjectService.Create(_subjectDto);
            _subjectRepositoryMock.Verify(x => x.Create(_subject), Times.Once);
        }

        [Test]
        public void EditSubject_Update_Success()
        {
            _subjectService.EditSubject(_subjectDto);
            _subjectRepositoryMock.Verify(x => x.Update(_subject), Times.Once);
        }

        [Test]
        public void DeleteSubject_Delete_Success()
        {
            _subjectService.DeleteSubject(It.IsAny<int>());
            _subjectRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetSubjects_Get_Success()
        {
            _subjectService.GetSubjects();
            _subjectRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetSubjects_Get_NotNull()
        {
            var res = _subjectService.GetSubjects();
            Assert.NotNull(res);
        }
    }
}