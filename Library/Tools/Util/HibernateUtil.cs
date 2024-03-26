using NHibernate.Criterion;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Util
{
    public static class HibernateUtil
    {
        static HibernateUtil() { }

        public static ICriteria DisjunctionsAdd(this ICriteria criteria, List<ICriterion> criterions)
        {
            if (criterions != null && criterions.Any()) {
                var disjunction = Restrictions.Disjunction();
                criterions.ForEach(x => disjunction.Add(x));
                criteria.Add(disjunction);
            }

            return criteria;
        }

        public static ICriteria ConjunctionsAdd(this ICriteria criteria, List<ICriterion> criterions)
        {
            if (criterions != null && criterions.Any()) {
                var conjunction = Restrictions.Conjunction();
                criterions.ForEach(x => conjunction.Add(x));
                criteria.Add(conjunction);
            }

            return criteria;
        }
    }
}
