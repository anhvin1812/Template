namespace App.Core.DataModels
{
    public interface ICategory  
    {
        int Id { get; }
        string Name { get; }
        int? ParentId { get; }
    }
}