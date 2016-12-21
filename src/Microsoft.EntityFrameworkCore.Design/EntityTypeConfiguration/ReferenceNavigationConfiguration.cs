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
    public class ReferenceNavigationConfiguration<TEntityType, TTargetEntity> where TEntityType : class where TTargetEntity : class
    {
        private List<Action<ReferenceNavigationBuilder<TEntityType, TTargetEntity>>> _actions = new List<Action<ReferenceNavigationBuilder<TEntityType, TTargetEntity>>>();

        internal void Apply(ReferenceNavigationBuilder<TEntityType, TTargetEntity> builder)
        {
            foreach (var a in _actions)
            {
                a(builder);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DependentNavigationPropertyConfiguration<TTargetEntity, TEntityType> WithMany()
        {
            DependentNavigationPropertyConfiguration<TTargetEntity, TEntityType> config = new DependentNavigationPropertyConfiguration<TTargetEntity, TEntityType>();
            _actions.Add(p =>
            {
                var builder = p.WithMany();
                config.Apply(builder);
            });

            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationPropertyExpression"></param>
        /// <returns></returns>
        public DependentNavigationPropertyConfiguration<TTargetEntity, TEntityType> WithMany(Expression<Func<TTargetEntity, IEnumerable<TEntityType>>> navigationPropertyExpression)
        {
            DependentNavigationPropertyConfiguration<TTargetEntity, TEntityType> config = new DependentNavigationPropertyConfiguration<TTargetEntity, TEntityType>();
            _actions.Add(p =>
            {
                var builder = p.WithMany(navigationPropertyExpression);
                config.Apply(builder);
            });

            return config;
        }
    }
}
