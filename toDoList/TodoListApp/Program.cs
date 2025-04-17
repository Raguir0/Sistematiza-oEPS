using System;
using System.Collections.Generic;
using System.Globalization;

class Task
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime Deadline { get; set; }
    public string Priority { get; set; }
    
    public string Status
    {
        get
        {
            if (IsCompleted) return "Concluída";
            return DateTime.Now > Deadline ? "Atrasada" : "Pendente";
        }
    }
}

class Program
{
    static List<Task> tasks = new List<Task>();
    static int nextId = 1;

    static void Main()
    {
        // Define a cultura padrão como PT-BR para evitar erros de formatação de data
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");

        while (true)
        {
            Console.WriteLine("\nSistema de Gerenciamento de Tarefas:");
            Console.WriteLine("1 - Adicionar tarefa");
            Console.WriteLine("2 - Listar tarefas");
            Console.WriteLine("3 - Marcar como concluída");
            Console.WriteLine("4 - Editar tarefa");
            Console.WriteLine("5 - Remover tarefa");
            Console.WriteLine("6 - Sair");
            Console.Write("Escolha uma opção: ");
            
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ListTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    EditTask();
                    break;
                case "5":
                    RemoveTask();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Descrição da tarefa: ");
        string description = Console.ReadLine();

        DateTime deadline;
        while (true)
        {
            Console.Write("Prazo (dd-MM-yyyy): ");
            string deadlineInput = Console.ReadLine();
            
            if (DateTime.TryParseExact(deadlineInput, "dd-MM-yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None, out deadline))
                break;

            Console.WriteLine("Data inválida! Informe no formato correto dd-MM-yyyy.");
        }

        string priority;
        while (true)
        {
            Console.Write("Prioridade (Baixa, Media, Alta): ");
            priority = Console.ReadLine().ToLower();

            if (priority == "baixa" || priority == "media" || priority == "alta")
                break;

            Console.WriteLine("Prioridade inválida! Escolha entre Baixa, Media ou Alta.");
        }

        tasks.Add(new Task { Id = nextId++, Description = description, Deadline = deadline, Priority = priority, IsCompleted = false });
        Console.WriteLine("Tarefa adicionada com sucesso!");
    }

    static void ListTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("\nNenhuma tarefa cadastrada.");
            return;
        }

        Console.WriteLine("\nLista de tarefas:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"[{task.Id}] {task.Description} - Prioridade: {task.Priority} - Prazo: {task.Deadline.ToString("dd-MM-yyyy")} - Status: {task.Status}");
        }
    }

    static void CompleteTask()
    {
        Console.Write("ID da tarefa para marcar como concluída: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Task task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                Console.WriteLine("Tarefa marcada como concluída!");
            }
            else
            {
                Console.WriteLine("Tarefa não encontrada!");
            }
        }
    }

    static void EditTask()
    {
        Console.Write("ID da tarefa para editar: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Task task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                Console.Write("Nova descrição (ou pressione Enter para manter): ");
                string newDescription = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDescription))
                {
                    task.Description = newDescription;
                }

                Console.Write("Novo prazo (dd-MM-yyyy, ou Enter para manter): ");
                string newDeadlineInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDeadlineInput) && DateTime.TryParseExact(newDeadlineInput, "dd-MM-yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime newDeadline))
                {
                    task.Deadline = newDeadline;
                }

                Console.Write("Nova prioridade (Baixa, Media, Alta, ou Enter para manter): ");
                string newPriority = Console.ReadLine().ToLower();
                if (!string.IsNullOrWhiteSpace(newPriority) && (newPriority == "baixa" || newPriority == "media" || newPriority == "alta"))
                {
                    task.Priority = newPriority;
                }

                Console.WriteLine("Tarefa editada com sucesso!");
            }
            else
            {
                Console.WriteLine("Tarefa não encontrada!");
            }
        }
    }

    static void RemoveTask()
    {
        Console.Write("ID da tarefa para remover: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Task task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Tarefa removida!");
            }
            else
            {
                Console.WriteLine("Tarefa não encontrada!");
            }
        }
    }
}