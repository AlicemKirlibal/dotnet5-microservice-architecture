﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CourseApp.Shared.Dtos
{
   public class Response<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }


        // Static Factory Methods
        //if you return a new object with static methods this means static factory method
        public static Response<T> Success(T data,int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccess = true };
        }

        public static Response<T> Fail(List<string> errors,int statusCode)
        {
            return new Response<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccess = false
            };
        }

        public static Response<T> Fail(string error,int statusCode)
        {
            return new Response<T>
            {
                Errors = new List<string> { error },
                IsSuccess = false,
                StatusCode = statusCode
            };
        }
    }
}