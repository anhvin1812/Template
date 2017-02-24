namespace App.Core.Html
{
    public interface ICategory  
    {
        int Id { get; }
        string Name { get; }
        int? ParentId { get; }
    }
}