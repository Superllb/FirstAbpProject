using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace FirstAbpProject.EntityFramework.Repositories
{
    public abstract class FirstAbpProjectRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<FirstAbpProjectDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FirstAbpProjectRepositoryBase(IDbContextProvider<FirstAbpProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class FirstAbpProjectRepositoryBase<TEntity> : FirstAbpProjectRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected FirstAbpProjectRepositoryBase(IDbContextProvider<FirstAbpProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
