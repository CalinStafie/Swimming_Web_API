using proiect_final_API.Data;
using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public class SubscriptionsRepository : GenericRepository<Subscription>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(projectContext context) : base(context) { }

        public void UpdateCost(long id, int newCost)
        {
            var subscription = new Subscription()
            {
                Id = id,
                Cost = newCost
            };
            _context.Subscriptions.Attach(subscription);
            _context.Entry(subscription).Property(x => x.Cost).IsModified = true;
            _context.SaveChanges();
        }
    }
}
