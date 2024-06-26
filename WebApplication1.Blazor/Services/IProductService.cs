namespace WebApplication1.Blazor.Services
{
    public interface IProductService<T> where T : class
    {
        event Action ListChanged;
        // Список объектов
        IEnumerable<T> Products { get; }
        // Номер текущей страницы
        int CurrentPage { get; }
        // Общее количество страниц
        int TotalPages { get; }
        // Получение списка объектов
        Task GetInst(int pageNo = 1, int pageSize = 3);
    }
}
