using AutoMapper;
using proiect_final_API.Data;
using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private projectContext _context;
        private IMapper _mapper;

        private PurchasesRepository purchasesRepository;
        private ReceptionistsRepository receptionistsRepository;
        private ClientsRepository clientsRepository;
        private AddressesRepository addressesRepository;
        private SubscriptionsRepository subscriptionsRepository;
        private GenericRepository<NormalWorkDay> normalWorkDaysRepository;
        private GenericRepository<VacationDay> vacationDaysRepository;

        public UnitOfWork(projectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IPurchasesRepository PurchasesRepository
        {
            get
            {

                if (this.purchasesRepository == null)
                {
                    this.purchasesRepository = new PurchasesRepository(_context, _mapper);
                }
                return purchasesRepository;
            }
        }

        public IReceptionistsRepository ReceptionistsRepository
        {
            get
            {

                if (this.receptionistsRepository == null)
                {
                    this.receptionistsRepository = new ReceptionistsRepository(_context);
                }
                return receptionistsRepository;
            }
        }

        public IClientsRepository ClientsRepository
        {
            get
            {

                if (this.clientsRepository == null)
                {
                    this.clientsRepository = new ClientsRepository(_context);
                }
                return clientsRepository;
            }
        }

        public IAddressesRepository AddressesRepository
        {
            get
            {
                if (this.addressesRepository == null)
                {
                    this.addressesRepository = new AddressesRepository(_context);
                }
                return addressesRepository;
            }
        }

        public ISubscriptionsRepository SubscriptionsRepository
        {
            get
            {

                if (this.subscriptionsRepository == null)
                {
                    this.subscriptionsRepository = new SubscriptionsRepository(_context);
                }
                return subscriptionsRepository;
            }
        }

        public IGenericRepository<NormalWorkDay> NormalWorkDaysRepository
        {
            get
            {

                if (this.normalWorkDaysRepository == null)
                {
                    this.normalWorkDaysRepository = new GenericRepository<NormalWorkDay>(_context);
                }
                return normalWorkDaysRepository;
            }
        }

        public IGenericRepository<VacationDay> VacationDaysRepository
        {
            get
            {

                if (this.vacationDaysRepository == null)
                {
                    this.vacationDaysRepository = new GenericRepository<VacationDay>(_context);
                }
                return vacationDaysRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
