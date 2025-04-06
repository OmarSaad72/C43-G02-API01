namespace Persistance.Repositories
{
    public static class SpecificationsEvalutor
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, Specifications<T> specification) where T : class
        {
            var query = inputQuery;
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);
            //foreach (var item in specification.IncludedExpressions)
            //    query = query.Include(item);
            query = specification.IncludedExpressions.Aggregate(query, (currentQuery, includeExpression)
                => currentQuery.Include(includeExpression));
            return query;
        }
    }
}
