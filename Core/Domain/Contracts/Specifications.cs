using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {

        public Expression<Func<T, bool>>? Criteria { get; } // Where
        public List<Expression<Func<T, object>>> IncludedExpressions { get; } = new(); // Include
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDesc { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; private set; }
        protected Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public void AddInclude(Expression<Func<T, object>> expression)
           => IncludedExpressions.Add(expression);
        public void SetOrderBy(Expression<Func<T, object>> expression)
           => OrderBy = expression;
        public void SetOrderByDesc(Expression<Func<T, object>> expression)
           => OrderByDesc = expression;
        protected void ApplyPagination(int pageIndex, int pageSize)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
    }
}
