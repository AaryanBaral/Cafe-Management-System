using Cafe_Management_System.Entities;
using Cafe_Management_System.Models.TableDto;

namespace Cafe_Management_System.Mappers;

public static class TableMapper
{
    public static Tables ToTable(this AddTableDto tableDto)
    {
        return new Tables()
        {
            TableNumber = tableDto.TableNo,
            IsOccupied = false,
            QrData = ""
        };
    }

    public static void UpdateTable(this Tables table, AddTableDto tableDto)
    {
        table.TableNumber = tableDto.TableNo;
    }

    public static ReadTableDto ToReadTable(this Tables table)
    {
        return new ReadTableDto()
        {
            TableId = table.TableId,
            TableNo = table.TableNumber,
        };
    }
}