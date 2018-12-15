using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curriculum.Infrastruction.ValueGenerator
{
    public class StringKeyValueGenerator : ValueGenerator<String>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            Guid guid = Guid.NewGuid();
            //string datetime = DateTime.Now.ToString("yyyyMMddhhmmss");
            //return datetime + guid.ToString().Replace("-","");
            return guid.ToString();
        }
    }
}
