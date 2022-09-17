using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookie.Core.Selfies.Infrastructures.Data;
using SelfiesAWookies.Core.Framework;


namespace SelfieAWookie.Core.Selfies.Infrastructures.Repositories
{
    public class DefaultSelfieRepository : ISelfieRepository
    {
        private SelfieContext context = null;
        public IUnitOfWork UnitOfWork => this.context;

        public DefaultSelfieRepository(SelfieContext context )
        {
            this.context = context;
        }

        public ICollection<Selfie> GetAll()
        {
            return context.Selfies.Include(item => item.Wookie).ToList();
        }
    }
}
