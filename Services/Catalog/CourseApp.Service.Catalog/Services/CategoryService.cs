using AutoMapper;
using CourseApp.Service.Catalog.DbSettings;
using CourseApp.Service.Catalog.Dtos;
using CourseApp.Service.Catalog.Models;
using CourseApp.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Service.Catalog.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(i => true).ToListAsync();
            var dataList = _mapper.Map<List<CategoryDto>>(categories);

            return Response<List<CategoryDto>>.Success(dataList, 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto createDto)
        {
            var data = _mapper.Map<Category>(createDto);
            await _categoryCollection.InsertOneAsync(data);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(data),200);

        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(i => i.Id == id).FirstOrDefaultAsync();

            if (category==null)
            {
                Response<CategoryDto>.Fail("Data couldnt found",404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CategoryDto categoryDto)
        {

            var updateCategory = _mapper.Map<Category>(categoryDto);
                
             var result= await _categoryCollection.FindOneAndReplaceAsync(i => i.Id == categoryDto.Id,updateCategory);

            if (result==null)
            {
                return Response<NoContent>.Fail("Category couldnt found",404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var deletedCategory = await _categoryCollection.DeleteOneAsync(i=>i.Id==id);

            if (deletedCategory.DeletedCount>0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Catgory couldnt found", 404);
            }
          

        }

    }
}
