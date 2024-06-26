using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Models;
using System.Text.Json;
namespace WebApplication1.Services
{
    public class ApiMusInstrumentsService(HttpClient httpClient) : IMuzInstrumentService
    {
       
        public async Task<ResponseData<MusInstruments>> GetInstByIdAsync(int id)
        {
            var apiUrl = $"{httpClient.BaseAddress.AbsoluteUri}{id}";
            var response = await httpClient.GetFromJsonAsync<MusInstruments>(apiUrl);
            return new ResponseData<MusInstruments>() { Data = response };
        }

        public async Task<ResponseData<MuzListModel<MusInstruments>>> GetInstListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var uri = httpClient.BaseAddress;
            var queryData = new Dictionary<string, string>();
            queryData.Add("pageNo", pageNo.ToString());
            if (!String.IsNullOrEmpty(categoryNormalizedName))
            {
                queryData.Add("category", categoryNormalizedName);
            }
            var query = QueryString.Create(queryData);

            var result = await httpClient.GetAsync(uri + query.Value);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<MuzListModel<MusInstruments>>>();
            };

            var response = new ResponseData<MuzListModel<MusInstruments>>
            { Success = false, ErrorMessage = "Ошибка чтения API" };
            return response;
        }

        public Task UpdateInstAsync(int id, MusInstruments mi, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInstAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<MusInstruments>> CreateInstAsync(MusInstruments mi, IFormFile? formFile)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            // Подготовить объект, возвращаемый методом
            var responseData = new ResponseData<MusInstruments>();
            // Послать запрос к API для сохранения объекта
            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, mi);
            if (!response.IsSuccessStatusCode)
            {
                responseData.Success = false;
                responseData.ErrorMessage = $"Не удалось создать объект:{response.StatusCode}";
                return responseData;
            }
            // Если файл изображения передан клиентом
            if (formFile != null)
            {
                // получить созданный объект из ответа Api-сервиса
                var muzi = await response.Content.ReadFromJsonAsync<MusInstruments>();
                // создать объект запроса
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/{mi.Id}")
                };
                // Создать контент типа multipart form-data
                var content = new MultipartFormDataContent();
                // создать потоковый контент из переданного файла
                var streamContent = new StreamContent(formFile.OpenReadStream());
                // добавить потоковый контент в общий контент по именем "image"
                content.Add(streamContent, "Image", formFile.FileName);
                // поместить контент в запрос
                request.Content = content;
                // послать запрос к Api-сервису
                response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    responseData.Success = false;
                    responseData.ErrorMessage = $"Не удалось сохранить изображение:{response.StatusCode}";
                }
            }
            return responseData;
        }

    }
}
