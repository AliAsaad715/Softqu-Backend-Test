using Softqu.Domain.Category;
using Softqu.Domain.Shared;
using Softqu.Domain.Shared.Exceptions;

namespace Softqu.Domain.PopularCategory
{
    public class PopularCategoryAggregate : AggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public int SortOrder { get; private set; }
        public CategoryAggregate Category { get; private set; }

        private PopularCategoryAggregate() { }

        public PopularCategoryAggregate(Guid categoryId, int sortOrder)
        {
            if (categoryId == Guid.Empty)
                throw new DomainException("Invalid CategoryId");
            CategoryId = categoryId;
            SortOrder = sortOrder;
        }

        public void UpdateSortOrder(int sortOrder)
        {
            if(sortOrder <= 0)
                throw new DomainException("SortOrder must be greater than 0");
            SortOrder = sortOrder;
        }
    }
}
