using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using ConsoleAppEntityFW.Entitys;
namespace ConsoleAppEntityFW
{
    class Program
    {
        public static void Using_The_Faker_Facade(int count)
        {
            using (EfContext context = new EfContext())
            {
                var faker = new Faker("en");
                for (int i = 0; i < count; i++)
                {
                    UserProfile aslan = new UserProfile
                    {
                        Name = faker.Name.FirstName(),
                        Image = faker.System.FileName() + ".jpg",
                        Telephone = faker.Phone.PhoneNumber("+##(###)###-##-##")
                    };
                    context.UserProfiles.Add(aslan);
                    context.SaveChanges(); // скорость 10 000 записей за 35 мин заливка Azure
                }
             }
         }

        public static void Using_The_Faker_Facade2(int count) // optimized
        {
            using (EfContext context = new EfContext())
            {
                Stopwatch st = new Stopwatch();
                Stopwatch st2 = new Stopwatch();
                var faker = new Faker("en");
                st.Start();
                st2.Start();
                for (int i = 0; i < count; i++)
                {
                    UserProfile aslan = new UserProfile
                    {
                        Name = faker.Name.FirstName(),
                        Image = faker.System.FileName() + ".jpg",
                        Telephone = faker.Phone.PhoneNumber("+##(###)###-##-##")
                    };
                    context.UserProfiles.Add(aslan);
                }
                st.Stop();
                context.SaveChanges(); // скорость 10 000 записей за 10 мин заливка Azure
                st2.Stop();
                TimeSpan ts = st.Elapsed;
                Console.WriteLine($"time: {ts}");
                TimeSpan ts2 = st2.Elapsed;
                Console.WriteLine($"time: {ts2}");
            }
        }

        static void Main(string[] args)
        {
            // добавлення данних
            using (EfContext context = new EfContext())
            {
                int line = 0;
                do
                {
                    Console.WriteLine($"0. Exit");
                    Console.WriteLine($"1. Add User");
                    Console.WriteLine($"2. Get all users");
                    Console.WriteLine($"3. Edit user");
                    Console.WriteLine($"4. Delete user");
                    Console.WriteLine($"5. Find user by Name");
                    Console.WriteLine($"6. Find user by tel number");
                    Console.WriteLine($"7. Find user by Id");
                    Console.WriteLine($"8. Fill Users");
                    Console.WriteLine($"9. Find good Users");
                    Console.WriteLine($"10. Find good RRRR Users");
                    line = int.Parse(Console.ReadLine());
                    switch (line)
                    {
                        case 1: // add
                            {
                                string name = "";
                                string image = "";
                                string tel = "";
                                Console.Write($"Enter new Name: ");
                                name = Console.ReadLine();
                                Console.Write($"Enter new Image: ");
                                image = Console.ReadLine();
                                Console.Write($"Enter new Telephone: ");
                                tel = Console.ReadLine();
                                UserProfile aslan = new UserProfile
                                {
                                    Name = name,
                                    Image = image,
                                    Telephone = tel
                                    //Name = "Aslan2",
                                    //Image = "asla2n.jpg",
                                    //Telephone = "+38(050)555-44-33"
                                };
                                context.UserProfiles.Add(aslan);
                                context.SaveChanges();
                                Console.WriteLine($"Userprofiles id={aslan.Id}");
                            }
                            break;

                        case 2: // get all
                            Console.Write($"Id\t Name\t Image\t\t\t\t\t Telephone\n");
                            foreach (var item in context.UserProfiles)
                            {
                                Console.Write($"{item.Id}\t {item.Name}\t {item.Image}\t\t\t\t\t {item.Telephone}\n");
                            }
                            break;

                        case 3: // edit user
                            {
                                Console.Write($"Enter Id: ");
                                int id = int.Parse(Console.ReadLine());
                                var user = context.UserProfiles.SingleOrDefault(u => u.Id == id);
                                if (user != null)
                                {
                                    Console.Write($"Enter new Name: ");
                                    string tmp = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(tmp))
                                        user.Name = tmp;
                                    Console.Write($"Enter new Image: ");
                                    tmp = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(tmp))
                                        user.Image = tmp;
                                    Console.Write($"Enter new Telephone: ");
                                    tmp = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(tmp))
                                        user.Telephone = tmp;
                                    context.SaveChanges();
                                }
                            }
                            break;
                            
                        case 4: // delete
                            {
                                Console.Write($"Enter Id: ");
                                int id = int.Parse(Console.ReadLine());
                                var user = context.UserProfiles.SingleOrDefault(u => u.Id == id);
                                if (user != null)
                                {
                                    context.UserProfiles.Remove(user);
                                    context.SaveChanges();
                                }
                                else
                                    Console.WriteLine($"Not Found.");
                            }
                            break;

                        case 5: // find user
                            {
                                Console.Write($"Enter Name: ");
                                string name = Console.ReadLine();
                                var users = context.UserProfiles.Where(u => u.Name.Contains(name));
                                if (users != null)
                                {
                                    foreach (var item in users)
                                    {
                                        Console.Write($"{item.Id}\t {item.Name}\t {item.Image}\t {item.Telephone}\n");
                                    }
                                }
                                else
                                    Console.WriteLine($"Not Found.");
                            }
                            break;

                        case 6: // find user by phone
                            {
                                Console.Write($"Enter number: ");
                                    string num = Console.ReadLine();
                                if (num != null)
                                {
                                    var users = context.UserProfiles.Where(u => u.Telephone.Contains(num));
                                    if (users != null)
                                    {
                                        foreach (var item in users)
                                        {
                                            Console.Write($"{item.Id}\t {item.Name}\t {item.Image}\t {item.Telephone}\n");
                                        }
                                    }
                                    else
                                        Console.Write($"Not found.");
                                }
                                else
                                {
                                    Console.Write($"Error. empty number.");
                                }
                            }
                            break;

                        case 7: // find user by Id
                            {
                                Console.Write($"Enter Id number: ");
                                try
                                {
                                    int num = int.Parse(Console.ReadLine());
                                    var users = context.UserProfiles.Where(u => u.Id == num);
                                    if (users != null)
                                    {
                                        foreach (var item in users)
                                        {
                                            Console.Write($"{item.Id}\t {item.Name}\t {item.Image}\t {item.Telephone}\n");
                                        }
                                    }
                                    else
                                        Console.Write($"Not found.");
                                }
                                catch
                                {
                                    Console.Write($"Error. Wrond number.");
                                }
                            }
                            break;

                        case 8: // fill
                            {
                                Console.Write($"Enter quantity: ");
                                try
                                {
                                    int num = int.Parse(Console.ReadLine());
                                    Using_The_Faker_Facade2(num);
                                }
                                catch { }
                            }
                            break;
                        case 9:
                            {
                                Stopwatch st = new Stopwatch();
                                
                                var query = context.UserProfiles.AsQueryable(); //sql
                                Console.Write($"Enter new Name: ");
                                string name = Console.ReadLine();
                                if (!string.IsNullOrEmpty(name))
                                    query = query.Where(u => u.Name.Contains(name)); //sql

                                Console.Write($"Enter new Image: ");
                                string image = Console.ReadLine();
                                if (!string.IsNullOrEmpty(image))
                                    query = query.Where(u => u.Image.Contains(image)); //sql

                                Console.Write($"Enter new Telephone: ");
                                string tel = Console.ReadLine();
                                if (!string.IsNullOrEmpty(tel))
                                    query = query.Where(u => u.Telephone.Contains(tel)); //sql
                                st.Start();
                                var users = query.ToList();
                                st.Stop();
                                foreach (var item in users)
                                {
                                    Console.Write($"{item.Id}\t {item.Name}\t {item.Image}\t {item.Telephone}\n");
                                }
                                TimeSpan ts = st.Elapsed;
                                Console.WriteLine($"time: {ts}");
                                break;
                            }
                        case 10:
                            {
                                var query = from us in context.UserProfiles.AsQueryable()
                                            select us;
                                //var query = context.UserProfiles.AsQueryable(); //sql
                                Console.Write($"Enter new Name: ");
                                string name = Console.ReadLine();
                                if (!string.IsNullOrEmpty(name))
                                    query = from q in query where q.Name.Contains(name) select q; //sql

                                Console.Write($"Enter new Image: ");
                                string image = Console.ReadLine();
                                if (!string.IsNullOrEmpty(image))
                                    query = from q in query where q.Image.Contains(image) select q; //sql

                                Console.Write($"Enter new Telephone: ");
                                string tel = Console.ReadLine();
                                if (!string.IsNullOrEmpty(tel))
                                    query = from q in query where q.Telephone.Contains(tel) select q; //sql

                                var users = query.ToList();

                                foreach (var item in users)
                                {
                                    Console.Write($"{item.Id}\t {item.Name}\t {item.Image}\t {item.Telephone}\n");
                                }

                                break;
                            }
                        default:
                            break;
                    }

                } while (line != 0);


            }

        }
    }
}
