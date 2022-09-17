namespace NKZSoft.Template.Application.Common.Paging;

public class CollectionViewModel<T>
{
    public CollectionViewModel()
    {
        Data = new HashSet<T>();
    }

    public CollectionViewModel(IEnumerable<T> list, int count)
    {
        Data = list;
        TotalCount = count;
    }

    public IEnumerable<T> Data { get; set; }

    public int TotalCount { get; set; }
}
