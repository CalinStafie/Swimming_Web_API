using System;
using proiect_final_API.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPurchasesRepository PurchasesRepository { get; }
        IReceptionistsRepository ReceptionistsRepository { get; }
        IClientsRepository ClientsRepository { get; }
        IAddressesRepository AddressesRepository { get; }
        ISubscriptionsRepository SubscriptionsRepository { get; }
        IGenericRepository<NormalWorkDay> NormalWorkDaysRepository { get; }
        IGenericRepository<VacationDay> VacationDaysRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
