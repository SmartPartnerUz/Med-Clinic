using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Patients;

namespace MedClinic.DataAccess.Repositories;

public class PatientWriteRepository : WriteRepository<Patient>, IPatientWriteRepository
{
    public PatientWriteRepository(AppDbContext context) : base(context)
    {
    }
}