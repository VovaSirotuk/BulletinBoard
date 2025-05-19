using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Models.ConstantCategories
{
    public static class AnnouncementCategories
    {
        public static readonly Dictionary<string, List<string>> CategoriesWithSubcategories = new()
        {
            {
                "Побутова техніка", new List<string>
                {
                    "Холодильники", "Пральні машини", "Бойлери",
                    "Печі", "Витяжки", "Мікрохвильові печі"
                }
            },
            {
                "Комп'ютерна техніка", new List<string>
                {
                    "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери"
                }
            },
            {
                "Смартфони", new List<string>
                {
                    "Android смартфони", "iOS/Apple смартфони"
                }
            },
            {
                "Інше", new List<string>
                {
                    "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки"
                }
            }
        };

        public static IEnumerable<string> AllCategories => CategoriesWithSubcategories.Keys;

        public static IEnumerable<string> GetSubcategories(string category)
        {
            return CategoriesWithSubcategories.TryGetValue(category, out var subs)
                ? subs
                : Enumerable.Empty<string>();
        }
    }
}
