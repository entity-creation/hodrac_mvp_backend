using Hodrac_MVP_Backend.DTOs.Category;
using Model = Hodrac_MVP_Backend.Models;

namespace Hodrac_MVP_Backend.Mappers.Category
{
    public static class CategoryMapper
    {
        public static Model.Category FromJsonDtoToCategory(this CategoryJsonDto categoryJsonDto)
        {
            return new Model.Category
            {
                Key = categoryJsonDto.Key,
                CategoryName = categoryJsonDto.Name,
                CategoryDescription = categoryJsonDto.Description,
            };
        }

        public static ClientCategoryDto FromCategoryToClientCategoryDto(
            this Model.Category category
        )
        {
            return new ClientCategoryDto { CategoryName = category.CategoryName };
        }
    }
}
