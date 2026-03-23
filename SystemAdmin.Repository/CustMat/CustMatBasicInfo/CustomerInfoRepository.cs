using Mapster;
using SqlSugar;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Entity;
using SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries;

namespace SystemAdmin.Repository.CustMat.CustMatBasicInfo
{
    public class CustomerInfoRepository
    {
        private readonly SqlSugarScope _db;

        public CustomerInfoRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增客户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertCustomerInfo(CustomerInfoEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<int> DeleteCustomerInfo(long customerId)
        {
            return await _db.Deleteable<CustomerInfoEntity>()
                            .Where(customer => customer.CustomerId == customerId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateCustomerInfo(CustomerInfoEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(customer => new
                            {
                                customer.CustomerId,
                                customer.CreatedBy,
                                customer.CreatedDate,
                            }).Where(customer => customer.CustomerId == entity.CustomerId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询客户实体
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<CustomerInfoDto> GetCustomerInfoEntity(long customerId)
        {
            var customerEntity = await _db.Queryable<CustomerInfoEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(customer => customer.CustomerId == customerId)
                                          .FirstAsync();
            return customerEntity.Adapt<CustomerInfoDto>();
        }

        /// <summary>
        /// 查询客户分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<CustomerInfoDto>> GetCustomerInfoPage(GetCustomerInfoPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<CustomerInfoEntity>()
                           .With(SqlWith.NoLock);

            // 客户编码
            if (!string.IsNullOrEmpty(getPage.CustomerCode))
            {
                query.Where(customer => customer.CustomerCode.Contains(getPage.CustomerCode));
            }
            // 客户名称
            if (!string.IsNullOrEmpty(getPage.CustomerName))
            {
                query.Where(customer => customer.CustomerNameCn.Contains(getPage.CustomerName) || customer.CustomerNameEn.Contains(getPage.CustomerName));
            }

            // 排序
            query = query.OrderBy(customer => customer.CreatedDate);

            var customerPage = await query.ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<CustomerInfoDto>.Ok(customerPage.Adapt<List<CustomerInfoDto>>(), totalCount, "");
        }
    }
}
