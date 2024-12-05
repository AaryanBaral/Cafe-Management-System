using Cafe_Management_System.Entities;
using Cafe_Management_System.Models.Category;

namespace Cafe_Management_System.Mappers;

public static class CategoryMapper
{
    public static Categories ToCategory(this AddCategoryDto categoryDto)
    {
        return new Categories()
        {
            CategoryName = categoryDto.CategoryName,
            CategoryDescription = categoryDto.Description
        };
    }

    public static void UpdateCategories(this Categories categories, AddCategoryDto categoryDto)
    {
        categories.CategoryName = categoryDto.CategoryName;
        categories.CategoryDescription = categoryDto.Description;
    }

    public static ReadCategoryDto ToReadCategoryDto(this Categories categories)
    {
        return new ReadCategoryDto()
        {
            CategoryId = categories.CategoryId,
            CategoryName = categories.CategoryName,
            Description = categories.CategoryDescription
        };
    }
}