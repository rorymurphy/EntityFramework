using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xintricity.DataAccess
{
    /// <summary>
    /// Supporting class for EntityTypeConfiguration implementing configuration options for primitive properties.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrimitivePropertyConfiguration<T>
    {
        private List<Action<PropertyBuilder<T>>> _actions = new List<Action<PropertyBuilder<T>>>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public PrimitivePropertyConfiguration()
        {

        }


        internal void Apply(PropertyBuilder<T> propertyBuilder)
        {
            foreach (var act in _actions)
            {
                act(propertyBuilder);
            }
        }

        /// <summary>
        /// Specifies the column name to map the property to in the data store
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PrimitivePropertyConfiguration<T> HasColumnName(string name)
        {
            _actions.Add(p => p.HasColumnName(name));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public PrimitivePropertyConfiguration<T> HasColumnAnnotation( string name, object value)
        {
            _actions.Add(p => p.HasAnnotation(name, value));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PrimitivePropertyConfiguration<T> IsRequired()
        {
            _actions.Add(p => p.IsRequired());
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PrimitivePropertyConfiguration<T> IsOptional()
        {
            _actions.Add(p => p.IsRequired(false));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public PrimitivePropertyConfiguration<T> HasMaxLength(int maxLength)
        {
            _actions.Add(p => p.HasMaxLength(maxLength));
            return this;
        }
    }
}
