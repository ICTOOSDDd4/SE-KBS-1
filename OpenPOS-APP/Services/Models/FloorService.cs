using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;

namespace OpenPOS_APP.Services.Models;

public class FloorService : IModelService<Floor>
{
    public static List<Floor> GetAll()
    {
        List<Floor> resultList = DatabaseService.Execute<Floor>("SELECT * FROM [dbo].[Floor]");
        return resultList;
    }

    public static Floor FindByID(int id)
    {
        Floor result = DatabaseService.ExecuteSingle<Floor>("SELECT * FROM [dbo].[Floor] WHERE [ID] = " + id);
        return result;
    }

    public static bool Delete(Floor obj)
    {
        int floorInt = obj.Id;
        DatabaseService.Execute("DELETE FROM [dbo].[Floor] WHERE [ID] = " + floorInt);
        return true;
    }

    public static bool Update(Floor obj)
    {
        int floorInt = obj.Id;
        string q = "storey = '" + obj.Storey + "' WHERE [ID] = " + floorInt;
        DatabaseService.Execute("UPDATE [dbo].[Floor] SET " + q);
        return true;
    }

    public static bool Create(Floor obj)
    {
        string q = "'" + obj.Storey + "'";
        DatabaseService.Execute("INSERT INTO [dbo].[Floor] ([storey]) VALUES (" + q + ")");
        return true;
    }
}