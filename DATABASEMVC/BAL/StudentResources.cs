using dal.dbb.Connected;
using BOL.student;
namespace BAL.resource;

public class StudentResource{

    public static List<Student> getAllStudent()
    {
        List<Student> slist=new List<Student>();

        slist=DBManager.getAllStudent();

        return slist;
    } 


    public static bool validate_user(String User_name,String Password)
    {
          return  DBManager.LoginUser(User_name,Password);
    }

    public static bool UpdateUser(int Id,String First_name,String Last_name)
    {
          return  DBManager.UpdateUser(Id,First_name,Last_name);
    }

     public static bool DeleteUser(int Id)
    {
          return  DBManager.DeleteUser(Id);
    }
}