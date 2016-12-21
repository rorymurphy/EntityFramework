using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xintricity.DataAccess
{
    /// <summary>
    /// Contains extension method for adding EntityTypeConfiguration objects to a ModelBuilder
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// A method for adding the model defined by the EntityTypeConfiguration to the ModelBuilder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ModelBuilder AddConfiguration<T>(this ModelBuilder builder, EntityTypeConfiguration<T> config) where T : class
        {
            config.Apply(builder.Entity<T>());
            return builder;
        }
    }
}
