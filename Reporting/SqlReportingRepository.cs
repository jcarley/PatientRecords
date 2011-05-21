using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace Reporting
{
    public class SqlReportingRepository : IReportingRepository<PatientDto>
    {
        private string _connectionStringName = string.Empty;

        public SqlReportingRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public IEnumerable<PatientDto> GetAll()
        {
            using (var context = new PatientDBEventStoreEntities(ConnectionString))
            {
                return (from p in context.Patients
                        select new PatientDto()
                        {
                            Id = p.PatientId,
                            Name = p.Name,
                            Status = p.Status,
                            Street = p.Street,
                            City = p.City,
                            State = p.State,
                            Zip = p.Zip
                        })
                        .ToList();
            }
        }

        public IEnumerable<PatientDto> GetAll(Func<PatientDto, bool> predicate)
        {
            using (var context = new PatientDBEventStoreEntities(ConnectionString))
            {
                return (from p in context.Patients
                        select new PatientDto()
                        {
                            Id = p.PatientId,
                            Name = p.Name,
                            Status = p.Status,
                            Street = p.Street,
                            City = p.City,
                            State = p.State,
                            Zip = p.Zip
                        })
                        .Where(predicate)
                        .ToList();
            }
        }

        public PatientDto GetById(Guid id)
        {
            using (var context = new PatientDBEventStoreEntities(ConnectionString))
            {
                return (from p in context.Patients
                        where p.PatientId == id
                        select new PatientDto()
                        {
                            Id = p.PatientId,
                            Name = p.Name,
                            Status = p.Status,
                            Street = p.Street,
                            City = p.City,
                            State = p.State,
                            Zip = p.Zip
                        })
                        .FirstOrDefault();
            }
        }

        public void Insert(PatientDto dto)
        {
            using (var context = new PatientDBEventStoreEntities(ConnectionString))
            {
                Patient patientEntity = new Patient()
                {
                    PatientId = dto.Id,
                    Name = dto.Name,
                    Status = dto.Status,
                    Street = dto.Street,
                    City = dto.City,
                    State = dto.State,
                    Zip = dto.Zip
                };

                context.Patients.AddObject(patientEntity);
                context.SaveChanges();
            }
        }

        public void Update(PatientDto dto)
        {
            using (var context = new PatientDBEventStoreEntities(ConnectionString))
            {
                Patient patientEntity = new Patient()
                {
                    PatientId = dto.Id,
                    Name = dto.Name,
                    Status = dto.Status,
                    Street = dto.Street,
                    City = dto.City,
                    State = dto.State,
                    Zip = dto.Zip
                };

                context.Patients.Attach(patientEntity);
                context.ObjectStateManager.ChangeObjectState(patientEntity, EntityState.Modified);
                context.SaveChanges();
            }
        }

        public void Delete(PatientDto dto)
        {
            using (var context = new PatientDBEventStoreEntities(ConnectionString))
            {
                Patient patientEntity = new Patient()
                {
                    PatientId = dto.Id
                };

                context.Patients.Attach(patientEntity);
                context.DeleteObject(patientEntity);
                context.SaveChanges();
            }
        }

        private string ConnectionString
        {
            get
            {
                return string.Format("name={0}", _connectionStringName);
            }
        }
    }
}
