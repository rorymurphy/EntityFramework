using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Xintricity.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    /// <typeparam name="TTargetEntity"></typeparam>
    public class ManyNavigationPropertyConfiguration<TEntityType, TTargetEntity> where TEntityType : class where TTargetEntity : class
    {
        private List<Action<CollectionNavigationBuilder<TEntityType, TTargetEntity>>> _actions = new List<Action<CollectionNavigationBuilder<TEntityType, TTargetEntity>>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Apply(CollectionNavigationBuilder<TEntityType, TTargetEntity> builder)
        {
            foreach(var a in _actions)
            {
                a(builder);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntity> WithMany()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntity> WithMany(Expression<Func<TTargetEntity, ICollection<TEntityType>>> selector)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity> WithOptional()
        {
            DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity> config = new DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity>();
            _actions.Add(p =>
            {
                var builder = p.WithOne();
                config.Apply(builder);
            });

            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity> WithRequired()
        {
            DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity> config = new DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity>();
            _actions.Add(p =>
            {
                var builder = p.WithOne().IsRequired();
                config.Apply(builder);
            });

            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity> WithRequired(Expression<Func<TTargetEntity, TEntityType>> expr)
        {
            DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity> config = new DependentNavigationPropertyConfiguration<TEntityType, TTargetEntity>();
            _actions.Add(p =>
            {
                var builder = p.WithOne(expr).IsRequired();
                config.Apply(builder);
            });

            return config;
        }
    }
}
