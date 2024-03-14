using MySql.Data.MySqlClient;
namespace dal.db.Disconnected;
using BOL.student;
using System.Data;


public class DBManager
{
    static String conectsString=@"server=192.168.10.150;port=3306;user=dac39;password=welcome;database=dac39";

    public static List<Student> getAllStudent()
    {
            List<Student> slist=new List<Student>();
                //step1:-make connection
            MySqlConnection con=new MySqlConnection();

            //step2:-connecting string
            con.ConnectionString=conectsString;
            MySqlCommand cmd=new MySqlCommand();
            // MySqlCommand cmd=new MySqlCommand(query,con); also valid...!!!
            try{
                // Empty data set
                    DataSet ds=new DataSet();
                    cmd.Connection=con;

                //step3:-writes queries
                    String query="select * from student_dumy";
                    cmd.CommandText=query;

                //Disconnected Data Access Logic:-
                
                // MySqlDataAdapter serves as a bridge between
                // a DataSet and MySQL for retrieving and saving data.
                MySqlDataAdapter da=new MySqlDataAdapter();
                da.SelectCommand=cmd; //MySqlCommand that is a SQL SELECT statement or stored procedure and
                                      // is set as the SelectCommand property of the MySqlDataAdapter.
            
                da.Fill(ds);
                //Fill():-  1) this method would fetch data from backend mysql
                        //  2) work as a Read() method...
                        //  3) fill results into dataset collection
                        //  4) deal with data which has been fetched from back end
                    
                DataTable dt=ds.Tables[0]; // contains multiple tables indexwise...
                DataRowCollection rows=dt.Rows; //works as a pointer who travels on perticular row..

                foreach(DataRow row in rows)
                {
                    int id=int.Parse(row["ID"].ToString());
                    string namefirst= row["namefirst"].ToString();
                    string namelast=row["namelast"].ToString();
                    Student s= new Student{
                        ID=id,
                        First_name=namefirst,
                        Last_name=namelast
                    };
                    slist.Add(s);
                }
            }catch(Exception e){
                    Console.WriteLine(e.Message);
            }finally{
                //connection close
                con.Close();
            }
            return slist;
    }


     public static bool LoginUser(String User_name,String Password)
    {
            MySqlConnection con=new MySqlConnection();

            //step2:-connecting string
            
            con.ConnectionString=conectsString;

            //step3:-writes queries
            String query="select * from UserRegistrations where Name=@User_name and password=@Password";


            MySqlCommand command=new MySqlCommand(query,con);
            command.Parameters.AddWithValue("@User_name",User_name);
            command.Parameters.AddWithValue("@Password",Password);

            try{
                con.Open();
            //step6:-read data object 
                MySqlDataReader reader=command.ExecuteReader();

                while(reader.Read())
                {
                    
                   return true;
                    
                }
            reader.Close();
            }catch(Exception e){
                    Console.WriteLine(e.Message+"Same eroor");
            }finally{
                //connection close
                con.Close();
            }

            return false;
    }
}