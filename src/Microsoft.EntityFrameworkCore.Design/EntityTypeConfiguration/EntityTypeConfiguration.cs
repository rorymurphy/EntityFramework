using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Xintricity.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        private List<Action<EntityTypeBuilder<TEntity>>> _actions = new List<Action<EntityTypeBuilder<TEntity>>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        internal virtual void Apply(EntityTypeBuilder<TEntity> builder)
        {
            foreach(var a in _actions)
            {
                a(builder);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected void ToTable(string name)
        {
            _actions.Add(b => b.ToTable(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="schema"></param>
        protected void ToTable(string name, string schema)
        {
            _actions.Add(b => b.ToTable(name, schema));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        protected PropertyConfiguration<T> Property<T>(Expression<Func<TEntity, T>> selector)
        {
            PropertyConfiguration<T> pConfig = new PropertyConfiguration<T>();
            _actions.Add(b => {
                var p = b.Property<T>(selector);
                pConfig.Apply(p);
            });
            return pConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected PropertyConfiguration<T> Property<T>(string name)
        {
            PropertyConfiguration<T> pConfig = new PropertyConfiguration<T>();
            _actions.Add(b => {
                var p = b.Property<T>(name);
                pConfig.Apply(p);
            });
            return pConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        protected void Ignore(Expression<Func<TEntity, object>> selector)
        {
            _actions.Add(b => {
                var p = b.Ignore(selector);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        protected EntityTypeConfiguration<TEntity> HasKey(Expression<Func<TEntity, object>> selector)
        {
            _actions.Add(b => {
                var keyBuilder = b.HasKey(selector);
            });
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTargetEntity"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        protected ManyNavigationPropertyConfiguration<TEntity, TTargetEntity> HasMany<TTargetEntity>(Expression<Func<TEntity, IEnumerable<TTargetEntity>>> selector) where TTargetEntity : class
        {
            ManyNavigationPropertyConfiguration<TEntity, TTargetEntity> config = new ManyNavigationPropertyConfiguration<TEntity, TTargetEntity>();

            _actions.Add(b => {
                var builder = b.HasMany<TTargetEntity>(selector);
                config.Apply(builder);
            });

            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTargetEntity"></typeparam>
        /// <param name="navigationPropertyExpression"></param>
        /// <returns></returns>
        protected ReferenceNavigationConfiguration<TEntity, TTargetEntity> HasOne<TTargetEntity>(Expression<Func<TEntity, TTargetEntity>> navigationPropertyExpression) where TTargetEntity : class
        {
            ReferenceNavigationConfiguration<TEntity, TTargetEntity> config = new ReferenceNavigationConfiguration<TEntity, TTargetEntity>();
            _actions.Add(b =>
            {
                var builder = b.HasOne<TTargetEntity>(navigationPropertyExpression);
                config.Apply(builder);
            });

            return config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="annotation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected EntityTypeConfiguration<TEntity> HasAnnotation(string annotation, object value)
        {
            _actions.Add(b => b.HasAnnotation(annotation, value));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        protected KeyConfiguration HasAlternateKey(params string[] propertyNames)
        {
            KeyConfiguration keyConfig = new KeyConfiguration();
            _actions.Add(b => {
                var builder = b.HasAlternateKey(propertyNames);
                keyConfig.Apply(builder);
            });
            return keyConfig;
        }
    }
}
