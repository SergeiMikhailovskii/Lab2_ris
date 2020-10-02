using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab2
{

    class Worker
    {
        private string name;
        private string position;
        private long salary;

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetPosition()
        {
            return position;
        }

        public void SetPosition(string position)
        {
            this.position = position;
        }

        public long GetSalary()
        {
            return salary;
        }

        public void SetSalary(long salary)
        {
            this.salary = salary;
        }

        public Worker(string name, string position, long salary)
        {
            this.name = name;
            this.position = position;
            this.salary = salary;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string choice;
            do
            {
                Console.WriteLine("Choose variant\n" +
                    "1) Add new worker\n" +
                    "2) Show all workers\n" +
                    "3) Search worker by name\n" +
                    "4) Delete worker\n" +
                    "5) Edit worker\n" +
                    "6) Sort by salary\n" +
                    "7) Exit");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            AddWorker();
                            break;
                        }
                    case "2":
                        {
                            ShowWorkers();
                            break;
                        }
                    case "3":
                        {
                            SearchWorkerByName();
                            break;
                        }
                    case "4":
                        {
                            DeleteWorker();
                            break;
                        }
                    case "5":
                        {
                            EditWorker();
                            break;
                        }
                    case "6":
                        {
                            SortBySalary();
                            break;
                        }
                }
            } while (!choice.Equals("7"));
            Console.ReadLine();
        }

        private static void SortBySalary()
        {
            string line;
            StreamReader reader = new StreamReader("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt");
            List<Worker> workers = new List<Worker>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] workerStr = line.Split(' ');
                workers.Add(new Worker(workerStr[0], workerStr[1], long.Parse(workerStr[2])));
            }

            reader.Close();

            IEnumerable<Worker> query = workers.OrderBy(worker => worker.GetSalary());

            foreach (Worker worker in query)
            {
                Console.WriteLine(worker.GetName() + " " + worker.GetPosition() + " " + worker.GetSalary().ToString());
            }

        }

        private static void EditWorker()
        {
            string line;

            Console.WriteLine("Enter name of worker to edit");
            string name = Console.ReadLine();

            Console.WriteLine("Enter new name");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter new position");
            string newPosition = Console.ReadLine();

            Console.WriteLine("Enter new salary");
            long newSalary = long.Parse(Console.ReadLine());

            StreamReader reader = new StreamReader("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt");
            List<Worker> workers = new List<Worker>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] workerStr = line.Split(' ');
                workers.Add(new Worker(workerStr[0], workerStr[1], long.Parse(workerStr[2])));
            }

            reader.Close();

            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].GetName().Equals(name))
                {
                    workers[i] = new Worker(newName, newPosition, newSalary);
                }
            }


            FileStream fileStream = new FileStream("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter streamWriter = new StreamWriter(fileStream);

            workers.ForEach(worker =>
            {
                streamWriter.WriteLine(worker.GetName() + " " + worker.GetPosition() + " " + worker.GetSalary().ToString());
            });

            streamWriter.Close();
            fileStream.Close();

        }

        private static void DeleteWorker()
        {
            string line;

            Console.WriteLine("Enter name");

            string name = Console.ReadLine();

            StreamReader reader = new StreamReader("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt");
            List<Worker> workers = new List<Worker>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] workerStr = line.Split(' ');
                workers.Add(new Worker(workerStr[0], workerStr[1], long.Parse(workerStr[2])));
            }

            reader.Close();

            workers = workers.FindAll(worker => !worker.GetName().Equals(name));

            string workerHolder = "";

            workers.ForEach(worker =>
            {
                workerHolder += worker.GetName() + " " + worker.GetPosition() + " " + worker.GetSalary().ToString() + '\n';
            });

            File.WriteAllText("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt", workerHolder);
        }

        private static void SearchWorkerByName()
        {
            string line;

            Console.WriteLine("Enter name");

            string name = Console.ReadLine();

            StreamReader reader = new StreamReader("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt");
            List<Worker> workers = new List<Worker>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] workerStr = line.Split(' ');
                workers.Add(new Worker(workerStr[0], workerStr[1], long.Parse(workerStr[2])));
            }

            reader.Close();

            workers = workers.FindAll(worker => worker.GetName().Equals(name));

            Console.WriteLine("NAME POSITION SALARY");

            workers.ForEach(worker =>
            {
                Console.WriteLine(worker.GetName() + " " + worker.GetPosition() + " " + worker.GetSalary());
            });

        }

        private static void AddWorker()
        {
            Console.WriteLine("Enter name:");
            string region = Console.ReadLine();

            Console.WriteLine("Enter position:");
            string type = Console.ReadLine();
            long amount = 0;

            while (true)
            {
                Console.WriteLine("Enter salary");
                bool isSuccess = long.TryParse(Console.ReadLine(), out amount);
                if (!isSuccess || amount < 0)
                {
                    Console.WriteLine("Wrong input");
                }
                else
                {
                    break;
                }
            }

            Worker worker = new Worker(region, type, amount);

            FileStream fileStream = new FileStream("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt", FileMode.OpenOrCreate, FileAccess.Write);

            fileStream.Seek(0, SeekOrigin.End);

            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.WriteLine(worker.GetName() + " " + worker.GetPosition() + " " + worker.GetSalary().ToString());
            streamWriter.Close();
            fileStream.Close();

        }

        private static void ShowWorkers()
        {
            string line;

            StreamReader reader = new StreamReader("E:\\Лабы\\7 сем\\рис\\Lab2\\Lab2\\text.txt");

            Console.WriteLine("NAME POSITION SALARY");
            while ((line = reader.ReadLine()) != null)
            {
                string[] workerStr = line.Split(' ');
                Console.WriteLine(workerStr[0] + " " + workerStr[1] + " " + workerStr[2]);
            }

            reader.Close();
        }

    }
}