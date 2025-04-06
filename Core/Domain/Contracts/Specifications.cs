using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {

        public Expression<Func<T, bool>>? Criteria { get; } // Where
        public List<Expression<Func<T, object>>> IncludedExpressions { get; } = new(); // Include
        protected Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public void AddInclude(Expression<Func<T, object>> expression)
        {
            IncludedExpressions.Add(expression);
        }
    }
}
