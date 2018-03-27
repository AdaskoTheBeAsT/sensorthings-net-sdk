﻿using System.Linq;

namespace sensorthings.ODATA
{
    public class QuerySelect : AbstractQuery<string[]>
    {
        public QuerySelect(string[] values) : base("select", values) { }

        public override string GetQueryValueString()
        {
            return Value.Aggregate((x, y) => $"{x},{y}");
        }
    }
}
