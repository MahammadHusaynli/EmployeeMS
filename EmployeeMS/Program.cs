using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeMS
{
	internal class Program
	{
		static List<Employee> employees = new List<Employee>();
		static List<Task> tasks = new List<Task>();

		static void Main()
		{
			int mainChoice;

			do
			{
				Console.Clear();
				Console.WriteLine("===== MAIN MENU =====");
				Console.WriteLine("1. Employee Operations");
				Console.WriteLine("2. Task Operations");
				Console.WriteLine("3. Reports");
				Console.WriteLine("0. Exit");

				mainChoice = Convert.ToInt32(Console.ReadLine());

				switch (mainChoice)
				{
					case 1:
						EmployeeMenu();
						break;
					case 2:
						TaskMenu();
						break;
					case 3:
						ReportMenu();
						break;
				}

			} while (mainChoice != 0);
		}

		static void EmployeeMenu()
		{
			int choice;

			do
			{
				Console.Clear();
				Console.WriteLine("===== EMPLOYEE OPERATIONS =====");
				Console.WriteLine("1. Add employee");
				Console.WriteLine("2. Deactivate employee");
				Console.WriteLine("3. Show all employees");
				Console.WriteLine("4. Show active employees");
				Console.WriteLine("5. Show inactive employees");
				Console.WriteLine("0. Back");

				choice = Convert.ToInt32(Console.ReadLine());

				switch (choice)
				{
					case 1:
						Console.Write("Name: ");
						string name = Console.ReadLine();

						Console.Write("Position: ");
						string position = Console.ReadLine();

						Console.Write("Salary: ");
						decimal salary = decimal.Parse(Console.ReadLine());

						employees.Add(new Employee
						{
							Id = employees.Count + 1,
							Name = name,
							Position = position,
							Salary = salary,
							HireDate = DateOnly.FromDateTime(DateTime.Now),
							IsActive = true
						});
						break;

					case 2:
						Console.Write("Employee ID: ");
						int id = int.Parse(Console.ReadLine());

						Employee emp = employees.FirstOrDefault(e => e.Id == id);
						if (emp != null)
							emp.IsActive = false;
						break;

					case 3:
						foreach (Employee e in employees)
							Console.WriteLine($"{e.Id} - {e.Name} - {e.Position} - Active: {e.IsActive}");
						Console.ReadKey();
						break;

					case 4:
						foreach (Employee e in employees)
							if (e.IsActive)
								Console.WriteLine($"{e.Id} - {e.Name}");
						Console.ReadKey();
						break;

					case 5:
						foreach (Employee e in employees)
							if (!e.IsActive)
								Console.WriteLine($"{e.Id} - {e.Name}");
						Console.ReadKey();
						break;
				}

			} while (choice != 0);
		}

		static void TaskMenu()
		{
			int choice;

			do
			{
				Console.Clear();
				Console.WriteLine("===== TASK OPERATIONS =====");
				Console.WriteLine("1. Create task");
				Console.WriteLine("2. Assign task");
				Console.WriteLine("3. Check deadlines");
				Console.WriteLine("4. Show tasks");
				Console.WriteLine("0. Back");

				choice = Convert.ToInt32(Console.ReadLine());

				switch (choice)
				{
					case 1:
						Console.Write("Title: ");
						string title = Console.ReadLine();

						Console.Write("Description: ");
						string desc = Console.ReadLine();

						Console.Write("Deadline (yyyy-MM-dd): ");
						DateOnly deadline = DateOnly.Parse(Console.ReadLine());

						tasks.Add(new Task
						{
							TaskId = tasks.Count + 1,
							Title = title,
							Description = desc,
							Deadline = deadline
						});
						break;

					case 2:
						Console.Write("Task ID: ");
						int taskId = int.Parse(Console.ReadLine());

						Task task = tasks.FirstOrDefault(t => t.TaskId == taskId);
						if (task == null)
							break;

						Console.Write("Employee Name: ");
						task.AssignedEmployeed = Console.ReadLine();
						break;

					case 3:
						DateOnly today = DateOnly.FromDateTime(DateTime.Now);

						foreach (Task t in tasks)
							if (t.Deadline < today)
								Console.WriteLine($"Task {t.Title} is LATE");

						Console.ReadKey();
						break;

					case 4:
						foreach (Task t in tasks)
							Console.WriteLine($"{t.TaskId} - {t.Title} - {t.AssignedEmployeed} - {t.Deadline}");
						Console.ReadKey();
						break;
				}

			} while (choice != 0);
		}

		static void ReportMenu()
		{
			int choice;

			do
			{
				Console.Clear();
				Console.WriteLine("===== REPORTS =====");
				Console.WriteLine("1. Task count per employee");
				Console.WriteLine("2. Late tasks list");
				Console.WriteLine("3. Employee with most tasks");
				Console.WriteLine("4. Active tasks list");
				Console.WriteLine("0. Back");

				choice = Convert.ToInt32(Console.ReadLine());
				DateOnly today = DateOnly.FromDateTime(DateTime.Now);

				switch (choice)
				{
					case 1:
						foreach (Employee e in employees)
						{
							int count = tasks.Count(t => t.AssignedEmployeed == e.Name);
							Console.WriteLine($"{e.Name} - {count} task");
						}
						Console.ReadKey();
						break;

					case 2:
						foreach (Task t in tasks)
							if (t.Deadline < today)
								Console.WriteLine($"{t.Title} - {t.AssignedEmployeed} - {t.Deadline}");
						Console.ReadKey();
						break;

					case 3:
						string topEmployee = "";
						int max = 0;

						foreach (Employee e in employees)
						{
							int count = tasks.Count(t => t.AssignedEmployeed == e.Name);
							if (count > max)
							{
								max = count;
								topEmployee = e.Name;
							}
						}

						if (max > 0)
							Console.WriteLine($"{topEmployee} - {max} tasks");
						else
							Console.WriteLine("No tasks assigned");

						Console.ReadKey();
						break;

					case 4:
						foreach (Task t in tasks)
							if (t.Deadline >= today)
								Console.WriteLine($"{t.Title} - {t.AssignedEmployeed} - {t.Deadline}");
						Console.ReadKey();
						break;
				}

			} while (choice != 0);
		}
	}
}
