using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Enum;
using EntityLayer.Concrete.ReportSql;
using EntityLayer.Request;
using EntityLayer.Response;
using EntityLayer.Response.Common;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AssessmentProject.Persons.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<PersonController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public PersonController(IRepositoryWrapper repositoryWrapper, ILogger<PersonController> logger, IPublishEndpoint publishEndpoint)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Yeni Bir Kullanıcı Eklemek İçin Oluşturulan Fonksiyondur.
        /// Bir Kullanıcıya Birden Fazla İletişim Bilgisini de Eklememize yaramaktadır.
        /// </summary>
        [HttpPost]
        public BaseResponse AddPerson(AddPersonRequest request)
        {
            BaseResponse response = new BaseResponse()
            {
                Message = string.Empty,
                Status = true
            };
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    throw new ArgumentNullException("İsim Alanı Boş Bırakılamaz");
                }
                if (string.IsNullOrEmpty(request.SurName))
                {
                    throw new ArgumentNullException("Soyadı Alanı Boş Bırakılamaz");
                }
                Person person = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    SurName = request.SurName,
                    Company = request.Company
                };
                List<Communication> communications = new List<Communication>();
                if (request.InformationTypes != null && request.InformationTypes.Count > 0)
                {
                    foreach (var item in request.InformationTypes)
                    {
                        if (!item.InformationType.HasValue)
                            continue;

                        communications.Add(new Communication()
                        {
                            Id = Guid.NewGuid(),
                            InformationContent = item.InformationContent,
                            InformationType = (CommunicationEnum)item.InformationType.Value
                        });
                    }
                    person.Communications = communications;
                }
                string saveMessage = _repositoryWrapper.PersonRepository.Insert(person);
                _repositoryWrapper.Save();
                if (!string.IsNullOrEmpty(saveMessage))
                {
                    response.Message = saveMessage;
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        /// <summary>
        /// Bir Kullanıcıyı Silmek İçin Oluşturulmuştur.
        /// ID Bilgisine Göre Silme İşlemini Gerçekleştirir.
        /// </summary>
        [HttpPost]
        public BaseResponse DeletePerson(DeletePersonRequest request)
        {
            BaseResponse response = new()
            {
                Message = string.Empty,
                Status = true
            };
            try
            {
                if (request.Id == null)
                {
                    throw new ArgumentNullException("Id Alanı Boş Bırakılamaz");
                }
                var person = _repositoryWrapper.PersonRepository.GetByIdIncludeCommunication(request.Id ?? Guid.Empty);
                if (person != null)
                {
                    string DeleteMessage = _repositoryWrapper.PersonRepository.Delete(person);
                    var communications = _repositoryWrapper.CommunicationRepository.FindByFilter(x => x.PersonId == person.Id).ToList();
                    foreach (var item in communications)
                    {
                        _repositoryWrapper.CommunicationRepository.Delete(item);
                    }
                    _repositoryWrapper.Save();

                    if (!string.IsNullOrEmpty(DeleteMessage))
                    {
                        response.Message = DeleteMessage;
                        response.Status = false;
                    }
                }
                else
                {
                    throw new ArgumentException("İlgili Kişi Bulunamadı.!");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        /// <summary>
        /// Var Olan Kullanıcıya İletişim Bilgisi Ekleme İşlemi İçin Yazılmıştır.
        /// Request inde ise Id Bilgisine Göre İlgili Kullanıcı Bulunur ve İstenilen İletişim Bilgisi Eklenmektedir.
        /// </summary>
        [HttpPost]
        public BaseResponse AddPersonCommunication(AddPersonCommunicationRequest request)
        {
            BaseResponse response = new()
            {
                Message = string.Empty,
                Status = true,
            };
            try
            {
                if (request.Id == null || request.Id == Guid.Empty)
                {
                    throw new ArgumentNullException("ID Alanı Boş Olamaz!.");
                }
                if (!request.InformationType.HasValue)
                {
                    throw new ArgumentException("İletişim Tipi Boş Bırakılamaz.!");
                }
                var person = _repositoryWrapper.PersonRepository.GetByIdIncludeCommunication(request.Id ?? Guid.Empty);
                if (person == null)
                {
                    throw new ArgumentException("Kişi Bulunamadı.!");
                }
                var communicationId = Guid.NewGuid();
                _repositoryWrapper.CommunicationRepository.Insert(new Communication()
                {
                    Id = communicationId,
                    InformationContent = request.InformationContent,
                    InformationType = (CommunicationEnum)request.InformationType.Value,
                    PersonId = person.Id,
                });
                _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        /// <summary>
        /// Var Olan Kullanıcının İstenilen İletişim Bilgisini Silmek İçin Oluşturulmuştur.
        /// </summary>
        [HttpPost]
        public BaseResponse DeletePersonCommunication(DeletePersonCommunicationRequest request)
        {
            BaseResponse response = new()
            {
                Message = string.Empty,
                Status = true,
            };
            try
            {
                if (request.Id == null || request.Id == Guid.Empty)
                {
                    throw new ArgumentNullException("Id Alanı Boş Bırakılamaz.");
                }
                if (request.CommunicationId == null || request.CommunicationId == Guid.Empty)
                {
                    throw new ArgumentNullException("İletişim Id Alanı Boş Bırakılamaz");
                }
                var person = _repositoryWrapper.PersonRepository.GetByIdIncludeCommunication(request.Id ?? Guid.Empty);
                if (person != null)
                {
                    var communication = person.Communications.Where(c => c.Id == request.CommunicationId).FirstOrDefault();
                    if (communication != null)
                    {
                        person.Communications.Remove(communication);
                    }
                    _repositoryWrapper.CommunicationRepository.Delete(communication);
                    _repositoryWrapper.Save();
                }
                else
                {
                    throw new ArgumentException("Kişi Bulunamadı.!");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        /// <summary>
        /// Rehberdeki Tüm Kullanıcıları ve Bu Kullanıcıların İletişim Bilgilerini Getirmektedir.
        /// </summary>
        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
            List<GetAllPersonResponse> response = new();
            try
            {
                var persons = _repositoryWrapper.PersonRepository.GetAllIncludeBy();
                if (persons != null)
                {
                    foreach (var item in persons)
                    {
                        var person = new GetAllPersonResponse()
                        {
                            Company = item.Company,
                            Name = item.Name,
                            SurName = item.SurName,
                            InformationTypes = new()
                        };
                        if (item.Communications != null && item.Communications.Count > 0)
                        {
                            foreach (var itemCommunication in item.Communications)
                            {
                                person.InformationTypes.Add(new()
                                {
                                    Id = itemCommunication.Id,
                                    InformationContent = itemCommunication.InformationContent,
                                    InformationType = ((byte)itemCommunication.InformationType)
                                });
                            }
                        }
                        response.Add(person);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return StatusCode(200, JsonSerializer.Serialize(response));
        }

        /// <summary>
        /// İlgili Kullanıcıya Göre Id Bilgisiyle Birlikte Kullanıcı Bilgilerini Ve İletişim Bilgilerini Getirmektedir.
        /// </summary>
        [HttpGet(Name = "GetByPerson")]
        public GetByPersonResponse GetByPerson(string? Id)
        {
            GetByPersonResponse response = new()
            {
                Message = string.Empty,
                Status = true
            };
            try
            {
                if (string.IsNullOrEmpty(Id) || Guid.Parse(Id) == Guid.Empty)
                {
                    throw new ArgumentNullException("Id Alanı Boş Bırakılamaz.");
                }
                var person = _repositoryWrapper.PersonRepository.GetByIdIncludeCommunication(Guid.Parse(Id));
                if (person is null)
                {
                    throw new ArgumentNullException("Kullanıcı Bulunamadı!");
                }
                response.Name = person.Name;
                response.SurName = person.SurName;
                response.Company = person.Company;
                if (person.Communications != null && person.Communications.Count > 0)
                {
                    var tempList = new List<GetByPersonInformationType>();
                    foreach (var item in person.Communications)
                    {
                        tempList.Add(new()
                        {
                            Id = item.Id,
                            InformationContent = item.InformationContent,
                            InformationType = ((byte)item.InformationType)
                        });
                    }
                    response.InformationTypes = tempList;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        [HttpGet(Name = "GetPersonByLocationReport")]
        public IActionResult GetPersonByLocationReport()
        {
            Report report = new()
            {
                Id = Guid.NewGuid(),
                ReportStatus = ReportStatusEnum.Hazırlanıyor.ToString(),
                RequestDate = DateTime.UtcNow,
            };
            _repositoryWrapper.ReportRepository.Insert(report);
            _repositoryWrapper.Save();
            var person = _repositoryWrapper.PersonRepository.GetAllIncludeBy();
            Thread thread = new(() => PersonByLocalitionReportAsync(person, report));
            thread.Start();
            return StatusCode(200, "İşlem Başarılı");
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task PersonByLocalitionReportAsync(List<Person> person, Report reportData)
        {
            List<Tuple<string, double, double>> report = new();
            if (person != null && person.Count > 0)
            {
                var filterPerson = person.Where(x => x.Communications != null && x.Communications.Any(y => y.InformationType == CommunicationEnum.Localion || y.InformationType == CommunicationEnum.Telephone)).ToList();
                if (filterPerson != null && filterPerson.Count > 0)
                {
                    foreach (var itemPerson in filterPerson)
                    {
                        if (itemPerson.Communications is null)
                            continue;

                        var Content = itemPerson.Communications.Where(x => x.InformationType == CommunicationEnum.Localion).FirstOrDefault()?.InformationContent;
                        foreach (var item in itemPerson.Communications)
                        {
                            var index = report.FindIndex(x => x.Item1.Equals(Content));
                            if (index > -1)
                            {
                                if (item.InformationType == CommunicationEnum.Localion)
                                {
                                    var tempReport = report[index];
                                    report[index] = new(tempReport.Item1, tempReport.Item2 + 1, tempReport.Item3);
                                }
                                else
                                {
                                    var tempReport = report[index];
                                    report[index] = new(tempReport.Item1, tempReport.Item2, tempReport.Item3 + 1);
                                }
                            }
                            else
                            {
                                if (item.InformationType == CommunicationEnum.Localion)
                                {
                                    report.Add(new(Content, 1, 0));
                                }
                                else
                                {
                                    report.Add(new(Content, 0, 1));
                                }
                            }
                        }
                    }
                }
            }
            ReportSqlModel results = new()
            {
                Id = reportData.Id,
                Detail = new()
            };
            foreach (var item in report)
            {
                results.Detail.Add(new ReportSqlModelDetail()
                {
                    InformationContent = item.Item1,
                    PersonCount = item.Item2,
                    PhoneCount = item.Item3
                });
            }
            await _publishEndpoint.Publish(results);
        }
    }
}
