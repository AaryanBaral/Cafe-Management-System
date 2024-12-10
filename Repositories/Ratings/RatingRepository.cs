using Cafe_Management_System.Data;
using Cafe_Management_System.Entities;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.RatingsDto;
using Cafe_Management_System.Repositories.MenuItem;
using Cafe_Management_System.Repositories.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Repositories.Ratings;

public class RatingRepository(
    AppDbContext context,
    UserManager<Users> userManager) : IRatingRepository
{
    private readonly AppDbContext _context = context;
    private readonly UserManager<Users> _userManager = userManager;

    public async Task CreateRating(AddRatingDto ratingDto)
    {
        var order = await _context.Orders.FindAsync(ratingDto) ?? throw new KeyNotFoundException("Order not found");
        var menuItem = await _context.MenuItems.FindAsync(ratingDto.MenuItemId) ??
                       throw new KeyNotFoundException("MenuItem not found");
        var customer = await _userManager.FindByIdAsync(ratingDto.CustomerId) ??
                       throw new KeyNotFoundException("MenuItem not found");
        var rating = ratingDto.ToRating(customer, menuItem);
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRating(string ratingId)
    {
        var rating = await _context.Ratings.FindAsync(ratingId) ?? throw new KeyNotFoundException("Rating not found");
        await _context.Ratings.Where(r => r.RatingId == ratingId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRating(string ratingId, UpdateRatingDto updateRating)
    {
        var rating = await _context.Ratings.FindAsync(ratingId) ?? throw new KeyNotFoundException("Rating not found");
        rating.UpdateRating(updateRating);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ReadRatingDto>> GetRatingByMenuItemId(string menuItemId)
    {
        var ratings = await _context.Ratings.Where(r => r.MenuItemId == menuItemId)
            .Include(r=>r.Customer)
            .Include(r=>r.MenuItem)
            .ToListAsync();
        return ratings.Select(r=>r.ToReadRatingDto(r.Customer, r.MenuItem)).ToList();
    }
}