using CourseApp.Service.Discount.Services.Abstract;
using CourseApp.Shared.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Service.Discount.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConneciton;

        public DiscountService(IConfiguration configuration, IDbConnection dbConneciton)
        {
            _configuration = configuration;
            _dbConneciton = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConneciton.ExecuteAsync("delete from discount where id==Id", new { Id = id });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found", 404);

        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConneciton.QueryAsync<Models.Discount>("select*from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConneciton.QueryAsync<Models.Discount>("select*from where userid==@UserId and code==@Code", new {UserId=userId,Code=code });

            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount==null)
            {
                return Response<Models.Discount>.Fail("discount not found",404);
            }

            return Response<Models.Discount>.Success(hasDiscount,200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConneciton.QueryAsync<Models.Discount>("select*from discount where id==@Id", new { Id = id })).SingleOrDefault();

            if (discount==null)
            {
                return Response<Models.Discount>.Fail("discount couldnt find",404);
            }

            return Response<Models.Discount>.Success(discount, 200);


        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConneciton.ExecuteAsync("insert into discount (userid,rate,code) values(@UserId,@Rate,@Code)",discount);

            if (saveStatus>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("an error accured while adding",500);

        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConneciton.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id==@Id",
                new {Id=discount.Id,UserId=discount.UserId,Code=discount.Code,Rate=discount.Rate });

            if (status>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("an error accured while updating",500);
        }
    }
}
