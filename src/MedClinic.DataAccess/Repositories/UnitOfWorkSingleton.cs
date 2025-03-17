using MedClinic.DataAccess.Interfaces;

namespace MedClinic.DataAccess.Repositories;

public static class UnitOfWorkSingleton
{
    private static IUnitOfWork? _instance;

    public static void Initialize(IUnitOfWork unitOfWork)
    {
        if (_instance == null)
            _instance = unitOfWork;
    }

    public static IUnitOfWork Instance
    {
        get
        {
            if (_instance == null)
                throw new InvalidOperationException("UnitOfWork is not initialized.");

            return _instance;
        }
    }
}