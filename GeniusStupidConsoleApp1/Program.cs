// 1. Заводим банк вопросов
// 2. Заводим банк ответов
// 3. Заволим счетчик
// 4. Выводим вопрос
// 5. получаем ответ пользователя
// 6. Если правильно то счетчик ++
// 7. Выводим счетчик
using System.Text;

Console.WriteLine("Ответами на все вопросы являются числа, вводите данные правильно!");
Console.Write("Введите свое имя: ");
string userName = Console.ReadLine();

string filePath1 = Path.Combine(Environment.CurrentDirectory, "result.tsv");

string filePath2 = Path.Combine(Environment.CurrentDirectory, "answersQuestions.tsv");

(List<string> questions, List<string> answers) = GatAnswersQuestions(filePath2);

while (true)
{
    Console.WriteLine("========== ГЛАВНОЕ МЕНЮ ==========");
    Console.WriteLine("========= 1-->НАЧАТЬ ИГРУ ========");
    Console.WriteLine("===== 2-->ТАБЛИЦА РЕЗУЛЬТАТОВ =====");
    Console.WriteLine("======= 3-->ДОБАВИТЬ ВОПРОС =======");
    Console.WriteLine("=========== 4-->ВЫЙТИ =============");
    int play = int.Parse(Console.ReadLine());
    if (play == 1)
    {
        string diagnosis;
        int correctAnswersCounter = 0;
        MixArray(questions, answers);

        for (int i = 0; i < questions.Count; i++)
        {
            Console.WriteLine(questions[i]);
            string answer = GetUserAnswer();
            string correctAnswer = answers[i];
            correctAnswersCounter = CheckingTheUserAnswers(answer, correctAnswer, correctAnswersCounter);
        }
        double correctAnswersPircent = correctAnswersCounter * 100 / answers.Count;
        PrintDiagnosis(correctAnswersPircent, userName);
        diagnosis = Diagnosis(correctAnswersPircent, userName);
        SaveResult(userName, correctAnswersCounter, diagnosis);
    }
    else if (play == 2)
    {
        VievResults(filePath1);
    }
    else if (play == 3)
    {
        (questions, answers) = AddQuestion(filePath2, questions, answers);
    }
    else if (play == 4)
    {
        Console.WriteLine("Досвидание!!!");
        break;
    }
}


static string GetUserAnswer()
{
    while (true)
    {
        Console.WriteLine("Введите ответ - число: ");
        string answer = Console.ReadLine();
        bool flag = true;
        foreach(char item in answer)
        {
            if (item > '9' || item < '0')
            {
                Console.WriteLine("Серьезно?");
                flag = false;
                break;
            }
        }
        if (flag)
        {
            return answer;
        }
    }
}

static int CheckingTheUserAnswers(string answer, string correctAnswer, int correctAnswersCounter)
{
    if (answer == correctAnswer)
    {
        correctAnswersCounter++;
    }
    return correctAnswersCounter;
}

static void PrintDiagnosis(double correctAnswersPircent, string userName)
{
    Console.Write($"{userName} вы ");
    if (correctAnswersPircent < 20)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ИДИОТ!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 40)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("КРИТИН!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 60)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("ДУРАК!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 80)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("НОРМАЛЬНЫЙ!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 100)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("ТАЛАНТ!");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ГЕНИЙ!");
        Console.ResetColor();
    }
}

static string Diagnosis(double correctAnswersPircent, string userName)
{
    if (correctAnswersPircent < 20)
    {
        return "ИДИОТ!";
    }
    else if (correctAnswersPircent < 40)
    {
        return "КРИТИН!";
    }
    else if (correctAnswersPircent < 60)
    {
        return "ДУРАК!";
    }
    else if (correctAnswersPircent < 80)
    {
        return "НОРМАЛЬНЫЙ!";
    }
    else if (correctAnswersPircent < 100)
    {
        return "ТАЛАНТ!";
    }
    else
    {
        return "ГЕНИЙ!";
    }
}

static void MixArray(List<string> questions, List<string> answers)
{
    for (int i = questions.Count - 1; i > 0; i--)
    {
        int j = Random.Shared.Next(i + 1);
        (questions[i], questions[j]) = (questions[j], questions[i]);
        (answers[i], answers[j]) = (answers[j], answers[i]);
    }
}

static void SaveResult (string userName, int points, string diagnosis)
{
    string filePath = Path.Combine(Environment.CurrentDirectory, "result.tsv");

    if (!File.Exists(filePath))
    {
        File.WriteAllText(filePath, 
        "Имя\tКоличество балов\tДиагноз\n", 
        Encoding.UTF8);
    }

    string result = $"{userName}\t{points}\t{diagnosis}\n";
    File.AppendAllText(filePath, result, Encoding.UTF8);
}

static void VievResults(string filePath)
{
    if (!File.Exists(filePath))
    {
        Console.WriteLine("\nИстория результатов пока что пуста");
        return;
    }
    string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
    if (lines.Length <= 1)
    {
        Console.WriteLine("\nИстория результатов пока пуста.");
        return;
    }

    Console.WriteLine("========== ИСТОРИЯ РЕЗУЛЬТАТОВ ==========");
    Console.WriteLine(
        "{0,-20} | {1,-20} | {2}",
        "Имя",
        "Баллы",
        "Диагноз"
    );

    Console.WriteLine(new string('-', 65));

    for (int i = 1; i < lines.Length; i++)
    {
        string[] data = lines[i].Split('\t');

        if (data.Length >= 3)
        {
            Console.WriteLine(
                "{0,-20} | {1,-20} | {2}",
                data[0],
                data[1],
                data[2]
            );
        }
    }
    Console.WriteLine(new string('-', 65));
}

static (List<string>, List<string>) AddQuestion(string filePath, List<string> questions, List<string> answers)
{
    Console.WriteLine("Введите вопрос, который хотите добавить");
    string newQuestion = Console.ReadLine();
    Console.WriteLine("Введите ответ, к вашему вопросу");
    string newAnswer = Console.ReadLine();

    File.AppendAllText(filePath, $"{newQuestion}\t{newAnswer}\n", Encoding.UTF8);
    questions.Add(newQuestion);
    answers.Add(newAnswer);
    return (questions, answers);
}

static (List<string>,List<string>) GatAnswersQuestions(string filePath)
{

    if (!File.Exists(filePath))
    {
        List<string> questions = new List<string>()
        {
            "Сколько будет 2 плюс 2, умноженное на 2?",
            "Бревно нжно распилить на 10 частей. Сколько распилов необходимо сделать?",
            "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
            "Укол делают каждые пол часа. Сколько минут нужно на 3 укола?",
            "5 свечей горело, 2 потухли. Сколько свечей осталось?",
            "Часы пробили 6 раз за 5 секунд. За сколько секунд они пробьют 12 раз?",
            "В семье два отца и два сына. Каждый съел по одному яблоку, но всего было съедено 3 яблока. Сколько человек в семье?",
            "Я — двузначное число. Сумма моих цифр равна 9. Если поменять цифры местами, число увеличится на 27. Какое я число?",
            "Во дворе гуляют куры и кролики. Всего у них 10 голов и 28 ног. Сколько во дворе кроликов?",
            "В ящике лежат носки трёх цветов: красные, синие и чёрные. Сколько носков нужно достать в темноте, чтобы гарантированно получить пару одного цвета?"
        };
        List<string> answers = new List<string>()
        {
            "6",
            "9",
            "25",
            "60",
            "2",
            "11",
            "3",
            "36",
            "4",
            "4"
        };

        File.WriteAllText(filePath, 
        "Вопросы\tОтветы\n", Encoding.UTF8);
        for (int i = 0; i < questions.Count; i++)
        {
            string quest = questions[i];
            string answ = answers[i];
            File.AppendAllText(filePath, $"{quest}\t{answ}\n", Encoding.UTF8);
        }
        return(questions, answers);
    }
    else
    {
        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
        List<string> questions = new List<string>();
        List<string> answers = new List<string>();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('\t');
            questions.Add(parts[0]);
            answers.Add(parts[1]);
        }
        return (questions, answers);
    }
}