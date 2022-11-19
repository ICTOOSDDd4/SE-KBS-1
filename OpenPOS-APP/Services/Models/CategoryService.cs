using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

public class CategoryService : IModelService<Category>
{
    public static List<Category> GetAll()
    {
        List<Category> resultList = DatabaseService.Execute<Category>(new SqlCommand("SELECT * FROM [dbo].[Category]"));
        return resultList;
    }

    public static Category FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Category] WHERE [Id] = @ID");
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        Category result = DatabaseService.ExecuteSingle<Category>(query);

        return result;
    }

    public static bool Delete(Category obj)
    {
        int categoryId = obj.Id;
        
       DatabaseService.Execute(new SqlCommand("DELETE FROM [dbo].[Category] WHERE [Id] = " + categoryId));
       
        return true;
    }

    public static bool Update(Category obj)
    {
        int categoryId = obj.Id;
        string q = "name = '" + obj.Name + "' WHERE [Id] = " + categoryId;
        
        DatabaseService.Execute(new SqlCommand("UPDATE [dbo].[Category] SET " + q));
        
        return true;
    }

    public static bool Create(Category obj)
    {
        DatabaseService.Execute(new SqlCommand("INSERT INTO [dbo].[Category] ([Name]) VALUES ('" + obj.Name + "')"));
        
        return true;
    }
}