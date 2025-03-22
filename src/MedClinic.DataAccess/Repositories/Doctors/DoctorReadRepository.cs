using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Doctors;
using Microsoft.EntityFrameworkCore;

namespace MedClinic.DataAccess.Repositories;

public class DoctorReadRepository : ReadRepository<Doctor>, IDoctorReadRepository
{
    private readonly AppDbContext _context;

    public DoctorReadRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Doctor> GetAllWithRelatedEntities()
    {
        return _context.Doctors
            .Include(d => d.Position)
            .Include(d => d.DoctorRoom)
            .Include(d => d.Role)
            .Include(d => d.User)
            .Include(d => d.HospitalService);
    }
}