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
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(i => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(i => i.Id == course.CategoryId).FirstAsync();
                }

            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);

        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            try
            {
                var course = await _courseCollection.Find(i => i.Id == id).FirstOrDefaultAsync();

                if (course == null)
                {
                    Response<CourseDto>.Fail("Data couldnt found", 404);
                }
               
                    return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
                
            }
            catch (Exception)
            {

               return Response<CourseDto>.Fail("Data couldnt found", 404); ;
            }
          
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto createDto)
        {
            var newCourse =  _mapper.Map<Course>(createDto);

            newCourse.CreatedTime = DateTime.Now;

               await  _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse),200);

        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto updateDto)
        {
            var updateCourse = _mapper.Map<Course>(updateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(i => i.Id == updateDto.Id, updateCourse);

            if (result==null)
            {
                return Response<NoContent>.Fail("Course couldnt found",404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {

            try
            {

                var result = await _courseCollection.DeleteOneAsync(i => i.Id == id);

                if (result.DeletedCount > 0)
                {
                    return Response<NoContent>.Success(204);
                }
                else
                {
                    return Response<NoContent>.Fail("Course couldnt found", 404);

                }
            }
            catch (Exception)
            {

                return Response<NoContent>.Fail("Course couldnt found", 404); ;
            }




        }


    }
}
