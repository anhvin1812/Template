using System.ComponentModel.DataAnnotations;
using App.Core.DataModels;

namespace App.Entities.NewsManagement
{
    public class NewsStatus : EntityBase
    {
        public int Id { get; set; }

        public string Status { get; set; }
    }

}
