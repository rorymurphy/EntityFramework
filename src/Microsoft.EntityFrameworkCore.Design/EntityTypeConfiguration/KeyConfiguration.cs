using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    public class KeyConfiguration
    {
        private List<Action<KeyBuilder>> _actions = new List<Action<KeyBuilder>>();

        internal void Apply(KeyBuilder builder)
        {
            foreach (var a in _actions)
            {
                a(builder);
            }
        }

        public KeyConfiguration HasAnnotation([NotNull] string annotation, [NotNull] object value)
        {
            _actions.Add(kb => kb.HasAnnotation(annotation, value));
            return this;
        }
    }
}
