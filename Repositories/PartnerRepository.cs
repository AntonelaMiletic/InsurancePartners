using Dapper;
using InsurancePartners.Data;
using InsurancePartners.Models;
using InsurancePartners.ViewModels;

namespace InsurancePartners.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public PartnerRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<PartnerListViewModel>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            var sql = @"
                SELECT 
                    p.Id,
                    CONCAT(p.FirstName, ' ', p.LastName) AS FullName,
                    p.PartnerNumber,
                    p.CroatianPIN,
                    p.PartnerTypeId,
                    p.CreatedAtUtc,
                    p.IsForeign,
                    p.Gender,
                    CASE 
                        WHEN COUNT(pl.Id) > 5 OR COALESCE(SUM(pl.PolicyAmount), 0) > 5000 
                        THEN 1 ELSE 0 
                    END AS IsImportant
                FROM Partners p
                LEFT JOIN Policies pl ON p.Id = pl.PartnerId
                GROUP BY p.Id
                ORDER BY p.CreatedAtUtc DESC;";

            var partners = await connection.QueryAsync<PartnerListViewModel>(sql);
            return partners.ToList();
        }

        public async Task<PartnerDetailsViewModel?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            var sql = @"
                SELECT 
                    p.Id,
                    CONCAT(p.FirstName, ' ', p.LastName) AS FullName,
                    p.Address,
                    p.PartnerNumber,
                    p.CroatianPIN,
                    p.PartnerTypeId,
                    p.CreatedAtUtc,
                    p.CreatedByUser,
                    p.IsForeign,
                    p.ExternalCode,
                    p.Gender,
                    COUNT(pl.Id) AS PolicyCount,
                    COALESCE(SUM(pl.PolicyAmount), 0) AS TotalPolicyAmount
                FROM Partners p
                LEFT JOIN Policies pl ON p.Id = pl.PartnerId
                WHERE p.Id = @Id
                GROUP BY p.Id;";

            return await connection.QueryFirstOrDefaultAsync<PartnerDetailsViewModel>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(Partner partner)
        {
            using var connection = _connectionFactory.CreateConnection();

            var sql = @"
                INSERT INTO Partners
                (
                    FirstName,
                    LastName,
                    Address,
                    PartnerNumber,
                    CroatianPIN,
                    PartnerTypeId,
                    CreatedByUser,
                    IsForeign,
                    ExternalCode,
                    Gender
                )
                VALUES
                (
                    @FirstName,
                    @LastName,
                    @Address,
                    @PartnerNumber,
                    @CroatianPIN,
                    @PartnerTypeId,
                    @CreatedByUser,
                    @IsForeign,
                    @ExternalCode,
                    @Gender
                );

                SELECT LAST_INSERT_ID();";

            return await connection.ExecuteScalarAsync<int>(sql, partner);
        }

        public async Task AddPolicyAsync(Policy policy)
        {
            using var connection = _connectionFactory.CreateConnection();

            var sql = @"
                INSERT INTO Policies
                (
                    PartnerId,
                    PolicyNumber,
                    PolicyAmount
                )
                VALUES
                (
                    @PartnerId,
                    @PolicyNumber,
                    @PolicyAmount
                );";

            await connection.ExecuteAsync(sql, policy);
        }
    }
}