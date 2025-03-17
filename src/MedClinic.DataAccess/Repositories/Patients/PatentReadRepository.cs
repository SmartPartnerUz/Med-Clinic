using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Patients;

namespace MedClinic.DataAccess.Repositories;

public class PatientReadRepository : ReadRepository<Patient>, IPatientReadRepository
{
    public PatientReadRepository(AppDbContext context) : base(context)
    {
    }
}