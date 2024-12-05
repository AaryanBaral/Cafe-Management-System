using Cafe_Management_System.Data;
using Cafe_Management_System.Mappers;
using Cafe_Management_System.Models.TableDto;
using Cafe_Management_System.Services.QrCode_Service;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cafe_Management_System.Repositories.Table;

public class TableRepository (
    AppDbContext context,
    QrCodeService qrCodeService
    )
    : ITableRepository
{
    private readonly AppDbContext _context = context;
    private readonly QrCodeService _qrCodeService = qrCodeService;

    public async Task<string> CreateTable(AddTableDto newTable)
    {
        var jsonData = new
        {
            TableNo = newTable.TableNo,
            SiteUrl = "https://www.youtube.com/"
        };
        var serializedJsonData = JsonConvert.SerializeObject(jsonData);
        var qrCodeData = _qrCodeService.GenerateQrCode(serializedJsonData);
        var table = newTable.ToTable(qrCodeData);
        await _context.Tables.AddAsync(table);
        await _context.SaveChangesAsync();
        return qrCodeData;
    }

    public async Task<string> UpdateTable(AddTableDto updateTable, string tableId)
    {
        var jsonData = new
        {
            TableNo = updateTable.TableNo,
            SiteUrl = "https://www.youtube.com/"
        };
        var serializedJsonData = JsonConvert.SerializeObject(jsonData);
        var qrCodeData = _qrCodeService.GenerateQrCode(serializedJsonData);
        var table = await _context.Tables.FindAsync(tableId)?? throw new KeyNotFoundException("Table not found");
        table.UpdateTable(updateTable, qrCodeData);
        _context.Tables.Update(table);
        await _context.SaveChangesAsync();
        return qrCodeData;
    }

    public async Task DeleteTable(string tableId)
    {
        var table = await _context.Tables.FindAsync(tableId)?? throw new KeyNotFoundException("Table not found");
        await _context.Tables.Where(t => t.TableId == tableId).ExecuteDeleteAsync();
    }
    
}
