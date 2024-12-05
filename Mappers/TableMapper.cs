using Cafe_Management_System.Entities;
using Cafe_Management_System.Models.TableDto;

namespace Cafe_Management_System.Mappers;

public static class TableMapper
{
    public static Tables ToTable(this AddTableDto tableDto, string qrData)
    {
        return new Tables()
        {
            TableNumber = tableDto.TableNo,
            IsOccupied = false,
            Qrdata = qrData
        };
    }

    public static void UpdateTable(this Tables table, AddTableDto tableDto, string qrCodeData)
    {
        table.TableNumber = tableDto.TableNo;
        table.Qrdata = qrCodeData;
    }
}