using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Core.Services
{
    public class KYCService : IKYCService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly KYCSettings _settings;
		private readonly IRestClient _restClient;

		public KYCService(HttpClient httpClient, IConfiguration configuration, IRestClient rest)
        {
            _httpClient = httpClient;
			_baseUrl = configuration.GetValue<string>("KYCSettings:url");
			_settings = configuration.GetValue<KYCSettings>("KYCSettings:ApiKey");
			_restClient = rest;
		}

		
		public async Task<APIResponse> VerifyUser([FromForm] KYC kyc)
		{
			var request = new RestRequest("", Method.Post);
			request.AddHeader("Accept", "application/json");
			request.AddJsonBody(kyc);

			var response = await _restClient.ExecuteAsync(request);

			if (!response.IsSuccessful)
			{
				var errorResponse = new APIResponse
				{
					StatusCode = response.StatusCode.ToString(),
					IsSuccess = false,
					Message = "Bvn verification unsuccessful"
				};
				return errorResponse;
			}

			var successResponse = new APIResponse
			{
				StatusCode = response.StatusCode.ToString(),
				IsSuccess = true,
				Message = "Bvn verification successful"
			};

			return successResponse;
		}
	}
	}
