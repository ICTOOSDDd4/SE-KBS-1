using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

public class RoleService : IModelService<Role>
{
    public static List<Role> GetAll()
    {
        List<Role> resultList = DatabaseService.Execute<Role>(new SqlCommand("SELECT * FROM [dbo].[role]"));

        return resultList;
    }

    public static Role FindByID(int id)
    {
        Role result = DatabaseService.ExecuteSingle<Role>(new SqlCommand("SELECT * FROM [dbo].[role] WHERE id = " + id));
        
        return result;
    }

    public static bool Delete(Role obj)
    {
        int roleId = obj.Id;
        
        DatabaseService.Execute(new SqlCommand("DELETE FROM [dbo].[role] WHERE id = " + roleId));
        
        return true;
    }

    public static bool Update(Role obj)
    {
        int roleId = obj.Id;
        string q = "title = '" + obj.Title + "' WHERE id = " + roleId;
        
        DatabaseService.Execute(new SqlCommand("UPDATE [dbo].[role] SET " + q));
        
        return true;
    }

    public static bool Create(Role obj)
    {
        string title = obj.Title;
        
        DatabaseService.Execute(new SqlCommand("INSERT INTO [dbo].[role] (title) VALUES ('" + title + "')"));
        
        return true;
    }
}