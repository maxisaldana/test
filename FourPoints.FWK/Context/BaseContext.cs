using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FourPoints.FWK.Domain;

namespace FourPoints.FWK.Context
{
    public abstract class BaseContext<Key> : DbContext
    {
        private IHttpContextAccessor _httpContext;
        public BaseContext(IHttpContextAccessor httpContext) : base()
        {
            _httpContext = httpContext;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            EntryStateHandler();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void EntryStateHandler()
        {
            var uid = _httpContext?.HttpContext?.User?.FindFirst("uid");
            foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Added))
            {
                if (entity.Entity is IAuditable<Key> created)
                {
                    created.UpdatedAt = DateTime.UtcNow;
                    created.CreatedAt = DateTime.UtcNow;
                    if (uid != null)
                    {
                        created.CreatedBy = (Key) Convert.ChangeType(uid.Value, typeof(Key));
                    }
                }
            }

            foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Modified))
            {
                if (entity.Entity is IAuditable<Key> updated)
                {
                    updated.UpdatedAt = DateTime.UtcNow;
                    this.Entry(updated).Property(x => x.CreatedAt).IsModified = false;
                    this.Entry(updated).Property(x => x.CreatedBy).IsModified = false;
                    if (uid != null)
                    {
                        updated.UpdatedBy = (Key)Convert.ChangeType(uid.Value, typeof(Key));
                    }
                }
            }

            foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted))
            {
                if (entity.Entity is IAuditable<Key> updated)
                {
                    updated.UpdatedAt = DateTime.Now;
                    if (uid != null)
                    {
                        updated.UpdatedBy = (Key)Convert.ChangeType(uid.Value, typeof(Key));
                    }
                }

                if (entity.Entity is ISoftDelete deleted)
                {
                    deleted.DeletedAt = DateTime.UtcNow;
                    entity.State = EntityState.Modified;
                }
            }
        }
    }
}
