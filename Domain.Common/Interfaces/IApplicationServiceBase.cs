using Domain.Common.Filters;
using Domain.Common.Model;

namespace Domain.Common.Interfaces
{
    public interface IApplicationServiceBase<TListModelResult, TModelResult, TModelIdResult, TFilter, TDto>
    {
        Task<TListModelResult> GetWithFilters(TFilter filters);
        Task<TModelResult> GetByIdAsync(int id);
        Task<TModelIdResult> CreateAsync(TDto entity);
        Task<TModelIdResult> UpdateAsync(TDto entity);
        Task<TModelIdResult> DeleteAsync(int id);

    }    
}
