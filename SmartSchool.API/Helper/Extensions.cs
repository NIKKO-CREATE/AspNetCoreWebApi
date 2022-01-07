﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.API.Helper
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, 
            int itemsPerPage, int totalPages, int totalItems)
        {
            var paginationsHeader = new PaginationHeader(currentPage, itemsPerPage, totalPages, totalItems);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationsHeader, camelCaseFormatter));
            response.Headers.Add("Access-control-expose-header", "pagination");
        }
    }
}
